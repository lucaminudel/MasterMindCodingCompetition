using System;

using NUnit.Framework;

using MasterMind.Tests;

namespace MasterMind.CompetitionEngine.Tests
{
    [TestFixture]
    public class MatchTest
    {
        private StubCodeMaker stubCodeMaker;
        private StubCodeBreaker stubCodeBreakerA;
        private StubCodeBreaker stubCodeBreakerB;
        private TimeSpan infiniteTimePerGame;

        private Code challenge;
        private Code wrongGuess;


        [SetUp]
        public void SetUp()
        {
            stubCodeMaker = new StubCodeMaker();

            stubCodeBreakerA = new StubCodeBreaker();
            stubCodeBreakerA.StubPropetyGetName("Code breaker A");
            stubCodeBreakerA.StubPropertyGetVersion(new Version(1,0));

            stubCodeBreakerB = new StubCodeBreaker();
            stubCodeBreakerB.StubPropetyGetName("Code breaker B");
            stubCodeBreakerB.StubPropertyGetVersion(new Version(2, 0));

            infiniteTimePerGame = new TimeSpan(999, 24, 60, 60, 1000);

            challenge = new Code(CodePeg.White, CodePeg.Yellow, CodePeg.Orange, CodePeg.Green);
            wrongGuess = new Code(CodePeg.Blue, CodePeg.Blue, CodePeg.Blue, CodePeg.Blue);

        }

        [Test]
        public void One_player_one_game_result()
        {
            stubCodeMaker.StubSequenceOfCallsToCreateCodeChallenge(new Code[] { challenge });
            IMasterMindCodeBreaker[] codeBreakers = new IMasterMindCodeBreaker[] { stubCodeBreakerA };

            Code[] returnCodes = new Code[Match.MaximumTurnsPerGame];
            for(int i=0; i < Match.MaximumTurnsPerGame -1; ++i)
            {
                returnCodes[i] = wrongGuess;
            }
            returnCodes[Match.MaximumTurnsPerGame - 1] = challenge;

            stubCodeBreakerA.StubSequenceOfCallsToGetGuessCode(returnCodes);
            Match target = new Match(codeBreakers, infiniteTimePerGame, stubCodeMaker);

            string results;
            target.PlayGames(1, out results);

            Assert.AreEqual(string.Format("Code breaker A v1.0 - Wins:1, Average victory rate 1, Guesses Count:{0}, Average Guess turns:{0}", Match.MaximumTurnsPerGame), results);
        }


        [Test]
        public void Two_players_one_game_result()
        {
            stubCodeMaker.StubSequenceOfCallsToCreateCodeChallenge(new Code[] { challenge });
            IMasterMindCodeBreaker[] codeBreakers = new IMasterMindCodeBreaker[] { stubCodeBreakerA, stubCodeBreakerB };

            Code[] returnCodesA = new Code[Match.MaximumTurnsPerGame];
            Code[] returnCodesB = new Code[Match.MaximumTurnsPerGame];
            for (int i = 0; i < Match.MaximumTurnsPerGame; ++i)
            {
                returnCodesA[i] = wrongGuess;
                returnCodesB[i] = wrongGuess;
            }
            returnCodesA[Match.MaximumTurnsPerGame - 1] = challenge;

            stubCodeBreakerA.StubSequenceOfCallsToGetGuessCode(returnCodesA);
            stubCodeBreakerB.StubSequenceOfCallsToGetGuessCode(returnCodesB);
            Match target = new Match(codeBreakers, infiniteTimePerGame, stubCodeMaker);

            string results;
            target.PlayGames(1, out results);

            Assert.AreEqual(string.Format("Code breaker A v1.0 - Wins:1, Average victory rate 1, Guesses Count:{0}, Average Guess turns:{0}\n", Match.MaximumTurnsPerGame) +
                            "Code breaker B v2.0 - Wins:0, Average victory rate 0, Guesses Count:0, Average Guess turns:0"
                            , results);
        }

        [Test]
        public void Two_players_two_games_result()
        {
            stubCodeMaker.StubSequenceOfCallsToCreateCodeChallenge(new Code[] { challenge, challenge });
            IMasterMindCodeBreaker[] codeBreakers = new IMasterMindCodeBreaker[] { stubCodeBreakerA, stubCodeBreakerB };

            Code[] returnCodesA = new Code[Match.MaximumTurnsPerGame +2];
            Code[] returnCodesB = new Code[Match.MaximumTurnsPerGame +2];
            for (int i = 0; i < Match.MaximumTurnsPerGame +2; ++i)
            {
                returnCodesA[i] = wrongGuess;
                returnCodesB[i] = wrongGuess;
            }
            returnCodesA[Match.MaximumTurnsPerGame - 1] = challenge;

            returnCodesA[Match.MaximumTurnsPerGame + 1] = challenge;
            returnCodesB[Match.MaximumTurnsPerGame + 1] = challenge;

            stubCodeBreakerA.StubSequenceOfCallsToGetGuessCode(returnCodesA);
            stubCodeBreakerB.StubSequenceOfCallsToGetGuessCode(returnCodesB);
            Match target = new Match(codeBreakers, infiniteTimePerGame, stubCodeMaker);

            string results;
            target.PlayGames(2, out results);

            Assert.AreEqual(string.Format("Code breaker A v1.0 - Wins:{0}, Average victory rate {1}, Guesses Count:{2}, Average Guess turns:{3}\n", 2, 1, Match.MaximumTurnsPerGame + 2, (Match.MaximumTurnsPerGame + 2) / 2.0) +
                            string.Format("Code breaker B v2.0 - Wins:{0}, Average victory rate {1}, Guesses Count:{2}, Average Guess turns:{3}", 1, 1/2.0, 2, 2) 
                            , results);
        }

    }
}
