using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaXia.BLL;
using DaXia.EntityDataModels;
using DaXia.WebFrameWork;

namespace DaXia.WebComAdmin
{
    public class ControlPanelMemberController : BaseController
    {
        private readonly Member_bll memberbll = BllInstance.MemberBll;
        private readonly School_bll schoolbll = BllInstance.SchoolBll;
        private readonly ShopDetail_bll shopbll = BllInstance.ShopDetailBll;
        private readonly WeChatMember_bll wechatMemberbll = BllInstance.WeChatMemberBll;


        #region 餐厅管理

        public ActionResult DiningRoom()
        {
            DiningListVM viewModel = new DiningListVM() { itemList = new List<DiningVM>() };

            string strWhere = string.Format(" where [type]=@0");

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
            strWhere += " order by CreationDateTime DESC";
            var itemList = memberbll.GetListPaging(strWhere, pageIndex, itemsPrePage, out totalPages, out totalItems, (int)UserType.Dining);
            viewModel.page = new Pager() { RecordAllCount = (int)totalItems, PageIndex = (int)pageIndex, PageAllCount = (int)totalPages, PageUrl = url };

            #endregion

            itemList.ForEach((p) =>
            {
                viewModel.itemList.Add(viewModel.ETV(p));
            });

            return View(viewModel);
        }

        public ActionResult DiningAddEdit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DiningAddEdit(DiningVM model)
        {
            string returnUrl = SiteUrls.Instance.M_DiningManage();
            string message = "添加成功";

            if (!IsUserExists(model.UserName))
            {
                message = "用户名已存在，请重新添加!";
                ViewBag.JS = UIMessage.AlertDialog(message);
            }
            else
            {
                Member entity = GetDiningModel(model);
                if (!memberbll.Insert(entity))
                {
                    message = "添加餐厅失败，请重新添加!";
                    ViewBag.JS = UIMessage.AlertDialog(message);
                }
                else
                {
                    ViewBag.JS = UIMessage.ShowDialogAndRedirct(message, returnUrl);
                }
            }

            return View(model);
        }

        #endregion

        #region 配送员管理

        public ActionResult Sender()
        {
            SenderListVM viewModel = new SenderListVM() { itemList = new List<SenderVM>() };

            string strWhere = string.Format(" where [type]=@0");

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
            strWhere += " order by CreationDateTime ASC";
            var itemList = memberbll.GetListPaging(strWhere, pageIndex, itemsPrePage, out totalPages, out totalItems, (int)UserType.Sender);
            viewModel.page = new Pager() { RecordAllCount = (int)totalItems, PageIndex = (int)pageIndex, PageAllCount = (int)totalPages, PageUrl = url };

            #endregion

            itemList.ForEach((p) =>
            {
                viewModel.itemList.Add(viewModel.ETV(p));
            });

            return View(viewModel);
        }

