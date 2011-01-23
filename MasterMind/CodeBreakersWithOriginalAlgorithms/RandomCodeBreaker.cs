using System;
using System.Security.Cryptography;

namespace MasterMind.CodeBreakersWithOriginalAlgorithms
{
	public class RandomCodeBreaker : IMasterMindCodeBreaker
	{
		private readonly RNGCryptoServiceProvider laDeaBendata = new RNGCryptoServiceProvider();
		private Code lastGuess = Code.Empty;

		public string Name
		{
			get { return "Random"; }
		}

		public Version Version
		{
			get { return new Version(0, 3); }
		}

		public void StartGame()
		{
#if RANDOM_DEBUG
			Console.WriteLine(" > New Game start");
#endif
		}

		public Code GetCodeGuess()
		{
			CodePeg[] randomPegs = new CodePeg[4];
			var randomBytes = new byte[4];
			laDeaBendata.GetBytes(randomBytes);
			for (int i = 0; i < 4; ++i)
			{
				randomPegs[i] = (CodePeg)(randomBytes[i] % (int)CodePeg.Count);
			}

			Code guess = new Code(randomPegs);

#if RANDOM_DEBUG
			Console.Write("   Guess: " + guess);
#endif
			lastGuess = guess;
			return guess;
		}

		public void SetGuessResult(GuessResult guessResult)
		{
#if RANDOM_DEBUG
			Console.WriteLine(" -> " + guessResult);
#endif
		}

		public void GameDismissed()
		{
#if RANDOM_DEBUG
			Console.WriteLine(" > Game Dismissed: time-out or exception !");
#endif
		}

		public void GameEnded(Code challenge)
		{
#if RANDOM_DEBUG
			if (lastGuess == challenge)
			{
				Console.WriteLine(" => !!!!! WIN !!!!!! ");
				Console.WriteLine(" > Game finished");
			}
			else
			{
				Console.WriteLine(" > Game finished, challenge: " + challenge);
			}
#endif
		}
	}
}
