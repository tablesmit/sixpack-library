namespace SixPack.CodeGen
{
	/// <summary>
	/// An abstract class that represents a namespace.
	/// </summary>
	public abstract class AbstractNamespace: IClassElement
	{
		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public abstract string Name { get; }

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