using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using PetaPoco;

namespace DaXia.SQLServerDAL
{
    public class WeChatQrcode_dal : Base_dal
    {
        private readonly string strSql = "select * from [WeChatQrcode]";

        public List<WeChatQrcode> GetAllItems()
        {
            return db.Fetch<WeChatQrcode>(string.Empty);
        }

        public List<WeChatQrcode> GetListPaging(string sqlWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            string sql = strSql + sqlWhere;

            Page<WeChatQrcode> page = db.Page<WeChatQrcode>(currentPage, itemsPerPage, sql, objects);

            totalPages = page.TotalPages;
            totalItems = page.TotalItems;

            return page.Items;
        }

        public WeChatQrcode GetModelByOpenId(string openId)
        {
            string sql = " where OpenId=@0";
            return db.Fetch<WeChatQrcode>(sql, openId).FirstOrDefault();
        }
    }
}
