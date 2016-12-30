using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using DaXia.SQLServerDAL;

namespace DaXia.BLL
{
    public class Manager_bll
    {
        private readonly Manager_dal dal = SQLDALFactory.Instance.ManagerDal;

        #region CRUD

        public Manager GetModel(int id)
        {
            return dal.GetModel<Manager>(id);
        }

        public bool Insert(Manager model)
        {
            return dal.Insert<Manager>(model);
        }

        public bool Update(Manager model)
        {
            return dal.Update<Manager>(model);
        }

        public bool Delete(Manager model)
        {
            return dal.Delete<Manager>(model);
        }

        public bool Delete(int id)
        {
            return dal.Delete<Manager>(id);
        }
        
        #endregion

        #region public methods

        public Manager GetModel(string username)
        {
            return dal.GetModel(username);
        }

        public List<Manager> GetAllAdmins()
        {
            return dal.GetAllAdmins();
        }

        public List<Manager> GetListPaging(string strWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            totalPages = 1;
            totalItems = 1;
            return dal.GetListPaging(strWhere, currentPage, itemsPerPage, out totalPages, out totalItems, objects);
        }

        #endregion
    }
}
