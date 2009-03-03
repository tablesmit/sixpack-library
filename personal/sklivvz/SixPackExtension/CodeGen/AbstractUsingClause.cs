namespace SixPack.CodeGen
{
	/// <summary>
	/// A class that represents a using clause
	/// </summary>
	public abstract class AbstractUsingClause: IClassElement
	{
		/// <summary>
		/// Gets the name of the name space.
		/// </summary>
		/// <value>The name of the name space.</value>
		public abstract string NamespaceName { get; }

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