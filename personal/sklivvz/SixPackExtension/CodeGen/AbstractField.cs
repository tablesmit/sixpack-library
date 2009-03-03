namespace SixPack.CodeGen
{
	/// <summary>
	/// An abstract class that represents a field
	/// </summary>
	public abstract class AbstractField: AbstractVariable, IClassElement
	{
		#region IClassElement Members

		/// <summary>
		/// Accepts the specified visitor.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		public void Accept(IClassVisitor visitor)
		{
			if (visitor != null)
				visitor.Visit(this);
		}

		#endregion
	}
}