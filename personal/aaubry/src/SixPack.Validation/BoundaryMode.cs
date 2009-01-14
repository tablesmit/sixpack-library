using System;

namespace SixPack.Validation
{
	/// <summary>
	/// Specifies whether a value is inclusive or exclusive.
	/// </summary>
	public enum BoundaryMode
	{
		/// <summary>
		/// Indicates that a value is inclusive.
		/// </summary>
		Inclusive,

		/// <summary>
		/// Indicates that a value is exclusive.
		/// </summary>
		Exclusive
	}
}