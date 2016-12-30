using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DaXia.BLL;
using DaXia.EntityDataModels;
using DaXia.WebComAdmin.Front;
using DaXia.WebComAdmin.lib;
using DaXia.WebFrameWork;
using WxPayAPI;

namespace DaXia.WebComAdmin.Controllers
{
    public class FrontPersonController : BaseFrontController
    {
        WeChatMember_bll memberBll = BllInstance.WeChatMemberBll;
        MemberAccount_bll memberAccountBll = BllInstance.MemberAccountBll;
        ProcessedHistoryInfo_bll processedHistoryInfoBll = BllInstance.ProcessedHistoryInfoBll;
        ProcessedInfo_bll processedBll = BllInstance.ProcessedInfoBll;
        ProcessingInfo_bll processingBll = BllInstance.ProcessingInfoBll;
        AddressInfo_bll addressBll = BllInstance.AddressInfoBll;
        Product_bll productBll = BllInstance.ProductBll;
        BookMark_bll bookMarkBll = BllInstance.BookMarkBll;
        ShopDetail_bll shopDBll = BllInstance.ShopDetailBll;
        ShopAccount_bll shopABll = BllInstance.ShopAccountBll;
        WeChatQrcode_bll qrCodeBll = BllInstance.WeChatQrcodeBll;
        TXAccount_bll txAccountBll = BllInstance.TXAccountBll;
        ProductOrder_bll productOrderBll = BllInstance.ProductOrderBll;


        #region 个人中心
        public ActionResult Index()
        {
            PersonIndexVM viewModel = new PersonIndexVM();
            WeChatMember member = memberBll.GetModelByOpenId(OpenId);
            if (member != null)
            {
                try
                {
                    WechatUserInfoEntity entity = Utility.JsonToObject<WechatUserInfoEntity>(member.WeChatName);
                    viewModel.ID = member.ID;
                    viewModel.headImg = entity.headimgurl;
                    viewModel.nickName = entity.nickname;
                    viewModel.balance = member.Balance;
                    viewModel.isShop = false;
                    viewModel.havePayed = false;

                    var shopDetailEntity = shopDBll.GetModelByOpenId(OpenId);
                    if (shopDetailEntity != null)
                    {
                        viewModel.isShop = true;

                        if (shopDetailEntity.Status == 1)
                        {
                            viewModel.havePayed = true;
                        }
                    }

                }
                catch (Exception ex)
                {
                    AddLogs("读取用户信息失败", OpenId + ":" + ex.Message);
                }
            }

            return View(viewModel);
        }
        #endregion

        #region 夺宝记录
        public ActionResult RewardRecords(string openId)
        {
            if (string.IsNullOrWhiteSpace(openId))
            {
                openId = OpenId;
            }

            RewardRecordListVM viewModel = new RewardRecordListVM()
            {
                HaveDoneList = new List<RewardRecordDoneVM>(),
                ProcessingList = new List<ProcessingRecordVM>()
            };

            //已揭晓记录
            string strWhere = string.Format(" where OpenID='{0}'", openId);
            var processedList = processedBll.GetAllItems(strWhere);

            if (processedList != null)
            {
                processedList.ForEach((p) =>
                {
                    Product productE = productBll.GetModel(p.ProductID);
                    WeChatMember memberE = memberBll.GetModelByOpenId(p.OpenID);
                    WechatUserInfoEntity entity = Utility.JsonToObject<WechatUserInfoEntity>(memberE.WeChatName);
                    viewModel.memberEntity = entity;
                    bool hasBind = false;
                    if (p.AddressId != null && p.AddressId != Guid.Empty)
                    {
                        hasBind = true;
                    }

                    RewardRecordDoneVM item = new RewardRecordDoneVM()
                    {
                        rewardId = p.ID,
                        productId = p.ProductID,
                        processStr = "第" + p.ProcessNum + "期",
                        productName = productE.Name,
                        productImg = productE.Image,
                        marketPrice = productE.MarketPrice.Value,
                        openId = p.OpenID,
                        userName = entity.nickname,
                        rewardDate = Utility.DTDefaultFormat(p.EndTime),
                        bindAddress = hasBind
                    };
                    viewModel.HaveDoneList.Add(item);
                });
            }

            //正在进行
            strWhere = string.Format(" where OpenID='{0}'", openId);
            var processingList = processingBll.GetAllItems(strWhere);

            if (processingList != null)
            {
                processingList.ForEach((p) =>
                {
                    Product productE = productBll.GetModel(p.ProductID);
                    int actNum = (int)productE.MarketPrice;
                    int haveActNum = processedHistoryInfoBll.GetActedNumberByProduct(productE.ID, 0);
                    int remainActNum = actNum - haveActNum;
                    string processedPercent = ((haveActNum * 100 / actNum)).ToString() + "%";

                    ProcessingRecordVM item = new ProcessingRecordVM()
                    {
                        productId = p.ProductID,
                        productName = productE.Name,
                        productImg = productE.Image,
                        actNumbers = actNum,
                        haveActNumbers = haveActNum,
                        remainActNumbers = remainActNum,
                        processedPercent = processedPercent
                    };
                    viewModel.ProcessingList.Add(item);
                });
            }

            return View(viewModel);
        }
        #endregion

        #region 购物订单
        public ActionResult BuyOrders()
        {
            BuyOrdersVM viewModel = new BuyOrdersVM() { items = new List<BuyOrderVM>() };

            string strWhere = string.Format(" where OpenId='{0}'", OpenId);
            List<ProductOrder> productOrders = productOrderBll.GetAllItems(strWhere);

            if (productOrders != null)
            {
                productOrders.ForEach(
                    (p) =>
                    {
                        Product productE = productBll.GetModel(p.ProductId);
                        AddressInfo addressInfoE = addressBll.GetModel(p.AddressId);

                        BuyOrderVM buyOrder = new BuyOrderVM()
                        {
                            id = p.ID,
                            productId = p.ProductId,
                            productCount = p.Count,
                            productName = productE.Name,
                            productImg = productE.Image,
                            marketPrice = productE.MarketPrice.Value,
                            amount = p.Amount,
                            openId = OpenId,
                            orderNo = p.OrderNo,
                            mobile = addressInfoE.Mobile,
                            receiver = addressInfoE.Receiver,
                            address = addressInfoE.Address,
                            havePay = p.Satus == (int)ProductOrderStatus.NoPay ? false : true
                        };
                        viewModel.items.Add(buyOrder);
                    });
            }

            return View(viewModel);
        }

