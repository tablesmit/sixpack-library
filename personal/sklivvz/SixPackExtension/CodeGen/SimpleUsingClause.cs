namespace SixPack.CodeGen
{
	/// <summary>
	/// A class that represents a using clause
	/// </summary>
	public class SimpleUsingClause: AbstractUsingClause
	{
		private readonly string namespaceName;

		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleUsingClause"/> class.
		/// </summary>
		/// <param name="namespaceName">Name of the namespace.</param>
		public SimpleUsingClause(string namespaceName)
		{
			this.namespaceName = namespaceName;
		}

		/// <summary>
		/// Gets the name of the name space.
		/// </summary>
		/// <value>The name of the name space.</value>
		public override string NamespaceName
		{
			get { return namespaceName; }
		}
	}
}