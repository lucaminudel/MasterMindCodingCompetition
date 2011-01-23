using System;
using System.Diagnostics;

namespace MasterMind.CompetitionEngine
{
	public class StopWatch : IStopWatch
	{
		private readonly Stopwatch stopwatch;

		public StopWatch()
		{
			//Stopwatch note:
			//On a multiprocessor computer, it does not matter which processor the thread runs on. However, because of bugs                 
			//in the BIOS or the Hardware Abstraction Layer (HAL), you can get different timing results on different processors. 
			//To specify processor affinity for a thread, use the ProcessThread.ProcessorAffinity method.
			// Source: http://msdn.microsoft.com/en-us/library/system.diagnostics.stopwatch%28v=VS.80%29.aspx

			stopwatch = new Stopwatch();
		}


		public void Start()
		{
			stopwatch.Start();
		}

		public void Stop()
		{
			stopwatch.Stop();
		}

		public bool ElapsedTimeExcedeed(TimeSpan timeLimit)
		{
			return stopwatch.Elapsed >= timeLimit;
		}

		public void Reset()
		{
			stopwatch.Reset();
		}
	}
}
