using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DaXia.EntityDataModels;
using DaXia.Common;
using PetaPoco;

namespace DaXia.SQLServerDAL
{
    public class Base_dal
    {
        //public readonly ZXXTDB db = ZXXTDB.GetInstance();
        public readonly ZXXTDB db = new ZXXTDB();

        #region CRUD

        /// <summary>
        /// 根据ID获取单个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetModel<T>(object id)
        {
            try
            {
                return db.SingleOrDefault<T>(id);
            }
            catch(Exception ex)
            {
                string msg = string.Format(DateTime.Now.ToString() + "," + ",获取实体错误,错误信息:" + ex.Message);
                FileLogHelper.LogToCSVFile(msg);
            }

            return default(T);
        }

        /// <summary>
        /// 添加一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Insert<T>(T model)
        {
            try
            {
                db.Insert(model);
                return true;
            }
            catch (Exception ex)
            {
                //这里应该把异常记录到日志文件里，文件格式用
                string msg = string.Format(DateTime.Now.ToString() + "," + model.GetType().ToString() + ",插入数据库错误,错误信息:" + ex.Message);
                FileLogHelper.LogToCSVFile(msg);
            }

            return false;  
        }

        /// <summary>
        /// 更新一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update<T>(T model)
        {
            try
            {
                int rows = db.Update(model);
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format(DateTime.Now.ToString() + "," + model.GetType().ToString() + ",更新数据错误,错误信息:" + ex.Message);
                FileLogHelper.LogToCSVFile(msg);
            }

            return false; 
        }

        /// <summary>
        /// 根据传入对象删除一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete<T>(T model)
        {
            try
            {
                int rows = db.Delete<T>(model);
                if (rows > 0)
                {
                    return true;
                }
            }
            catch(Exception ex)
            {
                string msg = string.Format(DateTime.Now.ToString() + "," + model.GetType().ToString() + ",删除数据错误,错误信息:" + ex.Message);
                FileLogHelper.LogToCSVFile(msg);
            }

            return false;
        }

        /// <summary>
        /// 传入主键，删除一个对象 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pocoOrPrimaryKey"></param>
        /// <returns></returns>
        public bool Delete<T>(object pocoOrPrimaryKey)
        {
            try
            {
                int rows = db.Delete<T>(pocoOrPrimaryKey);
                if (rows > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format(DateTime.Now.ToString() + "," + pocoOrPrimaryKey.ToString() + ",删除数据错误,错误信息:" + ex.Message);
                FileLogHelper.LogToCSVFile(msg);
            }

            return false;
        }

        #endregion                

        #region Page

        public List<T> GetAllItems<T>(string sqlWhere)        
        {
            return db.Fetch<T>(sqlWhere);
        }

        public List<T> Pages<T>(string strWhere, long currentPage, long itemsPerPage, out long totalPages, out long totalItems, params object[] objects)
        {

            var page = db.Page<T>(currentPage, itemsPerPage, strWhere, objects);

            totalPages = page.TotalPages;
            totalItems = page.TotalItems;

            return page.Items;
        }

        #endregion               
    }
}
