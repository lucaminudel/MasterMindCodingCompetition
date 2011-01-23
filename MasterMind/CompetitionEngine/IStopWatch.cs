using System;

namespace MasterMind.CompetitionEngine
{
	public interface IStopWatch
	{
		void Start();
		void Stop();
		bool ElapsedTimeExcedeed(TimeSpan timeLimit);

		void Reset();
	}
}
