using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using DaXia.SQLServerDAL;

namespace DaXia.BLL
{
    public class MemberAccount_bll
    {
        private readonly MemberAccount_dal dal = SQLDALFactory.Instance.MemberAccountDal;

        #region CRUD

        public MemberAccount GetModel(Guid id)
        {
            return dal.GetModel<MemberAccount>(id);
        }

        public bool Insert(MemberAccount model)
        {
            return dal.Insert<MemberAccount>(model);
        }

        public bool Update(MemberAccount model)
        {
            return dal.Update<MemberAccount>(model);
        }

        public bool Delete(MemberAccount model)
        {
            return dal.Delete<MemberAccount>(model);
        }

        public bool Delete(Guid id)
        {
            return dal.Delete<MemberAccount>(id);
        }

        #endregion

        #region public methods
        
        public List<MemberAccount> GetListPaging(string strWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            totalPages = 1;
            totalItems = 1;
            return dal.GetListPaging(strWhere, currentPage, itemsPerPage, out totalPages, out totalItems, objects);
        }

        public List<MemberAccount> GetAllItems(string strWhere)
        {
            return dal.GetAllItems(strWhere);
        }      

        #endregion
    }
}
