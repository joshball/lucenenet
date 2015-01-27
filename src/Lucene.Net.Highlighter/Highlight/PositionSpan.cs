namespace Lucene.Net.Search.Highlight
{
    /// <summary>Utility class to record Positions Spans</summary>
    /// <lucene.internal></lucene.internal>
    public class PositionSpan
    {
        internal int start;

        internal int end;

        public PositionSpan(int start, int end)
        {
            this.start = start;
            this.end = end;
        }
    }
}