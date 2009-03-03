using System;

namespace SixPack.CodeGen
{
	public class SimpleProperty: AbstractProperty
	{
		private readonly AbstractGetter getter;
		private readonly string name;
		private readonly string prefix;
		private readonly AbstractSetter setter;
		private readonly Type variableType;

		public SimpleProperty(string name, Type type, string prefix, AbstractGetter getter, AbstractSetter setter)
		{
			this.name = name;
			this.setter = setter;
			this.getter = getter;
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

		public override AbstractGetter Getter
		{
			get { return getter; }
		}

		public override AbstractSetter Setter
		{
			get { return setter; }
		}
	}
}