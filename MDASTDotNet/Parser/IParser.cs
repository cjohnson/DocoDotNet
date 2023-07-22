namespace MDASTDotNet.Parser;

/// <summary>
/// General parsing interface, defining an interface for trying to deserialize a string target (a line or snippet)
/// into a model type, <typeparamref name="T"/>.
/// <br/>
/// If the parse operation is a success, the <see cref="Parse(string)"/> method returns the model type.
/// If the parse operation is a failure, null is returned.
/// </summary>
/// <typeparam name="T">The type of model that the parser returns on success.</typeparam>
internal interface IParser<T>
{
	/// <summary>
	/// Performs a parse operation with the <paramref name="target"/> as the input text.
	/// <br/>
	/// If the operation is a success, an instance of <typeparamref name="T"/> is returned.
	/// If the operation is a failure, null is returned.
	/// </summary>
	/// <param name="target">The string target to try to deserialize</param>
	/// <returns></returns>
	public T? Parse(string target);
}
