using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web;
using System.Data;
using System.Web.UI;

namespace YBWH.YJYL.Common
{
    public class Pager
    {

        /// <summary>
        ///  Repeter分页类
        /// </summary>
        /// <param name="ds">daset</param>
        /// <param name="datalistname">repter名称</param>
        /// <param name="pagesize">页数</param>
        /// <param name="param">参数，无参数请传null</param>
        /// <returns></returns>
        public static string GetPageNum(DataSet ds, Repeater rptName, int pagesize, string[] param, string url)
        {
            PagedDataSource objPds = new PagedDataSource();
            objPds.DataSource = ds.Tables[0].DefaultView;
            objPds.AllowPaging = true;
            int total = ds.Tables[0].Rows.Count;
            objPds.PageSize = pagesize;
            int page;
            if (HttpContext.Current.Request.QueryString["page"] != null)
                page = Convert.ToInt32(HttpContext.Current.Request.QueryString["page"]);
            else
                page = 1;
            objPds.CurrentPageIndex = page - 1;
            rptName.DataSource = objPds;
            rptName.DataBind();
            int allpage = 0;
            int next = 0;
            int pre = 0;
            int startcount = 0;
            int endcount = 0;
            string pagestr = "";
            string RawUrl = HttpContext.Current.Request.RawUrl;
            string parameters = "";
            if (param != null)
            {
                foreach (string par in param)
                {
                    parameters = HttpContext.Current.Request.QueryString[par];
                }

            }
            else
            {
                parameters = "";
            }
            if (page < 1) { page = 1; }
            //计算总页数 
            if (pagesize != 0)
            {
                allpage = (total / pagesize);
                allpage = ((total % pagesize) != 0 ? allpage + 1 : allpage);
                allpage = (allpage == 0 ? 1 : allpage);
            }
            next = page + 1;
            pre = page - 1;
            startcount = (page + 5) > allpage ? allpage - 9 : page - 4;//中间页起始序号 
            //中间页终止序号 
            endcount = page < 5 ? 10 : page + 5;
            if (startcount < 1) { startcount = 1; } //为了避免输出的时候产生负数，设置如果小于1就从序号1开始 
            if (allpage < endcount) { endcount = allpage; } //页码+5的可能性就会产生最终输出序号大于总页码，那么就要将其控制在页码数之内 
            pagestr = "共" + allpage + "页";
            if (RawUrl.Contains(".html"))
            {
                pagestr += page > 1 ? "<a href=\"" + url + parameters + "-1.html" + "\">首页</a>&nbsp;&nbsp;<a href=\"" + url + parameters + "-" + pre + ".html\">上一页</a>" : "<span class='none'>首页</span> <span class='none'>上一页</span>";
                //中间页处理，这个增加时间复杂度，减小空间复杂度 
                for (int i = startcount; i <= endcount; i++)
                {
                    pagestr += page == i ? "&nbsp;&nbsp;<span class='now'>" + i + "</span>" : "&nbsp;&nbsp;<a href=\"" + url + parameters + "-" + i + ".html\">" + i + "</a>";
                }
                pagestr += page != allpage ? "&nbsp;&nbsp;<a href=\"" + url + parameters + "-" + next + ".html\">下一页</a>&nbsp;&nbsp;<a href=\"" + url + parameters + "-" + allpage + ".html\">末页</a>" : " <span class='none'>下一页</span> <span class='none'>末页</span>";
            }
            else
            {
                pagestr += page > 1 ? "<a href=\"" + url + "\">首页</a>&nbsp;&nbsp;<a href=\"" + url + pre + "\">上一页</a>" : "<span class='none'>首页</span> <span class='none'>上一页</span>";
                //中间页处理，这个增加时间复杂度，减小空间复杂度 
                for (int i = startcount; i <= endcount; i++)
                {
                    pagestr += page == i ? "&nbsp;&nbsp;<span class='now'>" + i + "</span>" : "&nbsp;&nbsp;<a href=\"" + url + i + "\">" + i + "</a>";
                }
                pagestr += page != allpage ? "&nbsp;&nbsp;<a href=\"" + url + next + "\">下一页</a>&nbsp;&nbsp;<a href=\"" + url + allpage + "\">末页</a>" : " <span class='none'>下一页</span> <span class='none'>末页</span>";
            }
            return pagestr;
        }
        /// <summary>
        ///  Repeter分页类
        /// </summary>
        /// <param name="ds">daset</param>
        /// <param name="datalistname">repter名称</param>
        /// <param name="pagesize">页数</param>
        /// <param name="param">参数，无参数请传null</param>
        /// <returns></returns>
        public static string GetPageNum(DataSet ds, Repeater rptName, int pagesize, string[] param)
        {
            PagedDataSource objPds = new PagedDataSource();
            objPds.DataSource = ds.Tables[0].DefaultView;
            objPds.AllowPaging = true;
            int total = ds.Tables[0].Rows.Count;
            objPds.PageSize = pagesize;
            int page;
            if (HttpContext.Current.Request.QueryString["page"] != null)
                page = Convert.ToInt32(HttpContext.Current.Request.QueryString["page"]);
            else
                page = 1;
            objPds.CurrentPageIndex = page - 1;
            rptName.DataSource = objPds;
            rptName.DataBind();
            int allpage = 0;
            int next = 0;
            int pre = 0;
            int startcount = 0;
            int endcount = 0;
            string pagestr = "";
            string url = HttpContext.Current.Request.CurrentExecutionFilePath;
            string parameters = "";
            if (param != null)
            {
                foreach (string par in param)
                {
                    parameters += par + "=" + HttpContext.Current.Server.UrlEncode(HttpContext.Current.Request.QueryString[par]) + "&";
                }
                parameters = "?" + parameters;
            }
            else
            {
                parameters = "?";
            }
            if (page < 1) { page = 1; }
            //计算总页数 
            if (pagesize != 0)
            {
                allpage = (total / pagesize);
                allpage = ((total % pagesize) != 0 ? allpage + 1 : allpage);
                allpage = (allpage == 0 ? 1 : allpage);
            }
            next = page + 1;
            pre = page - 1;
            startcount = (page + 5) > allpage ? allpage - 9 : page - 4;//中间页起始序号 
            //中间页终止序号 
            endcount = page < 5 ? 10 : page + 5;
            if (startcount < 1) { startcount = 1; } //为了避免输出的时候产生负数，设置如果小于1就从序号1开始 
            if (allpage < endcount) { endcount = allpage; } //页码+5的可能性就会产生最终输出序号大于总页码，那么就要将其控制在页码数之内 
            //pagestr = "共" + allpage + "页";
            pagestr += page > 1 ? "<a href=\"" + url + parameters + "page=1" + "\">首页</a>&nbsp;&nbsp;<a href=\"" + url + parameters + "page=" + pre + "\">上一页</a>" : "<span class='none'>首页</span> <span class='none'>上一页</span>";
            //中间页处理，这个增加时间复杂度，减小空间复杂度 
            for (int i = startcount; i <= endcount; i++)
            {
                pagestr += page == i ? "&nbsp;&nbsp;<span class='now'>" + i + "</span>" : "&nbsp;&nbsp;<a href=\"" + url + parameters + "page=" + i + "\">" + i + "</a>";
            }
            pagestr += page != allpage ? "&nbsp;&nbsp;<a href=\"" + url + parameters + "page=" + next + "\">下一页</a>&nbsp;&nbsp;<a href=\"" + url + parameters + "page=" + allpage + "\">末页</a>" : " <span class='none'>下一页</span> <span class='none'>末页</span>";
            return pagestr;
        }
        /// <summary>
        ///  Repeter分页类
        /// </summary>
        /// <param name="ds">daset</param>
        /// <param name="datalistname">repter名称</param>
        /// <param name="pagesize">页数</param>
        /// <param name="param">参数，无参数请传null</param>
        /// <returns></returns>
        public static string GetPageNum<T>(List<T> t, Repeater rptName, int pagesize, string[] param, string url)
        {
            PagedDataSource objPds = new PagedDataSource();
            objPds.DataSource = t;
            objPds.AllowPaging = true;
            int total = t.Count;
            objPds.PageSize = pagesize;
            int page;
            if (HttpContext.Current.Request.QueryString["page"] != null)
                page = Convert.ToInt32(HttpContext.Current.Request.QueryString["page"]);
            else
                page = 1;
            objPds.CurrentPageIndex = page - 1;
            rptName.DataSource = objPds;
            rptName.DataBind();
            int allpage = 0;
            int next = 0;
            int pre = 0;
            int startcount = 0;
            int endcount = 0;
            string pagestr = "";
            string RawUrl = HttpContext.Current.Request.RawUrl;
            string parameters = "";
            if (param != null)
            {
                foreach (string par in param)
                {
                    parameters = HttpContext.Current.Request.QueryString[par];
                }

            }
            else
            {
                parameters = "";
            }
            if (page < 1) { page = 1; }
            //计算总页数 
            if (pagesize != 0)
            {
                allpage = (total / pagesize);
                allpage = ((total % pagesize) != 0 ? allpage + 1 : allpage);
                allpage = (allpage == 0 ? 1 : allpage);
            }
            next = page + 1;
            pre = page - 1;
            startcount = (page + 5) > allpage ? allpage - 9 : page - 4;//中间页起始序号 
            //中间页终止序号 
            endcount = page < 5 ? 10 : page + 5;
            if (startcount < 1) { startcount = 1; } //为了避免输出的时候产生负数，设置如果小于1就从序号1开始 
            if (allpage < endcount) { endcount = allpage; } //页码+5的可能性就会产生最终输出序号大于总页码，那么就要将其控制在页码数之内 
            pagestr = "共" + allpage + "页";
            if (RawUrl.Contains(".html"))
            {
                pagestr += page > 1 ? "<a href=\"" + url + parameters + "-1.html" + "\">首页</a>&nbsp;&nbsp;<a href=\"" + url + parameters + "-" + pre + ".html\">上一页</a>" : "<span class='none'>首页</span> <span class='none'>上一页</span>";
                //中间页处理，这个增加时间复杂度，减小空间复杂度 
                for (int i = startcount; i <= endcount; i++)
                {
                    pagestr += page == i ? "&nbsp;&nbsp;<span class='now'>" + i + "</span>" : "&nbsp;&nbsp;<a href=\"" + url + parameters + "-" + i + ".html\">" + i + "</a>";
                }
                pagestr += page != allpage ? "&nbsp;&nbsp;<a href=\"" + url + parameters + "-" + next + ".html\">下一页</a>&nbsp;&nbsp;<a href=\"" + url + parameters + "-" + allpage + ".html\">末页</a>" : " <span class='none'>下一页</span> <span class='none'>末页</span>";
            }
            else
            {
                pagestr += page > 1 ? "<a href=\"" + url + "\">首页</a>&nbsp;&nbsp;<a href=\"" + url + pre + "\">上一页</a>" : "<span class='none'>首页</span> <span class='none'>上一页</span>";
                //中间页处理，这个增加时间复杂度，减小空间复杂度 
                for (int i = startcount; i <= endcount; i++)
                {
                    pagestr += page == i ? "&nbsp;&nbsp;<span class='now'>" + i + "</span>" : "&nbsp;&nbsp;<a href=\"" + url + i + "\">" + i + "</a>";
                }
                pagestr += page != allpage ? "&nbsp;&nbsp;<a href=\"" + url + next + "\">下一页</a>&nbsp;&nbsp;<a href=\"" + url + allpage + "\">末页</a>" : " <span class='none'>下一页</span> <span class='none'>末页</span>";
            }
            return pagestr;
        }
        /// <summary>
        ///  Repeter分页类
        /// </summary>
        /// <param name="ds">daset</param>
        /// <param name="datalistname">repter名称</param>
        /// <param name="pagesize">页数</param>
        /// <param name="param">参数，无参数请传null</param>
        /// <returns></returns>
        public static string GetPageNum<T>(List<T> t, Repeater rptName, int pagesize, string[] param)
        {
            PagedDataSource objPds = new PagedDataSource();
            objPds.DataSource = t;
            objPds.AllowPaging = true;
            int total = t.Count;
            objPds.PageSize = pagesize;
            int page;
            if (HttpContext.Current.Request.QueryString["page"] != null)
                page = Convert.ToInt32(HttpContext.Current.Request.QueryString["page"]);
            else
                page = 1;
            objPds.CurrentPageIndex = page - 1;
            rptName.DataSource = objPds;
            rptName.DataBind();
            int allpage = 0;
            int next = 0;
            int pre = 0;
            int startcount = 0;
            int endcount = 0;
            string pagestr = "";
            string url = HttpContext.Current.Request.CurrentExecutionFilePath;
            string parameters = "";
            if (param != null)
            {
                foreach (string par in param)
                {
                    parameters += par + "=" + HttpContext.Current.Server.UrlEncode(HttpContext.Current.Request.QueryString[par]) + "&";
                }
                parameters = "?" + parameters;
            }
            else
            {
                parameters = "?";
            }
            if (page < 1) { page = 1; }
            //计算总页数 
            if (pagesize != 0)
            {
                allpage = (total / pagesize);
                allpage = ((total % pagesize) != 0 ? allpage + 1 : allpage);
                allpage = (allpage == 0 ? 1 : allpage);
            }
            next = page + 1;
            pre = page - 1;
            startcount = (page + 5) > allpage ? allpage - 9 : page - 4;//中间页起始序号 
            //中间页终止序号 
            endcount = page < 5 ? 10 : page + 5;
            if (startcount < 1) { startcount = 1; } //为了避免输出的时候产生负数，设置如果小于1就从序号1开始 
            if (allpage < endcount) { endcount = allpage; } //页码+5的可能性就会产生最终输出序号大于总页码，那么就要将其控制在页码数之内 
            pagestr = "共" + allpage + "页";
            pagestr += page > 1 ? "<a href=\"" + url + parameters + "page=1" + "\">首页</a>&nbsp;&nbsp;<a href=\"" + url + parameters + "page=" + pre + "\">上一页</a>" : "<span class='none'>首页</span> <span class='none'>上一页</span>";
            //中间页处理，这个增加时间复杂度，减小空间复杂度 
            for (int i = startcount; i <= endcount; i++)
            {
                pagestr += page == i ? "&nbsp;&nbsp;<span class='now'>" + i + "</span>" : "&nbsp;&nbsp;<a href=\"" + url + parameters + "page=" + i + "\">" + i + "</a>";
            }
            pagestr += page != allpage ? "&nbsp;&nbsp;<a href=\"" + url + parameters + "page=" + next + "\">下一页</a>&nbsp;&nbsp;<a href=\"" + url + parameters + "page=" + allpage + "\">末页</a>" : " <span class='none'>下一页</span> <span class='none'>末页</span>";
            return pagestr;
        }
        /// <summary>
        ///  Repeter分页类
        /// </summary>
        /// <param name="ds">daset</param>
        /// <param name="datalistname">repter名称</param>
        /// <param name="pagesize">页数</param>
        /// <param name="param">参数，无参数请传null</param>
        /// <returns></returns>
        //public static string GetPageNum(Page currentPage, DataSet ds, Repeater rptName, int pagesize, string[] param, string url)
        //{
        //    PagedDataSource objPds = new PagedDataSource();
        //    objPds.DataSource = ds.Tables[0].DefaultView;
        //    objPds.AllowPaging = true;
        //    int total = ds.Tables[0].Rows.Count;
        //    objPds.PageSize = pagesize;
        //    int page;
        //    if (HttpContext.Current.Request.QueryString["page"] != null)
        //        page = Convert.ToInt32(HttpContext.Current.Request.QueryString["page"]);
        //    else if (currentPage.RouteData.Values["page"] != null)
        //        page = Convert.ToInt32(currentPage.RouteData.Values["page"]);
        //    else
        //        page = 1;
        //    objPds.CurrentPageIndex = page - 1;
        //    rptName.DataSource = objPds;
        //    rptName.DataBind();
        //    int allpage = 0;
        //    int next = 0;
        //    int pre = 0;
        //    int startcount = 0;
        //    int endcount = 0;
        //    string pagestr = "";
        //    string RawUrl = HttpContext.Current.Request.RawUrl;
        //    string parameters = "";
        //    if (param != null)
        //    {
        //        foreach (string par in param)
        //        {
        //            if (HttpContext.Current.Request.QueryString[par] != null)
        //            {
        //                parameters = HttpContext.Current.Request.QueryString[par];
        //            }
        //            else
        //            {
        //                parameters = currentPage.RouteData.Values[par].ToString();
        //            }

