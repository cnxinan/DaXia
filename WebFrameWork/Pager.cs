using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaXia.WebFrameWork
{
    public class Pager
    {
        //总记录数
        public int RecordAllCount { get; set; }
        //当前页索引
        public int PageIndex { get; set; }
        //总页数
        public int PageAllCount { get; set; }
        //每页显示数量
        public int PageNumber { get; set; }
        //分页链接
        public string PageUrl { get; set; }
        //是否首页
        public bool IsFirstPage { get; set; }
        //是否末页
        public bool IsLastPage { get; set; }
        //是否上一页
        public bool IsProPage { get; set; }
        //是否下一页
        public bool IsNextPage { get; set; }
        //是否有前置省略号
        public bool IsEllipsisFront { get; set; }
        //是否有后置省略号
        public bool IsEllipsisBack { get; set; }
    }
}