using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.BLL;

namespace DaXia.WebFrameWork
{
    public class SiteContext
    {
        private static volatile SiteContext _instance = null;
        private static readonly object _lockObj = new object();
        private static volatile int _maxUserNo = -1;
        private static volatile int _maxUserAdminNo = -1;
        private static volatile int _maxOrderNo = -1;

        public static SiteContext Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock (_lockObj)
                    {
                        _instance = new SiteContext();
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 最大用户编号
        /// </summary>
        public int MaxUserNo
        {
            get
            {
                if (_maxUserNo < 0)
                {
                    //从数据库获取当前最大编号
                    //_maxUserNo = memberBll.GetMaxUserNo();                    
                }

                _maxUserNo++;
                return _maxUserNo;
            }
        }

        

        /// <summary>
        /// 当日最大订单编号
        /// </summary>
        public int MaxOrderNo
        {
            get
            {
                if (_maxOrderNo < 0)
                {
                    //从数据库获取当前最大编号
                    //_maxOrderNo = userAdminBll.GetMaxUserAdminNo();
                }

                _maxOrderNo++;
                return _maxOrderNo;
            }
        }
    }
}
