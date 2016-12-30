using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DaXia.BLL;
using DaXia.WebFrameWork;
using DaXia.EntityDataModels;

namespace DaXia.WebComAdmin.Controllers
{
    [ManageAuthorize]
    public class ControlPanelProductController : Controller
    {
        private readonly Product_bll _productBll = BLLFactory.Instance.ProductBll;
        private readonly ProductCatalog_bll _productCatalogBll = BLLFactory.Instance.ProductCatalogBll;
        private readonly ProductImage_bll _productImageBll = BLLFactory.Instance.ProductImageBll;
        private readonly ProductType_bll _productTypeBll = BLLFactory.Instance.ProductTypeBll;
        private readonly ShopDetail_bll _shopDetialBll = BLLFactory.Instance.ShopDetailBll;

        public ActionResult ProductTypes()
        {
            ProductTypeListVM viewModel = new ProductTypeListVM() { itemList = new List<ProductTypeVM>() };

            string strWhere = string.Format("");

            #region 分页

            long pageIndex = Utility.pageIndex;
            if (Request["Page"] != null)
            {
                pageIndex = long.Parse(Request["Page"]);
            }
            long itemsPrePage = Utility.itemsPrePage;
            long totalPages = 0;
            long totalItems = 0;
            string url = Request.Url.AbsolutePath + "?1=1";
            strWhere += " order by CreationTime DESC";
            var itemList = _productTypeBll.GetListPaging(strWhere, pageIndex, itemsPrePage, out totalPages, out totalItems, (int)UserType.Dining);
            viewModel.page = new Pager() { RecordAllCount = (int)totalItems, PageIndex = (int)pageIndex, PageAllCount = (int)totalPages, PageUrl = url };

            #endregion

            itemList.ForEach((p) =>
            {
                viewModel.itemList.Add(viewModel.ETV(p));
            });

            return View(viewModel);
        }

