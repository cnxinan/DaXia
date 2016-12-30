using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;
using DaXia.BLL;
using DaXia.WebFrameWork;
using DaXia.EntityDataModels;

namespace DaXia.WebComAdmin.Controllers
{
    public class WeChatApiController : BaseFrontController
    {

        /// <summary>
        /// 关注时访问的方法接口
        /// </summary>
        /// <returns></returns>
        public ActionResult AddUser()
        {
            string openId = Request["openId"];
            string userInfo = Request["userInfo"];
            if (!string.IsNullOrWhiteSpace(openId) && !string.IsNullOrWhiteSpace(userInfo))
            {
                //InsertOrUpdateUserInfo(openId, userInfo);
            }

            return Content("访问成功");
        }

        public ActionResult AddShop()
        {
            string parentId = Request["parentId"];
            string openId = Request["openId"];
            InsertShopInfo(openId, parentId);

            return Content("添加成功");
        }

        private void InsertShopInfo(string openId, string parentId)
        {
            ShopDetail_bll shopDBll = BllInstance.ShopDetailBll;
            ShopDetail entity = shopDBll.GetModelByOpenId(openId);
            if (entity == null)
            {
                entity = new ShopDetail()
                {
                    ID = Guid.NewGuid(),
                    OpenID = openId,
                    ParentID = parentId,
                    WeChatName = string.Empty,
                    Contacts = string.Empty,
                    Mobile = string.Empty,
                    Address = string.Empty,
                    Note = string.Empty,
                    ProductNum = 100,
                    Status = 0,
                    CreationTime = DateTime.Now
                };
                shopDBll.Insert(entity);
            }
            else
            {
                entity.ParentID = parentId;
                shopDBll.Update(entity);
            }
        }
    }
}
