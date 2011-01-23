using System.Collections.Generic;

namespace MasterMind.CompetitionEngine.Tests
{
    public class StubCodeMaker : ICodeMaker
    {
        private Queue<Code> createCodeChallengeStubbedReturnValues = new Queue<Code>();

        public void StubSequenceOfCallsToCreateCodeChallenge(Code[] returnValues)
        {
            foreach (Code code in returnValues)
            {
                createCodeChallengeStubbedReturnValues.Enqueue(code);
            }
        }

        Code ICodeMaker.CreateCodeChallenge()
        {
            if (createCodeChallengeStubbedReturnValues.Count > 0)
            {
                Code value = createCodeChallengeStubbedReturnValues.Dequeue();
                return value;
            }

            return Code.Empty;
        }
    }
}
