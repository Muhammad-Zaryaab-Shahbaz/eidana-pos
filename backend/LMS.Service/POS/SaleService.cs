using LMS.DAL.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Service.POS
{
    public partial class SaleService: BaseService, ISaleService
    {

        #region Class Variables

        public ISaleDAL _soDAL;

        #endregion
        #region Constructor 

        public SaleService(ISaleDAL soDAL)
        {
            _soDAL = soDAL;
        }
        #endregion
    }
}

