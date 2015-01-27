namespace Lucene.Net.Search.Highlight
{
    /// <summary>
    ///     Processes terms found in the original text, typically by applying some form
    ///     of mark-up to highlight terms in HTML search results pages.
    /// </summary>
    /// <remarks>
    ///     Processes terms found in the original text, typically by applying some form
    ///     of mark-up to highlight terms in HTML search results pages.
    /// </remarks>
    public interface IFormatter
    {
        /// <param name="originalText">The section of text being considered for markup</param>
        /// <param name="tokenGroup">
        ///     contains one or several overlapping Tokens along with
        ///     their scores and positions.
        /// </param>
        string HighlightTerm(string originalText, TokenGroup tokenGroup);
    }
}