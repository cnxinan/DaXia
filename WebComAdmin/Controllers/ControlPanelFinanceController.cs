using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaXia.WebFrameWork;
using DaXia.BLL;
using DaXia.EntityDataModels;

namespace DaXia.WebComAdmin.Controllers
{
    public class ControlPanelFinanceController : BaseController
    {
        private readonly ShopAccount_bll shopABll = BllInstance.ShopAccountBll;
        private readonly ShopDetail_bll shopDBll = BllInstance.ShopDetailBll;
        private readonly TXAccount_bll txABll = BllInstance.TXAccountBll;

        /// <summary>
        /// 个人账户
        /// </summary>
        /// <returns></returns>
        public ActionResult PersonalAccount()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult _AjaxChangeBalance()
        {
            string result = "修改成功";

            return Content(result);
        }

        /// <summary>
        /// 加盟充值
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopCharge()
        {
            FinanceListVM viewModel = new FinanceListVM()
            {
                itemList =new List<FinanceVM>()
            };

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
            strWhere += " and Type=0";
            strWhere += " order by CreationTime DESC";
            var itemList = shopABll.GetListPaging(strWhere, pageIndex, itemsPrePage, out totalPages, out totalItems);
            viewModel.page = new Pager() { RecordAllCount = (int)totalItems, PageIndex = (int)pageIndex, PageAllCount = (int)totalPages, PageUrl = url };
             
            #endregion

            itemList.ForEach((p) =>
            {
                FinanceVM item = new FinanceVM()
                {
                    OrderNo = p.OrderNo,
                    OpenId = p.OpenId,
                    Amount = p.Amount,
                    Status = p.Status == 0 ? "未支付" : "已支付",
                    IsPay = p.Status == 0 ? false : true,
                    CreationTime = Utility.DTDefaultFormat(p.CreationTime)
                };
                viewModel.itemList.Add(item);
            });

            return View(viewModel);
        }

        /// <summary>
        /// 设置为已支付
        /// </summary>
        /// <returns></returns>
        public ActionResult _AjaxSetPaied()
        {
            string result = "设置成功";

            return Content(result);
        }

        /// <summary>
        /// 商户返利
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopReward()
        {
            FinanceListVM viewModel = new FinanceListVM()
            {
                itemList = new List<FinanceVM>()
            };

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
            strWhere += " and Type<>0";
            strWhere += " order by CreationTime DESC";
            var itemList = shopABll.GetListPaging(strWhere, pageIndex, itemsPrePage, out totalPages, out totalItems);
            viewModel.page = new Pager() { RecordAllCount = (int)totalItems, PageIndex = (int)pageIndex, PageAllCount = (int)totalPages, PageUrl = url };

            #endregion

            itemList.ForEach((p) =>
            {
                FinanceVM item = new FinanceVM()
                {
                    OrderNo = p.OrderNo,
                    OpenId = p.OpenId,
                    FromOpenId = p.FromOpenId,
                    Amount = p.Amount,
                    Status = p.Status == 0 ? "未支付" : "已支付",
                    IsPay = p.Status == 0 ? false : true,
                    Note = p.Note,
                    CreationTime = Utility.DTDefaultFormat(p.CreationTime)
                };
                viewModel.itemList.Add(item);
            });

            return View(viewModel);
        }

        /// <summary>
        /// 提现订单
        /// </summary>
        /// <returns></returns>
        public ActionResult TXOrder()
        {
            TxAccountListVM viewModel = new TxAccountListVM()
            {
                itemList = new List<TxAccountVM>()
            };

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
            strWhere += " order by Status asc, CreationDate DESC";
            var itemList = txABll.GetListPaging(strWhere, pageIndex, itemsPrePage, out totalPages, out totalItems);
            viewModel.page = new Pager() { RecordAllCount = (int)totalItems, PageIndex = (int)pageIndex, PageAllCount = (int)totalPages, PageUrl = url };

            #endregion

            itemList.ForEach((p) =>
            {
                ShopDetail shopDE = shopDBll.GetModelByOpenId(p.OpenId);

                TxAccountVM item = new TxAccountVM()
                {
                    ID = p.ID,
                    OrderNo = p.OrderNo,
                    OpenId = p.OpenId,
                    Amount = p.Amount,
                    IsCheced = p.Status == (int)TXOrderStatus.Checked ? true : false,
                    Status = p.Status == (int)TXOrderStatus.Checked ? "已审核" : "未审核",
                    Address = shopDE.Address,
                    Contacts = shopDE.Contacts,
                    Mobile = shopDE.Mobile,
                    CreationTime = Utility.DTDefaultFormat(p.CreationDate)
                };
                viewModel.itemList.Add(item);
            });

            return View(viewModel);
        }

        /// <summary>
        /// 设置为已提现
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult _AjaxTXSuccess(Guid id)
        {
            string result = "审核成功";

            var txAccounteE = txABll.GetModel(id);
            if (txAccounteE != null)
            {
                txAccounteE.Status = (int)TXOrderStatus.Checked;
                if (!txABll.Update(txAccounteE))
                {
                    result = "审核失败，请重新审核!";
                }
            }
            else
            {
                result = "订单不存在!";
            }
            return Content(result);
        }

    }
}
