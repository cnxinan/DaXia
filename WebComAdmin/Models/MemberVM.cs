using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaXia.EntityDataModels;
using DaXia.WebFrameWork;

namespace DaXia.WebComAdmin
{
    public class DiningListVM
	{
        public List<DiningVM> itemList { get; set; }

        public Pager page { get; set; }

        public DiningVM ETV(Member entity)
        {
            return new DiningVM() 
            {
                Id = entity.ID,
                UserName = entity.UserName,
                DiningName = entity.DiningName,
                RealName = entity.RealName,
                Mobile = entity.Mobile,
                Tel = entity.Tel,
                Address = entity.Address,
                Status = Utility.GetEnumDescription<UserStatus>((UserStatus)entity.Status),
                CreationTime = Utility.DTDefaultFormat(entity.CreationDateTime)
            };
        }
	}

    public class DiningVM
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Pwd { get; set; }

        public string DiningName { get; set; }
        
        public string RealName { get; set; }

        public string Mobile { get; set; }

        public string Tel { get; set; }

        public string Address { get; set; }

        public string Status { get; set; }

        public string CreationTime { get; set; }
    }

    public class SenderListVM
    {
        public List<SenderVM> itemList { get; set; }

        public Pager page { get; set; }

        public SenderVM ETV(Member entity)
        {
            return new SenderVM()
            {
                Id = entity.ID,
                UserName = entity.UserName,
                RealName = entity.RealName,
                Mobile = entity.Mobile,
                Status = Utility.GetEnumDescription<UserStatus>((UserStatus)entity.Status),
                IsOut = entity.IsOut.Value,
                CreationTime = Utility.DTDefaultFormat(entity.CreationDateTime)
            };
        }
    }

    public class SenderVM
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Pwd { get; set; }

        public string RealName { get; set; }

        public string Mobile { get; set; }

        public string Status { get; set; }

        public int IsOut { get; set; }

        public string CreationTime { get; set; }
    }

    public class BindSchoolVM
    {
        public string Type { get; set; }

        public string Name { get; set; }

        public List<SelectListItem> Schools { get; set; }

        public string ListName { get; set; }
    }

    public class ShopListVM
    {
        public List<ShopDetailsVM> itemList { get; set; }

        public Pager page { get; set; }

        public ShopDetailsVM ETV(ShopDetail entity)
        {
            var vm = new ShopDetailsVM();

            vm.Id = entity.ID;
            vm.OpenId = entity.OpenID;
            vm.ParentOpenId = string.IsNullOrWhiteSpace(entity.ParentID) ? "无" : entity.ParentID;
            vm.Contacts = entity.Contacts;
            vm.Mobile = entity.Mobile;
            vm.Address = entity.Address;
            vm.Note = entity.Note;
            vm.ProductNum = entity.ProductNum;
            vm.Status = (ShopStatus)entity.Status;
            vm.CreationTime = Utility.DTDefaultFormat(entity.CreationTime);
            return vm;
        }
    }

    public class ShopDetailsVM
    {
        public Guid Id { get; set; }
        public string ParentOpenId { get; set; }
        public string OpenId { get; set; }
        public string Contacts { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public int ProductNum { get; set; }
        public ShopStatus Status { get; set; }
        public string CreationTime { get; set; }
    }

    public class MemberListVM
    {
        public List<MemberVM> itemList { get; set; }

        public Pager page { get; set; }

        public MemberVM ETV(WeChatMember entity)
        {
            var vm = new MemberVM();
            WechatUserInfoEntity wechatModel = Utility.JsonToObject<WechatUserInfoEntity>(entity.WeChatName);
            vm.Id = entity.ID;
            vm.OpenId = entity.OpenID;
            vm.WeChatName = wechatModel.nickname;
            vm.WeChatImage = entity.WeChatImage;
            vm.Balance = entity.Balance;
            vm.CreationTime = Utility.DTDefaultFormat(entity.CreationTime);
            return vm;
        }
    }

    public class MemberVM
    {
        public Guid Id { get; set; }

        public string OpenId { get; set; }

        public string WeChatName { get; set; }

        public string WeChatImage { get; set; }

        public decimal Balance { get; set; }

        public string CreationTime { get; set; } 
    }
}