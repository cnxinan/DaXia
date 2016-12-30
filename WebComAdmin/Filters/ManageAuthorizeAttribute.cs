using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaXia.EntityDataModels;
using DaXia.WebFrameWork;

namespace DaXia.WebComAdmin
{
    /// <summary>
    /// 后台身份验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class ManageAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {

        private bool ignore = false;
        public bool Ignore
        {
            get{return this.ignore;}
            set{this.ignore = value;}
        }
                
        /// <summary>
        /// 是否需要检查Cookie
        /// </summary>
        private bool checkCookie = true;
        public bool CheckCookie
        {
            get { return this.checkCookie; }
            set { this.checkCookie = value; }
        }  
      
        #region IAuthorizationFilter 成员
        
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (Ignore)
            {
                return;
            }

            if (!AuthorizeCore(filterContext))
            {
                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new JsonResult() { Data = new StatusMessageData(StatusMessageType.Hint, "您必须先以管理员身份登录后台，才能继续操作") };
                }
                else
                {
                    filterContext.Controller.TempData["StatusMessageData"] = new StatusMessageData(StatusMessageType.Hint, "请以管理员身份登录");
                    filterContext.Result = new RedirectResult("/ControlPanel/Login");
                }
            }
        }

        #endregion

        #region private methods

        public bool AuthorizeCore(AuthorizationContext filterContext)
        {
            Manager currentUser = UserContext.CurrentManager;
            if (currentUser == null)
                return false;

            if (CheckCookie)
            {
                HttpCookie adminCookie = filterContext.HttpContext.Request.Cookies[AuthorizeHelper.SystemCookie];
                if (adminCookie != null)
                {
                    bool isLoginMarked = false;
                    try
                    {
                        bool.TryParse(Utility.DecryptTokenForAdminCookie(adminCookie.Value), out isLoginMarked);
                    }
                    catch { }

                    if (isLoginMarked)
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        #endregion
    }
}