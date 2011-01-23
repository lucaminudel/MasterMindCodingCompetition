
using System;

namespace MasterMind.CompetitionEngine.Tests
{
    public class MockStopWatch : IStopWatch
    {
        private int expectedCallsToReset = 0;
        private int actualCallsToReset = 0;

        public void SetExpectedCallToReset()
        {
            expectedCallsToReset = 1;
        }

        public void VerifyAllExpectations()
        {
            if (expectedCallsToReset != actualCallsToReset)
            {
                throw new Exception(string.Format("Expected calls to Reset: {0} but were: {1}", expectedCallsToReset, actualCallsToReset));
            }

        }

        void IStopWatch.Start()
        {
        }

        void IStopWatch.Stop()
        {
        }

        bool IStopWatch.ElapsedTimeExcedeed(TimeSpan timeLimit)
        {
            return false;
        }

        void IStopWatch.Reset()
        {
            actualCallsToReset += 1;
        }
    }
}
