using Lucene.Net.Analysis;

namespace Lucene.Net.Search.Highlight
{
    /// <summary>
    ///     <see cref="IFragmenter">IFragmenter</see>
    ///     implementation which does not fragment the text.
    ///     This is useful for highlighting the entire content of a document or field.
    /// </summary>
    public class NullFragmenter : IFragmenter
    {
        public virtual void Start(string s, TokenStream tokenStream)
        {
        }

        public virtual bool IsNewFragment()
        {
            return false;
        }
    }
}