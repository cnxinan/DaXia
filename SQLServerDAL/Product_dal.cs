using System.Collections.Generic;
using DaXia.EntityDataModels;

namespace DaXia.SQLServerDAL
{
    public class Product_dal : Base_dal
    {
        private readonly string strSql = "select * from [Product]";

        public List<Product> GetAllItems(string sqlWhere)
        {
            return db.Fetch<Product>(sqlWhere);
        }

        public List<Product> GetListPaging(string sqlWhere, long currentPage, long itemsPerPage, out long totalPages,
            out long totalItems, params object[] objects)
        {
            var sql = strSql + sqlWhere;

            var page = db.Page<Product>(currentPage, itemsPerPage, sql, objects);

            totalPages = page.TotalPages;
            totalItems = page.TotalItems;

            return page.Items;
        }
    }
}