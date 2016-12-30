using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using DaXia.SQLServerDAL;

namespace DaXia.BLL
{
    public class ProcessedHistoryInfo_bll
    {
        private readonly ProcessedHistoryInfo_dal dal = SQLDALFactory.Instance.ProcessedHistoryInfoDal;

        #region CRUD

        public ProcessedHistoryInfo GetModel(Guid id)
        {
            return dal.GetModel<ProcessedHistoryInfo>(id);
        }

        public bool Insert(ProcessedHistoryInfo model)
        {
            return dal.Insert<ProcessedHistoryInfo>(model);
        }

        public bool Update(ProcessedHistoryInfo model)
        {
            return dal.Update<ProcessedHistoryInfo>(model);
        }

        public bool Delete(ProcessedHistoryInfo model)
        {
            return dal.Delete<ProcessedHistoryInfo>(model);
        }

        public bool Delete(int id)
        {
            return dal.Delete<ProcessedHistoryInfo>(id);
        }

        #endregion

        #region public methods
        
        public List<ProcessedHistoryInfo> GetListPaging(string strWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            totalPages = 1;
            totalItems = 1;
            return dal.GetListPaging(strWhere, currentPage, itemsPerPage, out totalPages, out totalItems, objects);
        }

        public List<ProcessedHistoryInfo> GetAllItems(string strWhere)
        {
            return dal.GetAllItems(strWhere);
        }

        public int GetActedNumberByProduct(Guid productId, int processNum)
        {
            return dal.GetActedNumberByProduct(productId, processNum);
        }

        public int GetActedNumOfMember(Guid productId, int processNum, string openId)
        {
            return dal.GetActedNumOfMember(productId,processNum, openId);
        }

        #endregion
    }
}
