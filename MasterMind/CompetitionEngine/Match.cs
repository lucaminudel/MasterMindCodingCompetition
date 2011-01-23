using System;

namespace MasterMind.CompetitionEngine
{
	public class Match
	{
		public const int MaximumTurnsPerGame = 7;

		private readonly MatchPlayers matchPlayers;
		private readonly ICodeMaker codeMaker;


		public Match(IMasterMindCodeBreaker[] codeBreakers, TimeSpan timePerGame)
			: this(codeBreakers, timePerGame, new CodeMaker())
		{
		}

		public Match(IMasterMindCodeBreaker[] codeBreakers, TimeSpan timePerGame, ICodeMaker codeMaker)
		{
			if (codeBreakers == null)
			{
				throw new ArgumentNullException("codeBreakers");
			}

			if(codeMaker == null)
			{
				throw new ArgumentNullException("codeMaker");
			}

			this.codeMaker = codeMaker;
			matchPlayers = new MatchPlayers(codeBreakers, timePerGame);
		}


		public void PlayGames(int gamesCount, out string result)
		{
			for (int game = gamesCount; game > 0; --game)
			{
#if FRAMEWORK_DEBUG
				Console.WriteLine("  game n." + (gamesCount - game +1));
#endif
				PlayOneGame();
			}

			string playersResults = string.Empty;
			matchPlayers.ForEach(delegate(IMatchPlayer matchPlayer)
									 {
										 playersResults += matchPlayer.ToString() + "\n";
									 });

			result = playersResults.TrimEnd('\n');
		}

		private void PlayOneGame()
		{
			Code challenge = codeMaker.CreateCodeChallenge();

			matchPlayers.ForEach(matchPlayer => matchPlayer.StartGame());



			for(int turn = 1; turn <= MaximumTurnsPerGame; ++turn)
			{
				int currentTurn = turn;
				matchPlayers.InPlay.ForEach(matchPlayer => matchPlayer.GuessCode(currentTurn, challenge));

				matchPlayers.InPlay.ForEach(matchPlayer => matchPlayer.SetGuessResult(challenge));
			}


			matchPlayers.InPlay.ForEach(matchPlayer => matchPlayer.EndGame(challenge));
		}
	}
}
