using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DaXia.EntityDataModels;
using DaXia.BLL;

namespace DaXia.WebComAdmin
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class ApiAuthorizeAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// 是否需要用户登陆后操作
        /// </summary>
        private bool isUserLogin = false;

        public bool IsUserLogin
        {
            get
            {
                return isUserLogin;
            }
            set
            {
                isUserLogin = value;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            string rawUrl = filterContext.HttpContext.Request.RawUrl;

            LogRequest(rawUrl);
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }  

            //验证是否带有用户ID
            if (IsUserLogin)
            {
 
            }
        }

        //记录访问的原始参数
        private void LogRequest(string rawUrl)
        {
            RequestLog log = new RequestLog()
            {
                ID = Guid.NewGuid(),
                RawUrl = rawUrl,
                CreationDateTime = DateTime.Now
            };

            BLLFactory.Instance.RequestLogBll.Insert(log);
        }

        //验证密钥是否正确
        private bool AuthorizeCore(AuthorizationContext filterContext)
        {
            return true;
        }
    }
}