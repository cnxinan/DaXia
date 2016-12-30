using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using DaXia.SQLServerDAL;

namespace DaXia.BLL
{
    public class Log_bll
    {
        private readonly Log_dal dal = SQLDALFactory.Instance.LogDal;

        #region CRUD

        public Log GetModel(Guid id)
        {
            return dal.GetModel<Log>(id);
        }

        public bool Insert(Log model)
        {
            return dal.Insert<Log>(model);
        }

        public bool Update(Log model)
        {
            return dal.Update<Log>(model);
        }

        public bool Delete(Log model)
        {
            return dal.Delete<Log>(model);
        }

        public bool Delete(int id)
        {
            return dal.Delete<Log>(id);
        }

        #endregion

        #region public methods
        
        public List<Log> GetListPaging(string strWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            totalPages = 1;
            totalItems = 1;
            return dal.GetListPaging(strWhere, currentPage, itemsPerPage, out totalPages, out totalItems, objects);
        }

        public List<Log> GetAllItems()
        {
            return dal.GetAllItems();
        }

        #endregion
    }
}
