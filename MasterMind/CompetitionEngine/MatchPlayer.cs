using System;

namespace MasterMind.CompetitionEngine
{
	public class MatchPlayer : IMatchPlayer
	{
		private readonly IMasterMindCodeBreaker codeBreaker;
		private readonly TimeSpan timePerGame;
		private readonly IStopWatch stopWatch;

		private bool gameFinished = true;
		private Code lastGuess = new Code();
		private int gameCount = 0;
		private int gameVictoryCount = 0;
		private int matchGuessesCount = 0;


		private delegate void Command();
		public MatchPlayer(IMasterMindCodeBreaker codeBreaker, TimeSpan timePerGame) : this(codeBreaker, timePerGame, new StopWatch())
		{
		}

		public MatchPlayer(IMasterMindCodeBreaker codeBreaker, TimeSpan timePerGame, IStopWatch stopWatch)
		{
			this.codeBreaker = codeBreaker;
			this.timePerGame = timePerGame;
			this.stopWatch = stopWatch;
		}

		public bool GameFinished
		{
			get { return gameFinished; }
		}

		public void StartGame()
		{
			gameCount += 1;
			gameFinished = false;
			stopWatch.Reset();
			ExecuteAndCheckElapsedTimeAndExceptions(() => codeBreaker.StartGame());
		}

		public void GuessCode(int turn, Code challenge)
		{
			ExecuteAndCheckElapsedTimeAndExceptions(() => lastGuess = codeBreaker.GetCodeGuess());

			if (lastGuess == challenge)
			{
				gameVictoryCount += 1;
				matchGuessesCount += turn;
				gameFinished = true;
				EndGame(challenge);
			}
		}

		public void SetGuessResult(Code challenge)
		{
			GuessResult guessResult = GuessResult.Calulate(challenge, lastGuess);

			ExecuteAndCheckElapsedTimeAndExceptions(() => codeBreaker.SetGuessResult(guessResult));
		}

		public void EndGame(Code challenge)
		{
			try
			{
				codeBreaker.GameEnded(challenge);
			}
			catch
			{
			}
		}

		private void Dismiss()
		{
			gameFinished = true;
			codeBreaker.GameDismissed();
		}

		public override string ToString()
		{
			var averageGuessTurn = matchGuessesCount / (double)gameVictoryCount;
			if (double.IsNaN(averageGuessTurn))
			{
				averageGuessTurn = 0;
			}

			var averageVictoryRate = gameVictoryCount / (double)gameCount;
			return
				string.Format("{0} v{1} - Wins:{2}, Average victory rate {3}, Guesses Count:{4}, Average Guess turns:{5}",
								codeBreaker.Name, codeBreaker.Version, gameVictoryCount, averageVictoryRate, matchGuessesCount, averageGuessTurn);
		}


		private void ExecuteAndCheckElapsedTimeAndExceptions(Command command)
		{
			stopWatch.Start();
			try
			{
				command();
			}
			catch
			{
				Dismiss();
				return;
			}
			finally
			{
				stopWatch.Stop();
			}

			if (stopWatch.ElapsedTimeExcedeed(timePerGame))
			{
				Dismiss();
			}

		}

	}
}
