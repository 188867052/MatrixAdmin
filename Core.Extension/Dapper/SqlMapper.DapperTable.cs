using System;
using System.Collections.Generic;

namespace Core.Extension.Dapper
{
    public static partial class SqlMapper
    {
        private sealed class DapperTable
        {
            private string[] _fieldNames;
            private readonly Dictionary<string, int> _fieldNameLookup;

            internal string[] FieldNames => this._fieldNames;

            public DapperTable(string[] fieldNames)
            {
                this._fieldNames = fieldNames ?? throw new ArgumentNullException(nameof(fieldNames));

                this._fieldNameLookup = new Dictionary<string, int>(fieldNames.Length, StringComparer.Ordinal);

                // if there are dups, we want the **first** key to be the "winner" - so iterate backwards
                for (int i = fieldNames.Length - 1; i >= 0; i--)
                {
                    string key = fieldNames[i];
                    if (key != null)
                    {
                        this._fieldNameLookup[key] = i;
                    }
                }
            }

            internal int IndexOfName(string name)
            {
                return (name != null && this._fieldNameLookup.TryGetValue(name, out int result)) ? result : -1;
            }

            internal int AddField(string name)
            {
                if (name == null)
                {
                    throw new ArgumentNullException(nameof(name));
                }

                if (this._fieldNameLookup.ContainsKey(name))
                {
                    throw new InvalidOperationException("Field already exists: " + name);
                }

                int oldLen = this._fieldNames.Length;
                Array.Resize(ref this._fieldNames, oldLen + 1); // yes, this is sub-optimal, but this is not the expected common case
                this._fieldNames[oldLen] = name;
                this._fieldNameLookup[name] = oldLen;
                return oldLen;
            }

            internal bool FieldExists(string key) => key != null && this._fieldNameLookup.ContainsKey(key);

            public int FieldCount => this._fieldNames.Length;
        }
    }
}
