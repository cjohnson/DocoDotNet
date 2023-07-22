﻿using MDASTDotNet.LeafBlocks;

namespace MDASTDotNet.Parser;

/// <summary>
/// General parsing interface, defining an interface for trying to deserialize line-by-line content
/// into a model type, <typeparamref name="T"/>.
/// <br/>
/// If the parse operation is a success, the <see cref="Parse(string)"/> method returns the model type.
/// If the parse operation is a failure, null is returned.
/// </summary>
/// <typeparam name="T">The type of model that the parser returns on success.</typeparam>
public interface IParser
{
	/// <summary>
	/// Performs a parse operation with the <paramref name="target"/> as the input text.
	/// <br/>
	/// If the operation is a success, an instance of <typeparamref name="T"/> is returned.
	/// If the operation is a failure, null is returned.
	/// </summary>
	/// <param name="contentLines">The remaining content (list of string lines) in the document</param>
	/// <returns></returns>
	public INode? Parse(List<string> contentLines);
}
