using System;

namespace SixPack.Validation
{
	/// <summary>
	/// Delegate for a method that creates an exception.
	/// </summary>
	/// <param name="message">The default error message.</param>
	public delegate Exception CreateExceptionFunction(string message);
}