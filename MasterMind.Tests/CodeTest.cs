using System;
using NUnit.Framework;

namespace MasterMind.Tests
{
    [TestFixture]
    public class CodeTest
    {
        [Test]
        public void Empty_codes_are_equals()
        {
            Code anEmptyCode = new Code();
            Assert.IsTrue(anEmptyCode == Code.Empty, "Equality of 2 distinct non empty codes");
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Construction_of_the_code_with_the_wrong_size_raise_an_exception()
        {
            CodePeg[] tooBigCodePegArray = new CodePeg[Code.Size + 1];
            new Code(tooBigCodePegArray);
        }

        [Test]
        public void Empty_code_is_not_equal_to_a_non_empty_code()
        {
            Code aNonEmptyCode = new Code(CodePeg.White, CodePeg.Yellow, CodePeg.Orange, CodePeg.Green);
            Assert.IsFalse(aNonEmptyCode == Code.Empty, "Equality of an empty code with a non empty code");
        }

        [Test]
        public void Two_codes_with_same_colors_in_different_positions_are_not_equals()
        {
            Code codeAlfa = new Code(CodePeg.White, CodePeg.Yellow, CodePeg.Orange, CodePeg.Green);
            Code permutatuionOfCodeAlfa = new Code(CodePeg.White, CodePeg.Yellow, CodePeg.Green, CodePeg.Orange);
            Assert.IsFalse(codeAlfa == permutatuionOfCodeAlfa, "Equality of a code and his permutation");
        }

        [Test]
        public void Two_codes_with_same_colors_in_the_same_positions_are_equals()
        {
            Code codeAlfa = new Code(CodePeg.White, CodePeg.Yellow, CodePeg.Green, CodePeg.Orange);
            Code codeBeta = new Code(CodePeg.White, CodePeg.Yellow, CodePeg.Green, CodePeg.Orange);
            Assert.IsTrue(codeAlfa == codeBeta, "Equality of 2 identical codes");
        }

        [Test]
        public void Colors_count_reflect_the_number_of_pegs_in_the_cose()
        {
            Code code = new Code(CodePeg.White, CodePeg.White, CodePeg.White, CodePeg.Green);

            Assert.AreEqual(3, code[CodePeg.White], "Number of Whits");
            Assert.AreEqual(0, code[CodePeg.Yellow], "Number of Yellows");
            Assert.AreEqual(0, code[CodePeg.Orange], "Number of Oranges");
            Assert.AreEqual(1, code[CodePeg.Green], "Number of Greens");
            Assert.AreEqual(0, code[CodePeg.Blue], "Number of Blues");
            Assert.AreEqual(0, code[CodePeg.Purple], "Number of Purples");
        }
    }
}
