namespace SixPack.CodeGen
{
	/// <summary>
	/// An abstract class that represents a property
	/// </summary>
	public abstract class AbstractProperty: AbstractVariable, IClassElement
	{
		/// <summary>
		/// Gets the getter.
		/// </summary>
		/// <value>The getter.</value>
		public abstract AbstractGetter Getter { get; }

		/// <summary>
		/// Gets the setter.
		/// </summary>
		/// <value>The setter.</value>
		public abstract AbstractSetter Setter { get; }

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