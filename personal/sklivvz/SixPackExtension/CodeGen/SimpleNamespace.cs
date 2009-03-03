namespace SixPack.CodeGen
{
	public class SimpleNamespace: AbstractNamespace
	{
		private readonly string name;

		public SimpleNamespace(string name)
		{
			this.name = name;
		}

		public override string Name
		{
			get { return name; }
		}
	}
}