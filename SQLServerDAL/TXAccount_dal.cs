using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using PetaPoco;

namespace DaXia.SQLServerDAL
{
    public class TXAccount_dal : Base_dal
    {
        private readonly string strSql = "select * from [TXAccount]";

        public List<TXAccount> GetAllItems(string strWhere)
        {
            return db.Fetch<TXAccount>(strWhere);
        }

        public List<TXAccount> GetListPaging(string sqlWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            string sql = strSql + sqlWhere;

            Page<TXAccount> page = db.Page<TXAccount>(currentPage, itemsPerPage, sql, objects);

            totalPages = page.TotalPages;
            totalItems = page.TotalItems;

            return page.Items;
        }

        public TXAccount GetModelByOpenIdType(string openId, int status)
        {
            string strWhere = " where OpenId=@0 and Status=@1";

            return db.Fetch<TXAccount>(strWhere, openId, status).FirstOrDefault();
        }
    }
}
