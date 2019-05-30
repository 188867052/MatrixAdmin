namespace Core.Extension.Dapper
{
    public static partial class DapperExtension
    {
        public class TableInfo
        {
            public string TableName { get; set; }

            public string ColumnName { get; set; }

            public bool IsPrimaryKey { get; set; }
        }
    }
}