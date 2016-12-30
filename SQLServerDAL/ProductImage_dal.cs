using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using PetaPoco;

namespace DaXia.SQLServerDAL
{
    public class ProductImage_dal : Base_dal
    {
        private readonly string strSql = "select * from [ProductImage]";

        public List<ProductImage> GetAllItems()
        {
            return db.Fetch<ProductImage>(string.Empty);
        }

        public List<ProductImage> GetListPaging(string sqlWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            string sql = strSql + sqlWhere;

            Page<ProductImage> page = db.Page<ProductImage>(currentPage, itemsPerPage, sql, objects);

            totalPages = page.TotalPages;
            totalItems = page.TotalItems;

            return page.Items;
        }
    }
}
