using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using DaXia.SQLServerDAL;

namespace DaXia.BLL
{
    public class BookMark_bll
    {
        private readonly BookMark_dal dal = SQLDALFactory.Instance.BookMarkDal;

        #region CRUD

        public BookMark GetModel(Guid id)
        {
            return dal.GetModel<BookMark>(id);
        }

        public bool Insert(BookMark model)
        {
            return dal.Insert<BookMark>(model);
        }

        public bool Update(BookMark model)
        {
            return dal.Update<BookMark>(model);
        }

        public bool Delete(BookMark model)
        {
            return dal.Delete<BookMark>(model);
        }

        public bool Delete(Guid id)
        {
            return dal.Delete<BookMark>(id);
        }

        #endregion

        #region public methods
        
        public List<BookMark> GetListPaging(string strWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            totalPages = 1;
            totalItems = 1;
            return dal.GetListPaging(strWhere, currentPage, itemsPerPage, out totalPages, out totalItems, objects);
        }

        public List<BookMark> GetAllItems(string strWhere)
        {
            return dal.GetAllItems(strWhere);
        }

        public bool GetIsBookMark(string openId, Guid productId)
        {
            return dal.GetIsBookMark(openId, productId);
        }

        public int GetBookNum(Guid productId)
        {
            return dal.GetBookNum(productId);
        }

        public BookMark GetBookMarkModel(string openId, Guid productId)
        {
            return dal.GetBookMarkModel(openId, productId);
        }
        
        #endregion
    }
}
