using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using PetaPoco;

namespace DaXia.SQLServerDAL
{
    public class ShopAccount_dal : Base_dal
    {
        private readonly string strSql = "select * from [ShopAccount]";

        public List<ShopAccount> GetAllItems(string strWhere)
        {
            return db.Fetch<ShopAccount>(strWhere);
        }

        public List<ShopAccount> GetListPaging(string sqlWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            string sql = strSql + sqlWhere;

            Page<ShopAccount> page = db.Page<ShopAccount>(currentPage, itemsPerPage, sql, objects);

            totalPages = page.TotalPages;
            totalItems = page.TotalItems;

            return page.Items;
        }

        public ShopAccount GetModelByOpenidAndType(string openId, int type)
        {
            string sql = " where OpenId=@0 and Type=@1";

            return db.Fetch<ShopAccount>(sql, openId, type).FirstOrDefault();
        }
    }
}
