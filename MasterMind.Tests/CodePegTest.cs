using System;
using NUnit.Framework;

namespace MasterMind.Tests
{
    [TestFixture]
    public class CodePegTest
    {
        [Test]
        public void Count_should_be_the_number_of_colors()
        {
            Assert.AreEqual((int)CodePeg.Count, Enum.GetValues(typeof(CodePeg)).Length -1, "Count of the number of colors");
            
        }

        [Test]
        public void Is_zero_based()
        {
            Assert.IsTrue(Enum.IsDefined(typeof(CodePeg), 0), "Exist an enum item with value zero");
        }
    }
}
