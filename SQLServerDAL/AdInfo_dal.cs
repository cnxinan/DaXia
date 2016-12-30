using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using PetaPoco;

namespace DaXia.SQLServerDAL
{
    public class AdInfo_dal : Base_dal
    {
        private readonly string strSql = "select * from [AdInfo]";

        public List<AdInfo> GetAllItems()
        {
            return db.Fetch<AdInfo>(string.Empty);
        }

        public List<AdInfo> GetListPaging(string sqlWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            string sql = strSql + sqlWhere;

            Page<AdInfo> page = db.Page<AdInfo>(currentPage, itemsPerPage, sql, objects);

            totalPages = page.TotalPages;
            totalItems = page.TotalItems;

            return page.Items;
        }
    }
}
