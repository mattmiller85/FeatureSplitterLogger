using System;
using System.Linq;

namespace FeatureSplitterLogger.Utils
{
    public class ArgParser
    {
        private readonly string[] _arguments;
        public ArgParser(string[] arguments)
        {
            if(arguments == null || arguments.Length == 0)
                throw new ArgumentNullException("arguments");
            _arguments = arguments.Select(s => s.ToLower()).ToArray();   
        }

        public Options GetOptions()
        {
            var options = new Options
            {
                FeaturePath = _arguments[0]
            };

            ParseProfile(options);
            ParseLineRange(options);

            return options; 
        }

        private void ParseProfile(Options options)
        {
            options.Profile = string.Empty;
            var profileOptionIndex = Array.IndexOf(_arguments, "-p");
            if(profileOptionIndex == -1)
                throw new ArgumentNullException("Please pass -p profilename");
            
            options.Profile = _arguments[profileOptionIndex + 1];
        }

        private void ParseLineRange(Options options)
        {
            options.HasLineNumberRange = false;
            var lineRangeOptionIndex = Array.IndexOf(_arguments, "-r");
            if(lineRangeOptionIndex == -1)
                return;
            
            options.HasLineNumberRange = true;

            var lineNumberRangeString = _arguments[lineRangeOptionIndex + 1];
            var splt = lineNumberRangeString.Split('-');

            options.LineNumberRange = new LineNumberRange { From = int.Parse(splt[0]), To = int.Parse(splt[1]) };
        }
    }
}