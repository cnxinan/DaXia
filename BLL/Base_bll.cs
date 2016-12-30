using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DaXia.BLL
{
    public class Base_bll<T> where T class
    {
        public T GetModel(int id)
        {
            return dal.GetModel<T>(id);
        }

        public bool Insert(T model)
        {
            return dal.Insert<T>(model);
        }

        public bool Update(T model)
        {
            return dal.Update<T>(model);
        }

        public bool Delete(T model)
        {
            return dal.Delete<T>(model);
        }

        public bool Delete(int id)
        {
            return dal.Delete<T>(id);
        }

        public List<T> GetListPaging(string strWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            totalPages = 1;
            totalItems = 1;
            return dal.GetListPaging(strWhere, currentPage, itemsPerPage, out totalPages, out totalItems, objects);
        }
    }
}
