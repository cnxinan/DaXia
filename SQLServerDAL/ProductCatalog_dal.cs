using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using PetaPoco;

namespace DaXia.SQLServerDAL
{
    public class ProductCatalog_dal : Base_dal
    {
        private readonly string strSql = "select * from [ProductCatalog]";

        public List<ProductCatalog> GetAllItems(string strWhere)
        {
            return db.Fetch<ProductCatalog>(strWhere);
        }

        public List<ProductCatalog> GetListPaging(string sqlWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            string sql = strSql + sqlWhere;

            Page<ProductCatalog> page = db.Page<ProductCatalog>(currentPage, itemsPerPage, sql, objects);

            totalPages = page.TotalPages;
            totalItems = page.TotalItems;

            return page.Items;
        }
    }
}
