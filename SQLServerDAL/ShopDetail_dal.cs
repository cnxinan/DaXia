using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using PetaPoco;

namespace DaXia.SQLServerDAL
{
    public class ShopDetail_dal : Base_dal
    {
        private readonly string strSql = "select * from [ShopDetails]";

        public List<ShopDetail> GetAllItems()
        {
            return db.Fetch<ShopDetail>(string.Empty);
        }

        public List<ShopDetail> GetListPaging(string sqlWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {
            string sql = strSql + sqlWhere;

            Page<ShopDetail> page = db.Page<ShopDetail>(currentPage, itemsPerPage, sql, objects);

            totalPages = page.TotalPages;
            totalItems = page.TotalItems;

            return page.Items;
        }

        public ShopDetail GetModelByOpenId(string openId)
        {
            string strWhere = string.Format(" where [OpenID]='{0}'",openId);
            return db.Fetch<ShopDetail>(strWhere).FirstOrDefault();
        }

        public List<ShopDetail> GetLevel1(string openId)
        {
            string strWhere = string.Format(" where ParentID = '{0}' ", openId);
            return db.Fetch<ShopDetail>(strWhere);
        }

        public List<ShopDetail> GetLevel2(string openId)
        {
            var level1List = this.GetLevel1(openId);
            List<ShopDetail> returnList = new List<ShopDetail>();
            if (level1List != null)
            {
                level1List.ForEach((p) =>
                {
                    var list = this.GetLevel1(p.OpenID);
                    returnList.AddRange(list);
                });
            }

            return returnList;
        }

        public List<ShopDetail> GetLevel3(string openId)
        {
            var level2List = this.GetLevel2(openId);
            List<ShopDetail> returnList = new List<ShopDetail>();
            if (level2List != null)
            {
                level2List.ForEach((p) =>
                {
                    var list = this.GetLevel1(p.OpenID);
                    returnList.AddRange(list);
                });
            }

            return returnList;
        }
    }
}
