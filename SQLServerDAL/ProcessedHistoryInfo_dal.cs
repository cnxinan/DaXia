using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using PetaPoco;

namespace DaXia.SQLServerDAL
{
    public class ProcessedHistoryInfo_dal : Base_dal
    {
        private readonly string strSql = "select * from [ProcessedHistoryInfo]";

        public List<ProcessedHistoryInfo> GetAllItems(string strWhere)
        {
            return db.Fetch<ProcessedHistoryInfo>(strWhere);
        }

        public List<ProcessedHistoryInfo> GetListPaging(string sqlWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            string sql = strSql + sqlWhere;

            Page<ProcessedHistoryInfo> page = db.Page<ProcessedHistoryInfo>(currentPage, itemsPerPage, sql, objects);

            totalPages = page.TotalPages;
            totalItems = page.TotalItems;

            return page.Items;
        }

        public int GetActedNumberByProduct(Guid productId,int processNum)
        {
            string sqlWhere = " where ProductID=@0 and ProcessNum=@1";

            return db.Fetch<ProcessedHistoryInfo>(sqlWhere, productId, processNum).Count;
        }

        public int GetActedNumOfMember(Guid productId, int processNum, string openId)
        {
            string sqlWhere = "where ProductID=@0 and ProcessNum=@1 and OpenID=@2";

            return db.Fetch<ProcessedHistoryInfo>(sqlWhere, productId, processNum, openId).Count;
        }
    }
}
