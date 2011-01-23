using System;

namespace MasterMind.CompetitionEngine.Tests
{
    public class StubMatchPlayer : IMatchPlayer
    {
        private bool gameFinished = false;

        public void StubPropertyGetGameFinished(bool returnValue)
        {
            gameFinished = returnValue;
        }

        bool IMatchPlayer.GameFinished
        {
            get { return gameFinished; }
        }

        void IMatchPlayer.StartGame()
        {
            throw new NotImplementedException();
        }

        void IMatchPlayer.GuessCode(int turn, Code challenge)
        {
            throw new NotImplementedException();
        }

        void IMatchPlayer.SetGuessResult(Code challenge)
        {
            throw new NotImplementedException();
        }

        void IMatchPlayer.EndGame(Code challenge)
        {
            throw new NotImplementedException();
        }
    }
}
