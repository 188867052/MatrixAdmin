using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Core.Extension.Dapper
{
    public static partial class SqlMapper
    {
        private class TypeDeserializerCache
        {
            private TypeDeserializerCache(Type type)
            {
                this.type = type;
            }

            private static readonly Hashtable byType = new Hashtable();
            private readonly Type type;

            internal static void Purge(Type type)
            {
                lock (byType)
                {
                    byType.Remove(type);
                }
            }

            internal static void Purge()
            {
                lock (byType)
                {
                    byType.Clear();
                }
            }

            internal static Func<IDataReader, object> GetReader(Type type, IDataReader reader, int startBound, int length, bool returnNullIfFirstMissing)
            {
                var found = (TypeDeserializerCache)byType[type];
                if (found == null)
                {
                    lock (byType)
                    {
                        found = (TypeDeserializerCache)byType[type];
                        if (found == null)
                        {
                            byType[type] = found = new TypeDeserializerCache(type);
                        }
                    }
                }

                return found.GetReader(reader, startBound, length, returnNullIfFirstMissing);
            }

            private readonly Dictionary<DeserializerKey, Func<IDataReader, object>> readers = new Dictionary<DeserializerKey, Func<IDataReader, object>>();

            private struct DeserializerKey : IEquatable<DeserializerKey>
            {
                private readonly int startBound, length;
                private readonly bool returnNullIfFirstMissing;
                private readonly IDataReader reader;
                private readonly string[] names;
                private readonly Type[] types;
                private readonly int hashCode;

                public DeserializerKey(int hashCode, int startBound, int length, bool returnNullIfFirstMissing, IDataReader reader, bool copyDown)
                {
                    this.hashCode = hashCode;
                    this.startBound = startBound;
                    this.length = length;
                    this.returnNullIfFirstMissing = returnNullIfFirstMissing;

                    if (copyDown)
                    {
                        this.reader = null;
                        this.names = new string[length];
                        this.types = new Type[length];
                        int index = startBound;
                        for (int i = 0; i < length; i++)
                        {
                            this.names[i] = reader.GetName(index);
                            this.types[i] = reader.GetFieldType(index++);
                        }
                    }
                    else
                    {
                        this.reader = reader;
                        this.names = null;
                        this.types = null;
                    }
                }

                public override int GetHashCode() => this.hashCode;

                public override string ToString()
                { // only used in the debugger
                    if (this.names != null)
                    {
                        return string.Join(", ", this.names);
                    }

                    if (this.reader != null)
                    {
                        var sb = new StringBuilder();
                        int index = this.startBound;
                        for (int i = 0; i < this.length; i++)
                        {
                            if (i != 0)
                            {
                                sb.Append(", ");
                            }

                            sb.Append(this.reader.GetName(index++));
                        }

                        return sb.ToString();
                    }

                    return base.ToString();
                }

                public override bool Equals(object obj)
                {
                    return obj is DeserializerKey && this.Equals((DeserializerKey)obj);
                }

                public bool Equals(DeserializerKey other)
                {
                    if (this.hashCode != other.hashCode
                        || this.startBound != other.startBound
                        || this.length != other.length
                        || this.returnNullIfFirstMissing != other.returnNullIfFirstMissing)
                    {
                        return false; // clearly different
                    }

                    for (int i = 0; i < this.length; i++)
                    {
                        if ((this.names?[i] ?? this.reader?.GetName(this.startBound + i)) != (other.names?[i] ?? other.reader?.GetName(this.startBound + i))
                            ||
                            (this.types?[i] ?? this.reader?.GetFieldType(this.startBound + i)) != (other.types?[i] ?? other.reader?.GetFieldType(this.startBound + i)))
                        {
                            return false; // different column name or type
                        }
                    }

                    return true;
                }
            }

            private Func<IDataReader, object> GetReader(IDataReader reader, int startBound, int length, bool returnNullIfFirstMissing)
            {
                if (length < 0)
                {
                    length = reader.FieldCount - startBound;
                }

                int hash = GetColumnHash(reader, startBound, length);
                if (returnNullIfFirstMissing)
                {
                    hash *= -27;
                }

                // get a cheap key first: false means don't copy the values down
                var key = new DeserializerKey(hash, startBound, length, returnNullIfFirstMissing, reader, false);
                Func<IDataReader, object> deser;
                lock (this.readers)
                {
                    if (this.readers.TryGetValue(key, out deser))
                    {
                        return deser;
                    }
                }

                deser = GetTypeDeserializerImpl(this.type, reader, startBound, length, returnNullIfFirstMissing);

                // get a more expensive key: true means copy the values down so it can be used as a key later
                key = new DeserializerKey(hash, startBound, length, returnNullIfFirstMissing, reader, true);
                lock (this.readers)
                {
                    return this.readers[key] = deser;
                }
            }
        }
    }
}
