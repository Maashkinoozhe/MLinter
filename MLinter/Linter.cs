using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace MLinter
{
    using System;
    using System.IO;

    public class Linter
    {
        private readonly string[] _paths;
        private readonly string[] _files;

        public Linter(string path)
        {
            _paths = new string[] { path };
            _files = new string[] { };
        }

        public Linter(string path, bool changedOnly)
        {
            _paths = changedOnly ? new string[] { } : new string[] { path };
            _files = changedOnly ? GetChangedFiles(path) : new string[] { };
        }

        public Linter(string[] paths, bool changedOnly)
        {
            _paths = changedOnly ? new string[] { } : paths;
            _files = changedOnly ? GetChangedFiles(paths) : new string[] { };
        }

        public MResult Lint()
        {

            List<string> files = _paths.SelectMany(GetFiles).Distinct().ToList();
            files.AddRange(_files.Distinct().Where(x => !files.Contains(x)).Where(y => y.ToLower().EndsWith(".cs")));

            Parser parser = new Parser(files.ToArray());

            var result = parser.Parse();

            IEnumerable<Violation> orderedViolations = result.Violations.OrderBy(x => x.Path).ThenBy(y => y.Line).ToList();
            result.Violations.Clear();
            result.Violations.AddRange(orderedViolations);

            return result;

        }

        private string[] GetChangedFiles(string[] paths)
        {
            return paths.SelectMany(GetChangedFiles).Distinct().ToArray();
        }

        private string[] GetChangedFiles(string path)
        {
            List<string> files = new List<string>();
            string strCmdText = "/C git status --porcelain";

            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.Arguments = strCmdText;
            cmd.StartInfo.RedirectStandardInput = false;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.WorkingDirectory = path;
            cmd.Start();

            cmd.WaitForExit();
            string state = cmd.StandardOutput.ReadToEnd();

            Regex pattern = new Regex("[MA]\\s*(\\S.*$)");
            foreach (var line in state.Split("\n"))
            {
                Match match = pattern.Match(line);
                if (match.Success)
                {
                    files.Add(match.Groups[1].Value);
                }
            }

            return files.ToArray();
        }

        private IEnumerable<string> GetFiles(string path)
        {
            string parsedPath = Path.GetDirectoryName(path);
            List<string> files =  Directory.EnumerateFiles(parsedPath, "*.cs", SearchOption.AllDirectories).ToList();
            return files;
        }
    }
}
