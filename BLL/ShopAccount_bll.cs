using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using DaXia.SQLServerDAL;

namespace DaXia.BLL
{
    public class ShopAccount_bll
    {
        private readonly ShopAccount_dal dal = SQLDALFactory.Instance.ShopAccountDal;

        #region CRUD

        public ShopAccount GetModel(Guid id)
        {
            return dal.GetModel<ShopAccount>(id);
        }

        public bool Insert(ShopAccount model)
        {
            return dal.Insert<ShopAccount>(model);
        }

        public bool Update(ShopAccount model)
        {
            return dal.Update<ShopAccount>(model);
        }

        public bool Delete(ShopAccount model)
        {
            return dal.Delete<ShopAccount>(model);
        }

        public bool Delete(int id)
        {
            return dal.Delete<ShopAccount>(id);
        }

        #endregion

        #region public methods
        
        public List<ShopAccount> GetListPaging(string strWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            totalPages = 1;
            totalItems = 1;
            return dal.GetListPaging(strWhere, currentPage, itemsPerPage, out totalPages, out totalItems, objects);
        }

        public List<ShopAccount> GetAllItems(string strWhere)
        {
            return dal.GetAllItems(strWhere);
        }

        public ShopAccount GetModelByOpenidAndType(string openId, int type)
        {
            return dal.GetModelByOpenidAndType(openId, type);
        }

        #endregion
    }
}
