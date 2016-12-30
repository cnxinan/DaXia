using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaXia.WebComAdmin.Front;
using DaXia.EntityDataModels;
using DaXia.BLL;
using DaXia.WebFrameWork;

namespace DaXia.WebComAdmin.Controllers
{
    public class FrontProductController : BaseFrontController
    {
        private readonly ProcessedInfo_bll processedBll = BllInstance.ProcessedInfoBll;
        private readonly ProcessingInfo_bll processingBll = BllInstance.ProcessingInfoBll;
        private readonly ProcessedHistoryInfo_bll processedHistoryBll = BllInstance.ProcessedHistoryInfoBll;
        private readonly Product_bll productBll = BllInstance.ProductBll;
        private readonly WeChatMember_bll memberBll = BllInstance.WeChatMemberBll;
        private readonly ProductCatalog_bll catalogBll = BllInstance.ProductCatalogBll;
        private readonly AddressInfo_bll addressBll = BllInstance.AddressInfoBll;
        private readonly ProductOrder_bll pOrderBll = BllInstance.ProductOrderBll;

        /// <summary>
        /// 分类
        /// </summary>
        /// <returns></returns>
        public ActionResult Catalogs()
        {
            CatalogListVM viewModel = new CatalogListVM()
            {
                catalogLists = new List<CatalogItemsVM>(),
                catalogNames = new List<string>()
            };
            string strWhere;
            List<ProductCatalog> catalogs;
            CatalogItemsVM items;
            string fenleiStr = "fenlei";

            //添加推荐分类
            strWhere = string.Format("select top 15 * from [ProductCatalog] where ParID<>'{0}' order by Sort desc", Guid.Empty);
            catalogs = catalogBll.GetAllItems(strWhere);
            if (catalogs != null)
            {
                items = new CatalogItemsVM() { titleName = "推荐分类", itemList = new List<CatalogItemVM>() };
                catalogs.ForEach((p) =>
                {
                    CatalogItemVM item = new CatalogItemVM()
                    {
                        catalogId = p.ID,
                        image = p.HeadImage,
                        name = p.Name
                    };
                    items.itemList.Add(item);
                });
                viewModel.catalogLists.Add(items);
            }

            //添加其他分类
            strWhere = string.Format(" where ParID='{0}' order by Sort desc", Guid.Empty);
            List<ProductCatalog> topCatalogs = catalogBll.GetAllItems(strWhere);
            if (topCatalogs != null)
            {
                topCatalogs.ForEach((p) =>
                {
                    viewModel.catalogNames.Add(p.Name);

                    items = new CatalogItemsVM() { titleName = p.Name, itemList = new List<CatalogItemVM>() };
                    strWhere = string.Format(" where ParID='{0}' order by Sort desc", p.ID);
                    catalogs = catalogBll.GetAllItems(strWhere);
                    if (catalogs != null)
                    {
                        catalogs.ForEach((q) =>
                        {
                            CatalogItemVM item = new CatalogItemVM()
                            {
                                catalogId = q.ID,
                                image = q.HeadImage,
                                name = q.Name
                            };
                            items.itemList.Add(item);
                        });
                        viewModel.catalogLists.Add(items);
                    }
                });
            }

            return View(viewModel);
        }