        public ActionResult DeleteBuyOrder(Guid id)
        {
            string result = "删除成功";

            if (!productOrderBll.Delete(id))
            {
                result = "删除失败";
            }

            return View(result);
        }

        #endregion

        #region 账单明细
        public ActionResult TradeRecords(string openId = "")
        {
            if (string.IsNullOrWhiteSpace(openId))
            {
                openId = OpenId;
            }

            TradeListVM viewModel = new TradeListVM() { rechargeItems = new List<TradeItemVM>(), shackItems = new List<TradeItemVM>() };

            WeChatMember wechatMemberEntity = memberBll.GetModelByOpenId(openId);
            if (wechatMemberEntity == null)
            {
                return View(viewModel);
            }

            viewModel.balance = wechatMemberEntity.Balance;

            //获取充值订单
            string strWhere = string.Format(" where OpenId='{0}' order by CreationDate desc", openId);
            List<MemberAccount> accounts = memberAccountBll.GetAllItems(strWhere);

            accounts.ForEach((p) =>
            {
                viewModel.rechargeTotal += p.Totalfee;
                TradeItemVM item = new TradeItemVM()
                {
                    totalfee = p.Totalfee,
                    creationDate = Utility.DTDefaultFormat(p.CreationDate),
                    tradeNo = p.TradeNo
                };
                viewModel.rechargeItems.Add(item);
            });

            //获取摇一摇订单
            strWhere = string.Format(" where OpenId='{0}' order by CreationTime desc", openId);
            List<ProcessedHistoryInfo> pHistoryInfo = processedHistoryInfoBll.GetAllItems(strWhere);
            viewModel.shackTotal = pHistoryInfo.Count;

            pHistoryInfo.ForEach((p) =>
            {
                TradeItemVM item = new TradeItemVM()
                {
                    totalfee = 1,
                    creationDate = Utility.DTDefaultFormat(p.CreationTime),
                    tradeNo = p.ID.ToString()
                };
                viewModel.shackItems.Add(item);
            });

            return View(viewModel);
        }
        #endregion

        #region 充值
        public ActionResult RechargeView()
        {
            string charge = Request["charge"];
            if (!string.IsNullOrWhiteSpace(charge))
            {
                ViewBag.display = "display:none";
                ViewBag.isreadonly = "readonly=readonly";
                int addPrice = int.Parse(Utility.GetConfigValue("addPrice"));
                if (Request["userAdd"] != null)
                {
                    addPrice = int.Parse(Utility.GetConfigValue("userAdd"));
                }
                ViewBag.value = addPrice;
            }
            else
            {
                ViewBag.value = 1;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Recharge()
        {
            try
            {
                string type = Request["type"];
                decimal amount = decimal.Parse(Request["amount"]);
                ViewBag.Amount = amount;
                if (string.IsNullOrWhiteSpace(type))
                {
                    //充值                   

                    WxPayData unifiedOrderResult = GetUnifiedOrderResult((int)(amount * 100)); //微信这里是以分为单位
                    string wxJsApiParam = GetJsApiParameters(unifiedOrderResult);//获取H5调起JS API参数 
                    ViewBag.wxJsApiParam = wxJsApiParam;
                }
                else if (type == "payforbuy")
                {
                    //购物
                    string orderNo = Request["orderNo"];

                    WxPayData unifiedOrderResult = GetUnifiedOrderResult((int)(amount * 100), orderNo, 1); //微信这里是以分为单位
                    string wxJsApiParam = GetJsApiParameters(unifiedOrderResult);//获取H5调起JS API参数 
                    //AddLogs("微信支付参数", wxJsApiParam);
                    ViewBag.wxJsApiParam = wxJsApiParam;
                }
                else
                {
                    var chargeEntity = shopABll.GetModelByOpenidAndType(OpenId, 0);
                    if (chargeEntity == null)
                    {
                        chargeEntity = new ShopAccount()
                        {
                            ID = Guid.NewGuid(),
                            OpenId = OpenId,
                            Amount = amount,
                            Type = 0,
                            Status = 0,
                            FromOpenId = string.Empty,
                            Note = "商户入驻",
                            OriginalPrice = amount,
                            OrderNo = Utility.GetRandomOrderNo().ToString(),
                            CreationTime = DateTime.Now
                        };

                        shopABll.Insert(chargeEntity);
                    }
                    else
                    {
                        chargeEntity.OrderNo = Utility.GetRandomOrderNo().ToString();
                        shopABll.Update(chargeEntity);
                    }

                    WxPayData unifiedOrderResult = GetUnifiedOrderResult((int)(amount * 100), chargeEntity.OrderNo); //微信这里是以分为单位
                    string wxJsApiParam = GetJsApiParameters(unifiedOrderResult);//获取H5调起JS API参数 
                    //AddLogs("微信支付参数", wxJsApiParam);
                    ViewBag.wxJsApiParam = wxJsApiParam;
                }
            }
            catch (Exception ex)
            {
                AddLogs("微信支付失败", ex.Message);
            }
            return View();
        }

        public ActionResult RechargeTest()
        {
            try
            {
                WxPayData unifiedOrderResult = GetUnifiedOrderResult(1);
                string wxJsApiParam = GetJsApiParameters(unifiedOrderResult);//获取H5调起JS API参数 
                //AddLogs("微信支付参数", wxJsApiParam);
                ViewBag.wxJsApiParam = wxJsApiParam;
            }
            catch (Exception ex)
            {
                AddLogs("微信支付失败", OpenId + ":" + ex.Message);
            }
            return View();
        }

        public ActionResult PaySuccessView()
        {
            return View();
        }

        //微信支付成功返回页面,异步通知
        public ActionResult PaySuccess()
        {
            WxPayData notifyData = GetNotifyData();
            //检查支付结果中transaction_id是否存在
            if (!notifyData.IsSet("transaction_id"))
            {
                //若transaction_id不存在，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "支付结果中微信订单号不存在");
                WxPayAPI.Log.Error(this.GetType().ToString(), "The Pay result is error : " + res.ToXml());
                Response.Write(res.ToXml());
                Response.End();
            }

            string transaction_id = notifyData.GetValue("transaction_id").ToString();

            //查询订单，判断订单真实性
            if (!QueryOrder(transaction_id))
            {
                //若订单查询失败，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "订单查询失败");
                WxPayAPI.Log.Error(this.GetType().ToString(), "Order query failure : " + res.ToXml());
                Response.Write(res.ToXml());
                Response.End();
            }
            //查询订单成功
            else
            {
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "SUCCESS");
                res.SetValue("return_msg", "OK");
                WxPayAPI.Log.Info(this.GetType().ToString(), "order query success : " + res.ToXml());

                try
                {
                    string totalfee = notifyData.GetValue("total_fee").ToString();
                    if (string.IsNullOrEmpty(OpenId))
                    {
                        string openIdReturn = notifyData.GetValue("openid").ToString();
                        OpenId = openIdReturn;
                    }
                    decimal totalfeeD = (int.Parse(totalfee)) / 100;

                    string attach = notifyData.GetValue("attach").ToString();
                    if (attach == "add")
                    {
                        var chargeEntity = shopABll.GetModelByOpenidAndType(OpenId, 0);
                        if (chargeEntity != null)
                        {
                            chargeEntity.Status = 1;
                        }
                        shopABll.Update(chargeEntity);
                        Fenxiao.FX(OpenId, totalfeeD, 1);
                        var shopDetail = shopDBll.GetModelByOpenId(OpenId);
                        if (shopDetail != null)
                        {
                            shopDetail.Status = 1;
                            shopDBll.Update(shopDetail);
                        }
                    }
                    else if (attach == "buy")
                    {
                        string orderNo = notifyData.GetValue("out_trade_no").ToString();
                        var productOrder = productOrderBll.GetModelByOrderNo(orderNo);
                        if (productOrder != null)
                        {
                            productOrder.Satus = (int)ProductOrderStatus.Paied;
                            productOrderBll.Update(productOrder);
                            Fenxiao.FX(OpenId, totalfeeD, 1);
                        }
                    }
                    else
                    {
                        WeChatMember mEntity = memberBll.GetModelByOpenId(OpenId);
                        if (mEntity != null)
                        {
                            mEntity.Balance += totalfeeD;
                            memberBll.Update(mEntity);
                        }
                        else
                        {
                            AddLogs("查找用户出错", "用户" + OpenId + "不存在");
                        }
                    }
                }
                catch (Exception ex)
                {
                    AddLogs("更新账户余额出错", ex.Message);
                }


                Response.Write(res.ToXml());
                Response.End();
            }


            return Content("");
        }

        #region 微信支付相关

        /**
         * 调用统一下单，获得下单结果
         * @return 统一下单结果
         * @失败时抛异常WxPayException
         */
        public WxPayData GetUnifiedOrderResult(int total_fee, string orderNo = "", int type = 0)
        {
            //统一下单
            WxPayData data = new WxPayData();
            data.SetValue("body", "账户充值");
            if (string.IsNullOrWhiteSpace(orderNo))
            {
                data.SetValue("attach", "recharge");
                data.SetValue("out_trade_no", WxPayApi.GenerateOutTradeNo());
            }
            else
            {
                if (type == 0)
                {
                    data.SetValue("attach", "add");
                }
                else if (type == 1)
                {
                    data.SetValue("attach", "buy");
                }
                data.SetValue("out_trade_no", orderNo);
            }
            data.SetValue("total_fee", total_fee);
            data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
            data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));
            data.SetValue("goods_tag", "test");
            data.SetValue("trade_type", "JSAPI");
            data.SetValue("openid", OpenId);

