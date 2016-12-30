using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using DaXia.SQLServerDAL;

namespace DaXia.BLL
{
    public class WeChatMember_bll
    {
        private readonly WeChatMember_dal dal = SQLDALFactory.Instance.WeChatMemberDal;

        #region CRUD

        public WeChatMember GetModel(Guid id)
        {
            return dal.GetModel<WeChatMember>(id);
        }

        public bool Insert(WeChatMember model)
        {
            return dal.Insert<WeChatMember>(model);
        }

        public bool Update(WeChatMember model)
        {
            return dal.Update<WeChatMember>(model);
        }

        public bool Delete(WeChatMember model)
        {
            return dal.Delete<WeChatMember>(model);
        }

        public bool Delete(Guid id)
        {
            return dal.Delete<WeChatMember>(id);
        }

        #endregion

        #region public methods
        
        public List<WeChatMember> GetListPaging(string strWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            totalPages = 1;
            totalItems = 1;
            return dal.GetListPaging(strWhere, currentPage, itemsPerPage, out totalPages, out totalItems, objects);
        }

        public List<WeChatMember> GetAllItems()
        {
            return dal.GetAllItems();
        }

        public WeChatMember GetModelByOpenId(string openId)
        {
            return dal.GetModelByOpenId(openId);
        }

        #endregion
    }
}
