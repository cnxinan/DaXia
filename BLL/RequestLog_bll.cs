using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DaXia.EntityDataModels;
using DaXia.SQLServerDAL;

namespace DaXia.BLL
{
    public class RequestLog_bll
    {
        private readonly RequestLog_dal dal = SQLDALFactory.Instance.RequestLogDal;

        #region CRUD

        public RequestLog GetModel(Guid id)
        {
            return dal.GetModel<RequestLog>(id);
        }

        public bool Insert(RequestLog model)
        {
            return dal.Insert<RequestLog>(model);
        }

        public bool Update(RequestLog model)
        {
            return dal.Update<RequestLog>(model);
        }

        public bool Delete(RequestLog model)
        {
            return dal.Delete<RequestLog>(model);
        }

        public bool Delete(Guid id)
        {
            return dal.Delete<RequestLog>(id);
        }

        #endregion

    }
}