        //        }

        //    }
        //    else
        //    {
        //        parameters = "";
        //    }
        //    if (page < 1) { page = 1; }
        //    //计算总页数 
        //    if (pagesize != 0)
        //    {
        //        allpage = (total / pagesize);
        //        allpage = ((total % pagesize) != 0 ? allpage + 1 : allpage);
        //        allpage = (allpage == 0 ? 1 : allpage);
        //    }
        //    next = page + 1;
        //    pre = page - 1;
        //    startcount = (page + 5) > allpage ? allpage - 9 : page - 4;//中间页起始序号 
        //    //中间页终止序号 
        //    endcount = page < 5 ? 10 : page + 5;
        //    if (startcount < 1) { startcount = 1; } //为了避免输出的时候产生负数，设置如果小于1就从序号1开始 
        //    if (allpage < endcount) { endcount = allpage; } //页码+5的可能性就会产生最终输出序号大于总页码，那么就要将其控制在页码数之内 
        //    pagestr = "共" + allpage + "页";
        //    if (RawUrl.Contains(".html"))
        //    {
        //        pagestr += page > 1 ? "<a href=\"" + url + parameters + "-1.html" + "\">首页</a>&nbsp;&nbsp;<a href=\"" + url + parameters + "-" + pre + ".html\">上一页</a>" : "<span class='none'>首页</span> <span class='none'>上一页</span>";
        //        //中间页处理，这个增加时间复杂度，减小空间复杂度 
        //        for (int i = startcount; i <= endcount; i++)
        //        {
        //            pagestr += page == i ? "&nbsp;&nbsp;<span class='now'>" + i + "</span>" : "&nbsp;&nbsp;<a href=\"" + url + parameters + "-" + i + ".html\">" + i + "</a>";
        //        }
        //        pagestr += page != allpage ? "&nbsp;&nbsp;<a href=\"" + url + parameters + "-" + next + ".html\">下一页</a>&nbsp;&nbsp;<a href=\"" + url + parameters + "-" + allpage + ".html\">末页</a>" : " <span class='none'>下一页</span> <span class='none'>末页</span>";
        //    }
        //    else
        //    {
        //        pagestr += page > 1 ? "<a href=\"" + url + "\">首页</a>&nbsp;&nbsp;<a href=\"" + url + pre + "\">上一页</a>" : "<span class='none'>首页</span> <span class='none'>上一页</span>";
        //        //中间页处理，这个增加时间复杂度，减小空间复杂度 
        //        for (int i = startcount; i <= endcount; i++)
        //        {
        //            pagestr += page == i ? "&nbsp;&nbsp;<span class='now'>" + i + "</span>" : "&nbsp;&nbsp;<a href=\"" + url + i + "\">" + i + "</a>";
        //        }
        //        pagestr += page != allpage ? "&nbsp;&nbsp;<a href=\"" + url + next + "\">下一页</a>&nbsp;&nbsp;<a href=\"" + url + allpage + "\">末页</a>" : " <span class='none'>下一页</span> <span class='none'>末页</span>";
        //    }
        //    return pagestr;
        //}
        /// <summary>
        /// 分页字符串拼接
        /// </summary>
        /// <param name="currentPage">当前页对象</param>
        /// <param name="total">总共多少条记录</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="param">参数</param>
        /// <param name="url">url</param>
        /// <returns></returns>
        //public static string GetPageNumMvc(Controller currentPage, int total, int pagesize, string url)
        //{
        //    if (total > pagesize)
        //    {
        //        int page;
        //        if (HttpContext.Current.Request.QueryString["page"] != null)
        //            page = Convert.ToInt32(HttpContext.Current.Request.QueryString["page"]);
        //        else if (currentPage.RouteData.Values["page"] != null)
        //            page = Convert.ToInt32(currentPage.RouteData.Values["page"]);
        //        else
        //            page = 1;

