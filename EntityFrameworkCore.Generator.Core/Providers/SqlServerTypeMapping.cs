using System;
using System.Data;

namespace EntityFrameworkCore.Generator.Core.Providers
{
    public class SqlServerTypeMapping : ProviderTypeMappingBase
    {
        protected override DbType MapDataType(string nativeType)
        {
            switch (nativeType.Trim().ToLower())
            {
                case "bigint": return DbType.Int64;
                case "binary": return DbType.Binary;
                case "bit": return DbType.Boolean;
                case "char": return DbType.AnsiStringFixedLength;
                case "date": return DbType.Date;
                case "datetime": return DbType.DateTime;
                case "datetime2": return DbType.DateTime2;
                case "datetimeoffset": return DbType.DateTimeOffset;
                case "decimal": return DbType.Decimal;
                case "float": return DbType.Double;
                case "image": return DbType.Binary;
                case "int": return DbType.Int32;
                case "money": return DbType.Decimal;
                case "nchar": return DbType.StringFixedLength;
                case "ntext": return DbType.String;
                case "numeric": return DbType.Decimal;
                case "nvarchar": return DbType.String;
                case "real": return DbType.Single;
                case "rowversion": return DbType.Binary;
                case "smalldatetime": return DbType.DateTime;
                case "smallint": return DbType.Int16;
                case "smallmoney": return DbType.Decimal;
                case "sql_variant": return DbType.Object;
                case "sysname": return DbType.StringFixedLength;
                case "text": return DbType.String;
                case "time": return DbType.Time;
                case "timestamp": return DbType.Binary;
                case "tinyint": return DbType.Byte;
                case "uniqueidentifier": return DbType.Guid;
                case "varbinary": return DbType.Binary;
                case "varchar": return DbType.AnsiString;
                case "xml": return DbType.Xml;

                default: return DbType.Object;
            }
        }

        protected override Type MapSystemType(string nativeType)
        {
            switch (nativeType.Trim().ToLower())
            {
                case "bigint": return typeof(long);
                case "binary": return typeof(byte[]);
                case "bit": return typeof(bool);
                case "char": return typeof(string);
                case "date": return typeof(DateTime);
                case "datetime": return typeof(DateTime);
                case "datetime2": return typeof(DateTime);
                case "datetimeoffset": return typeof(DateTimeOffset);
                case "decimal": return typeof(decimal);
                case "float": return typeof(double);
                case "image": return typeof(byte[]);
                case "int": return typeof(int);
                case "money": return typeof(decimal);
                case "nchar": return typeof(string);
                case "ntext": return typeof(string);
                case "numeric": return typeof(decimal);
                case "nvarchar": return typeof(string);
                case "real": return typeof(float);
                case "rowversion": return typeof(byte[]);
                case "smalldatetime": return typeof(DateTime);
                case "smallint": return typeof(short);
                case "smallmoney": return typeof(decimal);
                case "sql_variant": return typeof(object);
                case "sysname": return typeof(string);
                case "text": return typeof(string);
                case "time": return typeof(TimeSpan);
                case "timestamp": return typeof(byte[]);
                case "tinyint": return typeof(byte);
                case "uniqueidentifier": return typeof(Guid);
                case "varbinary": return typeof(byte[]);
                case "varchar": return typeof(string);
                case "xml": return typeof(string);

                default: return typeof(object);
            }
        }
    }
}