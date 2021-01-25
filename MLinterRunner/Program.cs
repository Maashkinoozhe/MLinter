using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MLinter;
using PowerArgs;

namespace MLinterRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            LinterRunnerArgs config = new LinterRunnerArgs();

            if (args.Any())
            {
                config = Args.Parse<LinterRunnerArgs>(args);
                if (config == null)
                {
                    // help was called, end program
                    return;
                }
            }

            string path = Directory.GetCurrentDirectory();
            Linter linter;
            if (config .Directories == null || !config.Directories.Any())
            {
                linter = new Linter(path, config.ChangedFilesOnly);
            }
            else
            {
                config.Directories = CleanPathNames(config.Directories);
                linter = new Linter(config.Directories.ToArray(), config.ChangedFilesOnly);
            }

            MResult result = linter.Lint();

            ConsoleColor previousColor = Console.ForegroundColor;

            foreach (Violation violation in result.Violations)
            {
                if (violation.Severity == Severity.Warning)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else if (violation.Severity == Severity.Error)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                Console.Out.WriteLine(violation.FullMessage);
            }
            Console.Out.Flush();
            Console.ForegroundColor = previousColor;
        }

        private static List<string> CleanPathNames(List<string> directories)
        {
            return directories
                .Select(x=>x.Replace("\\","/"))
                .Select(x => !x.EndsWith("/")?x +"/":x)
                .ToList();
        }
    }
}
