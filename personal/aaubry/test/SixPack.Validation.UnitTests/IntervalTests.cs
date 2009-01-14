using System;
using MbUnit.Framework;

namespace SixPack.Validation.UnitTests
{
	[TestFixture]
	public class IntervalTests
	{
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void InvalidThrowsException()
		{
			Interval.Validate(2, 5, 3, "param1");
		}

		[Test]
		public void MinInclusiveWorks()
		{
			Interval.Validate(2, 2, BoundaryMode.Inclusive, 3, BoundaryMode.Inclusive, "param1");
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void MinExclusiveWorks()
		{
			Interval.Validate(2, 2, BoundaryMode.Exclusive, 3, BoundaryMode.Inclusive, "param1");
		}

		[Test]
		public void MaxInclusiveWorks()
		{
			Interval.Validate(3, 2, BoundaryMode.Inclusive, 3, BoundaryMode.Inclusive, "param1");
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void MaxExclusiveWorks()
		{
			Interval.Validate(3, 2, BoundaryMode.Inclusive, 3, BoundaryMode.Exclusive, "param1");
		}

		[Test]
		public void ValidDoesNotThrowException()
		{
			Interval.Validate(2, 1, 3, "param1");
		}

		[Test]
		public void NullDoesNotThrowException()
		{
			Interval.Validate(null, "aaa", "bbb", "param1");
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void InvalidThrowsCorrectException()
		{
			Interval.Validate(2, 5, 3, "param1", message => new InvalidOperationException(message));
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException), Description = "empty value")]
		public void InvalidThrowsCorrectMessage()
		{
			Interval.Validate(2, 5, 3, "param1", "empty value");
		}
	}
}