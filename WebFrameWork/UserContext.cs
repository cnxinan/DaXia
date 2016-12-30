using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DaXia.EntityDataModels;
using DaXia.BLL;

namespace DaXia.WebFrameWork
{
    public class UserContext
    {
        private static readonly Manager_bll managerBll = new Manager_bll();

        //管理员系统获取登录实体
        public static Manager CurrentManager
        {
            get 
            {
                AuthorizeHelper authorizeHelper = new AuthorizeHelper();
                var currentUser = authorizeHelper.GetAuthenticatedManager();

                if (currentUser != null)
                {
                    return currentUser;
                }               

                return null;
            }
        }        
    }
}
