using System;

using NUnit.Framework;

using MasterMind.Tests;

namespace MasterMind.CompetitionEngine.Tests
{
    [TestFixture]
    public class MatchPlayer_GameFinishedTest
    {
        TimeSpan infiniteTimePerGame;
        StubCodeBreaker stubCodeBreaker;


        [SetUp]
        public void SetUp()
        {
            infiniteTimePerGame = new TimeSpan(99999, 24, 60, 60, 1000);
            stubCodeBreaker = new StubCodeBreaker();
        }

        [Test]
        public void StartGame_set_GameFinished_to_false()
        {
            IMatchPlayer target = new MatchPlayer(stubCodeBreaker, infiniteTimePerGame);

            target.StartGame();

            Assert.IsFalse(target.GameFinished, "Game finished");
        }

        [Test]
        public void After_the_time_run_out_gameFinished_is_true()
        {
            TimeSpan waitTime = new TimeSpan(0, 0, 0, 0, 100);
            stubCodeBreaker.StubCallToGetGuessCode(waitTime);

            TimeSpan timePerGame = new TimeSpan(0, 0, 0, 0, 10);
            IMatchPlayer target = new MatchPlayer(stubCodeBreaker, timePerGame);


            target.StartGame();
            target.GuessCode(0, Code.Empty);


            Assert.IsTrue(target.GameFinished);
        }

        [Test]
        public void After_codeBreaker_raise_an_exception_gameFinished_is_true()
        {
            const bool raiseAnException = true;
            stubCodeBreaker.StubCallToGetGuessCode(raiseAnException);
            IMatchPlayer target = new MatchPlayer(stubCodeBreaker, infiniteTimePerGame);

            target.StartGame();
            target.GuessCode(0, Code.Empty);

            Assert.IsTrue(target.GameFinished);
        }

    }
}
