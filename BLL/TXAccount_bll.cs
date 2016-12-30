using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using DaXia.SQLServerDAL;

namespace DaXia.BLL
{
    public class TXAccount_bll
    {
        private readonly TXAccount_dal dal = SQLDALFactory.Instance.TXAccountDal;

        #region CRUD

        public TXAccount GetModel(Guid id)
        {
            return dal.GetModel<TXAccount>(id);
        }

        public bool Insert(TXAccount model)
        {
            return dal.Insert<TXAccount>(model);
        }

        public bool Update(TXAccount model)
        {
            return dal.Update<TXAccount>(model);
        }

        public bool Delete(TXAccount model)
        {
            return dal.Delete<TXAccount>(model);
        }

        public bool Delete(int id)
        {
            return dal.Delete<TXAccount>(id);
        }

        #endregion

        #region public methods
        
        public List<TXAccount> GetListPaging(string strWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            totalPages = 1;
            totalItems = 1;
            return dal.GetListPaging(strWhere, currentPage, itemsPerPage, out totalPages, out totalItems, objects);
        }

        public List<TXAccount> GetAllItems(string strWhere)
        {
            return dal.GetAllItems(strWhere);
        }

        public TXAccount GetModelByOpenIdType(string openId,int type)
        {
            return dal.GetModelByOpenIdType(openId, type);
        }

        #endregion
    }
}