        public ActionResult CatalogDetails(Guid id)
        {
            CatalogDetailListVM viewModel = new CatalogDetailListVM()
            {
                processingList = new List<CatalogDetailvVM>(),
                mostBookMarkList = new List<CatalogDetailvVM>(),
                newestList = new List<CatalogDetailvVM>(),
                priceList = new List<CatalogDetailvVM>()
            };

            List<Product> products;
            string sqlWhere;

            //即将揭晓
            sqlWhere = string.Format(" where CatalogID='{0}' and ID in (select ProductID from ProcessingInfo)", id);
            products = productBll.GetAllItems(sqlWhere);

            if (products != null)
            {
                products.ForEach((p) =>
                {
                    int actNum = (int)p.MarketPrice;
                    int haveActNum = processedHistoryBll.GetActedNumberByProduct(p.ID, 0);
                    int remainActNum = actNum - haveActNum;
                    CatalogDetailvVM item = new CatalogDetailvVM()
                    {
                        productId = p.ID,
                        image = p.Image,
                        name = p.Name,
                        actNumbers = actNum,
                        haveActNumbers = haveActNum,
                        remainActNumbers = remainActNum,
                        processedPercent = ((haveActNum * 100 / actNum)).ToString() + "%"
                    };
                    viewModel.processingList.Add(item);
                });
            }

            //人气
            sqlWhere = string.Format(" where CatalogID='{0}' ", id);
            products = productBll.GetAllItems(sqlWhere);

            if (products != null)
            {
                products.ForEach((p) =>
                {
                    int actNum = (int)p.MarketPrice;
                    int haveActNum = processedHistoryBll.GetActedNumberByProduct(p.ID, 0);
                    int remainActNum = actNum - haveActNum;
                    CatalogDetailvVM item = new CatalogDetailvVM()
                    {
                        productId = p.ID,
                        image = p.Image,
                        name = p.Name,
                        actNumbers = actNum,
                        haveActNumbers = haveActNum,
                        remainActNumbers = remainActNum,
                        processedPercent = ((haveActNum * 100 / actNum)).ToString() + "%"
                    };
                    viewModel.mostBookMarkList.Add(item);
                });
            }
            //最新
            sqlWhere = string.Format(" where CatalogID='{0}' order by CreationTime desc", id);
            products = productBll.GetAllItems(sqlWhere);

            if (products != null)
            {
                products.ForEach((p) =>
                {
                    int actNum = (int)p.MarketPrice;
                    int haveActNum = processedHistoryBll.GetActedNumberByProduct(p.ID, 0);
                    int remainActNum = actNum - haveActNum;
                    CatalogDetailvVM item = new CatalogDetailvVM()
                    {
                        productId = p.ID,
                        image = p.Image,
                        name = p.Name,
                        actNumbers = actNum,
                        haveActNumbers = haveActNum,
                        remainActNumbers = remainActNum,
                        processedPercent = ((haveActNum * 100 / actNum)).ToString() + "%"
                    };
                    viewModel.newestList.Add(item);
                });
            }
            //价格
            sqlWhere = string.Format(" where CatalogID='{0}' order by MarketPrice asc", id);
            products = productBll.GetAllItems(sqlWhere);

            if (products != null)
            {
                products.ForEach((p) =>
                {
                    int actNum = (int)p.MarketPrice;
                    int haveActNum = processedHistoryBll.GetActedNumberByProduct(p.ID, 0);
                    int remainActNum = actNum - haveActNum;
                    CatalogDetailvVM item = new CatalogDetailvVM()
                    {
                        productId = p.ID,
                        image = p.Image,
                        name = p.Name,
                        actNumbers = actNum,
                        haveActNumbers = haveActNum,
                        remainActNumbers = remainActNum,
                        processedPercent = ((haveActNum * 100 / actNum)).ToString() + "%"
                    };
                    viewModel.priceList.Add(item);
                });
            }
            return View(viewModel);
        }

        /// <summary>
        /// 产品详情
        /// </summary>
        /// <returns></returns>
        public ActionResult Product(Guid id)
        {
            ProductDetialVM viewModel = new ProductDetialVM() { processList = new List<string>(), imageList = new List<string>(), productId = id };

            EntityDataModels.Product productEntity = productBll.GetModel(id);

            viewModel.processNumStr = "第" + 0.ToString() + "期";
            viewModel.productName = productEntity.Name;
            viewModel.marketPrice = productEntity.MarketPrice.Value;
            viewModel.productIamge = productEntity.Image;
            viewModel.status = "进行中";
            viewModel.actNumbers = (int)productEntity.MarketPrice;
            viewModel.haveActNumbers = processedHistoryBll.GetActedNumberByProduct(productEntity.ID, 0);
            viewModel.remainActNumbers = viewModel.actNumbers - viewModel.haveActNumbers;
            viewModel.processedPercent = ((viewModel.haveActNumbers * 100 / viewModel.actNumbers)).ToString() + "%";

            //获取当前进度冠军信息
            ProcessingInfo processingInfoEntity = processingBll.GetChampionInfo(id, 0);
            if (processingInfoEntity != null)
            {
                viewModel.haveChampion = true;

                WeChatMember championMemberEntity = memberBll.GetModelByOpenId(processingInfoEntity.OpenID);
                if (championMemberEntity != null)
                {
                    try
                    {
                        WechatUserInfoEntity entity = Utility.JsonToObject<WechatUserInfoEntity>(championMemberEntity.WeChatName);
                        viewModel.championOpenId = processingInfoEntity.OpenID;
                        viewModel.championName = entity.nickname;
                        viewModel.championHeadImage = entity.headimgurl;
                        viewModel.championCity = entity.city;
                        viewModel.championDate = Utility.DTDefaultFormat(processingInfoEntity.StartTime);
                        viewModel.championNo = championMemberEntity.WeChatImage;
                        viewModel.rewardTimes = processedBll.GetRewardTimes(processingInfoEntity.OpenID);

                    }
                    catch (Exception ex)
                    {
                        AddLogs("读取用户信息失败", processingInfoEntity.OpenID + ":" + ex.Message);
                    }
                }
            }

            return View(viewModel);
        }

