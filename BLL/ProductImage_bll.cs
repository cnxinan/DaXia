using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using DaXia.SQLServerDAL;

namespace DaXia.BLL
{
    public class ProductImage_bll
    {
        private readonly ProductImage_dal dal = SQLDALFactory.Instance.ProductImageDal;

        #region CRUD

        public ProductImage GetModel(Guid id)
        {
            return dal.GetModel<ProductImage>(id);
        }

        public bool Insert(ProductImage model)
        {
            return dal.Insert<ProductImage>(model);
        }

        public bool Update(ProductImage model)
        {
            return dal.Update<ProductImage>(model);
        }

        public bool Delete(ProductImage model)
        {
            return dal.Delete<ProductImage>(model);
        }

        public bool Delete(int id)
        {
            return dal.Delete<ProductImage>(id);
        }

        #endregion

        #region public methods
        
        public List<ProductImage> GetListPaging(string strWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            totalPages = 1;
            totalItems = 1;
            return dal.GetListPaging(strWhere, currentPage, itemsPerPage, out totalPages, out totalItems, objects);
        }

        public List<ProductImage> GetAllItems()
        {
            return dal.GetAllItems();
        }

        #endregion
    }
}
