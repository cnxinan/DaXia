using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Script.Serialization;
using DaXia.BLL;
using DaXia.EntityDataModels;
using DaXia.WebComAdmin.Front;
using DaXia.WebFrameWork;
using WxPayAPI;

namespace DaXia.WebComAdmin.Controllers
{
    public class FrontHomeController : BaseFrontController
    {
        private readonly AdInfo_bll adInfoBll = BllInstance.AdInfoBll;
        private readonly Product_bll productBll = BllInstance.ProductBll;
        private readonly ProcessedInfo_bll processedInfoBll = BllInstance.ProcessedInfoBll;
        private readonly WeChatMember_bll weChatMemberBll = BllInstance.WeChatMemberBll;
        private readonly ProcessedHistoryInfo_bll processedHistoryInfoBll = BllInstance.ProcessedHistoryInfoBll;
        private readonly ProcessingInfo_bll processingBll = BllInstance.ProcessingInfoBll;
        private readonly BookMark_bll bookMarkBll = BllInstance.BookMarkBll;        
        private readonly long itemNumPerPage = 10;
                
        #region 首页

        public ActionResult Index()
        {
            GetUserInfo();

            HomeIndexVM viewModel = new HomeIndexVM() { ads = new List<HomeAd>(), products = new List<HomeProduct>(), openId = OpenId };

            //加载广告
            var adEntityList = adInfoBll.GetAllItems();
            if (adEntityList != null && adEntityList.Count > 0)
            {
                adEntityList.ForEach((p) =>
                {
                    viewModel.ads.Add(viewModel.AdEtv(p));
                });
            }

            //加载第一页产品
            string productStr = " where SerialNo <> '1'";
            long totalProductPages, totalProductItems;

            var productEneityList = productBll.GetListPaging(productStr, 1, itemNumPerPage, out totalProductPages, out totalProductItems);
            if (productEneityList != null)
            {
                productEneityList.ForEach((p) =>
                {
                    viewModel.products.Add(viewModel.ProductEtv(p));
                });
            }

            viewModel.currentPage = 1;
            viewModel.totalPage = (int)totalProductPages;

            return View(viewModel);
        }

        private void GetUserInfo()
        {
            if (!string.IsNullOrWhiteSpace(OpenId))
            {
                return;
            }
            //根据OAuth2.0获取用户OpenId
            //AddLogs("测试菜单跳转", Request.RawUrl);

            string code = Request["code"];
            string state = Request["state"];

            if (state == "1")
            {
                WebClient client = new WebClient();
                client.Encoding = System.Text.Encoding.UTF8;

                //string getOpenIdUrl = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid=wx53ba599c0a8429fe&secret=2ed69f3f5865214b7d7f7a43192592e2&code={0}&grant_type=authorization_code", code);
                string getOpenIdUrl = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={1}&secret={2}&code={0}&grant_type=authorization_code", code, WxPayConfig.APPID, WxPayConfig.APPSECRET);
                string accessTokeJson = client.DownloadString(getOpenIdUrl);

                try
                {
                    WechatReturnModel returnModel = GetWechatReturn(accessTokeJson);
                    string openId = returnModel.openid;
                    OpenId = returnModel.openid;

                    //获取平台accesstoken
                    //string getAccessTokenUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wx53ba599c0a8429fe&secret=2ed69f3f5865214b7d7f7a43192592e2";
                    string getAccessTokenUrl = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", WxPayConfig.APPID, WxPayConfig.APPSECRET);
                    string accessTokenJson = client.DownloadString(getAccessTokenUrl);
                    JavaScriptSerializer Jss = new JavaScriptSerializer();
                    Dictionary<string, object> respDic = (Dictionary<string, object>)Jss.DeserializeObject(accessTokenJson);
                    string accessToken = respDic["access_token"].ToString();

                    string getUserInfoUrl = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN", accessToken, openId);
                    string userInfo = client.DownloadString(getUserInfoUrl);
                    //AddLogs("获取用户信息成功", userInfo);
                    InsertOrUpdateUserInfo(openId, userInfo);
                }
                catch (Exception ex)
                {
                    AddLogs("获取accessToken失败", ex.Message);
                }

                //根据返回json列表获取用户信息
            }
        }

        private void UpdateUserInfoTest()
        {
            string openid = Request["openid"];
            string userInfoJson = Request["userInfoJson"];

            if (!string.IsNullOrWhiteSpace(openid) && !string.IsNullOrWhiteSpace(userInfoJson))
            {
                InsertOrUpdateUserInfo(openid, userInfoJson);
            }
        }

