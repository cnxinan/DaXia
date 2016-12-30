using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsShow.WebFrameWork
{
    public class User
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string RealName { get; set; }

        public bool IsAdmin { get; set; }               //是否是管理员

        public Guid Department { get; set; }            //市场部

        public string DepartmentName { get; set; }      //管理员所在市场部

        public int UserType { get; set; }               //人员类型，管理员则为UserAdminType，会员则为UserType

        public int Status { get; set; }                 //人员状态

        public Guid? Parent { get; set; }               //专员的经理ID

        public bool IsReporter { get; set; }            //是否为报单员

        public int SaleNet { get; set; }                //网络类型

        public int SaleDisc { get; set; }               //会员等级
    }
}