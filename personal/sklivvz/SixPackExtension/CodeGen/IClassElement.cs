namespace SixPack.CodeGen
{
	/// <summary>
	/// Classes implementing this interface will accept a Class Visitor for rendering
	/// </summary>
	public interface IClassElement
	{
		/// <summary>
		/// Accepts the specified visitor.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		void Accept(IClassVisitor visitor);
	}
}