namespace SixPack.CodeGen
{
	/// <summary>
	/// An interface that represents class visitors. 
	/// These classes can take an abstract class and 
	/// represent it in another form, like a source code file.
	/// </summary>
	public interface IClassVisitor
	{
		/// <summary>
		/// Visits the specified class.
		/// </summary>
		/// <param name="concreteClass">The class.</param>
		void Visit(AbstractClass concreteClass);

		/// <summary>
		/// Visits the specified constructor.
		/// </summary>
		/// <param name="constructor">The constructor.</param>
		void Visit(AbstractConstructor constructor);

		/// <summary>
		/// Visits the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		void Visit(AbstractMethod method);

		/// <summary>
		/// Visits the specified field.
		/// </summary>
		/// <param name="field">The field.</param>
		void Visit(AbstractField field);

		/// <summary>
		/// Visits the specified property.
		/// </summary>
		/// <param name="concreteProperty">The property.</param>
		void Visit(AbstractProperty concreteProperty);

		/// <summary>
		/// Visits the specified parameter.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		void Visit(AbstractParameter parameter);

		/// <summary>
		/// Visits the specified clause.
		/// </summary>
		/// <param name="clause">The clause.</param>
		void Visit(AbstractUsingClause clause);

		/// <summary>
		/// Visits the specified name space.
		/// </summary>
		/// <param name="concreteNamespace">The name space.</param>
		void Visit(AbstractNamespace concreteNamespace);

		/// <summary>
		/// Visits the specified getter.
		/// </summary>
		/// <param name="getter">The getter.</param>
		void Visit(AbstractGetter getter);

		/// <summary>
		/// Visits the specified setter.
		/// </summary>
		/// <param name="setter">The setter.</param>
		void Visit(AbstractSetter setter);
	}
}