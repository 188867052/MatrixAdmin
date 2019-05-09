using System;
using System.Data;

namespace Core.Extension.Dapper
{
    internal class WrappedReader : IWrappedDataReader
    {
        private IDataReader reader;
        private IDbCommand cmd;

        public IDataReader Reader
        {
            get
            {
                var tmp = this.reader;
                if (tmp == null) throw new ObjectDisposedException(this.GetType().Name);
                return tmp;
            }
        }

        IDbCommand IWrappedDataReader.Command
        {
            get
            {
                var tmp = this.cmd;
                if (tmp == null) throw new ObjectDisposedException(this.GetType().Name);
                return tmp;
            }
        }

        public WrappedReader(IDbCommand cmd, IDataReader reader)
        {
            this.cmd = cmd;
            this.reader = reader;
        }

        void IDataReader.Close() => this.reader?.Close();

        int IDataReader.Depth => this.Reader.Depth;

        DataTable IDataReader.GetSchemaTable() => this.Reader.GetSchemaTable();

        bool IDataReader.IsClosed => this.reader?.IsClosed ?? true;

        bool IDataReader.NextResult() => this.Reader.NextResult();

        bool IDataReader.Read() => this.Reader.Read();

        int IDataReader.RecordsAffected => this.Reader.RecordsAffected;

        void IDisposable.Dispose()
        {
            this.reader?.Close();
            this.reader?.Dispose();
            this.reader = null;
            this.cmd?.Dispose();
            this.cmd = null;
        }

        int IDataRecord.FieldCount => this.Reader.FieldCount;

        bool IDataRecord.GetBoolean(int i) => this.Reader.GetBoolean(i);

        byte IDataRecord.GetByte(int i) => this.Reader.GetByte(i);

        long IDataRecord.GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length) =>
            this.Reader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);

        char IDataRecord.GetChar(int i) => this.Reader.GetChar(i);

        long IDataRecord.GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length) =>
            this.Reader.GetChars(i, fieldoffset, buffer, bufferoffset, length);

        IDataReader IDataRecord.GetData(int i) => this.Reader.GetData(i);

        string IDataRecord.GetDataTypeName(int i) => this.Reader.GetDataTypeName(i);

        DateTime IDataRecord.GetDateTime(int i) => this.Reader.GetDateTime(i);

        decimal IDataRecord.GetDecimal(int i) => this.Reader.GetDecimal(i);

        double IDataRecord.GetDouble(int i) => this.Reader.GetDouble(i);

        Type IDataRecord.GetFieldType(int i) => this.Reader.GetFieldType(i);

        float IDataRecord.GetFloat(int i) => this.Reader.GetFloat(i);

        Guid IDataRecord.GetGuid(int i) => this.Reader.GetGuid(i);

        short IDataRecord.GetInt16(int i) => this.Reader.GetInt16(i);

        int IDataRecord.GetInt32(int i) => this.Reader.GetInt32(i);

        long IDataRecord.GetInt64(int i) => this.Reader.GetInt64(i);

        string IDataRecord.GetName(int i) => this.Reader.GetName(i);

        int IDataRecord.GetOrdinal(string name) => this.Reader.GetOrdinal(name);

        string IDataRecord.GetString(int i) => this.Reader.GetString(i);

        object IDataRecord.GetValue(int i) => this.Reader.GetValue(i);

        int IDataRecord.GetValues(object[] values) => this.Reader.GetValues(values);

        bool IDataRecord.IsDBNull(int i) => this.Reader.IsDBNull(i);

        object IDataRecord.this[string name] => this.Reader[name];

        object IDataRecord.this[int i] => this.Reader[i];
    }
}
