using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using DaXia.SQLServerDAL;

namespace DaXia.BLL
{
    public class ShopDetail_bll
    {
        private readonly ShopDetail_dal dal = SQLDALFactory.Instance.ShopDetailDal;

        #region CRUD

        public ShopDetail GetModel(Guid id)
        {
            return dal.GetModel<ShopDetail>(id);
        }

        public bool Insert(ShopDetail model)
        {
            return dal.Insert<ShopDetail>(model);
        }

        public bool Update(ShopDetail model)
        {
            return dal.Update<ShopDetail>(model);
        }

        public bool Delete(ShopDetail model)
        {
            return dal.Delete<ShopDetail>(model);
        }

        public bool Delete(int id)
        {
            return dal.Delete<ShopDetail>(id);
        }

        #endregion

        #region public methods
        
        public List<ShopDetail> GetListPaging(string strWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            totalPages = 1;
            totalItems = 1;
            return dal.GetListPaging(strWhere, currentPage, itemsPerPage, out totalPages, out totalItems, objects);
        }

        public List<ShopDetail> GetAllItems()
        {
            return dal.GetAllItems();
        }

        public ShopDetail GetModelByOpenId(string openId)
        {            
            return dal.GetModelByOpenId(openId);
        }

        public List<ShopDetail> GetLevel1(string openId)
        {
            return dal.GetLevel1(openId);
        }

        public List<ShopDetail> GetLevel2(string openId)
        {
            return dal.GetLevel2(openId);
        }

        public List<ShopDetail> GetLevel3(string openId)
        {
            return dal.GetLevel3(openId);
        }

        #endregion
    }
}
