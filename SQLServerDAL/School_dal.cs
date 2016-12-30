using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DaXia.EntityDataModels;
using PetaPoco;

namespace DaXia.SQLServerDAL
{
    public class School_dal : Base_dal
    {
        private readonly string strSql = "select * from [School]";

        public List<School> GetAllItems()
        {
            return db.Fetch<School>(string.Empty);
        }

        public List<School> GetListPaging(string sqlWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            string sql = strSql + sqlWhere;

            Page<School> page = db.Page<School>(currentPage, itemsPerPage, sql, objects);

            totalPages = page.TotalPages;
            totalItems = page.TotalItems;

            return page.Items;
        }

        public School GetModel(string name)
        {
            string sqlWhere = " where [Name]=@0";
            return db.Fetch<School>(sqlWhere, name).FirstOrDefault();
        }
    }
}
