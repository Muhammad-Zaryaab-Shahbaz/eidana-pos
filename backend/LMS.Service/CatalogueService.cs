using LMS.Core.Entities;
using LMS.DAL;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Service
{
    public partial class CatalogueService
    {
        #region Class Variables
        
        public CoreDAL _coreDAL;
        public Logger _logger;
        
        #endregion
        public CatalogueService()
        {
            
            _coreDAL = new CoreDAL();
            _logger = LogManager.GetLogger("fileLogger");
            
        }
      
   
    }
      

    
    }
