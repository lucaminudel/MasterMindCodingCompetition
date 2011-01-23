using System;
using System.Threading;
using System.Collections.Generic;

namespace MasterMind.Tests
{
    public class MockCodeBreaker : IMasterMindCodeBreaker
    {
        private Dictionary<string, int> expectedCalls;
        private Dictionary<string, int> actualCalls;

        private Dictionary<string, object> expectedCallArgument;
        private Dictionary<string, object> actualCallArgument;

        private Dictionary<string, object> expectedReturnValue;

        private Dictionary<string, bool> callToMethodWillRaiseAnException;

        private TimeSpan expectedWaitForGetGuessCode = new TimeSpan(0);

        private static class Methods
        {
            public const string StartGame = "StartGame";
            public const string GetGuessCode = "GetGuessCode";
            public const string GameEnded = "GameEnded";
            public const string SetGuessResult = "SetGuessResult";
            public const string GameDismissed = "GameDismissed";            
        }

        public MockCodeBreaker()
        {
            expectedCalls = new Dictionary<string, int>();
            actualCalls = new Dictionary<string, int>();
            ResetActualAndExpectedCalls(Methods.StartGame);
            ResetActualAndExpectedCalls(Methods.GetGuessCode);
            ResetActualAndExpectedCalls(Methods.GameEnded);
            ResetActualAndExpectedCalls(Methods.SetGuessResult);
            ResetActualAndExpectedCalls(Methods.GameDismissed);

            expectedCallArgument = new Dictionary<string, object>();
            actualCallArgument = new Dictionary<string, object>();
            ResetActualAndExpectedCallArgument(Methods.GameEnded, Code.Empty);
            ResetActualAndExpectedCallArgument(Methods.SetGuessResult, new GuessResult(0, 0));

            expectedReturnValue = new Dictionary<string, object>();
            expectedReturnValue[Methods.GetGuessCode] = Code.Empty;

            callToMethodWillRaiseAnException = new Dictionary<string, bool>();
            callToMethodWillRaiseAnException[Methods.GetGuessCode] = false;
            callToMethodWillRaiseAnException[Methods.GameEnded] = false;
        }



        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public Version Version
        {
            get { throw new NotImplementedException(); }
        }

        public void SetExpectedCallToStartGame()
        {
            SetMethodExpectations(Methods.StartGame, null, false, null);
        }

        public void SetExpectedCallToGetGuessCode(Code returnValue)
        {
            SetMethodExpectations(Methods.GetGuessCode, null, false, returnValue);
        }

        public void SetExpectedCallToGetGuessCode(TimeSpan waitFor)
        {
            SetMethodExpectations(Methods.GetGuessCode, null, false, null);
            expectedWaitForGetGuessCode = waitFor;
        }

        public void SetExpectedCallToGetGuessCode(bool raiseAnException)
        {
            SetMethodExpectations(Methods.GetGuessCode, null, raiseAnException, null);
        }

        public void SetExpectedCallToSetGuessResult(GuessResult expectedGuessResult)
        {
            SetMethodExpectations(Methods.SetGuessResult, expectedGuessResult, false, null);
        }

        public void SetExpectedCallToGameDismissed()
        {
            SetMethodExpectations(Methods.GameDismissed, null, false, null);
        }

        public void SetExpectedCallToGameEnded(bool raiseAnException)
        {
            SetMethodExpectations(Methods.GameEnded, null, raiseAnException, null);
        }

        public void SetExpectedCallToGameEnded(Code expecedChallenge)
        {
            SetMethodExpectations(Methods.GameEnded, expecedChallenge, false, null);
        }



        public void VerifyAllExpectations()
        {
            VerifyExpectations(Methods.StartGame);

            VerifyExpectations(Methods.GetGuessCode);

            VerifyExpectations(Methods.GameEnded);

            VerifyExpectations(Methods.SetGuessResult);

            VerifyExpectations(Methods.GameDismissed);
        }



        void IMasterMindCodeBreaker.StartGame()
        {
            RecordActualCallAndPlaybackExpectations(Methods.StartGame, null);
        }

        Code IMasterMindCodeBreaker.GetCodeGuess()
        {
            Code returnValue = (Code)RecordActualCallAndPlaybackExpectations(Methods.GetGuessCode, null);

            Thread.Sleep(expectedWaitForGetGuessCode);

            return returnValue;
        }

        void IMasterMindCodeBreaker.GameEnded(Code challenge)
        {
            RecordActualCallAndPlaybackExpectations(Methods.GameEnded, challenge);
        }

        void IMasterMindCodeBreaker.SetGuessResult(GuessResult guessResult)
        {
            RecordActualCallAndPlaybackExpectations(Methods.SetGuessResult, guessResult);
        }

        void IMasterMindCodeBreaker.GameDismissed()
        {
            RecordActualCallAndPlaybackExpectations(Methods.GameDismissed, null);
        }


        private void ResetActualAndExpectedCalls(string methodName)
        {
            expectedCalls[methodName] = 0;
            actualCalls[methodName] = 0;
        }

        private void ResetActualAndExpectedCallArgument(string methodName, object callArgumentResetValue)
        {
            expectedCallArgument[methodName] = callArgumentResetValue;
            actualCallArgument[methodName] = callArgumentResetValue;
        }

        private void SetMethodExpectations(string method, object expectedArgument, bool raiseAnException, object returnValue)
        {
            expectedCalls[method] = 1;
            expectedCallArgument[method] = expectedArgument;
            callToMethodWillRaiseAnException[method] = raiseAnException;
            expectedReturnValue[method] = returnValue;
        }

        private object RecordActualCallAndPlaybackExpectations(string method, object argument)
        {
            actualCalls[method] += 1;

            actualCallArgument[method] = argument;

            if (callToMethodWillRaiseAnException.ContainsKey(method) && callToMethodWillRaiseAnException[method])
            {
                throw new Exception("Expected exception");
            }

            return expectedReturnValue[method];
        }

        private void VerifyExpectations(string method)
        {
            VerifyExpectedCalls(method);
            VerifyExpectedArgument(method);
        }

        private void VerifyExpectedCalls(string methodName)
        {
            if (expectedCalls[methodName] != actualCalls[methodName])
            {
                throw new Exception(string.Format("Expected calls to {0}: {1} but were: {2}", methodName, expectedCalls[methodName], actualCalls[methodName]));
            }
        }

        private void VerifyExpectedArgument(string methodName)
        {
            bool expectationSetAndArgumentSpecified = expectedCallArgument.ContainsKey(methodName) && expectedCallArgument[methodName] != null;
            if (expectationSetAndArgumentSpecified && expectedCallArgument[methodName].Equals(actualCallArgument[methodName]) == false)
            {
                throw new Exception(string.Format("Expected argument of the calls to {0}: {1} but was: {2}", methodName, expectedCallArgument[methodName], actualCallArgument[methodName]));
            }
        }
    }
}
