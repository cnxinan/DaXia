using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DaXia.EntityDataModels;
using DaXia.BLL;

namespace DaXia.WebComAdmin.Front
{

    #region 首页

    public class HomeIndexVM
    {

        private readonly ProcessedHistoryInfo_bll processedHistoryBll = BLL.BLLFactory.Instance.ProcessedHistoryInfoBll;
        private readonly BookMark_bll bookMarkBll = BLL.BLLFactory.Instance.BookMarkBll;

        public int currentPage { get; set; }

        public int totalPage { get; set; }

        public List<HomeAd> ads { get; set; }

        public List<HomeProduct> products { get; set; }

        public string openId { get; set; }

        public HomeAd AdEtv(AdInfo entity)
        {
            return new HomeAd()
            {
                adId = entity.ID,
                adImage = entity.AdImage,
                adName = entity.AdName,
                adUrl = entity.AdUrl
            };
        }

        public HomeProduct ProductEtv(Product entity)
        {
            string iconImg = string.Empty;
            int actNum = (int)entity.MarketPrice;
            int haveActNum = processedHistoryBll.GetActedNumberByProduct(entity.ID, 0);
            int remainActNum = actNum - haveActNum;
            int bookMarkNum = bookMarkBll.GetBookNum(entity.ID);
            bool isBookMark = bookMarkBll.GetIsBookMark(openId, entity.ID);
            string processedPercent = ((haveActNum * 100 / actNum)).ToString() + "%";

            return new HomeProduct()
            {
                productId = entity.ID,
                productName = entity.Name,
                productIamge = entity.Image,
                icoImage = iconImg,
                actNumbers = actNum,
                haveActNumbers = haveActNum,
                remainActNumbers = remainActNum,
                processedPercent = processedPercent,
                bookMarkNumbers = bookMarkNum,
                IsBookMark = isBookMark
            };
        }
    }

    public class HomeProduct
    {
        public Guid productId { get; set; }

        public string productName { get; set; }

        public string productIamge { get; set; }

        public string icoImage { get; set; }

        public int actNumbers { get; set; }

        public int haveActNumbers { get; set; }

        public int remainActNumbers { get; set; }

        public string processedPercent { get; set; }

        public int bookMarkNumbers { get; set; }

        public bool IsBookMark { get; set; }

    }

    public class HomeAd
    {
        public Guid adId { get; set; }

        public string adImage { get; set; }

        public string adName { get; set; }

        public string adUrl { get; set; }
    }

    public class PersonIndexVM
    {
        public Guid ID { get; set; }

        public string headImg { get; set; }

        public string nickName { get; set; }

        public decimal balance { get; set; }

        public bool isShop { get; set; }

        public bool havePayed { get; set; }
    }

    #endregion

    #region 产品详情
    /// <summary>
    /// 产品详情页
    /// </summary>
    public class ProductDetialVM : HomeProduct
    {
        public List<string> processList { get; set; }

        public string processNumStr { get; set; }

        public List<string> imageList { get; set; }

        public decimal marketPrice { get; set; }

        public string content { get; set; }

        public bool haveChampion { get; set; }

        public string status { get; set; }

        public string championOpenId { get; set; }

        public string championName { get; set; }

        public string championHeadImage { get; set; }

        public string championCity { get; set; }

        public string championDate { get; set; }

        public string championNo { get; set; }

        public int rewardTimes { get; set; }
    }
    #endregion

    #region 摇一摇
    /// <summary>
    /// 摇一摇
    /// </summary>
    public class ShackVM
    {
        public Guid productId { get; set; }

        public int haveActNumbers { get; set; }

    }


    /// <summary>
    /// 排行榜
    /// </summary>
    public class RankListVM
    {
        public List<RankItemVM> itemList { get; set; }

