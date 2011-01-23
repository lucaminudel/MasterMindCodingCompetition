using System;
using MasterMind.CompetitionEngine;
using MasterMind.CodeBreakersWithOriginalAlgorithms;

namespace MasterMind
{
    class Program
    {
        static void Main()
        {
            var opponents = new IMasterMindCodeBreaker[] { new RandomCodeBreaker(), new RandomCodeBreaker(), new RandomCodeBreaker() };

#if FRAMEWORK_DEBUG
            Console.WriteLine("Match begin");
            Console.WriteLine("----------------------------------------------------------------");
#endif 
            var masterMingCompetitionMatch = new Match(opponents, TimeSpan.FromSeconds(4));
            string results;
            masterMingCompetitionMatch.PlayGames(1000, out results);

#if FRAMEWORK_DEBUG
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Match end");
#endif
            Console.WriteLine(results);
            Console.ReadLine();
        }
    }
}
