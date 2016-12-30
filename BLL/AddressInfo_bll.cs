using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using DaXia.SQLServerDAL;

namespace DaXia.BLL
{
    public class AddressInfo_bll
    {
        private readonly AddressInfo_dal dal = SQLDALFactory.Instance.AddressInfoDal;

        #region CRUD

        public AddressInfo GetModel(Guid id)
        {
            return dal.GetModel<AddressInfo>(id);
        }

        public bool Insert(AddressInfo model)
        {
            return dal.Insert<AddressInfo>(model);
        }

        public bool Update(AddressInfo model)
        {
            return dal.Update<AddressInfo>(model);
        }

        public bool Delete(AddressInfo model)
        {
            return dal.Delete<AddressInfo>(model);
        }

        public bool Delete(Guid id)
        {
            return dal.Delete<AddressInfo>(id);
        }

        #endregion

        #region public methods
        
        public List<AddressInfo> GetListPaging(string strWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            totalPages = 1;
            totalItems = 1;
            return dal.GetListPaging(strWhere, currentPage, itemsPerPage, out totalPages, out totalItems, objects);
        }

        public List<AddressInfo> GetAllItems(string strWhere)
        {
            return dal.GetAllItems(strWhere);
        }

        #endregion
    }
}
