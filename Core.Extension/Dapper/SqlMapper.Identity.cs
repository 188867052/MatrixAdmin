using System;
using System.Data;

namespace Core.Extension.Dapper
{
    public static partial class SqlMapper
    {
        /// <summary>
        /// Identity of a cached query in Dapper, used for extensibility.
        /// </summary>
        public class Identity : IEquatable<Identity>
        {
            /// <summary>
            /// The raw SQL command.
            /// </summary>
            public readonly string Sql;

            /// <summary>
            /// The SQL command type.
            /// </summary>
            public readonly CommandType? CommandType;

            /// <summary>
            /// The hash code of this Identity.
            /// </summary>
            public readonly int HashCode;

            /// <summary>
            /// The grid index (position in the reader) of this Identity.
            /// </summary>
            public readonly int GridIndex;

            /// <summary>
            /// This <see cref="System.Type"/> of this Identity.
            /// </summary>
            public readonly Type Type;

            /// <summary>
            /// The connection string for this Identity.
            /// </summary>
            public readonly string ConnectionString;

            /// <summary>
            /// The type of the parameters object for this Identity.
            /// </summary>
            public readonly Type ParametersType;

            /// <summary>
            /// Initializes a new instance of the <see cref="Identity"/> class.
            /// </summary>
            /// <param name="sql"></param>
            /// <param name="commandType"></param>
            /// <param name="connection"></param>
            /// <param name="type"></param>
            /// <param name="parametersType"></param>
            /// <param name="otherTypes"></param>
            internal Identity(string sql, CommandType? commandType, IDbConnection connection, Type type, Type parametersType, Type[] otherTypes)
                : this(sql, commandType, connection.ConnectionString, type, parametersType, otherTypes, 0)
            { /* base call */
            }

            private Identity(string sql, CommandType? commandType, string connectionString, Type type, Type parametersType, Type[] otherTypes, int gridIndex)
            {
                this.Sql = sql;
                this.CommandType = commandType;
                this.ConnectionString = connectionString;
                this.Type = type;
                this.ParametersType = parametersType;
                this.GridIndex = gridIndex;
                unchecked
                {
                    this.HashCode = 17; // we *know* we are using this in a dictionary, so pre-compute this
                    this.HashCode = this.HashCode * 23 + commandType.GetHashCode();
                    this.HashCode = this.HashCode * 23 + gridIndex.GetHashCode();
                    this.HashCode = this.HashCode * 23 + (sql?.GetHashCode() ?? 0);
                    this.HashCode = this.HashCode * 23 + (type?.GetHashCode() ?? 0);
                    if (otherTypes != null)
                    {
                        foreach (var t in otherTypes)
                        {
                            this.HashCode = this.HashCode * 23 + (t?.GetHashCode() ?? 0);
                        }
                    }

                    this.HashCode = this.HashCode * 23 + (connectionString == null ? 0 : connectionStringComparer.GetHashCode(connectionString));
                    this.HashCode = this.HashCode * 23 + (parametersType?.GetHashCode() ?? 0);
                }
            }

            /// <summary>
            /// Create an identity for use with DynamicParameters, internal use only.
            /// </summary>
            /// <param name="type">The parameters type to create an <see cref="Identity"/> for.</param>
            /// <returns>Identity.</returns>
            public Identity ForDynamicParameters(Type type) => new Identity(this.Sql, this.CommandType, this.ConnectionString, this.Type, type, null, -1);

            /// <summary>
            /// Whether this <see cref="Identity"/> equals another.
            /// </summary>
            /// <param name="obj">The other <see cref="object"/> to compare to.</param>
            /// <returns></returns>
            public override bool Equals(object obj) => this.Equals(obj as Identity);

            /// <summary>
            /// Gets the hash code for this identity.
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode() => this.HashCode;

            /// <summary>
            /// Compare 2 Identity objects.
            /// </summary>
            /// <param name="other">The other <see cref="Identity"/> object to compare.</param>
            /// <returns>Whether the two are equal.</returns>
            public bool Equals(Identity other)
            {
                return other != null
                    && this.GridIndex == other.GridIndex
                    && this.Type == other.Type
                    && this.Sql == other.Sql
                    && this.CommandType == other.CommandType
                    && connectionStringComparer.Equals(this.ConnectionString, other.ConnectionString)
                    && this.ParametersType == other.ParametersType;
            }

            internal Identity ForGrid(Type primaryType, int gridIndex) =>
            new Identity(this.Sql, this.CommandType, this.ConnectionString, primaryType, this.ParametersType, null, gridIndex);

            internal Identity ForGrid(Type primaryType, Type[] otherTypes, int gridIndex) =>
                new Identity(this.Sql, this.CommandType, this.ConnectionString, primaryType, this.ParametersType, otherTypes, gridIndex);
        }
    }
}
