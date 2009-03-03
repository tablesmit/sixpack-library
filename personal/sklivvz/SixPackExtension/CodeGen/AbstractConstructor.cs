namespace SixPack.CodeGen
{
	/// <summary>
	/// An abstract class that represents a constructor
	/// </summary>
	public abstract class AbstractConstructor: AbstractMethod
	{
		/// <summary>
		/// Gets the postfix.
		/// </summary>
		/// <value>The postfix.</value>
		public abstract string Postfix { get; }

		/// <summary>
		/// Accepts the specified visitor.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		public override void Accept(IClassVisitor visitor)
		{
			if (visitor != null)
				visitor.Visit(this);
		}
	}
}