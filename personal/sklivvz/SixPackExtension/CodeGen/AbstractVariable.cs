using System;

namespace SixPack.CodeGen
{
	/// <summary>
	/// A class that represents a variable (field, property, parameter)
	/// </summary>
	public abstract class AbstractVariable
	{
		public abstract string Name { get; }
		public abstract Type VariableType { get; }
		public abstract string Prefix { get; }
	}
}