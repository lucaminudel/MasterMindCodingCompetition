using System;
using NUnit.Framework;

using MasterMind.Tests;

namespace MasterMind.CompetitionEngine.Tests
{
    [TestFixture]
    public class MatchPlayerTest
    {
        TimeSpan infiniteTimePerGame;
        MockCodeBreaker mockCodeBreaker;


        [SetUp]
        public void SetUp()
        {
            infiniteTimePerGame = new TimeSpan(99999, 24, 60, 60, 1000);
            mockCodeBreaker = new MockCodeBreaker();
        }

        [Test]
        public void StartGame_calls_codeBreaker_StartGame()
        {
            mockCodeBreaker.SetExpectedCallToStartGame();
            IMatchPlayer target = new MatchPlayer(mockCodeBreaker, infiniteTimePerGame);

            target.StartGame();

            mockCodeBreaker.VerifyAllExpectations();
        }

        [Test]
        public void GuessCode_calls_codeBreaker_GuessCode_with_the_challenge()
        {
            Code challenge = Code.Empty;
            Code guess = new Code(CodePeg.Purple, CodePeg.Yellow, CodePeg.Green, CodePeg.Green);
            mockCodeBreaker.SetExpectedCallToGetGuessCode(guess);
            IMatchPlayer target = new MatchPlayer(mockCodeBreaker, infiniteTimePerGame);
            
            target.GuessCode(0, challenge);

            mockCodeBreaker.VerifyAllExpectations();
        }

        [Test]
        public void When_GuessCode_guess_the_right_challenge_the_game_end()
        {
            Code challenge = new Code(CodePeg.Purple, CodePeg.Yellow, CodePeg.Green, CodePeg.Green);
            Code guess = challenge;
            mockCodeBreaker.SetExpectedCallToGetGuessCode(guess);
            mockCodeBreaker.SetExpectedCallToGameEnded(challenge);            
            IMatchPlayer target = new MatchPlayer(mockCodeBreaker, infiniteTimePerGame);

            target.GuessCode(0, challenge);

            mockCodeBreaker.VerifyAllExpectations();
        }

        [Test]
        public void SetGuessResult_calls_codeBreaker_SetGuessResul_with_the_result_of_the_last_guess_try()
        {
            Code challenge = new Code(CodePeg.Purple, CodePeg.Yellow, CodePeg.White, CodePeg.Green);
            Code guess = new Code(CodePeg.Purple, CodePeg.Blue, CodePeg.Blue, CodePeg.Yellow);
            mockCodeBreaker.SetExpectedCallToGetGuessCode(guess);
            GuessResult guessResult = new GuessResult(1, 1);
            mockCodeBreaker.SetExpectedCallToSetGuessResult(guessResult);
            IMatchPlayer target = new MatchPlayer(mockCodeBreaker, infiniteTimePerGame);

            target.GuessCode(0, challenge);
            target.SetGuessResult(challenge);

            mockCodeBreaker.VerifyAllExpectations();
        }

        [Test]
        public void When_the_time_run_out_codeBreaker_GameDismissed_is_called()
        {
            TimeSpan waitTime = new TimeSpan(0, 0, 0, 0, 100);
            mockCodeBreaker.SetExpectedCallToGetGuessCode(waitTime);
            mockCodeBreaker.SetExpectedCallToGameDismissed();
            Code challenge = new Code(CodePeg.Purple, CodePeg.Yellow, CodePeg.White, CodePeg.Green);
            TimeSpan timePerGame = new TimeSpan(0, 0, 0, 0, 10);
            IMatchPlayer target = new MatchPlayer(mockCodeBreaker, timePerGame);


            target.GuessCode(0, challenge);


            mockCodeBreaker.VerifyAllExpectations();            
        }

        [Test]
        public void When_a_codeBreaker_method_raise_an_exception_codeBreaker_GameDismissed_is_called()
        {
            const bool raiseAnException = true;
            mockCodeBreaker.SetExpectedCallToGetGuessCode(raiseAnException);
            mockCodeBreaker.SetExpectedCallToGameDismissed();
            Code challenge = new Code(CodePeg.Purple, CodePeg.Yellow, CodePeg.White, CodePeg.Green);
            IMatchPlayer target = new MatchPlayer(mockCodeBreaker, infiniteTimePerGame);


            target.GuessCode(0, challenge);


            mockCodeBreaker.VerifyAllExpectations();
        }

        [Test]
        public void When_a_codeBreaker_EndGame_method_raise_an_exception_it_is_ignored()
        {
            const bool raiseAnException = true;
            mockCodeBreaker.SetExpectedCallToGameEnded(raiseAnException);
            Code challenge = new Code(CodePeg.Purple, CodePeg.Yellow, CodePeg.White, CodePeg.Green);
            IMatchPlayer target = new MatchPlayer(mockCodeBreaker, infiniteTimePerGame);


            target.EndGame(challenge);


            mockCodeBreaker.VerifyAllExpectations();
        }


        [Test]
        public void StartGame_reset_the_timer()
        {
            StubCodeBreaker stubCodeBreaker = new StubCodeBreaker();
            MockStopWatch mockStopWatch = new MockStopWatch();
            mockStopWatch.SetExpectedCallToReset();
            IMatchPlayer target = new MatchPlayer(stubCodeBreaker, infiniteTimePerGame, mockStopWatch);

            target.StartGame();

            mockStopWatch.VerifyAllExpectations();
        }

    }
}
