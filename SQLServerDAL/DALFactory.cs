/*
 * SQL数据访问工厂类，创建SqlServerDal
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DaXia.SQLServerDAL
{
    public class SQLDALFactory
    {
        private static volatile SQLDALFactory _instance = null;
        private static readonly object _lockObj = new object();

        private SQLDALFactory() { }

        #region Instance

        public static SQLDALFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        _instance = new SQLDALFactory();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Manager_dal

        private Manager_dal _managerDal = null;
        public Manager_dal ManagerDal
        {
            get
            {
                if (_managerDal == null)
                {
                    _managerDal = new Manager_dal();
                }

                return _managerDal;
            }
        }

        #endregion

        #region User_dal

        private User_dal _userDal = null;
        public User_dal UserDal
        {
            get
            {
                if (_userDal == null)
                {
                    _userDal = new User_dal();
                }

                return _userDal;
            }
        }

        #endregion

        #region RequestLog_dal

        private RequestLog_dal _requestLogDal = null;
        public RequestLog_dal RequestLogDal
        {
            get
            {
                if (_requestLogDal == null)
                {
                    _requestLogDal = new RequestLog_dal();
                }

                return _requestLogDal;
            }
        }

        #endregion

        #region School_Dal

        private School_dal _schoolDal = null;
        public School_dal SchoolDal
        {
            get
            {
                if (_schoolDal == null)
                {
                    _schoolDal = new School_dal();
                }

                return _schoolDal;
            }
        }

        #endregion

        #region Member

        private Member_dal _memberDal = null;
        public Member_dal MemberDal
        {
            get
            {
                if (_memberDal == null)
                {
                    _memberDal = new Member_dal();
                }

                return _memberDal;
            }
        }

        #endregion

        #region ShopDetails_dal

        private ShopDetail_dal _shopDetailDal = null;
        public ShopDetail_dal ShopDetailDal
        {
            get
            {
                if (_shopDetailDal == null)
                {
                    _shopDetailDal = new ShopDetail_dal();
                }

                return _shopDetailDal;
            }
        }

        #endregion

        #region ProductType_dal

        private ProductType_dal _productTypeDal = null;
        public ProductType_dal ProductTypeDal
        {
            get
            {
                if (_productTypeDal == null)
                {
                    _productTypeDal = new ProductType_dal();
                }

                return _productTypeDal;
            }
        }

        #endregion

        #region ProductCatalog_dal

        private ProductCatalog_dal _productCatalogDal = null;
        public ProductCatalog_dal ProductCatalogDal
        {
            get
            {
                if (_productCatalogDal == null)
                {
                    _productCatalogDal = new ProductCatalog_dal();
                }

                return _productCatalogDal;
            }
        }

        #endregion

        #region Product_dal

        private Product_dal _productDal = null;
        public Product_dal ProductDal
        {
            get
            {
                if (_productDal == null)
                {
                    _productDal = new Product_dal();
                }

                return _productDal;
            }
        }

        #endregion

        #region ProductImage_dal

        private ProductImage_dal _productImageDal = null;
        public ProductImage_dal ProductImageDal
        {
            get
            {
                if (_productImageDal == null)
                {
                    _productImageDal = new ProductImage_dal();
                }

                return _productImageDal;
            }
        }

        #endregion

        #region WeChatMember_dal

        private WeChatMember_dal _weChatMemberDal = null;
        public WeChatMember_dal WeChatMemberDal
        {
            get
            {
                if (_weChatMemberDal == null)
                {
                    _weChatMemberDal = new WeChatMember_dal();
                }

                return _weChatMemberDal;
            }
        }

        #endregion

        #region AddressInfo_dal

        private AddressInfo_dal _addressInfoDal = null;
        public AddressInfo_dal AddressInfoDal
        {
            get
            {
                if (_addressInfoDal == null)
                {
                    _addressInfoDal = new AddressInfo_dal();
                }

                return _addressInfoDal;
            }
        }

        #endregion

        #region BookMark_dal

        private BookMark_dal _bookMarkDal = null;
        public BookMark_dal BookMarkDal
        {
            get
            {
                if (_bookMarkDal == null)
                {
                    _bookMarkDal = new BookMark_dal();
                }

                return _bookMarkDal;
            }
        }

        #endregion
        
        #region ShopAccount_dal

        private ShopAccount_dal _shopAccountDal = null;
        public ShopAccount_dal ShopAccountDal
        {
            get
            {
                if (_shopAccountDal == null)
                {
                    _shopAccountDal = new ShopAccount_dal();
                }

                return _shopAccountDal;
            }
        }

        #endregion

        #region ProcessedInfo_dal

        private ProcessedInfo_dal _processedInfoDal = null;
        public ProcessedInfo_dal ProcessedInfoDal
        {
            get
            {
                if (_processedInfoDal == null)
                {
                    _processedInfoDal = new ProcessedInfo_dal();
                }

                return _processedInfoDal;
            }
        }

        #endregion

        #region ProcessedHistoryInfo_dal

        private ProcessedHistoryInfo_dal _processedHistoryInfoDal = null;
        public ProcessedHistoryInfo_dal ProcessedHistoryInfoDal
        {
            get
            {
                if (_processedHistoryInfoDal == null)
                {
                    _processedHistoryInfoDal = new ProcessedHistoryInfo_dal();
                }

                return _processedHistoryInfoDal;
            }
        }

        #endregion

        #region ProcessingInfo_dal

        private ProcessingInfo_dal _processingInfoDal = null;
        public ProcessingInfo_dal ProcessingInfoDal
        {
            get
            {
                if (_processingInfoDal == null)
                {
                    _processingInfoDal = new ProcessingInfo_dal();
                }

                return _processingInfoDal;
            }
        }

        #endregion

        #region SystemCtrl_dal

        private SystemCtrl_dal _systemCtrlDal = null;
        public SystemCtrl_dal SystemCtrlDal
        {
            get
            {
                if (_systemCtrlDal == null)
                {
                    _systemCtrlDal = new SystemCtrl_dal();
                }

                return _systemCtrlDal;
            }
        }

        #endregion

        #region AdInfo_dal

        private AdInfo_dal _adInfoDal = null;
        public AdInfo_dal AdInfoDal
        {
            get
            {
                if (_adInfoDal == null)
                {
                    _adInfoDal = new AdInfo_dal();
                }

                return _adInfoDal;
            }
        }

        #endregion

        #region Log_dal

        private Log_dal _logDal = null;
        public Log_dal LogDal
        {
            get
            {
                if (_logDal == null)
                {
                    _logDal = new Log_dal();
                }

                return _logDal;
            }
        }
        
        #endregion

        #region MemberAccount_dal

        private MemberAccount_dal _memberAccountDal = null;
        public MemberAccount_dal MemberAccountDal
        {
            get
            {
                if (_memberAccountDal == null)
                {
                    _memberAccountDal = new MemberAccount_dal();
                }

                return _memberAccountDal;
            }
        }        
        #endregion

        #region TXAccount_dal

        private TXAccount_dal _txAccountDal = null;
        public TXAccount_dal TXAccountDal
        {
            get
            {
                if (_txAccountDal == null)
                {
                    _txAccountDal = new TXAccount_dal();
                }

                return _txAccountDal;
            }
        }
        #endregion

        #region WeChatQrcode_dal

        private WeChatQrcode_dal _weChatQrcodeDal = null;
        public WeChatQrcode_dal WeChatQrcodeDal
        {
            get
            {
                if (_weChatQrcodeDal == null)
                {
                    _weChatQrcodeDal = new WeChatQrcode_dal();
                }

                return _weChatQrcodeDal;
            }
        }
        #endregion

        #region ProductOrder_dal

        private ProductOrder_dal _productOrderDal = null;
        public ProductOrder_dal ProductOrderDal
        {
            get
            {
                if (_productOrderDal == null)
                {
                    _productOrderDal = new ProductOrder_dal();
                }

                return _productOrderDal;
            }
        }
        #endregion
    }
}
