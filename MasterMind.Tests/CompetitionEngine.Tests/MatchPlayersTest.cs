using NUnit.Framework;

namespace MasterMind.CompetitionEngine.Tests
{
    [TestFixture]
    public class MatchPlayersTest
    {
        private StubMatchPlayer[] stubMatchPlayers;

        [SetUp]
        public void SetUp()
        {
            stubMatchPlayers = new StubMatchPlayer[7];
            for (int i = 0; i < stubMatchPlayers.Length; ++i)
            {
                stubMatchPlayers[i] = new StubMatchPlayer();
            }
        }

        [Test]
        public void With_all_players_in_play_InPlay_returns_all_the_players()
        {
            foreach (StubMatchPlayer matchPlayer in stubMatchPlayers)
            {
                matchPlayer.StubPropertyGetGameFinished(false);
            }

            MatchPlayers target = new MatchPlayers(stubMatchPlayers);

            Assert.AreEqual(7, target.InPlay.Count);
        }

        [Test]
        public void With_3_players_in_play_InPlay_returns_only_the_3_players()
        {
            stubMatchPlayers[0].StubPropertyGetGameFinished(true);
            stubMatchPlayers[2].StubPropertyGetGameFinished(true);
            stubMatchPlayers[4].StubPropertyGetGameFinished(true);
            stubMatchPlayers[6].StubPropertyGetGameFinished(true);

            stubMatchPlayers[1].StubPropertyGetGameFinished(false);
            stubMatchPlayers[3].StubPropertyGetGameFinished(false);
            stubMatchPlayers[5].StubPropertyGetGameFinished(false);
            
            MatchPlayers target = new MatchPlayers(stubMatchPlayers);

            Assert.AreEqual(3, target.InPlay.Count);
        }

        [Test]
        public void With_no_players_in_play_InPlay_returns_no_players()
        {
            foreach (StubMatchPlayer matchPlayer in stubMatchPlayers)
            {
                matchPlayer.StubPropertyGetGameFinished(true);
            }

            MatchPlayers target = new MatchPlayers(stubMatchPlayers);

            Assert.AreEqual(0, target.InPlay.Count);
        }

    }
}
