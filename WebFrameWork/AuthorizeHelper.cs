using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using DaXia.BLL;
using DaXia.EntityDataModels;

namespace DaXia.WebFrameWork
{
    public class AuthorizeHelper
    {
        private readonly Manager_bll managerBll = new Manager_bll();
        private Manager _signedInManager;
        
        public static string SystemCookie
        {
            get
            {
                return "DaXiaCookie";
            }
        }

        /// <summary>
        /// 获取管理员系统登陆用户
        /// </summary>
        /// <returns></returns>
        public Manager GetAuthenticatedManager()
        {
            HttpContext httpContext = HttpContext.Current;
            if (httpContext == null || !httpContext.Request.IsAuthenticated || !(httpContext.User.Identity is FormsIdentity))
            {
                return null;
            }

            if (_signedInManager != null)
            {
                return _signedInManager;
            }

            _signedInManager = managerBll.GetModel(httpContext.User.Identity.Name);

            return _signedInManager;
        }
    }
}
