using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaXia.WebComAdmin
{
    public class WechatUserInfoEntity
    {
        public string subscribe { get; set; }

        public string openid { get; set; }

        public string nickname { get; set; }

        public string sex { get; set; }

        public string language { get; set; }

        public string city { get; set; }

        public string province { get; set; }

        public string country { get; set; }

        public string headimgurl { get; set; }

        public string subscribe_time { get; set; }

        public string remark { get; set; }

        public string groupid { get; set; }

        public string[] tagid_list { get; set; }
    }
}