        public int bestRank { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class RankItemVM
    {
        public string openId { get; set; }

        public string styleStr { get; set; }

        public string headImg { get; set; }

        public string nickName { get; set; }

        public int score { get; set; }

        public string rankOrder { get; set; }
    }
    #endregion

    #region 账户明细
    public class TradeListVM
    {
        public decimal balance { get; set; }

        public decimal rechargeTotal { get; set; }

        public decimal shackTotal { get; set; }

        public List<TradeItemVM> rechargeItems { get; set; }

        public List<TradeItemVM> shackItems { get; set; }

    }

    public class TradeItemVM
    {
        public string creationDate { get; set; }

        public decimal totalfee { get; set; }

        public string tradeNo { get; set; }
    }
    #endregion

    #region 最新揭晓

    public class LateestRewardsListVM
    {
        public List<LateestRewardItemVM> itemList { get; set; }
    }

    public class LateestRewardItemVM
    {
        public Guid productId { get; set; }

        public string productHeadImg { get; set; }

        public string openId { get; set; }

        public string memberHeadImg { get; set; }

        public string memberName { get; set; }

        public int boughtCountg { get; set; }

        public string GotTime { get; set; }
    }    

    #endregion

    #region 产品分类

    public class CatalogListVM
    {
        public List<string> catalogNames { get; set; }

        public List<CatalogItemsVM> catalogLists { get; set; }
    }

    public class CatalogItemsVM
    {
        public string titleName { get; set; }

        public List<CatalogItemVM> itemList { get; set; }
    }

    public class CatalogItemVM
    {
        public Guid catalogId { get; set; }

        public string image { get; set; }

        public string name { get; set; }
    }

    public class CatalogDetailListVM
    {
        public List<CatalogDetailvVM> processingList { get; set; }

        public List<CatalogDetailvVM> mostBookMarkList { get; set; }

        public List<CatalogDetailvVM> newestList { get; set; }

        public List<CatalogDetailvVM> priceList { get; set; }
    }

    public class CatalogDetailvVM
    {
        public Guid productId { get; set; }

        public string image { get; set; }

        public string name { get; set; }

        public int actNumbers { get; set; }

        public int haveActNumbers { get; set; }

        public int remainActNumbers { get; set; }

        public string processedPercent { get; set; }
    }

    #endregion

    #region 地址管理

    public class AddressListVM
    {
        public List<AddressVM> addressList { get; set; }

        public Guid pId { get; set; }
    }

    public class AddressVM
    {
        public Guid addressId { get; set; }

        public string receiverName { get; set; }

        public string mobile { get; set; }

        public string address { get; set; }

        public string zipcode { get; set; }

        public string province { get; set; }

        public bool isBindAddress { get; set; }
    }

    #endregion

    #region 夺宝记录

    public class RewardRecordListVM
    { 
        public List<RewardRecordDoneVM> HaveDoneList { get; set; }

        public List<ProcessingRecordVM> ProcessingList { get; set; }

        public WechatUserInfoEntity memberEntity { get; set; }
    }

    public class RewardRecordDoneVM
    {
        public Guid rewardId { get; set; }

        public Guid productId { get; set; }

        public string processStr { get; set; }

        public string productName { get; set; }

        public string productImg { get; set; }

        public decimal marketPrice { get; set; }

        public string openId { get; set; }

        public string userName { get; set; }

        public string rewardDate { get; set; }

        public bool bindAddress { get; set; }
    }

    public class ProcessingRecordVM
    {
        public Guid productId { get; set; }

        public string productName { get; set; }

        public string productImg { get; set; }

        public int actNumbers { get; set; }

        public int haveActNumbers { get; set; }

        public int remainActNumbers { get; set; }

        public string processedPercent { get; set; }
    }

    #endregion

    #region 购物订单

    public class BuyOrdersVM
    {
        public List<BuyOrderVM> items { get; set; }
    }

    public class BuyOrderVM
    {
        public Guid id { get; set; }

        public Guid productId { get; set; }

        public int productCount { get; set; }

        public string productName { get; set; }

        public string productImg { get; set; }

        public decimal marketPrice { get; set; }

        public decimal amount { get; set; }

        public string openId { get; set; }

        public string orderNo { get; set; }

        public string mobile { get; set; }

        public string receiver { get; set; }

        public string address { get; set; }

        public bool havePay { get; set; } 
    }

    #endregion

    #region 我的收藏

    public class MyFollowListVM
    {
        public List<HomeProduct> followedList { get; set; }
    }

    #endregion

    #region 购买支付

    public class PaymentVM
    {
        public Guid addressId { get; set; }

        public string reciver { get; set; }

        public string mobile { get; set; }

        public string address { get; set;}

        public Guid productId { get; set; }

        public string productName { get; set; }

        public string productImg { get; set; }

        public decimal price { get; set; }

        public int count { get; set; }
    }

    public class ProductOrderVM
    {
        public string productName { get; set; }

        public string orderNo { get; set; }

        public decimal price { get; set; }

        public int count { get; set; }

        public decimal amount { get; set; }

        public string reciver { get; set; }

        public string mobile { get; set; }

        public string address { get; set; }

    }

    #endregion

    #region 商户管理

    public class DirectorVM
    {
        public string headImg { get; set; }

        public string nickName { get; set; }

        public string userNo { get; set; }

        public decimal sales { get; set; }

        public decimal bonus { get; set; }

        public string referrer { get; set; }

        public int staffNum { get; set; }

        public string orderNum { get; set; }

        public decimal orderTotal { get; set; }

        public decimal balance { get; set; }
    }

    public class JoinVM
    {
        public Guid id { get; set; }

        public string contacts { get; set; }

        public string mobile { get; set; }

        public string weChatName { get; set; }

        public string address { get; set; }

        public string note { get; set; }
    }

    public class MyBankVM
    {
        public decimal sholdGet { get; set; }

        public decimal got { get; set; }

        public decimal noGot { get; set; }

        public decimal canTX { get; set; }
    }

    public class MyDirectorVM
    {
        public string headImg { get; set; }

        public string nickName { get; set; }

        public string userNo { get; set; }

        public decimal sales { get; set; }

        public decimal bonus { get; set; }

        public int level1Count { get; set; }

        public int level2Count { get; set; }

        public int level3Count { get; set; }
    }

    public class TXOrderListVM
    {
        public string headImg { get; set; }

        public string nickName { get; set; }

        public string userNo { get; set; }

        public List<TXOrderVM> itemList { get; set; }
    }

    public class TXOrderVM
    {
        public string orderNo { get; set; }

        public decimal amount { get; set; }

        public string status { get; set; }
    }

    public class MyOrderListVM 
    {
        public string headImg { get; set; }

        public string nickName { get; set; }

        public string userNo { get; set; }

        public int waitForSentNum { get; set; }

        public List<MyOrderVM> WaitForSendList { get; set; }

        public List<MyOrderVM> SentList { get; set; }
    }

    public class MyOrderVM
    {
        public Guid orderId { get; set; }

        public Guid productId { get; set; }

        public string productName { get; set; }

        public string productImg { get; set; }

        public decimal productPrice { get; set; }

        public string gotDate { get; set; }

        public string gotUserName { get; set; }

        public int productCount { get; set; }
    }

    public class OrderDetailVM
    {
        public Guid orderId { get; set; }

        public decimal productPrice { get; set; }

        public string receiver { get; set; }

        public string mobile { get; set; }

        public string address { get; set; }

        public Guid productId { get; set; }

        public string productName { get; set; }

        public string productImg { get; set; }

        public string gotDate { get; set; }

        public int processedNum { get; set; }

        public bool haveSent { get; set; }

        public int bugCount { get; set; }

        public decimal amount { get; set; }

        public string orderNo { get; set; }

        public string status { get; set; }
    }

    #endregion
}