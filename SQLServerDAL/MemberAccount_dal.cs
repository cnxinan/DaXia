using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using PetaPoco;

namespace DaXia.SQLServerDAL
{
    public class MemberAccount_dal : Base_dal
    {
        private readonly string strSql = "select * from [MemberAccount]";

        public List<MemberAccount> GetAllItems(string strWhere)
        {
            return db.Fetch<MemberAccount>(strWhere);
        }

        public List<MemberAccount> GetListPaging(string sqlWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            string sql = strSql + sqlWhere;

            Page<MemberAccount> page = db.Page<MemberAccount>(currentPage, itemsPerPage, sql, objects);

            totalPages = page.TotalPages;
            totalItems = page.TotalItems;

            return page.Items;
        }
    }
}
