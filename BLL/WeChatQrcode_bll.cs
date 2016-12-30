using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using DaXia.SQLServerDAL;

namespace DaXia.BLL
{
    public class WeChatQrcode_bll
    {
        private readonly WeChatQrcode_dal dal = SQLDALFactory.Instance.WeChatQrcodeDal;

        #region CRUD

        public WeChatQrcode GetModel(Guid id)
        {
            return dal.GetModel<WeChatQrcode>(id);
        }

        public bool Insert(WeChatQrcode model)
        {
            return dal.Insert<WeChatQrcode>(model);
        }

        public bool Update(WeChatQrcode model)
        {
            return dal.Update<WeChatQrcode>(model);
        }

        public bool Delete(WeChatQrcode model)
        {
            return dal.Delete<WeChatQrcode>(model);
        }

        public bool Delete(Guid id)
        {
            return dal.Delete<WeChatQrcode>(id);
        }

        #endregion

        #region public methods
        
        public List<WeChatQrcode> GetListPaging(string strWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            totalPages = 1;
            totalItems = 1;
            return dal.GetListPaging(strWhere, currentPage, itemsPerPage, out totalPages, out totalItems, objects);
        }

        public List<WeChatQrcode> GetAllItems()
        {
            return dal.GetAllItems();
        }

        public WeChatQrcode GetModelByOpenId(string openId)
        {
            return dal.GetModelByOpenId(openId);
        }

        #endregion
    }
}
