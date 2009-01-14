﻿using System;
using MbUnit.Framework;
using SixPack.Validation.PostSharp;

namespace SixPack.Validation.PostSharp.UnitTests
{
    [TestFixture]
    public class PatternTests
    {
        /// <summary>
        /// Tests the email pattern helper.
        /// </summary>
        /// <param name="email">The email.</param>
        private static void TestEmailPatternHelper([NotNull][Pattern(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")] string email)
        {
            Assert.IsNotNull(email, "It should not be possible to invoke this method passing null.");
        }

        [Test]
        public void EmailPattern()
        {
            TestEmailPatternHelper("nuno.lourenco@fullsix.com");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EmailPatternDefaultException()
        {
            TestEmailPatternHelper("nuno.lourenco%fullsix.com");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EmailPatternNullContent()
        {
            TestEmailPatternHelper(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EmailPatternEmptyContent()
        {
            TestEmailPatternHelper(string.Empty);
        }
    }
}