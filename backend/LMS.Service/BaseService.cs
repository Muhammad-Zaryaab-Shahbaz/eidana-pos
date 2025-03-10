﻿using LMS.DAL;
using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Service
{
    public abstract class BaseService
    {
        public Logger _logger;
        public String? _entity { get; set; }
        internal MySqlCommand? cmd;
        public CoreDAL _coreDAL;
        public BaseService()
        {
            _coreDAL = new CoreDAL();
            _logger = LogManager.GetLogger("fileLogger");
        }
    }
}