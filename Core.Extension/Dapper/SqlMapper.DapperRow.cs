using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Core.Extension.Dapper
{
    public static partial class SqlMapper
    {
        private sealed partial class DapperRow : IDictionary<string, object>, IReadOnlyDictionary<string, object>
        {
            private readonly DapperTable _table;
            private object[] _values;

            public DapperRow(DapperTable table, object[] values)
            {
                this._table = table ?? throw new ArgumentNullException(nameof(table));
                this._values = values ?? throw new ArgumentNullException(nameof(values));
            }

            ICollection<string> IDictionary<string, object>.Keys => this.Select(kv => kv.Key).ToArray();

            ICollection<object> IDictionary<string, object>.Values => this.Select(kv => kv.Value).ToArray();

            IEnumerable<string> IReadOnlyDictionary<string, object>.Keys => this.Select(kv => kv.Key);

            int IReadOnlyCollection<KeyValuePair<string, object>>.Count => this._values.Count(t => !(t is DeadValue));

            bool ICollection<KeyValuePair<string, object>>.IsReadOnly => false;

            IEnumerable<object> IReadOnlyDictionary<string, object>.Values => this.Select(kv => kv.Value);

            int ICollection<KeyValuePair<string, object>>.Count => this._values.Count(t => !(t is DeadValue));

            object IReadOnlyDictionary<string, object>.this[string key]
            {
                get
                {
                    this.TryGetValue(key, out object val);
                    return val;
                }
            }

            object IDictionary<string, object>.this[string key]
            {
                get
                {
                    this.TryGetValue(key, out object val);
                    return val;
                }

                set
                {
                    this.SetValue(key, value, false);
                }
            }

            public bool TryGetValue(string key, out object value)
                => this.TryGetValue(this._table.IndexOfName(key), out value);

            public override string ToString()
            {
                var sb = GetStringBuilder().Append("{DapperRow");
                foreach (var kv in this)
                {
                    var value = kv.Value;
                    sb.Append(", ").Append(kv.Key);
                    if (value != null)
                    {
                        sb.Append(" = '").Append(kv.Value).Append('\'');
                    }
                    else
                    {
                        sb.Append(" = NULL");
                    }
                }

                return sb.Append('}').ToStringRecycle();
            }

            public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
            {
                var names = this._table.FieldNames;
                for (var i = 0; i < names.Length; i++)
                {
                    object value = i < this._values.Length ? this._values[i] : null;
                    if (!(value is DeadValue))
                    {
                        yield return new KeyValuePair<string, object>(names[i], value);
                    }
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
            {
                IDictionary<string, object> dic = this;
                dic.Add(item.Key, item.Value);
            }

            void ICollection<KeyValuePair<string, object>>.Clear()
            { // removes values for **this row**, but doesn't change the fundamental table
                for (int i = 0; i < this._values.Length; i++)
                {
                    this._values[i] = DeadValue.Default;
                }
            }

            bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
            {
                return this.TryGetValue(item.Key, out object value) && Equals(value, item.Value);
            }

            void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
            {
                foreach (var kv in this)
                {
                    array[arrayIndex++] = kv; // if they didn't leave enough space; not our fault
                }
            }

            bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
            {
                IDictionary<string, object> dic = this;
                return dic.Remove(item.Key);
            }

            bool IDictionary<string, object>.ContainsKey(string key)
            {
                int index = this._table.IndexOfName(key);
                if (index < 0 || index >= this._values.Length || this._values[index] is DeadValue)
                {
                    return false;
                }

                return true;
            }

            void IDictionary<string, object>.Add(string key, object value) => this.SetValue(key, value, true);

            bool IDictionary<string, object>.Remove(string key) => this.Remove(this._table.IndexOfName(key));

            bool IReadOnlyDictionary<string, object>.ContainsKey(string key)
            {
                int index = this._table.IndexOfName(key);
                return index >= 0 && index < this._values.Length && !(this._values[index] is DeadValue);
            }

            public object SetValue(string key, object value)
            {
                return this.SetValue(key, value, false);
            }

            internal bool Remove(int index)
            {
                if (index < 0 || index >= this._values.Length || this._values[index] is DeadValue)
                {
                    return false;
                }

                this._values[index] = DeadValue.Default;
                return true;
            }

            internal bool TryGetValue(int index, out object value)
            {
                if (index < 0)
                { // doesn't exist
                    value = null;
                    return false;
                }

                // exists, **even if** we don't have a value; consider table rows heterogeneous
                value = index < this._values.Length ? this._values[index] : null;
                if (value is DeadValue)
                { // pretend it isn't here
                    value = null;
                    return false;
                }

                return true;
            }

            internal object SetValue(int index, object value)
            {
                int oldLength = this._values.Length;
                if (oldLength <= index)
                {
                    // we'll assume they're doing lots of things, and
                    // grow it to the full width of the table
                    Array.Resize(ref this._values, this._table.FieldCount);
                    for (int i = oldLength; i < this._values.Length; i++)
                    {
                        this._values[i] = DeadValue.Default;
                    }
                }

                return this._values[index] = value;
            }

            private object SetValue(string key, object value, bool isAdd)
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                int index = this._table.IndexOfName(key);
                if (index < 0)
                {
                    index = this._table.AddField(key);
                }
                else if (isAdd && index < this._values.Length && !(this._values[index] is DeadValue))
                {
                    // then semantically, this value already exists
                    throw new ArgumentException("An item with the same key has already been added", nameof(key));
                }

                return this.SetValue(index, value);
            }

            private sealed class DeadValue
            {
                public static readonly DeadValue Default = new DeadValue();

                private DeadValue()
                { /* hiding constructor */
                }
            }
        }
    }
}
