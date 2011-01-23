using NUnit.Framework;

namespace MasterMind.Tests
{

    [TestFixture]
    public class GuessResultTest
    {
        [Test]
        public void The_correct_guess_has_all_pegs_with_rigth_colors_in_the_right_position()
        {
            Code challege = new Code(CodePeg.Purple, CodePeg.Green, CodePeg.Green, CodePeg.Yellow);
            Code rightGuess = new Code(CodePeg.Purple, CodePeg.Green, CodePeg.Green, CodePeg.Yellow);

            GuessResult result = GuessResult.Calulate(challege, rightGuess);

            Assert.AreEqual(Code.Size, result.NumberOfPegsWithCorrectColorAndPosition, "Number of pegs with right color and position");
            Assert.AreEqual(0, result.NumberOfPegsWithCorrectColorAndWrongPosition, "Number of pegs with right color and wrong position");
        }

        [Test]
        public void A_guess_with_all_wrong_colors_scores_zero()
        {
            Code challege = new Code(CodePeg.Purple, CodePeg.Green, CodePeg.Green, CodePeg.Yellow);
            Code wrongGuess = new Code(CodePeg.White, CodePeg.Blue, CodePeg.Orange, CodePeg.Orange);

            GuessResult result = GuessResult.Calulate(challege, wrongGuess);

            Assert.AreEqual(0, result.NumberOfPegsWithCorrectColorAndPosition, "Number of pegs with right color and position");
            Assert.AreEqual(0, result.NumberOfPegsWithCorrectColorAndWrongPosition, "Number of pegs with right color and wrong position");
        }

        [Test]
        public void With_Challenge_WYOG_the_guess_WBYG_scores_2_1()
        {
            Code challege = new Code(CodePeg.White, CodePeg.Yellow, CodePeg.Orange, CodePeg.Green);
            Code guess = new Code(CodePeg.White, CodePeg.Blue, CodePeg.Yellow, CodePeg.Green);

            GuessResult result = GuessResult.Calulate(challege, guess);

            Assert.AreEqual(2, result.NumberOfPegsWithCorrectColorAndPosition, "Number of pegs with right color and position");
            Assert.AreEqual(1, result.NumberOfPegsWithCorrectColorAndWrongPosition, "Number of pegs with right color and wrong position");
        }

        [Test]
        public void With_a_challenge_with_a_double_color_a_guess_with_one_in_the_right_place_and_another_in_the_wrong_scores_1_1()
        {
            Code challege = new Code(CodePeg.White, CodePeg.Yellow, CodePeg.Orange, CodePeg.White);
            Code guess = new Code(CodePeg.White, CodePeg.Blue, CodePeg.White, CodePeg.Green);

            GuessResult result = GuessResult.Calulate(challege, guess);

            Assert.AreEqual(1, result.NumberOfPegsWithCorrectColorAndPosition, "Number of pegs with right color and position");
            Assert.AreEqual(1, result.NumberOfPegsWithCorrectColorAndWrongPosition, "Number of pegs with right color and wrong position");
        }

        [Test]
        public void A_guess_with_a_double_color_and_only_one_in_the_challenge_in_the_wrong_position_scores_0_1()
        {
            Code challege = new Code(CodePeg.White, CodePeg.Yellow, CodePeg.Orange, CodePeg.Green);
            Code guess = new Code(CodePeg.Purple, CodePeg.White, CodePeg.White, CodePeg.Purple);

            GuessResult result = GuessResult.Calulate(challege, guess);

            Assert.AreEqual(0, result.NumberOfPegsWithCorrectColorAndPosition, "Number of pegs with right color and position");
            Assert.AreEqual(1, result.NumberOfPegsWithCorrectColorAndWrongPosition, "Number of pegs with right color and wrong position");
        }

    }
}
