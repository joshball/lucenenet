/*
 * This code is derived from MyJavaLibrary (http://somelinktomycoollibrary)
 * 
 * If this is an open source Java library, include the proper license and copyright attributions here!
 */

namespace Lucene.Net.Search.VectorHighlight
{
	/// <summary>IFragListBuilder is an interface for FieldFragList builder classes.</summary>
	/// <remarks>
	/// IFragListBuilder is an interface for FieldFragList builder classes.
	/// A IFragListBuilder class can be plugged in to Highlighter.
	/// </remarks>
	public interface IFragListBuilder
	{
		/// <summary>create a FieldFragList.</summary>
		/// <remarks>create a FieldFragList.</remarks>
		/// <param name="fieldPhraseList">FieldPhraseList object</param>
		/// <param name="fragCharSize">the length (number of chars) of a fragment</param>
		/// <returns>the created FieldFragList object</returns>
		FieldFragList CreateFieldFragList(FieldPhraseList fieldPhraseList, int fragCharSize
			);
	}
}