        public ActionResult ProductTypeAddEdit(Guid? id)
        {
            ProductTypeVM model = new ProductTypeVM();

            if (id.HasValue)
            {
                var entity = _productTypeBll.GetModel(id.Value);
                if (entity != null)
                {
                    model.Id = entity.ID;
                    model.Name = entity.Name;
                    model.CreationTime = Utility.DTDefaultFormat(entity.CreationTime);
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult ProductTypeAddEdit(ProductTypeVM model)
        {
            string returnUrl = "/ControlPanelProduct/ProductTypes";
            string message;

            if (model.Id == Guid.Empty)
            {
                ProductType entity = new ProductType()
                {
                    ID = Guid.NewGuid(),
                    Name = model.Name,
                    CreationTime = DateTime.Now
                };

                if (_productTypeBll.Insert(entity))
                {
                    message = "添加成功";
                    ViewBag.JS = UIMessage.ShowDialogAndRedirct(message, returnUrl);
                }
                else
                {
                    message = "添加失败";
                    ViewBag.JS = UIMessage.AlertDialog(message);
                }
            }
            else
            {
                ProductType entity = _productTypeBll.GetModel(model.Id);
                if (entity != null)
                {
                    entity.Name = model.Name;
                    if (_productTypeBll.Update(entity))
                    {
                        message = "更新成功";
                        ViewBag.JS = UIMessage.ShowDialogAndRedirct(message, returnUrl);
                    }
                    else
                    {
                        message = "更新失败";
                        ViewBag.JS = UIMessage.AlertDialog(message);
                    }
                }
            }

            return View(model);
        }

        public ActionResult ProductCatalogs()
        {
            ProductCatalogListVM viewModel = new ProductCatalogListVM() { itemList = new List<ProductCatalogVM>() };

            string strWhere = string.Format("");

            #region 分页

            long pageIndex = Utility.pageIndex;
            if (Request["Page"] != null)
            {
                pageIndex = long.Parse(Request["Page"]);
            }
            long itemsPrePage = Utility.itemsPrePage;
            long totalPages = 0;
            long totalItems = 0;
            string url = Request.Url.AbsolutePath + "?1=1";
            strWhere += " order by CreationTime DESC";
            var itemList = _productCatalogBll.GetListPaging(strWhere, pageIndex, itemsPrePage, out totalPages, out totalItems, (int)UserType.Dining);
            viewModel.page = new Pager() { RecordAllCount = (int)totalItems, PageIndex = (int)pageIndex, PageAllCount = (int)totalPages, PageUrl = url };

            #endregion

            itemList.ForEach((p) =>
            {
                viewModel.itemList.Add(viewModel.ETV(p));
            });

            return View(viewModel);
        }

        public ActionResult ProductCatalogAddEdit(Guid? id)
        {
            ProductCatalogVM model = new ProductCatalogVM();
            Guid selectedCatalog = Guid.Empty;

            if (id.HasValue)
            {
                var entity = _productCatalogBll.GetModel(id.Value);
                if (entity != null)
                {
                    selectedCatalog = entity.ParID;
                    model.Name = entity.Name;
                    model.Image = entity.HeadImage;
                    model.Sort = entity.Sort;
                }
            }

            model.Catalogs = GetCatalogDropDownList(selectedCatalog);

            return View(model);
        }

        [HttpPost]
        public ActionResult ProductCatalogAddEdit(ProductCatalogVM model)
        {
            string returnUrl = "/ControlPanelProduct/ProductCatalogs";
            string message;

            Guid parId = new Guid(model.ParId);

            model.Catalogs = GetCatalogDropDownList(parId);

            if (model.Id == Guid.Empty)
            {
                ProductCatalog entity = new ProductCatalog();
                entity.ID = Guid.NewGuid();
                entity.ParID = parId;
                entity.Name = model.Name;
                entity.HeadImage = model.Image;
                entity.Sort = model.Sort;
                entity.CreationTime = DateTime.Now;

                if (_productCatalogBll.Insert(entity))
                {
                    message = "添加成功";
                    ViewBag.JS = UIMessage.ShowDialogAndRedirct(message, returnUrl);
                }
                else
                {
                    message = "添加失败";
                    ViewBag.JS = UIMessage.AlertDialog(message);
                }
            }
            else
            {
                ProductCatalog entity = _productCatalogBll.GetModel(model.Id);
                if (entity != null)
                {
                    entity.ParID = new Guid(model.ParId);
                    entity.Name = model.Name;
                    entity.HeadImage = model.Image;
                    entity.Sort = model.Sort;
                    if (_productCatalogBll.Update(entity))
                    {
                        message = "更新成功";
                        ViewBag.JS = UIMessage.ShowDialogAndRedirct(message, returnUrl);
                    }
                    else
                    {
                        message = "更新失败";
                        ViewBag.JS = UIMessage.AlertDialog(message);
                    }
                }
                else
                {
                    message = "更新失败";
                    ViewBag.JS = UIMessage.AlertDialog(message);
                }
            }

            return View(model);
        }

        public ActionResult Products()
        {
            ProductListVM viewModel = new ProductListVM() { itemList = new List<ProductVM>() };

            string strWhere = string.Format(" where 1=1");

            #region 分页

            long pageIndex = Utility.pageIndex;
            if (Request["Page"] != null)
            {
                pageIndex = long.Parse(Request["Page"]);
            }
            long itemsPrePage = Utility.itemsPrePage;
            long totalPages = 0;
            long totalItems = 0;
            string url = Request.Url.AbsolutePath + "?1=1";
            if (Request["productName"] != null)
            {
                strWhere += string.Format(" and Name like '%{0}%'", Request["productName"]);
            }
            strWhere += " order by CreationTime DESC";
            var itemList = _productBll.GetListPaging(strWhere, pageIndex, itemsPrePage, out totalPages, out totalItems, (int)UserType.Dining);
            viewModel.page = new Pager() { RecordAllCount = (int)totalItems, PageIndex = (int)pageIndex, PageAllCount = (int)totalPages, PageUrl = url };

            #endregion

            itemList.ForEach((p) =>
            {
                viewModel.itemList.Add(viewModel.ETV(p));
            });

            return View(viewModel);

        }

        public ActionResult ProductAddEdit(Guid? id)
        {
            ProductVM model = new ProductVM();

            if (id.HasValue)
            {
                var entity = _productBll.GetModel(id.Value);

                if (entity != null)
                {
                    model.Types = GetTypeDropDownList(entity.TypeID);
                    model.Name = entity.Name;
                    model.Image = entity.Image;
                    model.Summery = entity.Summery;
                    model.Details = entity.Details;
                    model.SalesCount = entity.SalesCount.HasValue ? entity.SalesCount.Value : 0;
                    model.FakeSalesCount = entity.FakeSalesCount.HasValue ? entity.FakeSalesCount.Value : 0;
                    model.MarketPrice = entity.MarketPrice.HasValue ? entity.MarketPrice.Value : 0;
                    model.StockPrice = entity.StockPrice.HasValue ? entity.StockPrice.Value : 0;
                    model.Specifications = entity.Specifications;
                    model.VideoLink = entity.VideoLink;
                    model.Sort = entity.Sort.HasValue ? entity.Sort.Value : 0;
                }
            }
            else
            {
                model.Types = GetTypeDropDownList(Guid.Empty);
            }


            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ProductAddEdit(ProductVM model)
        {
            string returnUrl = "/ControlPanelProduct/Products";
            string message;            

            model.Types = GetTypeDropDownList(new Guid(model.TypeId));

            if (model.Id == Guid.Empty)
            {
                Product entity = new Product();

                entity.ID = Guid.NewGuid();
                entity.TypeID = new Guid(model.TypeId);
                entity.SerialNo = string.Empty;
                entity.Name = model.Name;
                entity.Image = model.Image;
                entity.Summery = model.Summery;
                entity.Details = model.Details;
                entity.SalesCount = model.SalesCount;
                entity.FakeSalesCount = model.FakeSalesCount;
                entity.MarketPrice = model.MarketPrice;
                entity.StockPrice = model.StockPrice;
                entity.Specifications = model.Specifications;
                entity.VideoLink = model.VideoLink;
                entity.Sort = model.Sort;
                entity.CreationTime = DateTime.Now;

                if (_productBll.Insert(entity))
                {
                    message = "添加成功";
                    ViewBag.JS = UIMessage.ShowDialogAndRedirct(message, returnUrl);
                }
                else
                {
                    message = "添加失败";
                    ViewBag.JS = UIMessage.AlertDialog(message);
                }
            }
            else
            {
                Product entity = _productBll.GetModel(model.Id);
                if (entity == null)
                {
                    message = "更新失败，产品不存在!";
                    ViewBag.JS = UIMessage.AlertDialog(message);
                }
                else
                {
                    entity.TypeID = new Guid(model.TypeId);
                    entity.SerialNo = string.Empty;
                    entity.Name = model.Name;
                    entity.Image = model.Image;
                    entity.Summery = model.Summery;
                    entity.Details = model.Details;
                    entity.SalesCount = model.SalesCount;
                    entity.FakeSalesCount = model.FakeSalesCount;
                    entity.MarketPrice = model.MarketPrice;
                    entity.StockPrice = model.StockPrice;
                    entity.Specifications = model.Specifications;
                    entity.VideoLink = model.VideoLink;
                    entity.Sort = model.Sort;

                    if (_productBll.Update(entity))
                    {
                        message = "更新成功";
                        ViewBag.JS = UIMessage.ShowDialogAndRedirct(message, returnUrl);
                    }
                    else
                    {
                        message = "更新失败";
                        ViewBag.JS = UIMessage.AlertDialog(message);
                    }
                }
            }

            return View(model);
        }

        public ActionResult _ajaxStartShack(Guid id)
        {
            string result = "该产品已开始抽奖";

            //var productEntity = _productBll.GetModel(id);
            //if (productEntity == null)
            //{
            //    result = "产品不存在";
            //}
            //else
            //{
            //    if (productEntity.ProcessNumber > 0)
            //    {
            //        result = "产品已在活动中";
            //    }
            //    else
            //    {
            //        productEntity.ProcessNumber = 1;
            //        if (!_productBll.Update(productEntity))
            //        {
            //            result = "产品状态修改失败，请联系管理员!!";
            //        }
            //    }
            //}

            return Content(result);
        }

        public ActionResult _ajaxDeleteProduct(Guid id)
        {
            string result = "删除成功";

            if (!_productBll.Delete(id))
            {
                result = "删除失败";
            }

            return Content(result);
        }

        public ActionResult _ajaxDisable(Guid id, int num)
        {
            string result = "修改失败";

            Product pE = _productBll.GetModel(id);

            if (pE != null)
            {
                if (num == 1)
                {
                    pE.SerialNo = "1";
                }
                else
                {
                    pE.SerialNo = string.Empty;
                }

                if (_productBll.Update(pE))
                {
                    result = "修改成功";
                }
            }

            return Content(result);
        }

        #region 产品多图

        public ActionResult ProductImgs(Guid id)
        {
            ProductImgListVM viewModel = new ProductImgListVM() { itemList = new List<ProductImgVM>(), ProductId = id };

            string strWhere = string.Format("");

            #region 分页

            long pageIndex = Utility.pageIndex;
            if (Request["Page"] != null)
            {
                pageIndex = long.Parse(Request["Page"]);
            }
            long itemsPrePage = Utility.itemsPrePage;
            long totalPages = 0;
            long totalItems = 0;
            string url = Request.Url.AbsolutePath + "?1=1";
            strWhere += " where ProductID=@0";
            strWhere += " order by Sort asc, CreationTime DESC";
            var itemList = _productImageBll.GetListPaging(strWhere, pageIndex, itemsPrePage, out totalPages, out totalItems, id);
            viewModel.page = new Pager() { RecordAllCount = (int)totalItems, PageIndex = (int)pageIndex, PageAllCount = (int)totalPages, PageUrl = url };

            #endregion

            itemList.ForEach((p) =>
            {
                viewModel.itemList.Add(viewModel.ETV(p));
            });

            return View(viewModel);
        }

        public ActionResult _ajaxAddImg(Guid productId, string url)
        {
            string result = "添加失败!";

            ProductImage model = new ProductImage() 
            { 
                ID = Guid.NewGuid(), 
                ProductID = productId, 
                Image = url,
                Sort = 0,
                CreationTime = DateTime.Now
            };

            if (_productImageBll.Insert(model))
            {
                result = "添加成功!";
            }

            return Content(result);
        }

        public ActionResult _ajaxRemoveImg(Guid id)
        {
            string result = "删除失败!";

            var img = _productImageBll.GetModel(id);

            if (img != null)
            {
                if (_productImageBll.Delete(img))
                {
                    result = "删除成功";
                }
            }
            else
            {
                result = "图片不存在!";
            }

            return Content(result);
        }

        #endregion

        /// <summary>
        /// 获取栏目下拉列表数据
        /// </summary>
        /// <param name="selectedItem"></param>
        /// <returns></returns>
        private List<SelectListItem> GetCatalogDropDownList(Guid selectedItem)
        {
            bool isDefault = false;

            if (selectedItem == Guid.Empty)
            {
                isDefault = true;
            }

            var allItems = _productCatalogBll.GetAllItems(string.Empty);
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem() { Text = "请选择", Value = Guid.Empty.ToString(), Selected = isDefault });

            foreach (var item in allItems)
            {
                if (selectedItem != item.ID)
                {
                    listItems.Add(new SelectListItem() { Text = item.Name, Value = item.ID.ToString() });
                }
                else
                {
                    listItems.Add(new SelectListItem() { Text = item.Name, Value = item.ID.ToString(), Selected = true });
                }

            }

            return listItems;
        }

        /// <summary>
        /// 获取产品类型下拉数据
        /// </summary>
        /// <param name="selectedItem"></param>
        /// <returns></returns>
        private List<SelectListItem> GetTypeDropDownList(Guid selectedItem)
        {
            bool isDefault = false;

            if (selectedItem == Guid.Empty)
            {
                isDefault = true;
            }

            var allItems = _productTypeBll.GetAllItems();
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem() { Text = "请选择", Value = Guid.Empty.ToString(), Selected = isDefault });

            foreach (var item in allItems)
            {
                if (selectedItem != item.ID)
                {
                    listItems.Add(new SelectListItem() { Text = item.Name, Value = item.ID.ToString() });
                }
                else
                {
                    listItems.Add(new SelectListItem() { Text = item.Name, Value = item.ID.ToString(), Selected = true });
                }
            }

            return listItems;
        }

        /// <summary>
        /// 获取商户下拉数据
        /// </summary>
        /// <param name="selectedItem"></param>
        /// <returns></returns>
        private List<SelectListItem> GetShopDropDownList(Guid selectedItem)
        {
            bool isDefault = false;

            if (selectedItem == Guid.Empty)
            {
                isDefault = true;
            }

            var allItems = _shopDetialBll.GetAllItems();
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem() { Text = "请选择", Value = Guid.Empty.ToString(), Selected = isDefault });

            foreach (var item in allItems)
            {
                if (selectedItem != item.ID)
                {
                    listItems.Add(new SelectListItem() { Text = item.Contacts, Value = item.ID.ToString() });
                }
                else
                {
                    listItems.Add(new SelectListItem() { Text = item.Contacts, Value = item.ID.ToString(), Selected = true });
                }
            }

            return listItems;
        }
    }
}