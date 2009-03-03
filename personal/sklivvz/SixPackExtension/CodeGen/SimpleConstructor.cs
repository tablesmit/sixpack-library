using System.Collections.Generic;

namespace SixPack.CodeGen
{
	public class SimpleConstructor: AbstractConstructor
	{
		private readonly List<string> body = new List<string>();
		private readonly SimpleVariable nameAndReturn;
		private readonly List<AbstractParameter> parameters = new List<AbstractParameter>();
		private readonly string postfix;
		private readonly string prefix;

		public SimpleConstructor(string name, string prefix, string postfix)
		{
			this.prefix = prefix;
			this.postfix = postfix;
			nameAndReturn = new SimpleVariable(name, null, null);
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

		public override string Postfix
		{
			get { return postfix; }
		}
	}
}