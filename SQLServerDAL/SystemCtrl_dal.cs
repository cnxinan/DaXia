using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using PetaPoco;

namespace DaXia.SQLServerDAL
{
    public class SystemCtrl_dal : Base_dal
    {
        private readonly string strSql = "select * from [SystemCtrl]";

        public List<SystemCtrl> GetAllItems()
        {
            return db.Fetch<SystemCtrl>(string.Empty);
        }

        public List<SystemCtrl> GetListPaging(string sqlWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            string sql = strSql + sqlWhere;

            Page<SystemCtrl> page = db.Page<SystemCtrl>(currentPage, itemsPerPage, sql, objects);

            totalPages = page.TotalPages;
            totalItems = page.TotalItems;

            return page.Items;
        }
    }
}
