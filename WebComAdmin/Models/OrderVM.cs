using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DaXia.WebFrameWork;

namespace DaXia.WebComAdmin
{
    public class OrderListVM
    {
        public List<OrderItem> itemList { get; set; }

        public Pager page { get; set; }        
    }

    public class OrderItem
    {
        public Guid ID { get; set; }

        public string memberNo {get;set;}

        public string memberOpenId { get; set; }

        public string nickName { get; set; }

        public string productName { get; set; }

        public int processNum { get; set; }

        public string shopName { get; set; }

        public string address { get; set; }

        public string mobile { get; set; }

        public string reciver { get; set; }

        public string status { get; set; }

        public string creationDate { get; set; }
    }

    public class FinanceListVM
    {
        public List<FinanceVM> itemList { get; set; }

        public Pager page { get; set; }    
    }

    public class FinanceVM
    {
        public Guid ID { get; set; }

        public string OrderNo { get; set; }

        public string OpenId { get; set; }

        public string FromOpenId { get; set; }

        public decimal Amount { get; set; }

        public decimal OriginalPrice { get; set; }

        public string Note { get; set; }

        public string Type { get; set; }

        public bool IsPay { get; set; }

        public string Status { get; set; }

        public string CreationTime { get; set; }
    }

    public class TxAccountListVM
    {
        public List<TxAccountVM> itemList { get; set; }

        public Pager page { get; set; }  
    }

    public class TxAccountVM
    {
        public Guid ID { get; set; }

        public string OrderNo { get; set; }

        public string OpenId { get; set; }

        public decimal Amount { get; set; }

        public bool IsCheced { get; set; }

        public string Status { get; set; }

        public string Contacts { get; set; }

        public string Mobile { get; set; }

        public string Address { get; set; }

        public string CreationTime { get; set; }
    }

    public class BuyOrderListVM
    {
        public List<OrderVM> itemList { get; set; }

        public Pager page { get; set; }   
    }

    public class OrderVM
    {
        public Guid ID { get; set; }

        public string orderNo { get; set; }

        public string openId { get; set; }

        public string productName { get; set; }

        public int count { get; set; }

        public decimal amount { get; set; }

        public int status { get; set; }

        public string receiver { get; set; }

        public string mobile { get; set; }

        public string address { get; set; }

        public string creationDate { get; set; }
    }
}