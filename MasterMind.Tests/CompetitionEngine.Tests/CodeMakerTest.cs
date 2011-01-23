using NUnit.Framework;

namespace MasterMind.CompetitionEngine.Tests
{
    [TestFixture]
    public class CodeMakerTest
    {
        [Test]
        public void CreateCodeChallenge_return_a_non_empty_code()
        {
            Code codeChallenge = new CodeMaker().CreateCodeChallenge();

            Assert.AreNotEqual(Code.Empty, codeChallenge, "Created code challege");
        }
    }
}
