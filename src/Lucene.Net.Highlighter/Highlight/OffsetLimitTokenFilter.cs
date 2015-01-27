using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Tokenattributes;
using Lucene.Net.Util;

namespace Lucene.Net.Search.Highlight
{
    /// <summary>
    ///     This TokenFilter limits the number of tokens while indexing by adding up the
    ///     current offset.
    /// </summary>
    /// <remarks>
    ///     This TokenFilter limits the number of tokens while indexing by adding up the
    ///     current offset.
    /// </remarks>
    public sealed class OffsetLimitTokenFilter : TokenFilter
    {
        private IEnumerator<AttributeSource.State> Iterator = null;
        private AttributeSource.State FinalState;



        private int offsetCount;
        
        private readonly OffsetAttribute offsetAttrib;
//        private readonly IOffsetAttribute offsetAttrib =  this.Attributes().HasAttribute<IOffsetAttribute>() ? dpEnum1.Attributes().GetAttribute<IOffsetAttribute>() : null;

        private readonly int offsetLimit;

        public OffsetLimitTokenFilter(TokenStream input, int offsetLimit) : base(input)
        {
            this.offsetAttrib =  GetAttribute<OffsetAttribute>();
            this.offsetLimit = offsetLimit;
        }

        /// <exception cref="System.IO.IOException"></exception>
        public override bool IncrementToken()
        {
            if (offsetCount < offsetLimit && input.IncrementToken())
            {
                var offsetLength = offsetAttrib.EndOffset() - offsetAttrib.StartOffset();
                offsetCount += offsetLength;
                return true;
            }
            return false;
        }

        /// <exception cref="System.IO.IOException"></exception>
        public override void Reset()
        {
            base.Reset();
            offsetCount = 0;
        }
    }
}