using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using DaXia.SQLServerDAL;

namespace DaXia.BLL
{
    public class ProcessingInfo_bll
    {
        private readonly ProcessingInfo_dal dal = SQLDALFactory.Instance.ProcessingInfoDal;

        #region CRUD

        public ProcessingInfo GetModel(Guid id)
        {
            return dal.GetModel<ProcessingInfo>(id);
        }

        public bool Insert(ProcessingInfo model)
        {
            return dal.Insert<ProcessingInfo>(model);
        }

        public bool Update(ProcessingInfo model)
        {
            return dal.Update<ProcessingInfo>(model);
        }

        public bool Delete(ProcessingInfo model)
        {
            return dal.Delete<ProcessingInfo>(model);
        }

        public bool Delete(int id)
        {
            return dal.Delete<ProcessingInfo>(id);
        }

        #endregion

        #region public methods
        
        public List<ProcessingInfo> GetListPaging(string strWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            totalPages = 1;
            totalItems = 1;
            return dal.GetListPaging(strWhere, currentPage, itemsPerPage, out totalPages, out totalItems, objects);
        }

        public List<ProcessingInfo> GetAllItems(string strWhere)
        {
            return dal.GetAllItems(strWhere);
        }

        public ProcessingInfo GetChampionInfo(Guid productId, int processNum)
        {
            return dal.GetChampionInfo(productId, processNum);
        }

        public ProcessingInfo GetBestRecord(Guid productid, int processNum, string openId)
        {
            return dal.GetBestRecord(productid, processNum, openId);
        }

        public List<ProcessingInfo> GetRankingList(Guid productid, int processNum)
        {
            return dal.GetRankingList(productid, processNum);
        }

        public int GetBestRank(Guid productid, int processNum, int bestScore)
        {
            return dal.GetBestRank(productid, processNum, bestScore);
        }

        #endregion
    }
}
