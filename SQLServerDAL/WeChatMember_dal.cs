using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using PetaPoco;

namespace DaXia.SQLServerDAL
{
    public class WeChatMember_dal : Base_dal
    {
        private readonly string strSql = "select * from [WeChatMember]";

        public WeChatMember GetModelByOpenId(string openId)
        {
            string sqlWhere = " where OpenID=@0";
            return db.Fetch<WeChatMember>(sqlWhere, openId).FirstOrDefault();
        }

        public List<WeChatMember> GetAllItems()
        {
            return db.Fetch<WeChatMember>(string.Empty);
        }

        public List<WeChatMember> GetListPaging(string sqlWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            string sql = strSql + sqlWhere;

            Page<WeChatMember> page = db.Page<WeChatMember>(currentPage, itemsPerPage, sql, objects);

            totalPages = page.TotalPages;
            totalItems = page.TotalItems;

            return page.Items;
        }
    }
}
