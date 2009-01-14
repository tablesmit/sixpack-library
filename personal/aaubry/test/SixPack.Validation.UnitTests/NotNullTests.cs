using System;
using MbUnit.Framework;

namespace SixPack.Validation.UnitTests
{
	[TestFixture]
	public class NotNullTests
	{
		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NullThrowsException()
		{
			NotNull.Validate(null, "param1");
		}

		[Test]
		public void NonNullDoesNotThrowException()
		{
			NotNull.Validate("not a null", "param1");
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NullableNullThrowsException()
		{
			int? x = null;
			NotNull.Validate(x, "param1");
		}

		[Test]
		public void NullableNonNullDoesNotThrowException()
		{
			int? x = 1;
			NotNull.Validate(x, "param1");
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void NullThrowsCorrectException()
		{
			NotNull.Validate(null, "param1", message => new InvalidOperationException(message));
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException), Description = "null value")]
		public void NullThrowsCorrectMessage()
		{
			NotNull.Validate(null, "param1", "null value");
		}
	}
}