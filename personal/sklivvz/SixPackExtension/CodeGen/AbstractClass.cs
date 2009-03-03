using System.Collections.Generic;

namespace SixPack.CodeGen
{
	/// <summary>
	/// A class that represents a class.
	/// </summary>
	public abstract class AbstractClass: IClassElement
	{
		/// <summary>
		/// Gets the fields.
		/// </summary>
		/// <value>The fields.</value>
		public abstract ICollection<AbstractField> Fields { get; }

		/// <summary>
		/// Gets the properties.
		/// </summary>
		/// <value>The properties.</value>
		public abstract ICollection<AbstractProperty> Properties { get; }

		/// <summary>
		/// Gets the methods.
		/// </summary>
		/// <value>The methods.</value>
		public abstract ICollection<AbstractMethod> Methods { get; }

		/// <summary>
		/// Gets the constructors.
		/// </summary>
		/// <value>The constructors.</value>
		public abstract ICollection<AbstractConstructor> Constructors { get; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public abstract string Name { get; protected set; }

		/// <summary>
		/// Gets or sets the prefix.
		/// </summary>
		/// <value>The prefix.</value>
		public abstract string Prefix { get; protected set; }

		/// <summary>
		/// Gets or sets the postfix.
		/// </summary>
		/// <value>The postfix.</value>
		public abstract string Postfix { get; protected set; }

		/// <summary>
		/// Gets or sets the namespace.
		/// </summary>
		/// <value>The namespace.</value>
		public abstract AbstractNamespace NamespaceDefinition { get; protected set; }

		/// <summary>
		/// Gets the using clauses.
		/// </summary>
		/// <value>The using clauses.</value>
		public abstract ICollection<AbstractUsingClause> UsingClauses { get; }

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