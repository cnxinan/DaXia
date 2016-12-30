using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DaXia.WebComAdmin
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            #region 登录/框架/首页

            //登录后首页
            routes.MapRoute(
                name: "Index",
                url: "admin/Index",
                defaults: new { controller = "ControlPanel", action = "Frame" }
            );

            //登录页面
            routes.MapRoute(
                name: "Login",
                url: "Login/Index",
                defaults: new { controller = "Login", action = "Index" }
            );

            #endregion
                       

            #region 默认路由

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "FrontHome", action = "Index", id = UrlParameter.Optional }
            );

            #endregion
            
        }
    }
}