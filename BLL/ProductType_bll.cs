using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using DaXia.SQLServerDAL;

namespace DaXia.BLL
{
    public class ProductType_bll
    {
        private readonly ProductType_dal dal = SQLDALFactory.Instance.ProductTypeDal;

        #region CRUD

        public ProductType GetModel(Guid id)
        {
            return dal.GetModel<ProductType>(id);
        }

        public bool Insert(ProductType model)
        {
            return dal.Insert<ProductType>(model);
        }

        public bool Update(ProductType model)
        {
            return dal.Update<ProductType>(model);
        }

        public bool Delete(ProductType model)
        {
            return dal.Delete<ProductType>(model);
        }

        public bool Delete(int id)
        {
            return dal.Delete<ProductType>(id);
        }

        #endregion

        #region public methods
        
        public List<ProductType> GetListPaging(string strWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            totalPages = 1;
            totalItems = 1;
            return dal.GetListPaging(strWhere, currentPage, itemsPerPage, out totalPages, out totalItems, objects);
        }

        public List<ProductType> GetAllItems()
        {
            return dal.GetAllItems();
        }

        #endregion
    }
}