        /// <summary>
        /// 图文详情
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductContent(Guid id)
        {
            EntityDataModels.Product productEntity = productBll.GetModel(id);

            return View(productEntity);
        }

        /// <summary>
        /// 摇一摇
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Shack(Guid id)
        {
            ShackVM viewModel = new ShackVM();
            WeChatMember nowUser = memberBll.GetModelByOpenId(OpenId);
            //判断余额 
            if (nowUser.Balance < 1)
            {
                return Redirect("/FrontPerson/RechargeView");
            }

            //扣除账户余额
            nowUser.Balance -= 1;
            memberBll.Update(nowUser);

            EntityDataModels.Product productEntity = productBll.GetModel(id);

            viewModel.productId = id;
            viewModel.haveActNumbers = processedHistoryBll.GetActedNumberByProduct(id, 0);


            return View(viewModel);
        }

        /// <summary>
        /// 摇奖排行:展示本期活动前10名+摇奖用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Ranking(Guid id, int score)
        {
            if (string.IsNullOrWhiteSpace(OpenId))
            {
                return Content(string.Empty);
            }

            RankListVM viewModel = new RankListVM() { itemList = new List<RankItemVM>() };

            EntityDataModels.Product productEntity = productBll.GetModel(id);

            int bestScore = score;
            bool hasScore = true;

            #region 把本次成绩插入数据库
            if (bestScore > -1)
            {
                ProcessedHistoryInfo pHistroyInfo = new ProcessedHistoryInfo()
                {
                    ID = Guid.NewGuid(),
                    ProcessNum = 0,
                    OpenID = OpenId,
                    ProductID = id,
                    Result = score,
                    CreationTime = DateTime.Now
                };

                if (!processedHistoryBll.Insert(pHistroyInfo))
                {
                    AddLogs("插入成绩错误", "");
                }
            }

            //对比正在进行的
            ProcessingInfo pIngInfo = processingBll.GetBestRecord(id, 0, OpenId);
            if (pIngInfo == null)
            {
                if (bestScore < 0)
                {
                    hasScore = false;
                }
                else
                {
                    pIngInfo = new ProcessingInfo()
                    {
                        ID = Guid.NewGuid(),
                        OpenID = OpenId,
                        ProcessNum = 0,
                        Result = score,
                        ProductID = id,
                        StartTime = DateTime.Now
                    };

                    processingBll.Insert(pIngInfo);
                }                
            }
            else
            {
                if (pIngInfo.Result < score)
                {
                    pIngInfo.Result = score;
                    processingBll.Update(pIngInfo);
                }
                else
                {
                    bestScore = pIngInfo.Result.Value;
                }

            }
            #endregion

            #region 查询成绩排行,查看本次活动成绩最好的10个人

            List<ProcessingInfo> pInfoList = processingBll.GetRankingList(id, 0);

            if (pInfoList == null)
            {
                return View(viewModel);
            }

            if (hasScore)
            {
                viewModel.bestRank = processingBll.GetBestRank(id, 0, bestScore) + 1;
            }
            else
            {
                viewModel.bestRank = -1;
            }

            List<WeChatMember> weChatMemberList = new List<WeChatMember>();

            pInfoList.ForEach((entity) =>
            {
                weChatMemberList.Add(memberBll.GetModelByOpenId(entity.OpenID));
            });

            ProcessingInfo tempProcessingInfo;
            weChatMemberList.ForEach((entity) =>
            {
                if (entity == null)
                    return;
                WechatUserInfoEntity wcUserInfo = Utility.JsonToObject<WechatUserInfoEntity>(entity.WeChatName);
                tempProcessingInfo = pInfoList.SingleOrDefault(p => p.OpenID == wcUserInfo.openid);

                viewModel.itemList.Add(new RankItemVM()
                {
                    headImg = wcUserInfo.headimgurl,
                    nickName = wcUserInfo.nickname,
                    openId = wcUserInfo.openid,
                    score = tempProcessingInfo.Result.Value
                });
            });

            viewModel.itemList = viewModel.itemList.OrderByDescending(p => p.score).ToList();
            int i = 0;
            viewModel.itemList.ForEach((item) =>
            {
                i++;
                switch (i)
                {
                    case 1:
                        item.styleStr = "first";
                        break;
                    case 2:
                        item.styleStr = "second";
                        break;
                    case 3:
                        item.styleStr = "third";
                        break;
                    default:
                        item.rankOrder = i.ToString();
                        break;
                }
            });


            #endregion

            #region 如果是抽奖已满足需要次数，则保存冠军到冠军表，并进行下一期

            int haveActNum = processedHistoryBll.GetActedNumberByProduct(productEntity.ID, 0);
            int actTotaNum = (int)productEntity.MarketPrice;
            if (haveActNum == actTotaNum)
            {
                //获取冠军插入到历史表
                ProcessingInfo championInfo = processingBll.GetChampionInfo(productEntity.ID, 0);
                ProcessedInfo championPInfo = new ProcessedInfo()
                {
                    ID = Guid.NewGuid(),
                    ProcessNum = 0,
                    OpenID = championInfo.OpenID,
                    ProductID = productEntity.ID,
                    Result = championInfo.Result.Value,
                    Status = 0,
                    EndTime = DateTime.Now
                };

                processedBll.Insert(championPInfo);

                //更新产品表进行期数
                productBll.Update(productEntity);
            }
            #endregion

            return View(viewModel);
        }