            WxPayData result = WxPayApi.UnifiedOrder(data);
            //插入到充值记录
            MemberAccount account = new MemberAccount()
            {
                ID = Guid.NewGuid(),
                OpenId = OpenId,
                Totalfee = total_fee / 100,
                TradeNo = data.GetValue("out_trade_no").ToString(),
                CreationDate = DateTime.Now
            };
            memberAccountBll.Insert(account);

            if (!result.IsSet("appid") || !result.IsSet("prepay_id") || result.GetValue("prepay_id").ToString() == "")
            {
                WxPayAPI.Log.Error(this.GetType().ToString(), "UnifiedOrder response error!");
                throw new WxPayException("UnifiedOrder response error!");
            }

            return result;
        }

        /**
        *  
        * 从统一下单成功返回的数据中获取微信浏览器调起jsapi支付所需的参数，
        * 微信浏览器调起JSAPI时的输入参数格式如下：
        * {
        *   "appId" : "wx2421b1c4370ec43b",     //公众号名称，由商户传入     
        *   "timeStamp":" 1395712654",         //时间戳，自1970年以来的秒数     
        *   "nonceStr" : "e61463f8efa94090b1f366cccfbbb444", //随机串     
        *   "package" : "prepay_id=u802345jgfjsdfgsdg888",     
        *   "signType" : "MD5",         //微信签名方式:    
        *   "paySign" : "70EA570631E4BB79628FBCA90534C63FF7FADD89" //微信签名 
        * }
        * @return string 微信浏览器调起JSAPI时的输入参数，json格式可以直接做参数用
        * 更详细的说明请参考网页端调起支付API：http://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=7_7
        * 
        */
        public string GetJsApiParameters(WxPayData unifiedOrderResult)
        {
            WxPayAPI.Log.Debug(this.GetType().ToString(), "JsApiPay::GetJsApiParam is processing...");

            WxPayData jsApiParam = new WxPayData();
            jsApiParam.SetValue("appId", unifiedOrderResult.GetValue("appid"));
            jsApiParam.SetValue("timeStamp", WxPayApi.GenerateTimeStamp());
            jsApiParam.SetValue("nonceStr", WxPayApi.GenerateNonceStr());
            jsApiParam.SetValue("package", "prepay_id=" + unifiedOrderResult.GetValue("prepay_id"));
            jsApiParam.SetValue("signType", "MD5");
            jsApiParam.SetValue("paySign", jsApiParam.MakeSign());

            string parameters = jsApiParam.ToJson();

            WxPayAPI.Log.Debug(this.GetType().ToString(), "Get jsApiParam : " + parameters);
            return parameters;
        }

