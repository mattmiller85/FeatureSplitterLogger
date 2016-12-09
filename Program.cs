using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using FeatureSplitterLogger.Utils;

namespace ConsoleApplication
{
    
    public class Program
    {
        const string FeatureRootPath = @"C:\TFS\Grange Commercial SEQ\Olive\Specifications\SEQ";

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
                commands.Add($"exec cucumber -p {options.Profile} {Path.Combine(options.FeaturePath)}:{ln} BROWSER=chrome");   
            });
            commands.ForEach(c => 
            {
                var psi = new ProcessStartInfo(@"C:\RailsInstaller\Ruby1.9.3\bin\bundle.bat", c);
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;
                psi.WorkingDirectory = FeatureRootPath;
                var proc = Process.Start(psi);
                var output = proc.StandardOutput.ReadToEnd();
                var error = proc.StandardError.ReadToEnd();
                proc.WaitForExit();
                Console.WriteLine(string.IsNullOrWhiteSpace(output) ? error : output);
            });
        }
    }
}
