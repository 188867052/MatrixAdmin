using System.Collections.Generic;

namespace Core.Extension.Dapper
{
    public static partial class SqlMapper
    {
        /// <summary>
        /// Represents a placeholder for a value that should be replaced as a literal value in the resulting sql.
        /// </summary>
        internal struct LiteralToken
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="LiteralToken"/> struct.
            /// </summary>
            /// <param name="token"></param>
            /// <param name="member"></param>
            internal LiteralToken(string token, string member)
            {
                this.Token = token;
                this.Member = member;
            }

            /// <summary>
            /// Gets the text in the original command that should be replaced.
            /// </summary>
            public string Token { get; }

            /// <summary>
            /// Gets the name of the member referred to by the token.
            /// </summary>
            public string Member { get; }

            internal static readonly IList<LiteralToken> None = new LiteralToken[0];
        }
    }
}
