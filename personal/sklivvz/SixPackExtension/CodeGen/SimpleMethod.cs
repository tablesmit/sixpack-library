using System;
using System.Collections.Generic;

namespace SixPack.CodeGen
{
	public class SimpleMethod: AbstractMethod
	{
		private readonly List<string> body = new List<string>();
		private readonly SimpleVariable nameAndReturn;
		private readonly List<AbstractParameter> parameters = new List<AbstractParameter>();
		private readonly string prefix;

		public SimpleMethod(string name, string prefix, Type type)
		{
			this.prefix = prefix;
			nameAndReturn = new SimpleVariable(name, type, null);
		}

		public override ICollection<string> Body
		{
			get { return body; }
		}

		public override ICollection<AbstractParameter> Parameters
		{
			get { return parameters; }
		}

		public override AbstractVariable NameAndReturn
		{
			get { return nameAndReturn; }
		}

		public override string Prefix
		{
			get { return prefix; }
		}
	}
}