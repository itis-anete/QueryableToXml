using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using qwertyuiop;

namespace NET_19_11_2018
{
	[TestFixture]
	public class xmltests
	{
		[Test]
		public void NoMethods()
		{
			var result = new XmlQueryable().Select(x => XmlQueryable.All);

			result.Should().BeEquivalentTo(new List<D4>
			{
				new D4("d4n0n-myself", "Go fuck yourself!"),
				new D4("d4n0n-myself", "Is this loss?"),
				new D4("halloneo", "hello, halloneo!")
			});
		}

		[Test]
		public void Select()
		{
			var result = new XmlQueryable().Select(x => XmlQueryable.All);

			result.Should().BeEquivalentTo(new List<D4>
			{
				new D4("d4n0n-myself", "Go fuck yourself!"),
				new D4("d4n0n-myself", "Is this loss?"),
				new D4("halloneo", "hello, halloneo!")
			});
		}

		[Test]
		public void Where()
		{
			var result = new XmlQueryable().Where(x => x.Name == "d4n0n-myself");

			result.Should().BeEquivalentTo(new List<D4>
			{
				new D4("d4n0n-myself", "Go fuck yourself!"),
				new D4("d4n0n-myself", "Is this loss?")
			});
		}

		[Test]
		public void TwoWhere()
		{
			var result = new XmlQueryable().Select(x => XmlQueryable.All)
				.Where(pair => pair.Name == "d4n0n-myself")
				.Where(pair => pair.Value == "Is this loss?");

			result.Should().BeEquivalentTo(new List<D4>
			{
				new D4("d4n0n-myself", "Is this loss?")
			});
		}

		[Test]
		public void SelectAndWhere()
		{
			var result = new XmlQueryable()
				.Select(x => XmlQueryable.All)
				.Where(pair => pair.Name == "d4n0n-myself");

			result.Should().BeEquivalentTo(new List<D4>
			{
				new D4("d4n0n-myself", "Go fuck yourself!"),
				new D4("d4n0n-myself", "Is this loss?")
			});
		}
	}
}