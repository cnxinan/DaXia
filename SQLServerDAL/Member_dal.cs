using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DaXia.EntityDataModels;
using PetaPoco;

namespace DaXia.SQLServerDAL
{
    public class Member_dal : Base_dal
    {
        private readonly string strSql = "select * from [Member]";

        public Member GetModel(string userName)
        {
            string sqlWhere = " where UserName=@0";
            return db.Fetch<Member>(sqlWhere, userName).FirstOrDefault();
        }

        public List<Member> GetListPaging(string sqlWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            string sql = strSql + sqlWhere;

            Page<Member> page = db.Page<Member>(currentPage, itemsPerPage, sql, objects);

            totalPages = page.TotalPages;
            totalItems = page.TotalItems;

            return page.Items;
        }
    }
}
