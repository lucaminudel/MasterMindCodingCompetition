
using System;
using System.Collections.Generic;
using System.Threading;

namespace MasterMind.Tests
{
    public class StubCodeBreaker : IMasterMindCodeBreaker
    {
        private TimeSpan getGuessCodeSubbedResponseDelay = new TimeSpan(0);
        private bool getGuessCodeStubbedRaiseException = false;
        private Queue<Code> getGuessCodeStubbedReturnValues = new Queue<Code>();

        private string nameStubbeValue = null;
        private Version versionStubbedValue = null;

        public void StubCallToGetGuessCode(TimeSpan waitFor)
        {
            getGuessCodeSubbedResponseDelay = waitFor;
        }

        public void StubCallToGetGuessCode(bool raiseAnException)
        {
            getGuessCodeStubbedRaiseException = raiseAnException;

        }

        public void StubSequenceOfCallsToGetGuessCode(Code[] codes)
        {
            foreach (Code code in codes)
            {
                getGuessCodeStubbedReturnValues.Enqueue(code);                
            }
        }

        public void StubPropetyGetName(string returnValue)
        {
            nameStubbeValue = returnValue;
        }

        public void StubPropertyGetVersion(Version returnValue)
        {
            versionStubbedValue = returnValue;
        }

        string IMasterMindCodeBreaker.Name
        {
            get { return nameStubbeValue; }
        }

        Version IMasterMindCodeBreaker.Version
        {
            get { return versionStubbedValue; }
        }

        void IMasterMindCodeBreaker.StartGame()
        {
        }

        Code IMasterMindCodeBreaker.GetCodeGuess()
        {

            if (getGuessCodeStubbedRaiseException)
            {
                throw new Exception("Expected exception");
            }

            Thread.Sleep(getGuessCodeSubbedResponseDelay);

            if (getGuessCodeStubbedReturnValues.Count > 0)
            {
                Code code = getGuessCodeStubbedReturnValues.Dequeue();
                return code;
            }
            
            return Code.Empty;
        }

        void IMasterMindCodeBreaker.SetGuessResult(GuessResult guessResult)
        {
        }

        void IMasterMindCodeBreaker.GameDismissed()
        {
        }

        void IMasterMindCodeBreaker.GameEnded(Code challenge)
        {
        }
    }
}
