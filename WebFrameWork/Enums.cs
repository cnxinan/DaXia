using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DaXia.WebFrameWork
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum Sex
    {
        [Display(Name = "男")]
        Male,
        [Display(Name = "女")]
        Female
    }

    /// <summary>
    /// 会员类型
    /// </summary>
    public enum UserType
    {
        Dining = 130,       //餐厅
        Sender = 140        //配送员
    };

    /// <summary>
    /// 会员等级
    /// </summary>
    public enum UserLevel
    {
        Common = 10,            //普通会员
        VIP = 20                //VIP会员
    }
    
    /// <summary>
    /// 人员状态
    /// </summary>
    public enum UserStatus
    {
        [Display(Name="正常")]
        Normal = 11,             //正    常        
        [Display(Name="禁用")]
        Disable = 35,            //禁    用        
    }

    /// <summary>
    /// 管理员类型
    /// </summary>
    public enum ManagerType
    {
        [Display(Name = "超级管理")]
        SuperAdmin      
    }

    /// <summary>
    /// 商家类型
    /// </summary>
    public enum ShopStatus
    {
        [Display(Name="已禁用")]
        Disable = -1,
        [Display(Name="未付款")]
        NoPay = 0,
        [Display(Name = "已付款")]
        Normal = 1
    } 

    /// <summary>
    /// 提现订单状态
    /// </summary>
    public enum TXOrderStatus
    {
        [Display(Name = "未审核")]
        NoCheck = 0,
        [Display(Name = "已审核")]
        Checked = 1
    }

    /// <summary>
    /// 购物订单状态
    /// </summary>
    public enum ProductOrderStatus
    {
        [Display(Name = "未支付")]
        NoPay = 0,
        [Display(Name = "已支付")]
        Paied = 1,
        [Display(Name = "已发货")]
        Returns = 2
    }
}
