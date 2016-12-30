using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using DaXia.SQLServerDAL;

namespace DaXia.BLL
{
    public class ProcessedInfo_bll
    {
        private readonly ProcessedInfo_dal dal = SQLDALFactory.Instance.ProcessedInfoDal;

        #region CRUD

        public ProcessedInfo GetModel(Guid id)
        {
            return dal.GetModel<ProcessedInfo>(id);
        }

        public bool Insert(ProcessedInfo model)
        {
            return dal.Insert<ProcessedInfo>(model);
        }

        public bool Update(ProcessedInfo model)
        {
            return dal.Update<ProcessedInfo>(model);
        }

        public bool Delete(ProcessedInfo model)
        {
            return dal.Delete<ProcessedInfo>(model);
        }

        public bool Delete(int id)
        {
            return dal.Delete<ProcessedInfo>(id);
        }

        #endregion

        #region public methods
        
        public List<ProcessedInfo> GetListPaging(string strWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            totalPages = 1;
            totalItems = 1;
            return dal.GetListPaging(strWhere, currentPage, itemsPerPage, out totalPages, out totalItems, objects);
        }

        public List<ProcessedInfo> GetAllItems(string strWhere)
        {
            return dal.GetAllItems(strWhere);
        }

        /// <summary>
        /// 获奖数
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public int GetRewardTimes(string openId)
        {
            return dal.GetRewardTimes(openId);
        }

        public List<ProcessedInfo> MyOrders(string openId)
        {
            return dal.MyOrders(openId);
        }

        #endregion
    }
}
