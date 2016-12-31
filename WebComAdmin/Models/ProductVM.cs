using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaXia.BLL;
using DaXia.EntityDataModels;
using DaXia.WebFrameWork;

namespace DaXia.WebComAdmin
{
    public class ProductTypeListVM
    {
        public List<ProductTypeVM> itemList { get; set; }

        public Pager page { get; set; }

        public ProductTypeVM ETV(ProductType entity)
        {
            return new ProductTypeVM()
            {
                Id = entity.ID,
                Name = entity.Name,
                CreationTime = Utility.DTDefaultFormat(entity.CreationTime)
            };
        }
    }

    public class ProductTypeVM
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string CreationTime { get; set; }
    }

    public class ProductCatalogListVM
    {
        public List<ProductCatalogVM> itemList { get; set; }

        public Pager page { get; set; }

        public ProductCatalogVM ETV(ProductCatalog entity)
        {
            string parName = string.Empty;
            var proModel = BLL.BLLFactory.Instance.ProductCatalogBll.GetModel(entity.ParID);
            if (proModel != null)
            {
                parName = proModel.Name;
            }

            return new ProductCatalogVM()
            {
                Id = entity.ID,
                Name = entity.Name,
                ParName = parName,
                Image = entity.HeadImage,
                Sort = entity.Sort,
                CreationTime = Utility.DTDefaultFormat(entity.CreationTime)
            };
        } 
    }

    public class ProductCatalogVM
    {
        public Guid Id { get; set; }

        public string ParId { get; set; }

        public string Name { get; set; }

        public string ParName { get; set; }

        public string Image { get; set; }

        public int Sort { get; set; }

        public IEnumerable<SelectListItem> Catalogs { get; set; }

        public string CreationTime { get; set; }
    }

    public class ProductListVM
    {
        public List<ProductVM> itemList { get; set; }

        public Pager page { get; set; }


        public ProductVM ETV(Product entity)
        {
            return new ProductVM()
            {
                Id = entity.ID,
                TypeName = BLLFactory.Instance.ProductTypeBll.GetModel(entity.TypeID).Name,
                SerianNo = entity.SerialNo,
                Name = entity.Name,
                Image = entity.Image,
                Summery = entity.Summery,
                Details = entity.Details,
                SalesCount = entity.SalesCount.HasValue ? entity.SalesCount.Value : 0,
                FakeSalesCount = entity.FakeSalesCount.HasValue ? entity.FakeSalesCount.Value : 0,
                Stock = entity.Stock.HasValue ? entity.Stock.Value : 0,
                MarketPrice = entity.MarketPrice.HasValue ? entity.MarketPrice.Value : 0M,
                StockPrice = entity.StockPrice.HasValue ? entity.StockPrice.Value : 0M,
                Sort = entity.Sort.HasValue ? entity.Sort.Value : 0,
                CreationTime = Utility.DTDefaultFormat(entity.CreationTime.Value)
            };
        }
    }

    public class ProductVM
    { 
        public Guid Id { get; set; }
        public string TypeName { get; set; }
        public string TypeId { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
        public string SerianNo { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Summery { get; set; }
        public string Details { get; set; }
        public int SalesCount { get; set; }
        public int FakeSalesCount { get; set; }
        public decimal MarketPrice { get; set; }
        public decimal StockPrice { get; set; }
        public string Specifications { get; set; }
        public string VideoLink { get; set; }
        public int Sort { get; set; }
        public string CreationTime { get; set; }
        public int Stock { get; set; }
    }

    public class ProductImgListVM
    {
        public List<ProductImgVM> itemList { get; set; }

        public Guid ProductId { get; set; }

        public Pager page { get; set; }

        public ProductImgVM ETV(ProductImage entity)
        {
            return new ProductImgVM()
            {
                Id = entity.ID,
                ProductId = entity.ProductID,
                Image = entity.Image,
                Sort = entity.Sort,
                CreationTime = Utility.DTDefaultFormat(entity.CreationTime)
            };
        }
    }

    public class ProductImgVM
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Image { get; set; }
        public int Sort { get; set; }
        public string CreationTime { get; set; }
    }
}