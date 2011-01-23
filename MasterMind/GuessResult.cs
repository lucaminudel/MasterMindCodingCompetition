namespace MasterMind
{
	public struct  GuessResult
	{
		private readonly int numberOfPegsWithCorrectColorAndPosition;
		private readonly int numberOfPegsWithCorrectColorAndWrongPosition;

		public GuessResult(int numberOfPegsWithCorrectColorAndPosition, int numberOfPegsWithCorrectColorAndWrongPosition)
		{
			this.numberOfPegsWithCorrectColorAndPosition = numberOfPegsWithCorrectColorAndPosition;
			this.numberOfPegsWithCorrectColorAndWrongPosition = numberOfPegsWithCorrectColorAndWrongPosition;
		}

		public int NumberOfPegsWithCorrectColorAndPosition
		{
			get { return numberOfPegsWithCorrectColorAndPosition; }
		}

		public int NumberOfPegsWithCorrectColorAndWrongPosition
		{
			get { return numberOfPegsWithCorrectColorAndWrongPosition; }
		}

		public static GuessResult Calulate(Code challenge, Code guess)
		{
			var colorsCount = new int[(int)CodePeg.Count];
			for (int color = 0; color < (int)CodePeg.Count; ++color)
			{
				colorsCount[color] = challenge[(CodePeg)color];
			}

			int numberOfPegsWithCorrectColorAndPosition = 0;
			for (int peg = 0; peg < Code.Size; ++peg)
			{
				if (guess[peg] == challenge[peg])
				{
					numberOfPegsWithCorrectColorAndPosition += 1;

					colorsCount[(int)challenge[peg]] -= 1;
				}
			}

			int numberOfPegsWithCorrectColorAndWrongPosition = 0;
			for (int peg = 0; peg < Code.Size; ++peg)
			{
				bool pegWithCorrectColorAndWrongPosition = colorsCount[(int)guess[peg]] > 0 && guess[peg] != challenge[peg];
				if (pegWithCorrectColorAndWrongPosition)
				{
					numberOfPegsWithCorrectColorAndWrongPosition += 1;
					colorsCount[(int)guess[peg]] -= 1;
				}
			}

			return new GuessResult(numberOfPegsWithCorrectColorAndPosition, numberOfPegsWithCorrectColorAndWrongPosition);
		}


		public override string ToString()
		{
			return "{ " + numberOfPegsWithCorrectColorAndPosition + ", " + numberOfPegsWithCorrectColorAndWrongPosition + " }";
		}
	}

}
