using System.Security.Cryptography;

namespace MasterMind.CompetitionEngine
{
	public class CodeMaker : ICodeMaker
	{
		private readonly RNGCryptoServiceProvider laDeaBendata;

		public CodeMaker()
		{
			laDeaBendata = new RNGCryptoServiceProvider();
		}

		public Code CreateCodeChallenge()
		{
			var challenge = new CodePeg[Code.Size];
			var randomBytes = new byte[Code.Size];
			laDeaBendata.GetBytes(randomBytes);
			for (int i = 0; i < Code.Size; ++i)
			{
				challenge[i] = (CodePeg) (randomBytes[i] % (int)CodePeg.Count);
			}

			return new Code(challenge);
		}
	}
}
