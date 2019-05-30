namespace Core.Extension.Dapper
{
    public class TableInfo
    {
        public string TableName { get; set; }

        public string ColumnName { get; set; }

        public bool IsPrimaryKey { get; set; }
    }
}