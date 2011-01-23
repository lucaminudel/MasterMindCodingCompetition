using System;
using System.Collections.Generic;

namespace MasterMind.CompetitionEngine
{
	public class MatchPlayers : List<IMatchPlayer>
	{
		public MatchPlayers(IMasterMindCodeBreaker[] codeBreakers, TimeSpan timePerGame)
			: this(GetMatchPalyers(codeBreakers, timePerGame))
		{
		}

		public MatchPlayers(IEnumerable<IMatchPlayer> matchPlayers)
		{
			if (matchPlayers == null)
			{
				throw new ArgumentNullException();
			}

			AddRange(matchPlayers);
		}


		public List<IMatchPlayer> InPlay
		{
			get
			{
				return FindAll(matchPlayer => matchPlayer.GameFinished == false);

			}
		}

		private static IEnumerable<IMatchPlayer> GetMatchPalyers(IMasterMindCodeBreaker[] codeBreakers, TimeSpan timePerGame)
		{
			if (codeBreakers == null)
			{
				throw new ArgumentNullException();
			}

			IMatchPlayer[] matchPlayers = new MatchPlayer[codeBreakers.Length];

			for (int i = 0; i < codeBreakers.Length; ++i)
			{
				matchPlayers[i] = new MatchPlayer(codeBreakers[i], timePerGame);
			}

			return matchPlayers;
		}

	}
}
