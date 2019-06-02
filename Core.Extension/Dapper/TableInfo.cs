using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Entity;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Core.Extension.Dapper
{

    public class TableInfo
    {
        public string TableName { get; set; }

        public string ColumnName { get; set; }

        public bool IsPrimaryKey { get; set; }
    }
}