        /// <summary>
        /// 购买支付
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Payment(Guid productId, Guid? addressId, Guid? orderId)
        {
            PaymentVM viewModel = new PaymentVM() { productId = productId, count = 1 };
            Product productE = productBll.GetModel(productId);
            viewModel.productImg = productE.Image;
            viewModel.productName = productE.Name;
            viewModel.price = productE.MarketPrice.Value;

            if (addressId.HasValue)
            {
                viewModel.addressId = addressId.Value;
                AddressInfo addressE = addressBll.GetModel(addressId.Value);
                viewModel.reciver = addressE.Receiver;
                viewModel.address = addressE.Address;
                viewModel.mobile = addressE.Mobile;
            }
            else
            {
                string strWhere = string.Format(" where OpenId='{0}'", OpenId);
                AddressInfo addressE = addressBll.GetAllItems(strWhere).FirstOrDefault();
                if (addressE != null)
                {
                    viewModel.addressId = addressE.ID;
                    viewModel.reciver = addressE.Receiver;
                    viewModel.address = addressE.Address;
                    viewModel.mobile = addressE.Mobile;
                }
            }

            return View(viewModel);
        }

        /// <summary>
        /// 新增购买订单
        /// </summary>
        /// <returns></returns>  
        [HttpPost]
        public ActionResult AddProductOrder(Guid? orderId)
        {
            Guid productId = Guid.Parse(Request["productId"]);
            Guid addressId = Guid.Parse(Request["addressId"]);
            int count = int.Parse(Request["goodCount"]);
            decimal amount = decimal.Parse(Request["orderAmount"]);

            ProductOrder order = new ProductOrder()
            {
                ID = Guid.NewGuid(),
                OrderNo = Utility.GetRandomOrderNo(),
                ProductId = productId,
                Count = count,
                Amount = amount,
                AddressId = addressId,
                OpenId = OpenId,
                Satus = (int)ProductOrderStatus.NoPay,
                CreationDate = DateTime.Now
            };

            pOrderBll.Insert(order);

            ProductOrderVM viewModel = new ProductOrderVM();
            Product productE = productBll.GetModel(productId);
            AddressInfo addressE = addressBll.GetModel(addressId);
            viewModel.productName = productE.Name;
            viewModel.orderNo = order.OrderNo;
            viewModel.price = productE.MarketPrice.Value;
            viewModel.count = order.Count;
            viewModel.amount = amount;
            viewModel.reciver = addressE.Receiver;
            viewModel.mobile = addressE.Mobile;
            viewModel.address = addressE.Address;

            return View(viewModel);
        }

        public ActionResult AddressBind(Guid addressId, Guid pId)
        {
            AddressListVM viewModel = new AddressListVM() { addressList = new List<AddressVM>(), pId = pId };

            string strWhere = string.Format(" where OpenID='{0}'", OpenId);
            List<AddressInfo> entities = addressBll.GetAllItems(strWhere);

            if (entities != null)
            {
                entities.ForEach((p) =>
                {
                    bool isBind = false;
                    if (p.ID == addressId)
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

    }
}
