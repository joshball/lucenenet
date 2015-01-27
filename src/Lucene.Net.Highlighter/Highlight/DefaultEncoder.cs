namespace Lucene.Net.Search.Highlight
{
    /// <summary>
    ///     Simple
    ///     <see cref="IEncoder">IEncoder</see>
    ///     implementation that does not modify the output
    /// </summary>
    public class DefaultEncoder : IEncoder
    {
        public virtual string EncodeText(string originalText)
        {
            return originalText;
        }
    }
}