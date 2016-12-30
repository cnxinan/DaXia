using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using DaXia.SQLServerDAL;

namespace DaXia.BLL
{
    public class ProductOrder_bll
    {
        private readonly ProductOrder_dal dal = SQLDALFactory.Instance.ProductOrderDal;

        #region CRUD

        public ProductOrder GetModel(Guid id)
        {
            return dal.GetModel<ProductOrder>(id);
        }

        public bool Insert(ProductOrder model)
        {
            return dal.Insert<ProductOrder>(model);
        }

        public bool Update(ProductOrder model)
        {
            return dal.Update<ProductOrder>(model);
        }

        public bool Delete(ProductOrder model)
        {
            return dal.Delete<ProductOrder>(model);
        }

        public bool Delete(Guid id)
        {
            return dal.Delete<ProductOrder>(id);
        }

        #endregion

        #region public methods
        
        public List<ProductOrder> GetListPaging(string strWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            totalPages = 1;
            totalItems = 1;
            return dal.GetListPaging(strWhere, currentPage, itemsPerPage, out totalPages, out totalItems, objects);
        }

        public List<ProductOrder> GetAllItems(string strWhere)
        {
            return dal.GetAllItems(strWhere);
        }

        public ProductOrder GetModelByOrderNo(string orderNo)
        {
            return dal.GetModelByOrderNo(orderNo);
        }

        #endregion
    }
}
