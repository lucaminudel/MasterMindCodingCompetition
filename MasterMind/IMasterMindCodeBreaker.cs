using System;

namespace MasterMind
{
	public interface IMasterMindCodeBreaker
	{
		string Name { get; }
		Version Version { get; }

		void StartGame();

		Code GetCodeGuess();
		void SetGuessResult(GuessResult guessResult);

		void GameDismissed();
		void GameEnded(Code challenge);
	}
}