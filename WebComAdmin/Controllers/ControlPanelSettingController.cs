/*
 * 系统设置控制器
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DaXia.WebFrameWork;
using DaXia.EntityDataModels;
using DaXia.BLL;

namespace DaXia.WebComAdmin
{
    public class ControlPanelSettingController : BaseController
    {
        private readonly Manager_bll managerbll = BllInstance.ManagerBll;
        private readonly School_bll schoolBll = BllInstance.SchoolBll;
        private readonly AdInfo_bll adInfoBll = BllInstance.AdInfoBll;
        private Manager currentUser = UserContext.CurrentManager;

        /// <summary>
        /// 系统参数设置
        /// </summary>
        /// <returns></returns>
        public ActionResult SiteSetting()
        {
            return View();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(PasswordVM model)
        {
            string msg = "修改登陆密码成功！";

            Manager memModel = managerbll.GetModel(currentUser.Username);
            string inputPassword = Utility.EncryptDES(model.oldPW, memModel.Salt);
            if (!(inputPassword == memModel.Password))
            {
                msg = "旧密码输入错误,请重新填写!";
                ViewBag.JS = UIMessage.AlertDialog(msg);
                return View(model);
            }

            memModel.Password = Utility.EncryptDES(model.newPW, memModel.Salt);

            if (!managerbll.Update(memModel))
            {
                msg = "保存数据错误!";
                ViewBag.JS = UIMessage.AlertDialog(msg);
                return View(model);
            }

            ViewBag.JS = UIMessage.AlertDialog(msg);

            return View(model);
        }

        #region 管理员管理

        /// <summary>
        /// 系统管理员管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ManageAdmins()
        {
            ManageAdminVM viewModel = new ManageAdminVM() { itemList = new List<AdminVM>() };

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
            var itemList = managerbll.GetListPaging(strWhere, pageIndex, itemsPrePage, out totalPages, out totalItems, currentUser.Username);
            viewModel.page = new Pager() { RecordAllCount = (int)totalItems, PageIndex = (int)pageIndex, PageAllCount = (int)totalPages, PageUrl = url };

            #endregion

            itemList.ForEach((p) =>
            {
                viewModel.itemList.Add(viewModel.ETV(p));
            });

            return View(viewModel);
        }

        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ManageAddAndEdit()
        {
            return View();
        }        

        [HttpPost]
        public ActionResult ManageAddAndEdit(AdminVM model)
        {
            #region 验证用户是否可注册，这里要考虑使用ajax方式提前验证

            var userModel = managerbll.GetModel(model.username);
            if (userModel != null)
            {
                ViewBag.JS = UIMessage.AlertDialog("用户已存在，请重新输入");

                return View(model);
            }

            #endregion

            Manager insertModel = GetManagerModel(model);
            string returnUrl = SiteUrls.Instance.M_ManageAdmins();
            string message = string.Empty;
            try
            {
                if (managerbll.Insert(insertModel))
                {
                    message = "添加成功";
                }
                else
                {
                    message = "添加失败";
                }
            }
            catch (Exception ex)
            {
                message = "出现异常:" + ex.Message;
            }

            ViewBag.JS = UIMessage.ShowDialogAndRedirct(message, returnUrl);

            return View(model);
        }

        #endregion

        #region 学校管理

        /// <summary>
        /// 学校管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ManageSchools()
        {
            ManageSchoolVM viewModel = new ManageSchoolVM() { itemList = new List<SchoolVM>() };

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
            strWhere += " order by CreationDateTime ASC";
            var itemList = schoolBll.GetListPaging(strWhere, pageIndex, itemsPrePage, out totalPages, out totalItems, currentUser.Username);
            viewModel.page = new Pager() { RecordAllCount = (int)totalItems, PageIndex = (int)pageIndex, PageAllCount = (int)totalPages, PageUrl = url };

            #endregion

            itemList.ForEach((p) =>
            {
                viewModel.itemList.Add(viewModel.ETV(p));
            });

            return View(viewModel);
        }

        public ActionResult SchoolAddAndEdit(int? id)
        {
            SchoolVM viewModel = new SchoolVM();
            if (id != null)
            {
                var entity = schoolBll.GetModel(id.Value);

                viewModel.ID = entity.ID;
                viewModel.Name = entity.Name;
                viewModel.Address = entity.Address;
                viewModel.CreateTime = Utility.DTDefaultFormat(entity.CreationDateTime);
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SchoolAddAndEdit(SchoolVM model)
        {

            #region 验证学校名是否可注册

            var schoolModel = schoolBll.GetModel(model.Name);
            if (schoolModel != null)
            {
                ViewBag.JS = UIMessage.AlertDialog("用户已存在，请重新输入");

                return View(model);
            }

            #endregion

            School insertModel = GetSchoolModel(model);
            string returnUrl = SiteUrls.Instance.M_SchoolManage();
            string message = string.Empty;
            try
            {
                if (schoolBll.Insert(insertModel))
                {
                    message = "添加成功";
                }
                else
                {
                    message = "添加失败";
                }
            }
            catch (Exception ex)
            {
                message = "出现异常:" + ex.Message;
            }

            ViewBag.JS = UIMessage.ShowDialogAndRedirct(message, returnUrl);

            return View(model);
        }

        #endregion

        #region 广告管理

        /// <summary>
        /// 广告管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ManageAds()
        {
            AdInfoListVM viewModel = new AdInfoListVM() { itemList = new List<AdInfoVM>() };

            string strWhere = string.Format(" where 1=1 ");

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
            strWhere += " order by Sort ASC";
            var itemList = adInfoBll.GetListPaging(strWhere, pageIndex, itemsPrePage, out totalPages, out totalItems, currentUser.Username);
            viewModel.page = new Pager() { RecordAllCount = (int)totalItems, PageIndex = (int)pageIndex, PageAllCount = (int)totalPages, PageUrl = url };

            #endregion

            itemList.ForEach((p) =>
            {
                viewModel.itemList.Add(viewModel.ETV(p));
            });

            return View(viewModel);
        }

        public ActionResult AdAddEdit(Guid? id)
        {
            AdInfoVM viewModel = new AdInfoVM();
            if (id != null)
            {
                var entity = adInfoBll.GetModel(id.Value);

                viewModel.ID = entity.ID;
                viewModel.AdName = entity.AdName;
                viewModel.AdImage = entity.AdImage;
                viewModel.AdUrl = entity.AdUrl;
                viewModel.Sort = entity.Sort;
                viewModel.CreationTime = Utility.DTDefaultFormat(entity.CreationTime);
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AdAddEdit(AdInfoVM model)
        {
            string returnUrl = "/ControlPanelSetting/ManageAds";
            string message = string.Empty;

            if (model.ID == Guid.Empty)
            {
                try
                {
                    AdInfo insertModel = new AdInfo() 
                    {
                        ID = Guid.NewGuid(),
                        AdName =  model.AdName,
                        AdImage = model.AdImage,
                        AdUrl = model.AdUrl,
                        Sort = model.Sort,
                        CreationTime = DateTime.Now
                    };

                    if (adInfoBll.Insert(insertModel))
                    {
                        message = "添加成功";
                        ViewBag.JS = UIMessage.ShowDialogAndRedirct(message, returnUrl);
                    }
                    else
                    {
                        message = "添加失败";
                        ViewBag.JS = UIMessage.AlertDialog(message);
                    }
                }
                catch (Exception ex)
                {
                    message = "出现异常:" + ex.Message;
                    ViewBag.JS = UIMessage.AlertDialog(message);
                }
            }
            else
            {
                try
                {
                    AdInfo updateModel = adInfoBll.GetModel(model.ID);

                    updateModel.AdName = model.AdName;
                    updateModel.AdImage = model.AdImage;
                    updateModel.AdUrl = model.AdUrl;
                    updateModel.Sort = model.Sort;

                    if (adInfoBll.Update(updateModel))
                    {
                        message = "更新成功";
                        ViewBag.JS = UIMessage.ShowDialogAndRedirct(message, returnUrl);
                    }
                    else
                    {
                        message = "更新失败";
                        ViewBag.JS = UIMessage.AlertDialog(message);
                    }
                }
                catch (Exception ex)
                {
                    message = "出现异常:" + ex.Message;
                    ViewBag.JS = UIMessage.AlertDialog(message);
                }
            }

            return View(model);
        }

        public ActionResult _ajaxDeleteAd(Guid id)
        {
            string result = "删除成功";

            if (!adInfoBll.Delete(id))
            {
                result = "删除失败";
            }

            return Content(result);
        }

        #endregion

        #region 账户管理

        public ActionResult AccountBackups()
        {
            AdInfoListVM viewModel = new AdInfoListVM() { itemList = new List<AdInfoVM>() };

            string strWhere = string.Format(" where 1=1 ");

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
            strWhere += " order by Sort ASC";
            var itemList = adInfoBll.GetListPaging(strWhere, pageIndex, itemsPrePage, out totalPages, out totalItems, currentUser.Username);
            viewModel.page = new Pager() { RecordAllCount = (int)totalItems, PageIndex = (int)pageIndex, PageAllCount = (int)totalPages, PageUrl = url };

            #endregion

            itemList.ForEach((p) =>
            {
                viewModel.itemList.Add(viewModel.ETV(p));
            });

            return View(viewModel);
        }

        #endregion

        #region 备份管理

        public ActionResult ManageBackups()
        {
            AdInfoListVM viewModel = new AdInfoListVM() { itemList = new List<AdInfoVM>() };

            string strWhere = string.Format(" where 1=1 ");

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
            strWhere += " order by Sort ASC";
            var itemList = adInfoBll.GetListPaging(strWhere, pageIndex, itemsPrePage, out totalPages, out totalItems, currentUser.Username);
            viewModel.page = new Pager() { RecordAllCount = (int)totalItems, PageIndex = (int)pageIndex, PageAllCount = (int)totalPages, PageUrl = url };

            #endregion

            itemList.ForEach((p) =>
            {
                viewModel.itemList.Add(viewModel.ETV(p));
            });

            return View(viewModel);
        }

        #endregion

        #region 意见管理

        public ActionResult ManageAdvise()
        {
            AdInfoListVM viewModel = new AdInfoListVM() { itemList = new List<AdInfoVM>() };

            string strWhere = string.Format(" where 1=1 ");

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
            strWhere += " order by Sort ASC";
            var itemList = adInfoBll.GetListPaging(strWhere, pageIndex, itemsPrePage, out totalPages, out totalItems, currentUser.Username);
            viewModel.page = new Pager() { RecordAllCount = (int)totalItems, PageIndex = (int)pageIndex, PageAllCount = (int)totalPages, PageUrl = url };

            #endregion

            itemList.ForEach((p) =>
            {
                viewModel.itemList.Add(viewModel.ETV(p));
            });

            return View(viewModel);
        }

        #endregion

        #region 日志管理

        public ActionResult ManageLogs()
        {
            AdInfoListVM viewModel = new AdInfoListVM() { itemList = new List<AdInfoVM>() };

            string strWhere = string.Format(" where 1=1 ");

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
            strWhere += " order by Sort ASC";
            var itemList = adInfoBll.GetListPaging(strWhere, pageIndex, itemsPrePage, out totalPages, out totalItems, currentUser.Username);
            viewModel.page = new Pager() { RecordAllCount = (int)totalItems, PageIndex = (int)pageIndex, PageAllCount = (int)totalPages, PageUrl = url };

            #endregion

            itemList.ForEach((p) =>
            {
                viewModel.itemList.Add(viewModel.ETV(p));
            });

            return View(viewModel);
        }

        #endregion

        /// <summary>
        /// 根据ViweModel获取需要插入的实体类
        /// </summary>
        /// <returns></returns>

        private Manager GetManagerModel(AdminVM model)
        {
            Manager insertModel = new Manager();
            insertModel.Salt = Utility.GetSalt();
            insertModel.Username = model.username;
            insertModel.Password = Utility.EncryptDES(model.password, insertModel.Salt);
            insertModel.LoginDatetime = DateTime.Now;
            insertModel.LoginIP = Request.UserHostAddress;

            return insertModel;
        }

        private School GetSchoolModel(SchoolVM model)
        {
            return new School()
            {
                Name = model.Name,
                Address = model.Address,
                CreationDateTime = DateTime.Now
            };
        }
        
    }
}