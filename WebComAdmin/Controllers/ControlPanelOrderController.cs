using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaXia.EntityDataModels;
using DaXia.BLL;
using DaXia.WebFrameWork;
using DaXia.WebComAdmin.lib;

namespace DaXia.WebComAdmin.Controllers
{
    public class ControlPanelOrderController : BaseController
    {
        #region 变量

        private readonly WeChatMember_bll weChatMemberBll = BllInstance.WeChatMemberBll;
        private readonly Product_bll productBll = BllInstance.ProductBll;
        private readonly ProcessedInfo_bll processedInfoBll = BllInstance.ProcessedInfoBll;
        private readonly ProcessingInfo_bll processingInfoBll = BllInstance.ProcessingInfoBll;
        private readonly ProcessedHistoryInfo_bll processedHistoryInfoBll = BllInstance.ProcessedHistoryInfoBll;
        private readonly ShopDetail_bll shopDetailBll = BllInstance.ShopDetailBll;
        private readonly AddressInfo_bll addressInfoBll = BllInstance.AddressInfoBll;
        private readonly ProductOrder_bll productOrderBll = BllInstance.ProductOrderBll;

        #endregion

        /// <summary>
        /// 中奖纪录
        /// </summary>
        /// <returns></returns>
        public ActionResult RewardRecords()
        {
            OrderListVM viewModel = new OrderListVM() { itemList = new List<OrderItem>() };

            string strWhere = string.Format("");

            #region 分页

            long pageIndex = Utility.pageIndex;
            if (Request["Page"] != null)
            {
                pageIndex = long.Parse(Request["Page"]);
            }
            long itemsPrePage = Utility.itemsPrePage;
            long totalPages = 0;
            long totalItems = 0;
            string url = Request.Url.AbsolutePath + "?1=1";
            strWhere += " order by EndTime DESC";
            var itemList = processedInfoBll.GetListPaging(strWhere, pageIndex, itemsPrePage, out totalPages, out totalItems, (int)UserType.Dining);
            viewModel.page = new Pager() { RecordAllCount = (int)totalItems, PageIndex = (int)pageIndex, PageAllCount = (int)totalPages, PageUrl = url };

            #endregion

            WeChatMember memberEntity;
            WechatUserInfoEntity wechatUserEntity;
            Product productEntity;
            AddressInfo addressEntity;
            ShopDetail shopDetailEntity;

            itemList.ForEach((p) =>
            {
                OrderItem item = new OrderItem();
                memberEntity = weChatMemberBll.GetModelByOpenId(p.OpenID);

                if (memberEntity != null)
                {
                    wechatUserEntity = Utility.JsonToObject<WechatUserInfoEntity>(memberEntity.WeChatName);
                    productEntity = productBll.GetModel(p.ProductID);
                    shopDetailEntity = shopDetailBll.GetModel(Guid.NewGuid());
                    if (p.AddressId.HasValue && p.AddressId != Guid.Empty)
                    {
                        addressEntity = addressInfoBll.GetModel(p.AddressId.Value);
                        item.address = addressEntity.Address;
                        item.mobile = addressEntity.Mobile;
                        item.reciver = addressEntity.Receiver;
                    }
                    item.ID = p.ID;
                    item.memberNo = memberEntity.WeChatImage;
                    item.memberOpenId = memberEntity.OpenID;
                    item.nickName = wechatUserEntity.nickname;
                    item.productName = productEntity.Name;
                    item.processNum = p.ProcessNum;
                    item.status = p.Status == 0 ? "未配送" : "已配送";
                    item.creationDate = Utility.DTDefaultFormat(p.EndTime);
                    viewModel.itemList.Add(item);
                }
            });

            return View(viewModel);
        }

        public ActionResult _ajaxSendRewards(Guid id)
        {
            string result = "发货成功";

            ProcessedInfo info = processedInfoBll.GetModel(id);

            if (info != null)
            {
                info.Status = 1;
                if (!processedInfoBll.Update(info))
                {
                    result = "发货失败";
                }

                //分销
                Product productE = productBll.GetModel(info.ProductID);
                if (productE != null)
                {
                    //ShopDetail shopDEntity = shopDetailBll.GetModel(productE.ShopID);
                    //if (shopDEntity != null)
                    //{
                    //    //Fenxiao.FX(shopDEntity.OpenID, productE.StockPrice);
                    //}
                }
                else
                {
                    result = "三级分销分润出错,产品不存在!";
                }
            }

            return Content(result);
        }

