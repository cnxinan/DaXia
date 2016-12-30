using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using DaXia.SQLServerDAL;

namespace DaXia.BLL
{
    public class SystemCtrl_bll
    {
        private readonly SystemCtrl_dal dal = SQLDALFactory.Instance.SystemCtrlDal;

        #region CRUD

        public SystemCtrl GetModel(Guid id)
        {
            return dal.GetModel<SystemCtrl>(id);
        }

        public bool Insert(SystemCtrl model)
        {
            return dal.Insert<SystemCtrl>(model);
        }

        public bool Update(SystemCtrl model)
        {
            return dal.Update<SystemCtrl>(model);
        }

        public bool Delete(SystemCtrl model)
        {
            return dal.Delete<SystemCtrl>(model);
        }

        public bool Delete(int id)
        {
            return dal.Delete<SystemCtrl>(id);
        }

        #endregion

        #region public methods
        
        public List<SystemCtrl> GetListPaging(string strWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            totalPages = 1;
            totalItems = 1;
            return dal.GetListPaging(strWhere, currentPage, itemsPerPage, out totalPages, out totalItems, objects);
        }

        public List<SystemCtrl> GetAllItems()
        {
            return dal.GetAllItems();
        }

        #endregion
    }
}
