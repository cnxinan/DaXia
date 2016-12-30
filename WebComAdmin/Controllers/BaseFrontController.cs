using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaXia.BLL;
using DaXia.EntityDataModels;
using DaXia.WebFrameWork;

namespace DaXia.WebComAdmin.Controllers
{
    public class BaseFrontController : Controller
    {
        private readonly object obj = new object();


        public static BLLFactory BllInstance
        {
            get
            {
                return BLLFactory.Instance;
            }
        }

        public static string OpenId
        {
            get
            {
                //测试代码
                string TestOpenId = System.Configuration.ConfigurationManager.AppSettings["TestOpenid"];
                if (string.IsNullOrWhiteSpace(TestOpenId))
                {
                    return CookieHelper.GetCookieValue("OPENID");
                }
                else
                {
                    return TestOpenId;
                }
            }
            set
            {
                CookieHelper.SetCookie("OPENID", value);
            }
        }

        public WechatReturnModel GetWechatReturn(string returnJson)
        {
            return Utility.JsonToObject<WechatReturnModel>(returnJson);
        }

        public bool InsertOrUpdateUserInfo(string openId, string userInfoJson)
        {

            WeChatMember_bll memberBll = BllInstance.WeChatMemberBll;
            WechatReturnModel wechatModel;
            try
            {
                wechatModel = GetWechatReturn(userInfoJson);
            }
            catch (Exception ex)
            {
                //AddLogs("用户信息错误", ex.Message);
            }

            lock (obj)
            {
                WeChatMember memberEntity = memberBll.GetModelByOpenId(openId);
                try
                {
                    if (memberEntity == null)
                    {
                        //添加新用户
                        memberEntity = new WeChatMember()
                        {
                            ID = Guid.NewGuid(),
                            OpenID = openId,
                            WeChatName = userInfoJson,
                            WeChatImage = Utility.GenerateSixRandom().ToString(),
                            Balance = 0,
                            CreationTime = DateTime.Now
                        };


                        if (!memberBll.Insert(memberEntity))
                        {
                            //添加报错日志
                            return false;
                        }
                    }
                    else
                    {
                        memberEntity.WeChatName = userInfoJson;
                        if (string.IsNullOrWhiteSpace(memberEntity.WeChatImage))
                        {
                            memberEntity.WeChatImage = Utility.GenerateSixRandom().ToString();
                        }
                        if (!memberBll.Update(memberEntity))
                        {
                            //添加报错日志
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    AddLogs("插入用户出错", ex.Message);
                }
            }
            return true;
        }

        public void AddLogs(string key, string value)
        {
            Log logEntity = new Log() { ID = Guid.NewGuid(), CreationTime = DateTime.Now };
            logEntity.Logkey = key;
            logEntity.LogData = value;

            Log_bll logBll = BllInstance.LogBll;
            logBll.Insert(logEntity);
        }

        public static string RenderPartialViewToString(Controller controller, string viewName, object model)
        {
            IView view = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName).View;
            controller.ViewData.Model = model;
            using (StringWriter writer = new StringWriter())
            {
                ViewContext viewContext = new ViewContext(controller.ControllerContext, view, controller.ViewData, controller.TempData, writer);
                viewContext.View.Render(viewContext, writer);
                return writer.ToString();
            }
        }
    }

    public class WechatReturnModel
    {
        public string access_token { get; set; }

        public string expires_in { get; set; }

        public string refresh_token { get; set; }

        public string openid { get; set; }

        public string scope { get; set; }
    }
}