        /// <summary>
        /// 获取微信支付通知
        /// </summary>
        /// <returns></returns>
        public WxPayData GetNotifyData()
        {
            //接收从微信后台POST过来的数据
            System.IO.Stream s = Request.InputStream;
            int count = 0;
            byte[] buffer = new byte[1024];
            StringBuilder builder = new StringBuilder();
            while ((count = s.Read(buffer, 0, 1024)) > 0)
            {
                builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
            }
            s.Flush();
            s.Close();
            s.Dispose();

            WxPayAPI.Log.Info(this.GetType().ToString(), "Receive data from WeChat : " + builder.ToString());

            //转换数据格式并验证签名
            WxPayData data = new WxPayData();
            try
            {
                data.FromXml(builder.ToString());
            }
            catch (WxPayException ex)
            {
                //若签名错误，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", ex.Message);
                WxPayAPI.Log.Error(this.GetType().ToString(), "Sign check error : " + res.ToXml());
                Response.Write(res.ToXml());
                Response.End();
            }

            WxPayAPI.Log.Info(this.GetType().ToString(), "Check sign success");
            return data;
        }

        //查询订单
        private bool QueryOrder(string transaction_id)
        {
            WxPayData req = new WxPayData();
            req.SetValue("transaction_id", transaction_id);
            WxPayData res = WxPayApi.OrderQuery(req);
            if (res.GetValue("return_code").ToString() == "SUCCESS" &&
                res.GetValue("result_code").ToString() == "SUCCESS")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #endregion

        #region 我的收藏

        public ActionResult MyFollows()
        {
            return View();
        }

        #endregion

        #region 地址管理
        public ActionResult Address()
        {
            AddressListVM viewModel = new AddressListVM() { addressList = new List<AddressVM>() };

            string strWhere = string.Format(" where OpenID='{0}'", OpenId);
            List<AddressInfo> entities = addressBll.GetAllItems(strWhere);

            if (entities != null)
            {
                entities.ForEach((p) =>
                {
                    AddressVM item = new AddressVM()
                    {
                        addressId = p.ID,
                        receiverName = p.Receiver,
                        mobile = p.Mobile,
                        address = p.Address
                    };
                    viewModel.addressList.Add(item);
                });
            }

            return View(viewModel);
        }

        public ActionResult AddAddressView(Guid? id)
        {
            AddressVM viewMode = new AddressVM();
            if (id != null)
            {
                AddressInfo model = addressBll.GetModel(id.Value);
                viewMode.addressId = model.ID;
                viewMode.address = model.Address;
                viewMode.mobile = model.Mobile;
                viewMode.receiverName = model.Receiver;
                viewMode.province = model.Province;
                viewMode.zipcode = model.Zipcode;
            }

            return View(viewMode);
        }

        [HttpPost]
        public ActionResult Ajax_Add(AddressVM model)
        {
            string result = "新增成功!";

            if (model.addressId != Guid.Empty)
            {
                //编辑
                AddressInfo entity = addressBll.GetModel(model.addressId);
                if (entity != null)
                {
                    entity.Address = model.address;
                    entity.Mobile = model.mobile;
                    entity.Province = model.province;
                    entity.Receiver = model.receiverName;
                    entity.Zipcode = model.zipcode;

                    if (!addressBll.Update(entity))
                    {
                        result = "更新失败!";
                    }
                    else
                    {
                        result = "更新成功!";
                    }
                }
                else
                {
                    result = "该地址不存在!";
                }
            }
            else
            {
                //新增
                AddressInfo entity = new AddressInfo()
                {
                    ID = Guid.NewGuid(),
                    OpenID = OpenId,
                    Address = model.address,
                    IsDefault = 0,
                    UsedTime = DateTime.Now,
                    CreationTime = DateTime.Now,
                    Receiver = model.receiverName,
                    Mobile = model.mobile,
                    Province = model.province,
                    Zipcode = model.zipcode
                };
                if (!addressBll.Insert(entity))
                {
                    result = "新增地址出错，请重新添加!";
                }
            }

            return Content(result);
        }

        public ActionResult AddressBind(Guid pId)
        {
            AddressListVM viewModel = new AddressListVM() { addressList = new List<AddressVM>(), pId = pId };

            string strWhere = string.Format(" where OpenID='{0}'", OpenId);
            List<AddressInfo> entities = addressBll.GetAllItems(strWhere);
            Guid? addressId = processedBll.GetModel(pId).AddressId;

            if (entities != null)
            {
                entities.ForEach((p) =>
                {
                    bool isBind = false;
                    if (addressId.HasValue && p.ID == addressId.Value)
                    {
                        isBind = true;
                    }

                    AddressVM item = new AddressVM()
                    {
                        addressId = p.ID,
                        receiverName = p.Receiver,
                        mobile = p.Mobile,
                        address = p.Address,
                        isBindAddress = isBind
                    };
                    viewModel.addressList.Add(item);

                });
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Ajax_BindAddress(Guid pId, Guid aId)
        {
            //修改为设置为本次发货地址
            string result = "设置成功!";

            ProcessedInfo pInfo = processedBll.GetModel(pId);
            pInfo.AddressId = aId;
            if (!processedBll.Update(pInfo))
            {
                result = "设置失败";
            }

            return Content(result);
        }

        [HttpPost]
        public ActionResult Ajax_Delete(Guid? addressId)
        {
            string result = "删除成功!";

            if (!addressId.HasValue)
            {
                result = "地址不存在!";
            }
            else
            {
                if (!addressBll.Delete(addressId.Value))
                {
                    result = "删除失败，请重新操作!";
                }
            }

            return Content(result);
        }

        #endregion

        #region 商户管理

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Director()
        {
            DirectorVM viewModel = new DirectorVM();
            viewModel.referrer = "系统";

            WeChatMember member = memberBll.GetModelByOpenId(OpenId);
            if (member != null)
            {
                try
                {
                    WechatUserInfoEntity entity = Utility.JsonToObject<WechatUserInfoEntity>(member.WeChatName);
                    viewModel.headImg = entity.headimgurl;
                    viewModel.nickName = entity.nickname;
                    viewModel.userNo = member.WeChatImage;
                    viewModel.balance = member.Balance;
                    viewModel.sales = 0;
                    viewModel.bonus = 0;
                }
                catch (Exception ex)
                {
                    AddLogs("读取用户信息失败", OpenId + ":" + ex.Message);
                }
            }

            //推荐人信息
            ShopDetail shopEntity = shopDBll.GetModelByOpenId(OpenId);
            if (shopEntity != null)
            {
                if (!string.IsNullOrWhiteSpace(shopEntity.ParentID) && shopEntity.ParentID != "无")
                {
                    member = memberBll.GetModelByOpenId(shopEntity.ParentID);
                    WechatUserInfoEntity entity = Utility.JsonToObject<WechatUserInfoEntity>(member.WeChatName);
                    viewModel.referrer = entity.nickname;
                }
            }

            //订单数和销售额
            decimal sales = 0M;
            string strWhere = string.Format(" where ProductID in(select id from [dbo].[Product] where ShopID in(select id from [dbo].[ShopDetails] where OpenID='{0}'))", OpenId);
            var myOrders = processedBll.GetAllItems(strWhere);
            Product productEntity;            
            if (myOrders != null)
            {
                myOrders.ForEach((p) =>
                {
                    productEntity = productBll.GetModel(p.ProductID);
                    if (productEntity != null)
                    {
                        viewModel.sales += productEntity.StockPrice.Value;
                    }
                });
            }
            var buyOrders = productOrderBll.GetAllItems(strWhere).Where(p => p.Satus != (int)ProductOrderStatus.NoPay).ToList();
            if (buyOrders != null)
            {
                buyOrders.ForEach((p) =>
                {
                    productEntity = productBll.GetModel(p.ProductId);
                    if (productEntity != null)
                    {
                        viewModel.sales += productEntity.StockPrice.Value * p.Count;
                    }
                });
            }
            //奖金
            strWhere = string.Format(" where OpenID='{0}' and type != 0", OpenId);
            var myAccounts = shopABll.GetAllItems(strWhere);
            if (myAccounts != null)
            {
                viewModel.orderNum = myAccounts.Count.ToString();
                myAccounts.ForEach((p) =>
                {
                    viewModel.bonus += p.Amount;
                });
            }
            viewModel.orderTotal = viewModel.bonus + viewModel.sales;
            //员工数             
            var listlevel1 = shopDBll.GetLevel1(OpenId);
            var listlevel2 = shopDBll.GetLevel2(OpenId);
            var listlevel3 = shopDBll.GetLevel3(OpenId);

            if (listlevel1 != null)
            {
                viewModel.staffNum += listlevel1.Count;
            }
            if (listlevel2 != null)
            {
                viewModel.staffNum += listlevel2.Count;
            }
            if (listlevel3 != null)
            {
                viewModel.staffNum += listlevel3.Count;
            }

            return View(viewModel);
        }

        /// <summary>
        /// 我要加入
        /// </summary>
        /// <returns></returns>
        public ActionResult Join()
        {
            JoinVM viewModel = new JoinVM();

            var entity = shopDBll.GetModelByOpenId(OpenId);
            if (entity != null)
            {
                viewModel.id = entity.ID;
                viewModel.contacts = entity.Contacts;
                viewModel.mobile = entity.Mobile;
                viewModel.weChatName = entity.WeChatName;
                viewModel.address = entity.Address;
                viewModel.note = entity.Note;
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult _AjaxAdd(JoinVM model)
        {
            string strRedirct = "/FrontPerson/RechargeView?charge=true";
            var entity = shopDBll.GetModel(model.id);

            int addPrice = int.Parse(Utility.GetConfigValue("addPrice"));
            if (Request["userAdd"] != null)
            {
                addPrice = int.Parse(Utility.GetConfigValue("userAdd"));
                strRedirct += "&userAdd=yes";
            }

            if (entity != null)
            {
                entity.Contacts = model.contacts;
                entity.Mobile = model.mobile;
                entity.Address = model.address;
                entity.WeChatName = model.weChatName;
                entity.Note = model.note;

                shopDBll.Update(entity);
            }
            else
            {
                entity = new ShopDetail()
                {
                    ID = Guid.NewGuid(),
                    OpenID = OpenId,
                    ParentID = string.Empty,
                    WeChatName = model.weChatName,
                    Contacts = model.contacts,
                    Mobile = model.mobile,
                    Address = model.address,
                    Note = model.address,
                    ProductNum = 100,
                    Status = -1,
                    CreationTime = DateTime.Now
                };
                shopDBll.Insert(entity);
            }

            var chargeEntity = shopABll.GetModelByOpenidAndType(OpenId, 0);

            if (chargeEntity == null)
            {
                chargeEntity = new ShopAccount()
                {
                    ID = Guid.NewGuid(),
                    OpenId = OpenId,
                    Amount = addPrice,
                    Type = 0,
                    Status = 0,
                    FromOpenId = string.Empty,
                    Note = "用户代言",
                    OriginalPrice = addPrice,
                    OrderNo = Utility.GetRandomOrderNo().ToString(),
                    CreationTime = DateTime.Now
                };
                shopABll.Insert(chargeEntity);
            }


            return Redirect(strRedirct);
        }

        /// <summary>
        /// 我的银行
        /// </summary>
        /// <returns></returns>
        public ActionResult MyBank()
        {
            MyBankVM viewModel = new MyBankVM();
            WeChatMember member = memberBll.GetModelByOpenId(OpenId);
            if (member != null)
            {
                string txStr = string.Format(" where OpenId='{0}'", OpenId);
                //已收账款
                var txAccountList = txAccountBll.GetAllItems(txStr);
                if (txAccountList != null)
                {
                    txAccountList.ForEach((p) =>
                    {
                        viewModel.got += p.Amount;
                    });
                }
                
                viewModel.noGot = member.Balance;
                if (member.Balance > 100)
                {
                    viewModel.canTX = member.Balance;
                }
            }

            //销售额
            decimal sales = 0M;
            string strWhere = string.Format(" where ProductID in(select id from [dbo].[Product] where ShopID in(select id from [dbo].[ShopDetails] where OpenID='{0}'))", OpenId);
            var myOrders = processedBll.GetAllItems(strWhere);
            Product productEntity;
            if (myOrders != null)
            {
                myOrders.ForEach((p) =>
                {
                    productEntity = productBll.GetModel(p.ProductID);
                    if (productEntity != null)
                    {
                        sales += productEntity.StockPrice.Value;
                    }
                });
            }
            var buyOrders = productOrderBll.GetAllItems(strWhere).Where(p => p.Satus != (int)ProductOrderStatus.NoPay).ToList();
            if (buyOrders != null)
            {
                buyOrders.ForEach((p) =>
                {
                    productEntity = productBll.GetModel(p.ProductId);
                    if (productEntity != null)
                    {
                        sales += productEntity.StockPrice.Value * p.Count;
                    }
                });
            }

            //奖金
            decimal bonus = 0M;
            strWhere = string.Format(" where OpenID='{0}' and type != 0", OpenId);
            var myAccounts = shopABll.GetAllItems(strWhere);
            if (myAccounts != null)
            {
                myAccounts.ForEach((p) =>
                {
                    bonus += p.Amount;
                });
            }
            viewModel.sholdGet = bonus + sales;

            return View(viewModel);
        }

        /// <summary>
        /// 我的董事局
        /// </summary>
        /// <returns></returns>
        public ActionResult MyDirector()
        {
            MyDirectorVM viewModel = new MyDirectorVM();
            WeChatMember member = memberBll.GetModelByOpenId(OpenId);
            if (member != null)
            {
                try
                {
                    WechatUserInfoEntity entity = Utility.JsonToObject<WechatUserInfoEntity>(member.WeChatName);
                    viewModel.headImg = entity.headimgurl;
                    viewModel.nickName = entity.nickname;
                    viewModel.userNo = member.WeChatImage;
                    viewModel.sales = 0;
                    viewModel.bonus = 0;
                    viewModel.level1Count = 0;
                    viewModel.level2Count = 0;
                    viewModel.level3Count = 0;
                }
                catch (Exception ex)
                {
                    AddLogs("读取用户信息失败", OpenId + ":" + ex.Message);
                }
            }

            //订单数和销售额
            string strWhere = string.Format(" where ProductID in(select id from [dbo].[Product] where ShopID in(select id from [dbo].[ShopDetails] where OpenID='{0}'))", OpenId);
            var myOrders = processedBll.GetAllItems(strWhere);
            Product productEntity;
            if (myOrders != null)
            {
                myOrders.ForEach((p) =>
                {
                    productEntity = productBll.GetModel(p.ProductID);
                    if (productEntity != null)
                    {
                        viewModel.sales += productEntity.StockPrice.Value;
                    }
                });
            }

            var buyOrders = productOrderBll.GetAllItems(strWhere).Where(p => p.Satus != (int)ProductOrderStatus.NoPay).ToList();
            if (buyOrders != null)
            {
                buyOrders.ForEach((p) =>
                {
                    productEntity = productBll.GetModel(p.ProductId);
                    if (productEntity != null)
                    {
                        viewModel.sales += productEntity.StockPrice.Value * p.Count;
                    }
                });
            }

            //奖金
            strWhere = string.Format(" where OpenID='{0}' and type != 0", OpenId);
            var myAccounts = shopABll.GetAllItems(strWhere);
            if (myAccounts != null)
            {
                myAccounts.ForEach((p) =>
                {
                    viewModel.bonus += p.Amount;
                });
            }
            //员工
            var listlevel1 = shopDBll.GetLevel1(OpenId);
            var listlevel2 = shopDBll.GetLevel2(OpenId);
            var listlevel3 = shopDBll.GetLevel3(OpenId);

            viewModel.level1Count = listlevel1.Count;
            viewModel.level2Count = listlevel2.Count;
            viewModel.level3Count = listlevel3.Count;

            return View(viewModel);
        }

        /// <summary>
        /// 提现订单
        /// </summary>
        /// <returns></returns>
        public ActionResult TXOrder()
        {
            TXOrderListVM viewModel = new TXOrderListVM() { itemList = new List<TXOrderVM>() };
            WeChatMember member = memberBll.GetModelByOpenId(OpenId);
            if (member != null)
            {
                try
                {
                    WechatUserInfoEntity entity = Utility.JsonToObject<WechatUserInfoEntity>(member.WeChatName);
                    viewModel.headImg = entity.headimgurl;
                    viewModel.nickName = entity.nickname;
                    viewModel.userNo = member.WeChatImage;
                }
                catch (Exception ex)
                {
                    AddLogs("读取用户信息失败", OpenId + ":" + ex.Message);
                }
            }

            //获取提现列表
            string strWhere = string.Format(" where OpenId='{0}' order by Status,CreationDate", OpenId);
            var orderList = txAccountBll.GetAllItems(strWhere);
            if (orderList != null)
            {
                orderList.ForEach((p) =>
                {
                    TXOrderVM item = new TXOrderVM()
                    {
                        orderNo = p.OrderNo,
                        amount = p.Amount,
                        status = p.Status == (int)TXOrderStatus.NoCheck ? "待审核" : "已审核"
                    };
                    viewModel.itemList.Add(item);
                });
            }

            return View(viewModel);
        }

        /// <summary>
        /// 提现
        /// </summary>
        /// <returns></returns>
        public ActionResult TX()
        {
            return View();
        }

        public ActionResult _AjaxTX(decimal amount)
        {
            string result = "提现成功，请等待管理员审核!";

            if (amount < 100)
            {
                result = "提现金额不足!";
            }
            else
            {
                WeChatMember currentUser = memberBll.GetModelByOpenId(OpenId);
                if (amount > currentUser.Balance)
                {
                    result = "余额不足!";
                }
                else
                {
                    TXAccount txAccount = txAccountBll.GetModelByOpenIdType(OpenId, (int)TXOrderStatus.NoCheck);
                    if (txAccount != null)
                    {
                        result = "已有订单在审核!";
                    }
                    else
                    {
                        txAccount = new TXAccount()
                        {
                            ID = Guid.NewGuid(),
                            OrderNo = Utility.GetRandomOrderNo(),
                            OpenId = OpenId,
                            Amount = amount,
                            Status = (int)TXOrderStatus.NoCheck,
                            CreationDate = DateTime.Now
                        };

                        if (txAccountBll.Insert(txAccount))
                        {
                            currentUser.Balance -= amount;
                            memberBll.Update(currentUser);
                        }
                    }
                }
            }

            return Content(result);
        }

        /// <summary>
        /// 我的订单
        /// </summary>
        /// <returns></returns>
        public ActionResult MyOrder()
        {
            MyOrderListVM viewModel = new MyOrderListVM() { WaitForSendList = new List<MyOrderVM>(), SentList = new List<MyOrderVM>() };

            WeChatMember member = memberBll.GetModelByOpenId(OpenId);
            if (member != null)
            {
                try
                {
                    WechatUserInfoEntity userEntity = Utility.JsonToObject<WechatUserInfoEntity>(member.WeChatName);
                    viewModel.headImg = userEntity.headimgurl;
                    viewModel.nickName = userEntity.nickname;
                    viewModel.userNo = member.WeChatImage;
                }
                catch (Exception ex)
                {
                    AddLogs("读取用户信息失败", OpenId + ":" + ex.Message);
                }
            }

            Product productE;
            WeChatMember memberE;
            WechatUserInfoEntity entity;

            #region 商铺所有摇奖订单

            string strWhere = string.Format(" where ProductID in(select id from [dbo].[Product] where ShopID in(select id from [dbo].[ShopDetails] where OpenID='{0}'))", OpenId);
            var myOrders = processedBll.GetAllItems(strWhere);
            if (myOrders != null)
            {
                //待发货订单            
                var waitingOrders = myOrders.Where(p => p.Status == 0).ToList();
                if (waitingOrders != null)
                {
                    waitingOrders.ForEach((p) =>
                    {
                        productE = productBll.GetModel(p.ProductID);
                        memberE = memberBll.GetModelByOpenId(p.OpenID);
                        entity = Utility.JsonToObject<WechatUserInfoEntity>(memberE.WeChatName);

                        MyOrderVM item = new MyOrderVM()
                        {
                            orderId = p.ID,
                            productId = productE.ID,
                            productName = productE.Name,
                            productImg = productE.Image,
                            productPrice = productE.StockPrice.Value,
                            gotDate = Utility.DTDefaultFormat(p.EndTime),
                            gotUserName = entity.nickname
                        };
                        viewModel.WaitForSendList.Add(item);
                    });
                }
                //已发货订单 
                var sentOrders = myOrders.Where(p => p.Status == 1).ToList();
                if (sentOrders != null)
                {                    
                    sentOrders.ForEach((p) =>
                    {
                        productE = productBll.GetModel(p.ProductID);
                        memberE = memberBll.GetModelByOpenId(p.OpenID);
                        entity = Utility.JsonToObject<WechatUserInfoEntity>(memberE.WeChatName);

                        MyOrderVM item = new MyOrderVM()
                        {
                            orderId = p.ID,
                            productId = productE.ID,
                            productName = productE.Name,
                            productImg = productE.Image,
                            productPrice = productE.StockPrice.Value,
                            gotDate = Utility.DTDefaultFormat(p.EndTime),
                            gotUserName = entity.nickname
                        };

                        viewModel.SentList.Add(item);
                    });
                }
            } 

            #endregion

            #region 商铺购物订单

            strWhere = string.Format(" where ProductID in(select id from [dbo].[Product] where ShopID in(select id from [dbo].[ShopDetails] where OpenID='{0}'))", OpenId);
            var buyOrders = productOrderBll.GetAllItems(strWhere);
            if (buyOrders != null)
            {
                //待发货订单            
                var waitingOrders = buyOrders.Where(p => p.Satus == 1).ToList();
                if (waitingOrders != null)
                {
                    
                    waitingOrders.ForEach((p) =>
                    {
                        productE = productBll.GetModel(p.ProductId);
                        memberE = memberBll.GetModelByOpenId(p.OpenId);
                        entity = Utility.JsonToObject<WechatUserInfoEntity>(memberE.WeChatName);

                        MyOrderVM item = new MyOrderVM()
                        {
                            orderId = p.ID,
                            productId = productE.ID,
                            productName = productE.Name,
                            productImg = productE.Image,
                            productPrice = productE.StockPrice.Value,
                            gotDate = Utility.DTDefaultFormat(p.CreationDate),
                            gotUserName = entity.nickname,
                            productCount = p.Count
                        };
                        viewModel.WaitForSendList.Add(item);
                    });
                }
                //已发货订单 
                var sentOrders = buyOrders.Where(p => p.Satus == 2).ToList();
                if (sentOrders != null)
                {                    
                    sentOrders.ForEach((p) =>
                    {
                        productE = productBll.GetModel(p.ProductId);
                        memberE = memberBll.GetModelByOpenId(p.OpenId);
                        entity = Utility.JsonToObject<WechatUserInfoEntity>(memberE.WeChatName);

                        MyOrderVM item = new MyOrderVM()
                        {
                            orderId = p.ID,
                            productId = productE.ID,
                            productName = productE.Name,
                            productImg = productE.Image,
                            productPrice = productE.StockPrice.Value,
                            gotDate = Utility.DTDefaultFormat(p.CreationDate),
                            gotUserName = entity.nickname,
                            productCount = p.Count
                        };

                        viewModel.SentList.Add(item);
                    });
                }
            } 

            #endregion

            viewModel.waitForSentNum = viewModel.WaitForSendList.Count;                    


            return View(viewModel);
        }

        /// <summary>
        /// 摇奖订单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult OrderDetail(Guid id)
        {
            OrderDetailVM viewModel = new OrderDetailVM() { orderId = id };

            ProcessedInfo processedE = processedBll.GetModel(id);
            viewModel.processedNum = processedE.ProcessNum;
            viewModel.gotDate = Utility.DTDefaultFormat(processedE.EndTime);
            if (processedE.Status == 1)
            {
                viewModel.haveSent = true;
            }
            Product productE = productBll.GetModel(processedE.ProductID);
            viewModel.productId = productE.ID;
            viewModel.productImg = productE.Image;
            viewModel.productName = productE.Name;
            viewModel.productPrice = productE.MarketPrice.Value;

            if (processedE.AddressId.HasValue)
            {
                AddressInfo addressE = addressBll.GetModel(processedE.AddressId.Value);
                viewModel.receiver = addressE.Receiver;
                viewModel.mobile = addressE.Mobile;
                viewModel.address = addressE.Address;
            }

            return View(viewModel);
        }

        public ActionResult _AjaxSent(Guid id)
        {
            string result = "发货成功";

            ProcessedInfo info = processedBll.GetModel(id);

            if (info != null)
            {
                info.Status = 1;
                if (!processedBll.Update(info))
                {
                    result = "发货失败";
                }

                //分销
                Product productE = productBll.GetModel(info.ProductID);
                if (productE != null)
                {
                    ShopDetail shopDEntity = shopDBll.GetModel(Guid.NewGuid());
                    if (shopDEntity != null)
                    {
                        Fenxiao.FX(shopDEntity.OpenID, productE.StockPrice.Value);
                    }
                }
                else
                {
                    result = "三级分销分润出错,产品不存在!";
                }
            }

            return Content(result);
        }

        /// <summary>
        /// 购物订单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult BuyOrderDetail(Guid id)
        {
            OrderDetailVM viewModel = new OrderDetailVM() { orderId = id };

            ProductOrder orderE = productOrderBll.GetModel(id);
            viewModel.amount = orderE.Amount;
            viewModel.gotDate = Utility.DTDefaultFormat(orderE.CreationDate);
            viewModel.bugCount = orderE.Count;
            viewModel.orderNo = orderE.OrderNo;
            switch (orderE.Satus)
            {
                case (int)ProductOrderStatus.NoPay:
                    viewModel.status = "未支付";
                    break;
                case (int)ProductOrderStatus.Paied:
                    viewModel.haveSent = true;
                    viewModel.status = "未发货";
                    break;
                case (int)ProductOrderStatus.Returns:
                    viewModel.status = "已发货";
                    break;
            }
            Product productE = productBll.GetModel(orderE.ProductId);
            viewModel.productId = productE.ID;
            viewModel.productImg = productE.Image;
            viewModel.productName = productE.Name;
            viewModel.productPrice = productE.MarketPrice.Value;

            if (orderE.AddressId != Guid.Empty)
            {
                AddressInfo addressE = addressBll.GetModel(orderE.AddressId);
                viewModel.receiver = addressE.Receiver;
                viewModel.mobile = addressE.Mobile;
                viewModel.address = addressE.Address;
            }

            return View(viewModel);
        }

        public ActionResult _AjaxBuySent(Guid id)
        {
            string result = "发货成功";

            //ProductOrder info = productOrderBll.GetModel(id);

            //if (info != null)
            //{
            //    info.Satus = 2;
            //    if (!productOrderBll.Update(info))
            //    {
            //        result = "发货失败";
            //    }

            //    //分销
            //    Product productE = productBll.GetModel(info.ProductId);
            //    if (productE != null)
            //    {
            //        ShopDetail shopDEntity = shopDBll.GetModel(productE.ShopID);
            //        if (shopDEntity != null)
            //        {
            //            Fenxiao.FX(shopDEntity.OpenID, productE.StockPrice * info.Count);
            //        }
            //    }
            //    else
            //    {
            //        result = "三级分销分润出错,产品不存在!";
            //    }
            //}

            return Content(result);
        }

        /// <summary>
        /// 我的二维码
        /// </summary>
        /// <returns></returns>
        public ActionResult Qrcode()
        {
            //判断是否已存在二维码，这里调取接口生成永久二维码
            WeChatQrcode qrcodeEntity = qrCodeBll.GetModelByOpenId(OpenId);
            if (qrcodeEntity != null)
            {
                //判断是否过期
                //DateTime dtQrCode = qrcodeEntity.CreationTime;
                //int qrDays = DateTime.Now.Subtract(dtQrCode).Days;
                //if (qrDays < 29 || true)
                //{                    
                //}
                ViewBag.qrcode = qrcodeEntity.QrcodePath;
                return View();
            }

            string imgPath = Server.MapPath("/qrcodes/");
            if (!System.IO.Directory.Exists(imgPath))
            {
                System.IO.Directory.CreateDirectory(imgPath);
            }
            WeChatMember member = memberBll.GetModelByOpenId(OpenId);
            imgPath += member.WeChatImage + ".jpg";
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;

            //获取平台accesstoken
            //string getAccessTokenUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wx53ba599c0a8429fe&secret=2ed69f3f5865214b7d7f7a43192592e2";
            string getAccessTokenUrl = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", WxPayConfig.APPID, WxPayConfig.APPSECRET);
            string accessTokenJson = client.DownloadString(getAccessTokenUrl);
            JavaScriptSerializer Jss = new JavaScriptSerializer();
            Dictionary<string, object> respDic = (Dictionary<string, object>)Jss.DeserializeObject(accessTokenJson);
            string accessToken = respDic["access_token"].ToString();
            string QrcodeUrl = string.Format("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={0}", accessToken);//WxQrcodeAPI接口
            string postString = "{\"action_name\": \"QR_LIMIT_STR_SCENE\", \"action_info\": {\"scene\": {\"scene_str\": \"" + member.OpenID + "\"}}}";

            byte[] postData = Encoding.UTF8.GetBytes(postString);//编码，尤其是汉字，事先要看下抓取网页的编码方式  
            WebClient webClient = new WebClient();
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");//采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可  
            byte[] responseData = webClient.UploadData(QrcodeUrl, "POST", postData);//得到返回字符流  
            string srcString = Encoding.UTF8.GetString(responseData);//解码
            respDic = (Dictionary<string, object>)Jss.DeserializeObject(srcString);
            string ticket = System.Web.HttpUtility.UrlEncode(respDic["ticket"].ToString());
            string getQrcodeUrl = string.Format("https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket={0}", ticket);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(getQrcodeUrl);
            req.Method = "GET";
            using (WebResponse wr = req.GetResponse())
            {
                HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();
                string strpath = myResponse.ResponseUri.ToString();

                WebClient mywebclient = new WebClient();
                try
                {
                    if (System.IO.File.Exists(imgPath))
                    {
                        System.IO.File.Delete(imgPath);
                    }

                    mywebclient.DownloadFile(strpath, imgPath);
                    string dbPath = "/qrcodes/" + member.WeChatImage + ".jpg";
                    ViewBag.qrcode = dbPath;
                    qrcodeEntity = new WeChatQrcode()
                    {
                        ID = Guid.NewGuid(),
                        OpenId = OpenId,
                        QrcodePath = dbPath,
                        CreationTime = DateTime.Now
                    };
                    qrCodeBll.Insert(qrcodeEntity);
                }
                catch (Exception ex)
                {
                    throw new Exception("获取二维码图片失败！" + ex.Message);
                }
            }

            return View();
        }

        #endregion

        #region 用户注册

        public ActionResult UserJoin()
        {
            JoinVM viewModel = new JoinVM();

            var entity = shopDBll.GetModelByOpenId(OpenId);
            if (entity != null)
            {
                viewModel.id = entity.ID;
                viewModel.contacts = entity.Contacts;
                viewModel.mobile = entity.Mobile;
                viewModel.weChatName = entity.WeChatName;
                viewModel.address = entity.Address;
                viewModel.note = entity.Note;
            }

            return View(viewModel);
        }

        #endregion

    }
}
