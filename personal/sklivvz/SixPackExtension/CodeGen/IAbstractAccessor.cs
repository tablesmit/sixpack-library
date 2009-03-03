using System.Collections.Generic;

namespace SixPack.CodeGen
{
	/// <summary>
	/// An interface that represents an accessor
	/// </summary>
	public interface IAbstractAccessor
	{
		/// <summary>
		/// Gets the body.
		/// </summary>
		/// <value>The body.</value>
		ICollection<string> Body { get; }

		/// <summary>
		/// Gets the prefix.
		/// </summary>
		/// <value>The prefix.</value>
		string Prefix { get; }
	}
}