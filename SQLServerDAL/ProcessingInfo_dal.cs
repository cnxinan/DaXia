using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using PetaPoco;

namespace DaXia.SQLServerDAL
{
    public class ProcessingInfo_dal : Base_dal
    {
        private readonly string strSql = "select * from [ProcessingInfo]";

        public List<ProcessingInfo> GetAllItems(string strWhere)
        {
            return db.Fetch<ProcessingInfo>(strWhere);
        }

        public List<ProcessingInfo> GetListPaging(string sqlWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            string sql = strSql + sqlWhere;

            Page<ProcessingInfo> page = db.Page<ProcessingInfo>(currentPage, itemsPerPage, sql, objects);

            totalPages = page.TotalPages;
            totalItems = page.TotalItems;

            return page.Items;
        }

        public ProcessingInfo GetChampionInfo(Guid productId, int processNum)
        {
            string strSql = "select top 1 * from [ProcessingInfo]  where ProductID=@0 and ProcessNum=@1 order by result desc";

            return db.Fetch<ProcessingInfo>(strSql, productId, processNum).FirstOrDefault();
        }

        public ProcessingInfo GetBestRecord(Guid productid, int processNum, string openId)
        {
            string strWhere = " where ProductID=@0 and ProcessNum=@1 and OpenID=@2";

            return db.Fetch<ProcessingInfo>(strWhere, productid, processNum, openId).FirstOrDefault();
        }

        public List<ProcessingInfo> GetRankingList(Guid productid, int processNum)
        {
            string strSql = "select top 10 * from [ProcessingInfo] where ProductID=@0 and ProcessNum=@1";

            return db.Fetch<ProcessingInfo>(strSql, productid, processNum).ToList();
        }

        public int GetBestRank(Guid productid, int processNum, int bestScore)
        {
            string strWhere = " where ProductID=@0 and ProcessNum=@1 and Result>@2 ";

            return db.Fetch<ProcessingInfo>(strWhere, productid, processNum, bestScore).Count;
        }
    }
}
