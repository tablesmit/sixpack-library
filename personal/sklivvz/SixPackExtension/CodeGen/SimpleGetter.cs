using System.Collections.Generic;

namespace SixPack.CodeGen
{
	public class SimpleGetter: AbstractGetter
	{
		private readonly ICollection<string> body = new List<string>();
		private readonly string prefix;

		public SimpleGetter(string prefix)
		{
			this.prefix = prefix;
		}

		public SimpleGetter(): this(string.Empty)
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