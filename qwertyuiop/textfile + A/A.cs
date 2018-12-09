namespace qwertyuiop
{
	internal class A
	{
		public int Id;

		public string ToString(string format)
		{
			return string.Format(base.ToString(), format);
		}
	}
}