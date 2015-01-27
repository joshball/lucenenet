using System;

namespace Lucene.Net.Search.Highlight
{
    /// <summary>
    ///     Exception thrown if TokenStream Tokens are incompatible with provided text
    /// </summary>
    [Serializable]
    public class InvalidTokenOffsetsException : Exception
    {
        public InvalidTokenOffsetsException(string message) : base(message)
        {
        }
    }
}