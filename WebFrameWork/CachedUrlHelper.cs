using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Tunynet.Caching;
using Tunynet;

namespace DaXia.WebFrameWork
{
    public class CachedUrlHelper
    {
        private static readonly string ControllerKey = "controller";
        private static readonly string ActionKey = "action";
        private static readonly string AreaKey = "area";

        /// <summary>
        /// 通过routeName获取url
        /// </summary>
        /// <param name="routeName">routeName</param>
        /// <returns>url</returns>
        public static string RouteUrl(string routeName)
        {
            return RouteUrl(routeName, null);
        }

        /// <summary>
        /// 通过routeName获取url
        /// </summary>
        /// <param name="routeName">routeName</param>
        /// <param name="routeValueDictionary">路由数据</param>
        /// <returns>url</returns>
        public static string RouteUrl(string routeName, RouteValueDictionary routeValueDictionary)
        {
            RouteValueDictionary routeParameters = new RouteValueDictionary();
            string[] values = null;
            if (routeValueDictionary != null)
            {
                if (routeValueDictionary.ContainsKey(ControllerKey))
                {
                    routeParameters[ControllerKey] = routeValueDictionary[ControllerKey];
                    routeValueDictionary.Remove(ControllerKey);
                }
                if (routeValueDictionary.ContainsKey(ActionKey))
                {
                    routeParameters[ActionKey] = routeValueDictionary[ActionKey];
                    routeValueDictionary.Remove(ActionKey);
                }

                values = new string[routeValueDictionary.Count];
                int index = 0;
                foreach (KeyValuePair<string, object> pair in routeValueDictionary)
                {
                    if (pair.Value == null)
                        values[index] = string.Empty;
                    else
                        values[index] = pair.Value.ToString();

                    routeParameters[pair.Key] = "{" + index + "}";
                    index++;
                }
            }

            string url = string.Empty; ;

            RequestContext requestContext = GetRequestContext();
            try
            {
                url = UrlHelper.GenerateUrl(routeName, null, null, routeParameters, RouteTable.Routes, requestContext, false);
            }
            catch
            {
                url = string.Empty;
            }

            if (string.IsNullOrEmpty(url))
                return string.Empty;
            //替换UrlEncode编码
            url = url.Replace("%7b", "{").Replace("%7d", "}").Replace("%7B", "{").Replace("%7D", "}").ToLower();

            if (values != null)
                return string.Format(url, values);
            else
                return url;
        }

        /// <summary>
        /// 通过Action/Controller获取url
        /// </summary>
        /// <param name="actionName">actionName</param>
        /// <param name="controllerName">controllerName</param>
        /// <param name="areaName">路由分区名称</param>
        /// <returns>返回对应的url</returns>
        public static string Action(string actionName, string controllerName, string areaName = "")
        {
            return Action(actionName, controllerName, areaName, null);
        }

        /// <summary>
        /// 通过Action/Controller获取url
        /// </summary>
        /// <param name="actionName">actionName</param>
        /// <param name="controllerName">controllerName</param>
        /// <param name="areaName">路由分区名称</param>
        /// <param name="routeValueDictionary">路由数据</param>
        /// <returns>返回对应的url</returns>
        public static string Action(string actionName, string controllerName, string areaName, RouteValueDictionary routeValueDictionary)
        {
            string cacheKey = string.Format("ActionUrl::c:{0}-a:{1}-r:{2}", controllerName, actionName, areaName ?? string.Empty);

            RouteValueDictionary routeParameters = new RouteValueDictionary();
            string[] values = null;
            if (routeValueDictionary != null)
            {
                values = new string[routeValueDictionary.Count];
                int index = 0;
                foreach (KeyValuePair<string, object> pair in routeValueDictionary)
                {

                    if (pair.Value == null)
                        values[index] = string.Empty;
                    else
                        values[index] = pair.Value.ToString();

                    routeParameters[pair.Key] = "{" + index + "}";

                    index++;
                }
            }

            string url = string.Empty; ;

            if (areaName != null)
                routeParameters.Add(AreaKey, areaName);

            RequestContext requestContext = GetRequestContext();
            try
            {
                url = UrlHelper.GenerateUrl(null, actionName, controllerName, routeParameters, RouteTable.Routes, requestContext, true);
            }
            catch (Exception e)
            {
                url = string.Empty;
            }

            if (string.IsNullOrEmpty(url))
                return string.Empty;
            //替换UrlEncode编码
            url = url.Replace("%7b", "{").Replace("%7d", "}").Replace("%7B", "{").Replace("%7D", "}").ToLower();

            if (values != null)
                return string.Format(url, values);
            else
                return url;
        }

        /// <summary>
        /// 获取RequestContext
        /// </summary>
        /// <returns></returns>
        private static RequestContext GetRequestContext()
        {
            HttpContext httpContext = HttpContext.Current;
            if (httpContext == null)
            {
                var httpRequest = new HttpRequest("", "http://a.com/", "");
                var httpResponse = new HttpResponse(new System.IO.StringWriter(new StringBuilder()));
                httpContext = new HttpContext(httpRequest, httpResponse);
            }
            RequestContext requestContext = new RequestContext(new HttpContextWrapper(httpContext), new RouteData());
            return requestContext;
        }
    }
}
