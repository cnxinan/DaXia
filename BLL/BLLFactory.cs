using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DaXia.BLL
{
    public class BLLFactory
    {
        private static volatile BLLFactory _bllFactory;
        private static readonly object lockObj = new object();

        private BLLFactory() { }

        #region Instance

        public static BLLFactory Instance
        {
            get
            {
                if (_bllFactory == null)
                {
                    _bllFactory = new BLLFactory();
                }

                return _bllFactory;
            }
        }

        #endregion
        
        #region Manager_bll

        private Manager_bll _managerBll;
        public Manager_bll ManagerBll
        {
            get
            {
                if(_managerBll == null)
                {
                    _managerBll = new Manager_bll();
                }

                return _managerBll;
            }
        }

        #endregion

        #region User_bll

        private User_bll _userBll;
        public User_bll UserBll
        {
            get
            {
                if (_userBll == null)
                {
                    _userBll = new User_bll();
                }

                return _userBll;
            }
        }

        #endregion

        #region School_bll

        private School_bll _schoolBll;
        public School_bll SchoolBll
        {
            get
            {
                if (_schoolBll == null)
                {
                    _schoolBll = new School_bll();
                }

                return _schoolBll;
            }
        }

        #endregion

        #region ShopDetail_bll

        private ShopDetail_bll _shopDetailBll;
        public ShopDetail_bll ShopDetailBll
        {
            get
            {
                if (_shopDetailBll == null)
                {
                    _shopDetailBll = new ShopDetail_bll();
                }

                return _shopDetailBll;
            }
        }

        #endregion

        #region RequestLog_bll

        private RequestLog_bll _requestLogBll;
        public RequestLog_bll RequestLogBll
        {
            get
            {
                if (_requestLogBll == null)
                {
                    _requestLogBll = new RequestLog_bll();
                }

                return _requestLogBll;
            }
        }

        #endregion

        #region Member_bll

        private Member_bll _memberBll;
        public Member_bll MemberBll
        {
            get
            {
                if (_memberBll == null)
                {
                    _memberBll = new Member_bll();
                }

                return _memberBll;
            }
        }        

        #endregion

        #region ProductType_bll

        private ProductType_bll _productTypeBll;
        public ProductType_bll ProductTypeBll
        {
            get
            {
                if (_productTypeBll == null)
                {
                    _productTypeBll = new ProductType_bll();
                }

                return _productTypeBll;
            }
        }

        #endregion

        #region ProductCatalog_bll

        private ProductCatalog_bll _productCatalogBll;
        public ProductCatalog_bll ProductCatalogBll
        {
            get
            {
                if (_productCatalogBll == null)
                {
                    _productCatalogBll = new ProductCatalog_bll();
                }

                return _productCatalogBll;
            }
        }

        #endregion

        #region Product_bll

        private Product_bll _productBll;
        public Product_bll ProductBll
        {
            get
            {
                if (_productBll == null)
                {
                    _productBll = new Product_bll();
                }

                return _productBll;
            }
        }

        #endregion

        #region ProductImage_bll

        private ProductImage_bll _productImageBll;
        public ProductImage_bll ProductImageBll
        {
            get
            {
                if (_productImageBll == null)
                {
                    _productImageBll = new ProductImage_bll();
                }

                return _productImageBll;
            }
        }

        #endregion

        #region WeChatMember_bll

        private WeChatMember_bll _weChatMemberBll;
        public WeChatMember_bll WeChatMemberBll
        {
            get
            {
                if (_weChatMemberBll == null)
                {
                    _weChatMemberBll = new WeChatMember_bll();
                }

                return _weChatMemberBll;
            }
        }

        #endregion

        #region AddressInfo_bll

        private AddressInfo_bll _addressInfoBll;
        public AddressInfo_bll AddressInfoBll
        {
            get
            {
                if (_addressInfoBll == null)
                {
                    _addressInfoBll = new AddressInfo_bll();
                }

                return _addressInfoBll;
            }
        }

        #endregion

        #region BookMark_bll

        private BookMark_bll _bookMarkBll;
        public BookMark_bll BookMarkBll
        {
            get
            {
                if (_bookMarkBll == null)
                {
                    _bookMarkBll = new BookMark_bll();
                }

                return _bookMarkBll;
            }
        }

        #endregion

        #region ShopAccount_bll

        private ShopAccount_bll _shopAccountBll;
        public ShopAccount_bll ShopAccountBll
        {
            get
            {
                if (_shopAccountBll == null)
                {
                    _shopAccountBll = new ShopAccount_bll();
                }

                return _shopAccountBll;
            }
        }

        #endregion

        #region ProcessedInfo_bll

        private ProcessedInfo_bll _processedInfoBll;
        public ProcessedInfo_bll ProcessedInfoBll
        {
            get
            {
                if (_processedInfoBll == null)
                {
                    _processedInfoBll = new ProcessedInfo_bll();
                }

                return _processedInfoBll;
            }
        }

        #endregion

        #region ProcessedHistoryInfo_bll

        private ProcessedHistoryInfo_bll _processedHistoryInfoBll;
        public ProcessedHistoryInfo_bll ProcessedHistoryInfoBll
        {
            get
            {
                if (_processedHistoryInfoBll == null)
                {
                    _processedHistoryInfoBll = new ProcessedHistoryInfo_bll();
                }

                return _processedHistoryInfoBll;
            }
        }

        #endregion

        #region ProcessingInfo_bll

        private ProcessingInfo_bll _processingInfoBll;
        public ProcessingInfo_bll ProcessingInfoBll
        {
            get
            {
                if (_processingInfoBll == null)
                {
                    _processingInfoBll = new ProcessingInfo_bll();
                }

                return _processingInfoBll;
            }
        }
        
        #endregion        

        #region SystemCtrl_bll

        private SystemCtrl_bll _systemCtrlBll;
        public SystemCtrl_bll SystemCtrlBll
        {
            get
            {
                if (_systemCtrlBll == null)
                {
                    _systemCtrlBll = new SystemCtrl_bll();
                }

                return _systemCtrlBll;
            }
        }

        #endregion

        #region AdInfo_bll

        private AdInfo_bll _adInfoBll;
        public AdInfo_bll AdInfoBll
        {
            get
            {
                if (_adInfoBll == null)
                {
                    _adInfoBll = new AdInfo_bll();
                }

                return _adInfoBll;
            }
        }

        #endregion

        #region Log_bll

        private Log_bll _logBll;
        public Log_bll LogBll
        {
            get
            {
                if (_logBll == null)
                {
                    _logBll = new Log_bll();
                }

                return _logBll;
            }
        }

        #endregion

        #region MemberAccount_bll

        private MemberAccount_bll _memberAccountBll;
        public MemberAccount_bll MemberAccountBll
        {
            get
            {
                if (_memberAccountBll == null)
                {
                    _memberAccountBll = new MemberAccount_bll();
                }

                return _memberAccountBll;
            }
        }

        #endregion

        #region TXAccount_bll

        private TXAccount_bll _txAccountBll;
        public TXAccount_bll TXAccountBll
        {
            get
            {
                if (_txAccountBll == null)
                {
                    _txAccountBll = new TXAccount_bll();
                }

                return _txAccountBll;
            }
        }

        #endregion

        #region WeChatQrcode_bll

        private WeChatQrcode_bll _weChatQrcodeBll;
        public WeChatQrcode_bll WeChatQrcodeBll
        {
            get
            {
                if (_weChatQrcodeBll == null)
                {
                    _weChatQrcodeBll = new WeChatQrcode_bll();
                }

                return _weChatQrcodeBll;
            }
        }

        #endregion

        #region ProductOrder_bll

        private ProductOrder_bll _productOrderBll;
        public ProductOrder_bll ProductOrderBll
        {
            get
            {
                if (_productOrderBll == null)
                {
                    _productOrderBll = new ProductOrder_bll();
                }

                return _productOrderBll;
            }
        }

        #endregion

    }
}
