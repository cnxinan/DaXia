using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DaXia.EntityDataModels;
using DaXia.BLL;

namespace DaXia.WebComAdmin.lib
{
    public class Fenxiao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="price"></param>
        /// <param name="type">0:商品分销 1:加盟提成</param>
        /// <returns></returns>
        public static bool FX(string openId, decimal price, int type = 0)
        {
            ShopDetail_bll shopDetailBll = BLLFactory.Instance.ShopDetailBll;
            if (price <= 0)
            {
                return false;
            }

            ShopDetail currentUser = shopDetailBll.GetModelByOpenId(openId);
            if (currentUser != null && !string.IsNullOrWhiteSpace(currentUser.ParentID) && currentUser.ParentID != "无")
            {
                JSJE(currentUser.ParentID, openId, price, 1, type);

                var level1User = shopDetailBll.GetModelByOpenId(currentUser.ParentID);
                if (level1User != null && !string.IsNullOrWhiteSpace(level1User.ParentID) && level1User.ParentID != "无")
                {
                    JSJE(level1User.ParentID, openId, price, 2, type);

                    var level2User = shopDetailBll.GetModelByOpenId(level1User.ParentID);
                    if (level2User != null && !string.IsNullOrWhiteSpace(level2User.ParentID) && level2User.ParentID != "无")
                    {
                        JSJE(level2User.ParentID, openId, price, 3, type);

                        var level3User = shopDetailBll.GetModelByOpenId(level2User.ParentID);
                        if (level3User != null && !string.IsNullOrWhiteSpace(level3User.ParentID) && level3User.ParentID != "无")
                        {
                            JSJE(level3User.ParentID, openId, price, 3, type);
                        }
                    }
                }
            }

            return true;
        }

        private static void JSJE(string openId, string fromOpenId, decimal price, int level,int type)
        {
            ShopAccount_bll shopAccountBll = BLLFactory.Instance.ShopAccountBll;
            WeChatMember_bll memberBll = BLLFactory.Instance.WeChatMemberBll;

            string note = "";
            decimal fxAmount = 0M;
            if (type != 0)
            {
                switch (level)
                {
                    case 1:
                        note = "一级奖励";
                        fxAmount = price * 0.07M;
                        break;
                    case 2:
                        note = "二级奖励";
                        fxAmount = price * 0.05M;
                        break;
                    case 3:
                        note = "三级奖励";
                        fxAmount = price * 0.02M;
                        break;
                }
            }
            else
            {
                switch (level)
                {
                    case 1:
                        note = "一级奖励";
                        fxAmount = price * 0.03M;
                        break;
                    case 2:
                        note = "二级奖励";
                        fxAmount = price * 0.02M;
                        break;
                    case 3:
                        note = "三级奖励";
                        fxAmount = price * 0.01M;
                        break;
                }
            }


            //if (type == 0)
            //{
            //    fxAmount /= fxAmount;
            //}

            ShopAccount entity = new ShopAccount()
            {
                ID = Guid.NewGuid(),
                Amount = fxAmount,
                FromOpenId = fromOpenId,
                OpenId = openId,
                OriginalPrice = fxAmount,
                Note = note,
                Type = level,
                Status = 1,
                CreationTime = DateTime.Now
            };
            shopAccountBll.Insert(entity);

            WeChatMember memberEntity = memberBll.GetModelByOpenId(openId);
            if(memberEntity != null)
            {
                memberEntity.Balance += fxAmount;
                memberBll.Update(memberEntity);
            }
        }

    }
}