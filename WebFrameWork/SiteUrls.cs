using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace DaXia.WebFrameWork
{
    public class SiteUrls
    {
        private static volatile SiteUrls _instance = null;
        private static readonly Object lockObject = new object();
        private readonly string defaultAreaName = string.Empty;
                
        private SiteUrls() { }
                
        public static SiteUrls Instance
        {
            get 
            {
                if (_instance == null)
                {
                    lock (lockObject)
                    {
                        _instance = new SiteUrls();
                    }
                }

                return _instance;
            }
        }

        #region 管理系统

        #region 注册登录/主面板

        //登陆页面
        public string M_Login()
        {
            return CachedUrlHelper.Action("Login", "ControlPanel", defaultAreaName, null);
        }

        #endregion

        #region 会员管理

        //餐厅管理
        public string M_DiningManage()
        {
            return CachedUrlHelper.Action("DiningRoom", "ControlPanelMember", defaultAreaName);
        }

        //创建/编辑餐厅
        public string M_DiningAddAndEdit(int? id = null)
        {
            RouteValueDictionary dictionary = new RouteValueDictionary();

            dictionary.Add("id", id);

            return CachedUrlHelper.Action("DiningAddEdit", "ControlPanelMember", defaultAreaName, dictionary);
        }

        //配送员管理
        public string M_SenderManage()
        {
            return CachedUrlHelper.Action("Sender", "ControlPanelMember", defaultAreaName);
        }

        //创建/编辑配送员
        public string M_SenderAddAndEdit(int? id = null)
        {
            RouteValueDictionary dictionary = new RouteValueDictionary();

            dictionary.Add("id", id);

            return CachedUrlHelper.Action("SenderAddEdit", "ControlPanelMember", defaultAreaName, dictionary);
        }

        #endregion

        #region 订单管理
        #endregion

        #region 财务管理
        #endregion        

        #region 数据统计
        #endregion

        #region 系统设置

        //管理员管理
        public string M_ManageAdmins()
        {
            return CachedUrlHelper.Action("ManageAdmins", "ControlPanelSetting", defaultAreaName);
        }

        //创建/编辑管理员
        public string M_ManageAddAndEdit(int? id = null)
        {
            RouteValueDictionary dictionary = new RouteValueDictionary();           

            dictionary.Add("id",id);

            return CachedUrlHelper.Action("ManageAddAndEdit", "ControlPanelSetting", defaultAreaName, dictionary);
        }

        public string M_SchoolManage()
        {
            return CachedUrlHelper.Action("ManageSchools", "ControlPanelSetting", defaultAreaName); 
        }

        public string M_SchoolAddAndEdit(int? id=null)
        {
            RouteValueDictionary dictionary = new RouteValueDictionary();

            dictionary.Add("id", id);

            return CachedUrlHelper.Action("SchoolAddAndEdit", "ControlPanelSetting", defaultAreaName, dictionary);
        }
        #endregion

        #endregion
    }
}