        //        int allpage = 0;
        //        int next = 0;
        //        int pre = 0;
        //        int startcount = 0;
        //        int endcount = 0;
        //        string pagestr = "";
        //        string RawUrl = HttpContext.Current.Request.RawUrl;

        //        if (page < 1) { page = 1; }
        //        //计算总页数 
        //        if (pagesize != 0)
        //        {
        //            allpage = (total / pagesize);
        //            allpage = ((total % pagesize) != 0 ? allpage + 1 : allpage);
        //            allpage = (allpage == 0 ? 1 : allpage);
        //        }
        //        next = page + 1;
        //        pre = page - 1;
        //        startcount = (page + 5) > allpage ? allpage - 9 : page - 4;//中间页起始序号 
        //        //中间页终止序号 
        //        endcount = page < 5 ? 10 : page + 5;
        //        if (startcount < 1) { startcount = 1; } //为了避免输出的时候产生负数，设置如果小于1就从序号1开始 
        //        if (allpage < endcount) { endcount = allpage; } //页码+5的可能性就会产生最终输出序号大于总页码，那么就要将其控制在页码数之内 
        //        pagestr = "共" + allpage + "页";
        //        pagestr += page > 1 ? "<a href=\"" + url + "\">首页</a>&nbsp;&nbsp;<a href=\"" + url + pre + "\">上一页</a>" : "<span class='none'>首页</span> <span class='none'>上一页</span>";
        //        //中间页处理，这个增加时间复杂度，减小空间复杂度 
        //        for (int i = startcount; i <= endcount; i++)
        //        {
        //            pagestr += page == i ? "&nbsp;&nbsp;<span class='now'>" + i + "</span>" : "&nbsp;&nbsp;<a href=\"" + url + i + "\">" + i + "</a>";
        //        }
        //        pagestr += page != allpage ? "&nbsp;&nbsp;<a href=\"" + url + next + "\">下一页</a>&nbsp;&nbsp;<a href=\"" + url + allpage + "\">末页</a>" : " <span class='none'>下一页</span> <span class='none'>末页</span>";

