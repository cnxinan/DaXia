using System;
using System.Collections.Generic;
using DaXia.EntityDataModels;
using DaXia.SQLServerDAL;

namespace DaXia.BLL
{
    public class Product_bll
    {
        private readonly Product_dal dal = SQLDALFactory.Instance.ProductDal;

        #region CRUD

        public Product GetModel(Guid id)
        {
            return dal.GetModel<Product>(id);
        }

        public bool Insert(Product model)
        {
            return dal.Insert(model);
        }

        public bool Update(Product model)
        {
            return dal.Update(model);
        }

        public bool Delete(Product model)
        {
            return dal.Delete(model);
        }

        public bool Delete(Guid id)
        {
            return dal.Delete<Product>(id);
        }

        #endregion

        #region public methods

        public List<Product> GetListPaging(string strWhere, long currentPage, long itemsPerPage, out long totalPages,
            out long totalItems, params object[] objects)
        {
            totalPages = 1;
            totalItems = 1;
            return dal.GetListPaging(strWhere, currentPage, itemsPerPage, out totalPages, out totalItems, objects);
        }

        public List<Product> GetAllItems(string sqlWhere)
        {
            return dal.GetAllItems(sqlWhere);
        }

        #endregion
    }
}