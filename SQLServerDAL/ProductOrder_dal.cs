using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using PetaPoco;

namespace DaXia.SQLServerDAL
{
    public class ProductOrder_dal : Base_dal
    {
        private readonly string strSql = "select * from [ProductOrder]";
              
        public List<ProductOrder> GetAllItems(string strWhere)
        {
            return db.Fetch<ProductOrder>(strWhere);
        }

        public List<ProductOrder> GetListPaging(string sqlWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            string sql = strSql + sqlWhere;

            Page<ProductOrder> page = db.Page<ProductOrder>(currentPage, itemsPerPage, sql, objects);

            totalPages = page.TotalPages;
            totalItems = page.TotalItems;

            return page.Items;
        }

        public ProductOrder GetModelByOrderNo(string orderNo)
        {
            string strWhere = " where OrderNo=@0";
            return db.Fetch<ProductOrder>(strWhere, orderNo).FirstOrDefault();
        }
    }
}
