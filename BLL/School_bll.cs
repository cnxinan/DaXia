using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using DaXia.SQLServerDAL;

namespace DaXia.BLL
{
    public class School_bll
    {
        private readonly School_dal dal = SQLDALFactory.Instance.SchoolDal;

        #region CRUD

        public School GetModel(int id)
        {
            return dal.GetModel<School>(id);
        }

        public bool Insert(School model)
        {
            return dal.Insert<School>(model);
        }

        public bool Update(School model)
        {
            return dal.Update<School>(model);
        }

        public bool Delete(School model)
        {
            return dal.Delete<School>(model);
        }

        public bool Delete(int id)
        {
            return dal.Delete<School>(id);
        }

        #endregion

        #region public methods

        public School GetModel(string name)
        {
            return dal.GetModel(name);
        }

        public List<School> GetListPaging(string strWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            totalPages = 1;
            totalItems = 1;
            return dal.GetListPaging(strWhere, currentPage, itemsPerPage, out totalPages, out totalItems, objects);
        }

        public List<School> GetAllItems()
        {
            return dal.GetAllItems();
        }

        #endregion
    }
}
