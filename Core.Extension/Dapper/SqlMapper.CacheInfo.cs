using System;
using System.Data;
using System.Threading;

namespace Core.Extension.Dapper
{
    public static partial class SqlMapper
    {
        private class CacheInfo
        {
            public DeserializerState Deserializer { get; set; }

            public Func<IDataReader, object>[] OtherDeserializers { get; set; }

            public Action<IDbCommand, object> ParamReader { get; set; }

            private int _hitCount;

            public int GetHitCount()
            {
                return Interlocked.CompareExchange(ref this._hitCount, 0, 0);
            }

            public void RecordHit()
            {
                Interlocked.Increment(ref this._hitCount);
            }
        }
    }
}