        public ActionResult SenderAddEdit(int? Id)
        {
            SenderVM model = new SenderVM();
            if (Id == null)
            {
                model.IsOut = 0;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult SenderAddEdit(SenderVM model)
        {
            string returnUrl = SiteUrls.Instance.M_DiningManage();
            string message = "添加成功";

            if (!IsUserExists(model.UserName))
            {
                message = "用户名已存在，请重新添加!";
                ViewBag.JS = UIMessage.AlertDialog(message);
            }
            else
            {

                Member entity = GetSenderModel(model);
                if (!memberbll.Insert(entity))
                {
                    message = "添加配送员失败，请重新添加!";
                    ViewBag.JS = UIMessage.AlertDialog(message);
                }
                else
                {
                    ViewBag.JS = UIMessage.ShowDialogAndRedirct(message, returnUrl);
                }
            }

            return View(model);
        }

        #endregion

        #region 绑定学校

        public ActionResult _bindSchool(int id, int type)
        {
            BindSchoolVM model = new BindSchoolVM() { Schools = new List<SelectListItem>() };

            model.Type = "餐厅";
            model.Name = "蜀香阁川菜馆";
            ViewData["ListName"] = "Schools";
            var schools = schoolbll.GetAllItems();
            foreach (var school in schools)
            {
                model.Schools.Add(new SelectListItem { Value = school.ID.ToString(), Text = school.Name, Selected = false });
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult _bindSchool(BindSchoolVM model)
        {
            return View(model);
        }

        #endregion

        #region 商铺管理
        public ActionResult Shops()
        {
            ShopListVM viewModel = new ShopListVM() { itemList = new List<ShopDetailsVM>() };

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
            if (Request["contact"] != null)
            {
                strWhere += string.Format(" and Contacts like '%{0}%'", Request["contact"]);
            }
            strWhere += " order by CreationTime DESC";
            var itemList = shopbll.GetListPaging(strWhere, pageIndex, itemsPrePage, out totalPages, out totalItems);
            viewModel.page = new Pager() { RecordAllCount = (int)totalItems, PageIndex = (int)pageIndex, PageAllCount = (int)totalPages, PageUrl = url };

            #endregion

            itemList.ForEach((p) =>
            {
                viewModel.itemList.Add(viewModel.ETV(p));
            });

            return View(viewModel);
        }

        public ActionResult ShopAddEdit(Guid? shopId = null)
        {
            var listModel = new ShopListVM();
            var model = new ShopDetailsVM();

            if (shopId != null)
            {
                model = listModel.ETV(shopbll.GetModel(shopId.Value));
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult ShopAddEdit(ShopDetailsVM model)
        {
            string returnUrl = "/ControlPanelMember/Shops";
            string message = "添加成功";

            if (model.Id == Guid.Empty)
            {
                var insertModel = GetShopDetail(model);
                insertModel.ID = Guid.NewGuid();
                insertModel.CreationTime = DateTime.Now;
                if (!shopbll.Insert(insertModel))
                {
                    message = "添加商铺失败，请重新添加!";
                    ViewBag.JS = UIMessage.AlertDialog(message);
                }
                else
                {
                    ViewBag.JS = UIMessage.ShowDialogAndRedirct(message, returnUrl);
                }
            }
            else
            {
                var updateModel = shopbll.GetModel(model.Id);
                updateModel.OpenID = model.OpenId;
                updateModel.ParentID = model.ParentOpenId;
                updateModel.WeChatName = string.Empty;
                updateModel.Contacts = model.Contacts;
                updateModel.Mobile = model.Mobile;
                updateModel.Address = model.Address;
                updateModel.Note = model.Note;
                updateModel.ProductNum = model.ProductNum;
                updateModel.Status = (int) model.Status;
                if (!shopbll.Update(updateModel))
                {
                    message = "更新失败，请重新更新!";
                    ViewBag.JS = UIMessage.AlertDialog(message);
                }
                else
                {
                    message = "更新成功!";
                    ViewBag.JS = UIMessage.ShowDialogAndRedirct(message, returnUrl);
                }
            }

            return View();
        }
        #endregion

        #region 用户管理

        public ActionResult Members()
        {
            MemberListVM viewModel = new MemberListVM() { itemList = new List<MemberVM>() };

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
            if (Request["userNo"] != null)
            {
                strWhere += string.Format(" and WeChatImage = '{0}'", Request["userNo"]);
            }
            strWhere += " order by CreationTime DESC";
            var itemList = wechatMemberbll.GetListPaging(strWhere, pageIndex, itemsPrePage, out totalPages, out totalItems, (int)UserType.Dining);
            viewModel.page = new Pager() { RecordAllCount = (int)totalItems, PageIndex = (int)pageIndex, PageAllCount = (int)totalPages, PageUrl = url };

            #endregion

            itemList.ForEach((p) =>
            {
                viewModel.itemList.Add(viewModel.ETV(p));
            });

            return View(viewModel);
        }

        #endregion

        #region 验证码管理

        public ActionResult Captcha()
        {
            MemberListVM viewModel = new MemberListVM() { itemList = new List<MemberVM>() };

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
            if (Request["userNo"] != null)
            {
                strWhere += string.Format(" and WeChatImage = '{0}'", Request["userNo"]);
            }
            strWhere += " order by CreationTime DESC";
            var itemList = wechatMemberbll.GetListPaging(strWhere, pageIndex, itemsPrePage, out totalPages, out totalItems, (int)UserType.Dining);
            viewModel.page = new Pager() { RecordAllCount = (int)totalItems, PageIndex = (int)pageIndex, PageAllCount = (int)totalPages, PageUrl = url };

            #endregion

            itemList.ForEach((p) =>
            {
                viewModel.itemList.Add(viewModel.ETV(p));
            });

            return View(viewModel);
        }


        #endregion

        #region 私有方法

        private bool IsUserExists(string userName)
        {
            if (!(memberbll.GetModel(userName) == null))
            {
                return false;
            }
            return true;
        }

        private Member GetDiningModel(DiningVM model)
        {
            string salt = Utility.GetSalt();
            return new Member()
            {
                UserName = model.UserName,
                Mobile = model.Mobile,
                Tel = model.Tel,
                RealName = model.RealName,
                DiningName = model.DiningName,
                Address = model.Address,
                Salt = salt,
                Password = Utility.EncryptDES(model.Pwd, salt),
                HeaderImg = string.Empty,
                Age = 0,
                Sex = 0,
                Province = 0,
                City = 0,
                Town = 0,
                Type = (int)UserType.Dining,
                CreationDateTime = DateTime.Now,
                Status = (int)UserStatus.Normal,
                IsOut = 0
            };
        }

        private Member GetSenderModel(SenderVM model)
        {
            string salt = Utility.GetSalt();
            return new Member()
            {
                UserName = model.UserName,
                Mobile = model.Mobile,
                Tel = string.Empty,
                RealName = model.RealName,
                DiningName = string.Empty,
                Address = string.Empty,
                Salt = salt,
                Password = Utility.EncryptDES(model.Pwd, salt),
                HeaderImg = string.Empty,
                Age = 0,
                Sex = 0,
                Province = 0,
                City = 0,
                Town = 0,
                Type = (int)UserType.Sender,
                CreationDateTime = DateTime.Now,
                Status = (int)UserStatus.Normal,
                IsOut = model.IsOut
            };
        }

        private ShopDetail GetShopDetail(ShopDetailsVM model)
        {
            return new ShopDetail()
            {
                OpenID = model.OpenId,
                ParentID = model.ParentOpenId,
                WeChatName = string.Empty,
                Contacts = model.Contacts,
                Mobile = model.Mobile,
                Address = model.Address,
                Note = model.Note,
                ProductNum = model.ProductNum,
                Status = (int)model.Status,
            };
        }

        #endregion
    }
}
