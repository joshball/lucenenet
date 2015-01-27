using System;
using System.Text;

namespace Lucene.Net.Search.Highlight
{
    /// <summary>
    ///     Formats text with different color intensity depending on the score of the
    ///     term.
    /// </summary>
    /// <remarks>
    ///     Formats text with different color intensity depending on the score of the
    ///     term.
    /// </remarks>
    public class GradientFormatter : IFormatter
    {
        private readonly float maxScore;

        internal int fgRMin;

        internal int fgGMin;

        internal int fgBMin;

        internal int fgRMax;

        internal int fgGMax;

        internal int fgBMax;

        protected internal bool highlightForeground;

        internal int bgRMin;

        internal int bgGMin;

        internal int bgBMin;

        internal int bgRMax;

        internal int bgGMax;

        internal int bgBMax;

        protected internal bool highlightBackground;

        /// <summary>Sets the color range for the IDF scores</summary>
        /// <param name="maxScore">
        ///     The score (and above) displayed as maxColor (See QueryScorer.getMaxWeight
        ///     which can be used to calibrate scoring scale)
        /// </param>
        /// <param name="minForegroundColor">
        ///     The hex color used for representing IDF scores of zero eg
        ///     #FFFFFF (white) or null if no foreground color required
        /// </param>
        /// <param name="maxForegroundColor">
        ///     The largest hex color used for representing IDF scores eg
        ///     #000000 (black) or null if no foreground color required
        /// </param>
        /// <param name="minBackgroundColor">
        ///     The hex color used for representing IDF scores of zero eg
        ///     #FFFFFF (white) or null if no background color required
        /// </param>
        /// <param name="maxBackgroundColor">
        ///     The largest hex color used for representing IDF scores eg
        ///     #000000 (black) or null if no background color required
        /// </param>
        public GradientFormatter(float maxScore, string minForegroundColor, string maxForegroundColor,
            string minBackgroundColor, string maxBackgroundColor)
        {
            highlightForeground = (minForegroundColor != null) && (maxForegroundColor != null);

            if (highlightForeground)
            {
                if (minForegroundColor.Length != 7)
                {
                    throw new ArgumentException("minForegroundColor is not 7 bytes long eg a hex " +
                                                "RGB value such as #FFFFFF");
                }
                if (maxForegroundColor.Length != 7)
                {
                    throw new ArgumentException("minForegroundColor is not 7 bytes long eg a hex " +
                                                "RGB value such as #FFFFFF");
                }
                fgRMin = HexToInt(minForegroundColor.Substring(1, 3));
                fgGMin = HexToInt(minForegroundColor.Substring(3, 5));
                fgBMin = HexToInt(minForegroundColor.Substring(5, 7));
                fgRMax = HexToInt(maxForegroundColor.Substring(1, 3));
                fgGMax = HexToInt(maxForegroundColor.Substring(3, 5));
                fgBMax = HexToInt(maxForegroundColor.Substring(5, 7));
            }

            highlightBackground = (minBackgroundColor != null) && (maxBackgroundColor != null);

            if (highlightBackground)
            {
                if (minBackgroundColor.Length != 7)
                {
                    throw new ArgumentException("minBackgroundColor is not 7 bytes long eg a hex " +
                                                "RGB value such as #FFFFFF");
                }
                if (maxBackgroundColor.Length != 7)
                {
                    throw new ArgumentException("minBackgroundColor is not 7 bytes long eg a hex " +
                                                "RGB value such as #FFFFFF");
                }
                bgRMin = HexToInt(minBackgroundColor.Substring(1, 3));
                bgGMin = HexToInt(minBackgroundColor.Substring(3, 5));
                bgBMin = HexToInt(minBackgroundColor.Substring(5, 7));
                bgRMax = HexToInt(minBackgroundColor.Substring(1, 3));
                bgGMax = HexToInt(minBackgroundColor.Substring(3, 5));
                bgBMax = HexToInt(minBackgroundColor.Substring(5, 7));
            }
            
            //        this.corpusReader = corpusReader;
            this.maxScore = maxScore;
        }

