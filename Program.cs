using System;
using System.Collections.Generic;
using System.Linq;
using FeatureSplitterLogger.Utils;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var argParser = new ArgParser(args);

            var options = argParser.GetOptions();
            
            var commands = new List<string>(); 
            if(options.HasLineNumberRange)
                AddCommandRange(options, commands);
        }

        private static void AddCommandRange(Options options, List<string> commands)
        {
            Enumerable.Range(options.LineNumberRange.From, options.LineNumberRange.To - options.LineNumberRange.From + 1).ToList().ForEach(ln => {
                commands.Add($"bundle exec cucumber -p {options.Profile} {options.FeaturePath}:{ln} BROWSER=chrome");   
            });
            commands.ForEach(Console.WriteLine);
        }
    }
}
