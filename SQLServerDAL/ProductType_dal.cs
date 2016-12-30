using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using PetaPoco;

namespace DaXia.SQLServerDAL
{
    public class ProductType_dal : Base_dal
    {
        private readonly string strSql = "select * from [ProductType]";

        public List<ProductType> GetAllItems()
        {
            return db.Fetch<ProductType>(string.Empty);
        }

        public List<ProductType> GetListPaging(string sqlWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            string sql = strSql + sqlWhere;

            Page<ProductType> page = db.Page<ProductType>(currentPage, itemsPerPage, sql, objects);

            totalPages = page.TotalPages;
            totalItems = page.TotalItems;

            return page.Items;
        }
    }
}
