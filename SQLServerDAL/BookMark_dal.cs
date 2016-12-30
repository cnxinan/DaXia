using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using PetaPoco;

namespace DaXia.SQLServerDAL
{
    public class BookMark_dal : Base_dal
    {
        private readonly string strSql = "select * from [BookMark]";

        public List<BookMark> GetAllItems(string strWhere)
        {
            return db.Fetch<BookMark>(strWhere);
        }

        public List<BookMark> GetListPaging(string sqlWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            string sql = strSql + sqlWhere;

            Page<BookMark> page = db.Page<BookMark>(currentPage, itemsPerPage, sql, objects);

            totalPages = page.TotalPages;
            totalItems = page.TotalItems;

            return page.Items;
        }

        public bool GetIsBookMark(string openId, Guid productId)
        {
            string sqlWhere = " where OpenID=@0 and ProductID=@1";
            return db.Fetch<BookMark>(sqlWhere, openId, productId).Count > 0 ? true : false;
        }

        public int GetBookNum(Guid productId)
        {
            string sqlWhere = " where ProductID=@0";
            return db.Fetch<BookMark>(sqlWhere, productId).Count;
        }

        public BookMark GetBookMarkModel(string openId, Guid productId)
        {
            string sqlWhere = " where OpenID=@0 and ProductID=@1";
            return db.Fetch<BookMark>(sqlWhere, openId, productId).FirstOrDefault();
        }
        
    }
}
