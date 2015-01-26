/*
 * This code is derived from MyJavaLibrary (http://somelinktomycoollibrary)
 * 
 * If this is an open source Java library, include the proper license and copyright attributions here!
 */

using System;
using Lucene.Net.Support;

namespace Lucene.Net.Search.Highlight
{
    /// <summary>
    ///     Low-level class used to record information about a section of a document
    ///     with a score.
    /// </summary>
    /// <remarks>
    ///     Low-level class used to record information about a section of a document
    ///     with a score.
    /// </remarks>
    public class TextFragment
    {
        internal int fragNum;
        internal ICharSequence markedUpText;
        internal float score;
        internal int textEndPos;
        internal int textStartPos;

        public TextFragment(ICharSequence markedUpText, int textStartPos, int fragNum)
        {
            this.markedUpText = markedUpText;
            this.textStartPos = textStartPos;
            this.fragNum = fragNum;
        }

        internal virtual void SetScore(float score)
        {
            this.score = score;
        }

        public virtual float GetScore()
        {
            return score;
        }

        /// <param name="frag2">Fragment to be merged into this one</param>
        public virtual void Merge(TextFragment frag2)
        {
            textEndPos = frag2.textEndPos;
            score = Math.Max(score, frag2.score);
        }

        /// <returns>true if this fragment follows the one passed</returns>
        public virtual bool Follows(TextFragment fragment)
        {
            return textStartPos == fragment.textEndPos;
        }

        /// <returns>the fragment sequence number</returns>
        public virtual int GetFragNum()
        {
            return fragNum;
        }

        public override string ToString()
        {
            return markedUpText.SubSequence(textStartPos, textEndPos).ToString();
        }
    }
}