        /// <summary>
        /// 下拉滑动加载产品
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Ajax_AdPoductWithPage(int pageIndex)
        {
            StringBuilder sbResult = new StringBuilder();
            HomeIndexVM modelList = new HomeIndexVM();
            long totalProductPages, totalProductItems;

            string productStr = " where SerialNo <> '1'";
            var productEneityList = productBll.GetListPaging(productStr, pageIndex, itemNumPerPage, out totalProductPages, out totalProductItems);
            if (productEneityList != null)
            {
                productEneityList.ForEach((p) => 
                {
                    var item = modelList.ProductEtv(p);
                    sbResult.Append(RenderPartialViewToString(this, "_ProductItem", item));
                });
            }

            return Content(sbResult.ToString());
        }        

        /// <summary>
        /// 关注
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Ajax_Follow(Guid id)
        {
            string result = "收藏成功";

            BookMark bookMarkModel = bookMarkBll.GetBookMarkModel(OpenId, id);

            if (bookMarkModel == null)
            {
                bookMarkModel = new BookMark()
                {
                    ID = Guid.NewGuid(),
                    OpenID = OpenId,
                    ProductID = id,
                    CreationTime = DateTime.Now
                };
                bookMarkBll.Insert(bookMarkModel);
            }
            else
            {
                bookMarkBll.Delete(bookMarkModel);
                result = "取消收藏成功";
            }

            return Content(result);
        } 
        #endregion

        #region 最新揭晓

        public ActionResult LatestRewardRecord()
        {
            LateestRewardsListVM viewModel = new LateestRewardsListVM() { itemList = new List<LateestRewardItemVM>() };

            long totalProductPages, totalProductItems;

            var processedInfoEneityList = processedInfoBll.GetListPaging(" order by EndTime desc", 1, itemNumPerPage, out totalProductPages, out totalProductItems);
            
            if(processedInfoEneityList != null)
            {
                processedInfoEneityList.ForEach((p) => 
                {
                    Product productEntity = productBll.GetModel(p.ProductID);
                    WeChatMember memberEntity = weChatMemberBll.GetModelByOpenId(p.OpenID);
                    WechatUserInfoEntity weChatUserinfoEntity = Utility.JsonToObject<WechatUserInfoEntity>(memberEntity.WeChatName);
                    LateestRewardItemVM item = new LateestRewardItemVM();
                    item.productId = p.ProductID;
                    item.productHeadImg = productEntity.Image;
                    item.openId = p.OpenID;
                    item.memberHeadImg = weChatUserinfoEntity.headimgurl;
                    item.memberName = weChatUserinfoEntity.nickname;
                    item.boughtCountg = processedHistoryInfoBll.GetActedNumOfMember(p.ProductID, p.ProcessNum, p.OpenID);
                    item.GotTime = Utility.DTDefaultFormat(p.EndTime);

                    viewModel.itemList.Add(item);
                });
            }

            return View(viewModel);
        }

        public ActionResult PeopleRewardRecord(string openId)
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
            var processedList = processedInfoBll.GetAllItems(strWhere);
            WeChatMember memberE = weChatMemberBll.GetModelByOpenId(openId);
            WechatUserInfoEntity entity = Utility.JsonToObject<WechatUserInfoEntity>(memberE.WeChatName);
            viewModel.memberEntity = entity;

            if (processedList != null)
            {
                processedList.ForEach((p) =>
                {
                    Product productE = productBll.GetModel(p.ProductID);                    
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

        #region 我的收藏

        public ActionResult MyFollow()
        {
            MyFollowListVM viewModel = new MyFollowListVM() { followedList = new List<HomeProduct>() };

            string strWhere = string.Format(" where OpenID='{0}'",OpenId);
            var bookMarkList = bookMarkBll.GetAllItems(strWhere);
            if(bookMarkList!=null)
            {                
                bookMarkList.ForEach((p) => 
                {
                    Product pEntity = productBll.GetModel(p.ProductID);
                    int actNum = (int)pEntity.MarketPrice;
                    int haveActNum = processedHistoryInfoBll.GetActedNumberByProduct(pEntity.ID, 0);
                    int remainActNum = actNum - haveActNum;
                    int bookMarkNum = bookMarkBll.GetBookNum(pEntity.ID);
                    string processedPercent = ((haveActNum * 100 / actNum)).ToString() + "%";
                    HomeProduct item = new HomeProduct() 
                    {
                        productId = p.ProductID,
                        productName = pEntity.Name,
                        productIamge = pEntity.Image,
                        actNumbers = actNum,
                        haveActNumbers = haveActNum,
                        remainActNumbers = remainActNum,
                        processedPercent = processedPercent,
                        bookMarkNumbers = bookMarkNum,
                    };
                    viewModel.followedList.Add(item);
                });
            }

            return View(viewModel);
        }

        #endregion

    }
}
