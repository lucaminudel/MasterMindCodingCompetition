using System;

namespace MasterMind
{
	public struct Code
	{
		private readonly CodePeg[] codePegs;
		private readonly int[] colorsCount;

		public const int Size = 4;
		public static readonly Code Empty = new Code();

		public Code(CodePeg[] code)
			: this(code[0], code[1], code[2], code[3])
		{

			if (code.Length != Size)
			{
				throw new ArgumentOutOfRangeException();
			}

		}

		
		public Code(CodePeg zero, CodePeg one, CodePeg two, CodePeg three)
		{
			codePegs = new [] {zero, one, two, three};

			colorsCount = new int[(int)CodePeg.Count];
			colorsCount[(int)zero] += 1;
			colorsCount[(int)one] += 1;
			colorsCount[(int)two] += 1;
			colorsCount[(int)three] += 1;
		}

		public CodePeg this[int i]
		{
			get
			{
				if (codePegs == null)
				{
					return CodePeg.White;
				}

				return codePegs[i];
			}
		}


		public int this[CodePeg color]
		{
			get
			{
				if (colorsCount == null)
				{
					return (color == CodePeg.White) ? int.MaxValue : 0;
				}

				return colorsCount[(int)color];
			}
		}


		public override bool Equals(object obj)
		{
			if (!(obj is Code))
			{
				return false;
			}
			var code = (Code) obj;

			return this == code;
		}

		public override int GetHashCode()
		{
			return codePegs != null ? codePegs.GetHashCode() : 0;
		}

		public static bool operator ==(Code code1, Code code2)
		{
			if (code1.codePegs == null)
			{
				return code2.codePegs == null;
			}

			if (code2.codePegs == null)
			{
				return false;
			}

			for (int i = 0; i < Size; ++i)
			{
				if (code1.codePegs[i] != code2.codePegs[i])
				{
					return false;
				}
			}

			return true;
		}

		public static bool operator !=(Code code1, Code code2)
		{
			return !(code1 == code2);
		}


		public override string ToString()
		{
			if (codePegs == null)
			{
				return "Empty";
			}

			string result = string.Empty;
			for (int i = 0; i < Size; ++i)
			{
				result += Enum.GetName(typeof(CodePeg), codePegs[i]);
				if (i + 1 < Size)
				{
					result += ", ";
				}
			}
			return "{ " + result + " }";
		}
	}
}