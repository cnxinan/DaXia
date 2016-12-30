using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DaXia.EntityDataModels;
using DaXia.WebFrameWork;

namespace DaXia.WebComAdmin
{
    public class ManageAdminVM
    {
        public List<AdminVM> itemList { get; set; }

        public Pager page { get; set; }

        public AdminVM ETV(Manager entity)
        {
            return new AdminVM()
            {
                username = entity.Username,
                lastLoginTime = Utility.DTDefaultFormat(entity.LoginDatetime.Value),
                lastLoginIp = entity.LoginIP
            };
        }
    }

    public class AdminVM
    {
        public string id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string lastLoginTime { get; set; }
        public string lastLoginIp { get; set; }
    }

    public class ManageSchoolVM
    {
        public List<SchoolVM> itemList { get; set; }

        public Pager page { get; set; }

        public SchoolVM ETV(School entity)
        {
            return new SchoolVM()
            {
                ID = entity.ID,
                Name = entity.Name,
                Address = entity.Address,
                CreateTime = Utility.DTDefaultFormat(entity.CreationDateTime)
            };
        }
    }

    public class SchoolVM
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string CreateTime { get; set; }
    }

    public class AdInfoListVM
    {
        public List<AdInfoVM> itemList { get; set; }

        public Pager page { get; set; }

        public AdInfoVM ETV(AdInfo entity)
        {
            return new AdInfoVM()
            {
                ID = entity.ID,
                AdName = entity.AdName,
                AdUrl = entity.AdUrl,
                AdImage = entity.AdImage,
                Sort = entity.Sort,
                CreationTime = Utility.DTDefaultFormat(entity.CreationTime)
            };
        }
    }

    public class AdInfoVM
    {
        public Guid ID { get; set; }

        public string AdName { get; set; }

        public string AdUrl { get; set; }

        public string AdImage { get; set; }

        public int Sort { get; set; }

        public string CreationTime { get; set; }
    }

    public class PasswordVM
    {
        #region 密码修改
        
        public string oldPW { get; set; }

        public string newPW { get; set; }

        public string reNewPW { get; set; }

        #endregion
    }
}