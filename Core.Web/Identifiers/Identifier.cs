namespace Core.Web.Identifiers
{
    /// <summary>
    /// Generates an identifier string.
    /// </summary>
    public class Identifier
    {
        private readonly UniqueValue _id;

        /// <summary>
        /// Initializes a new instance of the <see cref="Identifier" /> class.
        /// </summary>
        public Identifier()
        {
            this._id = new UniqueValue();
        }

        /// <summary>
        /// Gets the identifier value.
        /// </summary>
        public string Value
        {
            get
            {
                return this._id.Value;
            }
        }
    }
}