        //        return pagestr;
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}
        /// <summary>
        /// 分页字符串拼接(后台调用)
        /// </summary>
        /// <param name="currentPage">当前页对象</param>
        /// <param name="total">总共多少条记录</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="param">参数</param>
        /// <param name="url">url</param>
        /// <returns></returns>
        //public static string GetPageNumMvcManage(Controller currentPage, int total, int pagesize, string url)
        //{

        //    int page;
        //    if (HttpContext.Current.Request.QueryString["page"] != null)
        //        page = Convert.ToInt32(HttpContext.Current.Request.QueryString["page"]);
        //    else if (currentPage.RouteData.Values["page"] != null)
        //        page = Convert.ToInt32(currentPage.RouteData.Values["page"]);
        //    else
        //        page = 1;

        //    int allpage = 0;
        //    int next = 0;
        //    int pre = 0;
        //    int startcount = 0;
        //    int endcount = 0;
        //    string pagestr = "";
        //    string RawUrl = HttpContext.Current.Request.RawUrl;

        //    if (page < 1) { page = 1; }
        //    //计算总页数 
        //    if (pagesize != 0)
        //    {
        //        allpage = (total / pagesize);
        //        allpage = ((total % pagesize) != 0 ? allpage + 1 : allpage);
        //        allpage = (allpage == 0 ? 1 : allpage);
        //    }
        //    next = page + 1;
        //    pre = page - 1;
        //    startcount = (page + 5) > allpage ? allpage - 9 : page - 4;//中间页起始序号 
        //    //中间页终止序号 
        //    endcount = page < 5 ? 10 : page + 5;
        //    if (startcount < 1) { startcount = 1; } //为了避免输出的时候产生负数，设置如果小于1就从序号1开始 
        //    if (allpage < endcount) { endcount = allpage; } //页码+5的可能性就会产生最终输出序号大于总页码，那么就要将其控制在页码数之内 
        //    pagestr = "共" + total + "条记录 ";
        //    pagestr += "共" + allpage + "页 ";
        //    pagestr += page > 1 ? "<a href=\"" + url + "\">首页</a>&nbsp;&nbsp;<a href=\"" + url + pre + "\">上一页</a>" : "<span class='none'>首页</span> <span class='none'>上一页</span>";
        //    //中间页处理，这个增加时间复杂度，减小空间复杂度 
        //    for (int i = startcount; i <= endcount; i++)
        //    {
        //        pagestr += page == i ? "&nbsp;&nbsp;<span class='now'>" + i + "</span>" : "&nbsp;&nbsp;<a href=\"" + url + i + "\">" + i + "</a>";
        //    }
        //    pagestr += page != allpage ? "&nbsp;&nbsp;<a href=\"" + url + next + "\">下一页</a>&nbsp;&nbsp;<a href=\"" + url + allpage + "\">末页</a>" : " <span class='none'>下一页</span> <span class='none'>末页</span>";

