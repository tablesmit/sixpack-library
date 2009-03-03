using System;

namespace SixPack.CodeGen
{
	public class SimpleVariable: AbstractVariable
	{
		private readonly string name;
		private readonly string prefix;
		private readonly Type variableType;

		public SimpleVariable(string name, Type type, string prefix)
		{
			this.name = name;
			this.prefix = prefix;
			variableType = type;
		}

		public override string Name
		{
			get { return name; }
		}

		public override Type VariableType
		{
			get { return variableType; }
		}

		public override string Prefix
		{
			get { return prefix; }
		}
	}
}