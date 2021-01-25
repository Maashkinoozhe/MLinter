using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MLinter
{
    public class Violation
    {
        public string Path { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }
        public string Message { get; set; }
        public Severity Severity { get; set; }

        public string ShortMessage => $"{GetSeverity()} {System.IO.Path.GetFileName(Path)} : {Line} {Message}";
        public string FullMessage => $"{Severity.ToString()} {System.IO.Path.GetFileName(Path)} : Line {Line}, {Message} | {Path}";

        private string GetSeverity()
        {
            return $"{Severity.ToString().Take(1).First()}".ToUpper();
        }

    }
}