        //    return pagestr;
        //}
        /// <summary>
        /// 分页字符串
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="total"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string GetPageNum(int pagesize, int total, string[] param)
        {
            int page;
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["page"]))
                page = Convert.ToInt32(HttpContext.Current.Request.QueryString["page"]);
            else
                page = 1;
            int allpage = 0;
            int next = 0;
            int pre = 0;
            int startcount = 0;
            int endcount = 0;
            string pagestr = "";
            string url = HttpContext.Current.Request.CurrentExecutionFilePath;
            string parameters = "";
            if (param != null)
            {
                foreach (string par in param)
                {
                    parameters += par + "=" + HttpContext.Current.Server.UrlEncode(HttpContext.Current.Request.QueryString[par]) + "&";
                }
                parameters = "?" + parameters;
            }
            else
            {
                parameters = "?";
            }
            if (page < 1) { page = 1; }
            //计算总页数 
            if (pagesize != 0)
            {
                allpage = (total / pagesize);
                allpage = ((total % pagesize) != 0 ? allpage + 1 : allpage);
                allpage = (allpage == 0 ? 1 : allpage);
            }
            next = page + 1;
            pre = page - 1;
            startcount = (page + 5) > allpage ? allpage - 9 : page - 4;//中间页起始序号 
            //中间页终止序号 
            endcount = page < 5 ? 10 : page + 5;
            if (startcount < 1) { startcount = 1; } //为了避免输出的时候产生负数，设置如果小于1就从序号1开始 
            if (allpage < endcount) { endcount = allpage; } //页码+5的可能性就会产生最终输出序号大于总页码，那么就要将其控制在页码数之内 
            //pagestr = "共" + allpage + "页";
            pagestr += page > 1 ? "<a href=\"" + url + parameters + "page=1" + "\">首页</a>&nbsp;&nbsp;<a href=\"" + url + parameters + "page=" + pre + "\">上一页</a>" : "<span class='none'>首页</span> <span class='none'>上一页</span>";
            //中间页处理，这个增加时间复杂度，减小空间复杂度 
            for (int i = startcount; i <= endcount; i++)
            {
                pagestr += page == i ? "&nbsp;&nbsp;<span class='now'>" + i + "</span>" : "&nbsp;&nbsp;<a href=\"" + url + parameters + "page=" + i + "\">" + i + "</a>";
            }
            pagestr += page != allpage ? "&nbsp;&nbsp;<a href=\"" + url + parameters + "page=" + next + "\">下一页</a>&nbsp;&nbsp;<a href=\"" + url + parameters + "page=" + allpage + "\">末页</a>" : " <span class='none'>下一页</span> <span class='none'>末页</span>";
            return pagestr;
        }
    }
}
