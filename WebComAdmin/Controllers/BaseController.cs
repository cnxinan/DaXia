using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaXia.BLL;
using DaXia.EntityDataModels;
using DaXia.WebFrameWork;

namespace DaXia.WebComAdmin
{
    [ManageAuthorize]
    public class BaseController : Controller
    {
        public static BLLFactory BllInstance
        {
            get
            {
                return BLLFactory.Instance;
            }
        }

        public static Manager CurrentUser
        {
            get
            {
                return UserContext.CurrentManager;
            }
        }

        #region functions

        public string ResultToJson(object result)
        {
            return Utility.ObjectToJson(result);
        }

        #endregion

    }
}
