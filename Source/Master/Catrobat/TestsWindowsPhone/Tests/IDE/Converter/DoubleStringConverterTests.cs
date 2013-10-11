﻿using Catrobat.IDE.Phone.Converters;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Catrobat.IDE.Phone.Tests.Tests.IDE.Converter
{
    [TestClass]
    public class DoubleStringConverterTests
    {
        [TestMethod]
        public void TestStringToDoubleConversion()
        {
            var conv = new DoubleStringConverter();
            object output = conv.ConvertBack((object)"4.2", null, null, null);
            Assert.IsNotNull(output);
            Assert.IsTrue(output is double);
            Assert.AreEqual(4.2d, (double)output);
        }

        [TestMethod]
        public void TestDoubleToStringConversion()
        {
            var conv = new DoubleStringConverter();
            object output = conv.Convert((object)4.2d, null, null, null);
            Assert.IsNotNull(output);
            Assert.IsTrue(output is string);
            Assert.AreEqual("4.2", (string)output);
        }

        [TestMethod]
        public void TestFaultyStringToDoubleConversion()
        {
            var conv = new DoubleStringConverter();
            object output = conv.ConvertBack((object)"4d2", null, 42d, null);
            Assert.IsNotNull(output);
            Assert.IsTrue(output is double);
            Assert.AreEqual(42d, (double)output);
        }
    }
}
