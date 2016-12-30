using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DaXia.EntityDataModels;
using DaXia.SQLServerDAL;

namespace DaXia.BLL
{
    public class Member_bll
    {
        private readonly Member_dal dal = SQLDALFactory.Instance.MemberDal;

        #region CRUD

        public Member GetModel(int id)
        {
            return dal.GetModel<Member>(id);
        }

        public bool Insert(Member model)
        {
            return dal.Insert<Member>(model);
        }

        public bool Update(Member model)
        {
            return dal.Update<Member>(model);
        }

        public bool Delete(Member model)
        {
            return dal.Delete<Member>(model);
        }

        public bool Delete(int id)
        {
            return dal.Delete<Member>(id);
        }

        #endregion

        #region public method

        public Member GetModel(string userName)
        {
            return dal.GetModel(userName);
        }

        public List<Member> GetListPaging(string strWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            totalPages = 1;
            totalItems = 1;
            return dal.GetListPaging(strWhere, currentPage, itemsPerPage, out totalPages, out totalItems, objects);
        }

        #endregion
    }
}
