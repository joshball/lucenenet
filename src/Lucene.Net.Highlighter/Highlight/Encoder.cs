namespace Lucene.Net.Search.Highlight
{
    /// <summary>
    ///     Encodes original text.
    /// </summary>
    /// <remarks>
    ///     Encodes original text. The Encoder works with the
    ///     <see cref="IFormatter">IFormatter</see>
    ///     to generate output.
    /// </remarks>
    public interface IEncoder
    {
        /// <param name="originalText">The section of text being output</param>
        string EncodeText(string originalText);
    }
}