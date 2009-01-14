using System;
using System.Collections.Generic;
using MbUnit.Framework;
using System.Collections;

namespace SixPack.Validation.UnitTests
{
	[TestFixture]
	public class NotEmptyTests
	{
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void EmptyStringThrowsException()
		{
			NotEmpty.Validate("", "param1");
		}

		[Test]
		public void NonEmptyStringDoesNotThrowException()
		{
			NotEmpty.Validate("not empty", "param1");
		}

		[Test]
		public void NullStringDoesNotThrowException()
		{
			NotEmpty.Validate((string)null, "param1");
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void EmptyStringThrowsCorrectException()
		{
			NotEmpty.Validate("", "param1", message => new InvalidOperationException(message));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException), Description = "empty value")]
		public void EmptyStringThrowsCorrectMessage()
		{
			NotEmpty.Validate("", "param1", "empty value");
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void EmptyCollectionThrowsException()
		{
			NotEmpty.Validate(new ArrayList(), "param1");
		}

		[Test]
		public void NonEmptyCollectionDoesNotThrowException()
		{
			ArrayList notEmpty = new ArrayList();
			notEmpty.Add("item1");
			NotEmpty.Validate(notEmpty, "param1");
		}

		[Test]
		public void NullCollectionDoesNotThrowException()
		{
			NotEmpty.Validate((ICollection)null, "param1");
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void EmptyCollectionThrowsCorrectException()
		{
			NotEmpty.Validate(new ArrayList(), "param1", message => new InvalidOperationException(message));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException), Description = "empty value")]
		public void EmptyCollectionThrowsCorrectMessage()
		{
			NotEmpty.Validate(new ArrayList(), "param1", "empty value");
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void EmptyGenericCollectionThrowsException()
		{
			NotEmpty.Validate((ICollection<string>)new List<string>(), "param1");
		}

		[Test]
		public void NonEmptyGenericCollectionDoesNotThrowException()
		{
			NotEmpty.Validate((ICollection<string>)new List<string> { "item1" }, "param1");
		}

		[Test]
		public void NullGenericCollectionDoesNotThrowException()
		{
			NotEmpty.Validate((ICollection<string>)null, "param1");
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void EmptyGenericCollectionThrowsCorrectException()
		{
			NotEmpty.Validate((ICollection<string>)new List<string>(), "param1", message => new InvalidOperationException(message));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException), Description = "empty value")]
		public void EmptyGenericCollectionThrowsCorrectMessage()
		{
			NotEmpty.Validate((ICollection<string>)new List<string>(), "param1", "empty value");
		}
	}
}