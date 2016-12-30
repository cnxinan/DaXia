using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using PetaPoco;

namespace DaXia.SQLServerDAL
{
    public class ProcessedInfo_dal : Base_dal
    {
        private readonly string strSql = "select * from [ProcessedInfo]";

        public List<ProcessedInfo> GetAllItems(string strWhere)
        {
            return db.Fetch<ProcessedInfo>(strWhere);
        }

        public List<ProcessedInfo> GetListPaging(string sqlWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            string sql = strSql + sqlWhere;

            Page<ProcessedInfo> page = db.Page<ProcessedInfo>(currentPage, itemsPerPage, sql, objects);

            totalPages = page.TotalPages;
            totalItems = page.TotalItems;

            return page.Items;
        }

        public int GetRewardTimes(string openId)
        {
            string strWhere = " where OpenID=@0";

            return db.Fetch<ProcessedInfo>(strWhere, openId).Count;
        }

        public List<ProcessedInfo> MyOrders(string openId) 
        {
            string strWhere = " where ProductID in (select ID from Product where ShopID in (select ID from ShopDetails where OpenID=@0))";

            return db.Fetch<ProcessedInfo>(strWhere, openId) ;
        }        
    }
}
