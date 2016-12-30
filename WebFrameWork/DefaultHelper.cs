using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DaXia.WebFrameWork
{
    public class DefaultHelper
    {
        #region 默认变量值

        public readonly static string defaultImgPath = @"/UpLoads/NoImage.jpg";
        public readonly static int defaultAge = 0;
        public readonly static Sex defaultSex = Sex.Female;
        public readonly static int defaultArea = 0;
        public readonly static string defaultSetting = "未设置";
        public readonly static int defaultEnum = -1;

        #region 分页变量

        //默认分页实体，给展示页面用
        public readonly static int pageIndex = 1;       //默认页
        public readonly static long itemsPrePage = 15;  //页面默认大小
        public readonly static Pager pagedefault = new Pager() { RecordAllCount = 1, PageIndex = pageIndex, PageAllCount = 1 };        

        #endregion

        #endregion
    }
}
