namespace MasterMind.CompetitionEngine
{
	public interface IMatchPlayer
	{
		bool GameFinished { get; }

		void StartGame();
		void GuessCode(int turn, Code challenge);
		void SetGuessResult(Code challenge);
		void EndGame(Code challenge);
		string ToString();
	}
}