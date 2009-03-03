using System.Collections.Generic;

namespace SixPack.CodeGen
{
	/// <summary>
	/// An abstract class that represents a method
	/// </summary>
	public abstract class AbstractMethod: IClassElement
	{
		/// <summary>
		/// Gets the body.
		/// </summary>
		/// <value>The body.</value>
		public abstract ICollection<string> Body { get; }

		/// <summary>
		/// Gets the parameters.
		/// </summary>
		/// <value>The parameters.</value>
		public abstract ICollection<AbstractParameter> Parameters { get; }

		/// <summary>
		/// Gets the name and return.
		/// </summary>
		/// <value>The name and return.</value>
		public abstract AbstractVariable NameAndReturn { get; }

		/// <summary>
		/// Gets the prefix.
		/// </summary>
		/// <value>The prefix.</value>
		public abstract string Prefix { get; }

		#region IClassElement Members

		/// <summary>
		/// Accepts the specified visitor.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		public virtual void Accept(IClassVisitor visitor)
		{
			if (visitor != null)
				visitor.Visit(this);
		}

		#endregion
	}
}