        /// <summary>
        /// 摇奖进行中
        /// </summary>
        /// <returns></returns>
        public ActionResult WatiForSending()
        {
            OrderListVM viewModel = new OrderListVM() { itemList = new List<OrderItem>() };

            string strWhere = string.Format("");

            #region 分页

            long pageIndex = Utility.pageIndex;
            if (Request["Page"] != null)
            {
                pageIndex = long.Parse(Request["Page"]);
            }
            long itemsPrePage = Utility.itemsPrePage;
            long totalPages = 0;
            long totalItems = 0;
            string url = Request.Url.AbsolutePath + "?1=1";
            strWhere += " order by StartTime DESC";
            var itemList = processingInfoBll.GetListPaging(strWhere, pageIndex, itemsPrePage, out totalPages, out totalItems, (int)UserType.Dining);
            viewModel.page = new Pager() { RecordAllCount = (int)totalItems, PageIndex = (int)pageIndex, PageAllCount = (int)totalPages, PageUrl = url };

            #endregion

            WeChatMember memberEntity;
            WechatUserInfoEntity wechatUserEntity;
            Product productEntity;

            itemList.ForEach((p) =>
            {
                OrderItem item = new OrderItem();

                memberEntity = weChatMemberBll.GetModelByOpenId(p.OpenID);
                if (memberEntity != null)
                {
                    wechatUserEntity = Utility.JsonToObject<WechatUserInfoEntity>(memberEntity.WeChatName);
                    productEntity = productBll.GetModel(p.ProductID);

                    item.ID = p.ID;
                    item.memberNo = memberEntity.WeChatImage;
                    item.memberOpenId = memberEntity.OpenID;
                    item.nickName = wechatUserEntity.nickname;
                    item.productName = productEntity.Name;
                    item.processNum = p.ProcessNum;
                    item.creationDate = Utility.DTDefaultFormat(p.StartTime);
                    viewModel.itemList.Add(item);
                }
            });

            return View(viewModel);
        }

        /// <summary>
        /// 摇奖历史
        /// </summary>
        /// <returns></returns>
        public ActionResult HaveSended()
        {
            OrderListVM viewModel = new OrderListVM() { itemList = new List<OrderItem>() };

            string strWhere = string.Format("");

            #region 分页

            long pageIndex = Utility.pageIndex;
            if (Request["Page"] != null)
            {
                pageIndex = long.Parse(Request["Page"]);
            }
            long itemsPrePage = Utility.itemsPrePage;
            long totalPages = 0;
            long totalItems = 0;
            string url = Request.Url.AbsolutePath + "?1=1";
            strWhere += " order by CreationTime DESC";
            var itemList = processedHistoryInfoBll.GetListPaging(strWhere, pageIndex, itemsPrePage, out totalPages, out totalItems, (int)UserType.Dining);
            viewModel.page = new Pager() { RecordAllCount = (int)totalItems, PageIndex = (int)pageIndex, PageAllCount = (int)totalPages, PageUrl = url };

            #endregion

            WeChatMember memberEntity;
            WechatUserInfoEntity wechatUserEntity;
            Product productEntity;

            itemList.ForEach((p) =>
            {
                OrderItem item = new OrderItem();
                memberEntity = weChatMemberBll.GetModelByOpenId(p.OpenID);
                if (memberEntity != null)
                {
                    wechatUserEntity = Utility.JsonToObject<WechatUserInfoEntity>(memberEntity.WeChatName);
                    productEntity = productBll.GetModel(p.ProductID);

                    item.ID = p.ID;
                    item.memberNo = memberEntity.WeChatImage;
                    item.memberOpenId = memberEntity.OpenID;
                    item.nickName = wechatUserEntity.nickname;
                    item.productName = productEntity.Name;
                    item.processNum = p.ProcessNum;
                    item.creationDate = Utility.DTDefaultFormat(p.CreationTime);
                    viewModel.itemList.Add(item);
                }
            });

            return View(viewModel);
        }

        /// <summary>
        /// 购物订单
        /// </summary>
        /// <returns></returns>
        public ActionResult BuyOrders()
        {
            BuyOrderListVM viewModel = new BuyOrderListVM() { itemList = new List<OrderVM>() };

            string strWhere = string.Format(" where 1=1");

            #region 分页

            long pageIndex = Utility.pageIndex;
            if (Request["Page"] != null)
            {
                pageIndex = long.Parse(Request["Page"]);
            }
            long itemsPrePage = Utility.itemsPrePage;
            long totalPages = 0;
            long totalItems = 0;
            string url = Request.Url.AbsolutePath + "?1=1";
            if (Request["orderNo"]!=null)
            {
                strWhere += string.Format(" and OrderNo='{0}'", Request["OrderNo"]);
            }
            strWhere += " order by CreationDate DESC";
            var itemList = productOrderBll.GetListPaging(strWhere, pageIndex, itemsPrePage, out totalPages, out totalItems);
            viewModel.page = new Pager() { RecordAllCount = (int)totalItems, PageIndex = (int)pageIndex, PageAllCount = (int)totalPages, PageUrl = url };

            #endregion

            Product productEntity;
            AddressInfo addressEntity;

            itemList.ForEach((p) =>
            {
                OrderVM item = new OrderVM();

                productEntity = productBll.GetModel(p.ProductId);
                addressEntity = addressInfoBll.GetModel(p.AddressId);

                item.ID = p.ID;
                item.orderNo = p.OrderNo;
                item.openId = p.OpenId;
                item.count = p.Count;
                item.productName = productEntity.Name;
                item.amount = p.Amount;
                item.status = p.Satus;
                item.receiver = addressEntity.Receiver;
                item.address = addressEntity.Address;
                item.mobile = addressEntity.Mobile;
                item.creationDate = Utility.DTDefaultFormat(p.CreationDate);
                viewModel.itemList.Add(item);
            });

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult _AjaxChangeStatus(Guid id, int status)
        {
            string result = "设置失败";

            ProductOrder productOrderE = productOrderBll.GetModel(id);

            if (productOrderE != null)
            {
                productOrderE.Satus = status;

                if (productOrderBll.Update(productOrderE))
                {
                    result = "设置成功";
                }
            }

            return View(result);
        }

        public ActionResult Others()
        {
            return View();
        }
    }
}
