using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using PetaPoco;

namespace DaXia.SQLServerDAL
{
    public class AddressInfo_dal : Base_dal
    {
        private readonly string strSql = "select * from [AddressInfo]";

        public List<AddressInfo> GetAllItems(string strWhere)
        {
            return db.Fetch<AddressInfo>(strWhere);
        }

        public List<AddressInfo> GetListPaging(string sqlWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            string sql = strSql + sqlWhere;

            Page<AddressInfo> page = db.Page<AddressInfo>(currentPage, itemsPerPage, sql, objects);

            totalPages = page.TotalPages;
            totalItems = page.TotalItems;

            return page.Items;
        }
    }
}
