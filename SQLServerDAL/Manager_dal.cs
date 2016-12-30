using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using PetaPoco;

namespace DaXia.SQLServerDAL
{
    public class Manager_dal : Base_dal
    {
        private readonly string strSql = "select * from [Manager]";

        public Manager GetModel(string username)
        {
            string  sqlWhere = " where UserName=@0";
            return db.Fetch<Manager>(sqlWhere, username).FirstOrDefault();
        }

        public List<Manager> GetAllAdmins()
        {
            return db.Fetch<Manager>(string.Empty);
        }

        public List<Manager> GetListPaging(string sqlWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            string sql = strSql + sqlWhere;

            Page<Manager> page = db.Page<Manager>(currentPage, itemsPerPage, sql, objects);

            totalPages = page.TotalPages;
            totalItems = page.TotalItems;

            return page.Items;
        }
    }
}
