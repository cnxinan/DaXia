using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using DaXia.SQLServerDAL;

namespace DaXia.BLL
{
    public class AdInfo_bll
    {
        private readonly AdInfo_dal dal = SQLDALFactory.Instance.AdInfoDal;

        #region CRUD

        public AdInfo GetModel(Guid id)
        {
            return dal.GetModel<AdInfo>(id);
        }

        public bool Insert(AdInfo model)
        {
            return dal.Insert<AdInfo>(model);
        }

        public bool Update(AdInfo model)
        {
            return dal.Update<AdInfo>(model);
        }

        public bool Delete(AdInfo model)
        {
            return dal.Delete<AdInfo>(model);
        }

        public bool Delete(Guid id)
        {
            return dal.Delete<AdInfo>(id);
        }

        #endregion

        #region public methods
        
        public List<AdInfo> GetListPaging(string strWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            totalPages = 1;
            totalItems = 1;
            return dal.GetListPaging(strWhere, currentPage, itemsPerPage, out totalPages, out totalItems, objects);
        }

        public List<AdInfo> GetAllItems()
        {
            return dal.GetAllItems();
        }

        #endregion
    }
}
