using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using DaXia.SQLServerDAL;

namespace DaXia.BLL
{
    public class User_bll
    {
        private readonly User_dal dal = SQLDALFactory.Instance.UserDal;

        #region CRUD

        //public User GetModel(int id)
        //{
        //    return dal.GetModel<Users>(id);
        //}

        //public bool Insert(Users model)
        //{
        //    return dal.Insert<Users>(model);
        //}

        //public bool Update(Users model)
        //{
        //    return dal.Update<Users>(model);
        //}

        //public bool Delete(Users model)
        //{
        //    return dal.Delete<Users>(model);
        //}

        //public bool Delete(int id)
        //{
        //    return dal.Delete<Users>(id);
        //}

        #endregion

        //public Users GetModel(string username)
        //{
        //    return dal.GetModel<Users>(username);
        //}

        //public Users GetModelByMobile(string mobile)
        //{
        //    return dal.GetModelByMobile(mobile);
        //}
    }
}
