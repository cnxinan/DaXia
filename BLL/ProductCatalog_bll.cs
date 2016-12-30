using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using DaXia.SQLServerDAL;

namespace DaXia.BLL
{
    public class ProductCatalog_bll
    {
        private readonly ProductCatalog_dal dal = SQLDALFactory.Instance.ProductCatalogDal;

        #region CRUD

        public ProductCatalog GetModel(Guid id)
        {
            return dal.GetModel<ProductCatalog>(id);
        }

        public bool Insert(ProductCatalog model)
        {
            return dal.Insert<ProductCatalog>(model);
        }

        public bool Update(ProductCatalog model)
        {
            return dal.Update<ProductCatalog>(model);
        }

        public bool Delete(ProductCatalog model)
        {
            return dal.Delete<ProductCatalog>(model);
        }

        public bool Delete(Guid id)
        {
            return dal.Delete<ProductCatalog>(id);
        }

        #endregion

        #region public methods
        
        public List<ProductCatalog> GetListPaging(string strWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            totalPages = 1;
            totalItems = 1;
            return dal.GetListPaging(strWhere, currentPage, itemsPerPage, out totalPages, out totalItems, objects);
        }

        public List<ProductCatalog> GetAllItems(string strWhere)
        {
            return dal.GetAllItems(strWhere);
        }        

        #endregion
    }
}