        //        totalNumDocs = corpusReader.numDocs();
        public virtual string HighlightTerm(string originalText, TokenGroup tokenGroup)
        {
            if (tokenGroup.GetTotalScore() == 0)
            {
                return originalText;
            }
            var score = tokenGroup.GetTotalScore();
            if (score == 0)
            {
                return originalText;
            }
            var sb = new StringBuilder();
            sb.Append("<font ");
            if (highlightForeground)
            {
                sb.Append("color=\"");
                sb.Append(GetForegroundColorString(score));
                sb.Append("\" ");
            }
            if (highlightBackground)
            {
                sb.Append("bgcolor=\"");
                sb.Append(GetBackgroundColorString(score));
                sb.Append("\" ");
            }
            sb.Append(">");
            sb.Append(originalText);
            sb.Append("</font>");
            return sb.ToString();
        }

        protected internal virtual string GetForegroundColorString(float score)
        {
            var rVal = GetColorVal(fgRMin, fgRMax, score);
            var gVal = GetColorVal(fgGMin, fgGMax, score);
            var bVal = GetColorVal(fgBMin, fgBMax, score);
            var sb = new StringBuilder();
            sb.Append("#");
            sb.Append(IntToHex(rVal));
            sb.Append(IntToHex(gVal));
            sb.Append(IntToHex(bVal));
            return sb.ToString();
        }

        protected internal virtual string GetBackgroundColorString(float score)
        {
            var rVal = GetColorVal(bgRMin, bgRMax, score);
            var gVal = GetColorVal(bgGMin, bgGMax, score);
            var bVal = GetColorVal(bgBMin, bgBMax, score);
            var sb = new StringBuilder();
            sb.Append("#");
            sb.Append(IntToHex(rVal));
            sb.Append(IntToHex(gVal));
            sb.Append(IntToHex(bVal));
            return sb.ToString();
        }

        private int GetColorVal(int colorMin, int colorMax, float score)
        {
            if (colorMin == colorMax)
            {
                return colorMin;
            }
            float scale = Math.Abs(colorMin - colorMax);
            var relScorePercent = Math.Min(maxScore, score)/maxScore;
            var colScore = scale*relScorePercent;
            return Math.Min(colorMin, colorMax) + (int) colScore;
        }

        private static readonly char[] hexDigits =
        {
            '0', '1', '2', '3', '4', '5', '6', '7'
            , '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'
        };

        private static string IntToHex(int i)
        {
            return string.Empty + hexDigits[(i & unchecked(0xF0)) >> 4] + hexDigits[i  & unchecked(0x0F)];
        }

        /// <summary>Converts a hex string into an int.</summary>
        /// <remarks>
        ///     Converts a hex string into an int. Integer.parseInt(hex, 16) assumes the
        ///     input is nonnegative unless there is a preceding minus sign. This method
        ///     reads the input as twos complement instead, so if the input is 8 bytes
        ///     long, it will correctly restore a negative int produced by
        ///     Integer.toHexString() but not necessarily one produced by
        ///     Integer.toString(x,16) since that method will produce a string like '-FF'
        ///     for negative integer values.
        /// </remarks>
        /// <param name="hex">
        ///     A string in capital or lower case hex, of no more then 16
        ///     characters.
        /// </param>
        /// <exception cref="System.FormatException">
        ///     if the string is more than 16 characters long, or if any
        ///     character is not in the set [0-9a-fA-f]
        /// </exception>
        public static int HexToInt(string hex)
        {
            var len = hex.Length;
            if (len > 16)
            {
                throw new FormatException();
            }
            return Convert.ToInt32(hex);
            // PORT (ok to switch to Convert.ToInt32?):
            //          int l = 0;
            //			for (int i = 0; i < len; i++)
            //			{
            //				l <<= 4;
            //				int c = char.Digit(hex[i], 16);
            //				if (c < 0)
            //				{
            //					throw new FormatException();
            //				}
            //				l |= c;
            //			}
            //			return l;
        }
    }
}