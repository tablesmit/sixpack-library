using System;
using MbUnit.Framework;

namespace SixPack.Validation.UnitTests
{
	[TestFixture]
	public class PatternTests
	{
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void InvalidThrowsException()
		{
			Pattern.Validate("abc", @"^\d$", "param1");
		}

		[Test]
		public void ValidDoesNotThrowException()
		{
			Pattern.Validate("1", @"^\d$", "param1");
		}

		[Test]
		public void NullDoesNotThrowException()
		{
			Pattern.Validate(null, @"^\d$", "param1");
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void InvalidThrowsCorrectException()
		{
			Pattern.Validate("abc", @"^\d$", "param1", message => new InvalidOperationException(message));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException), Description = "empty value")]
		public void InvalidThrowsCorrectMessage()
		{
			Pattern.Validate("abc", @"^\d$", "param1", "empty value");
		}
	}
}