using System.Collections.Generic;

namespace SixPack.CodeGen
{
	public class SimpleSetter: AbstractSetter
	{
		private readonly ICollection<string> body = new List<string>();
		private readonly string prefix;

		public SimpleSetter(string prefix)
		{
			this.prefix = prefix;
		}

		public SimpleSetter(): this(string.Empty)
		{
		}

		public override ICollection<string> Body
		{
			get { return body; }
		}

		public override string Prefix
		{
			get { return prefix; }
		}
	}
}