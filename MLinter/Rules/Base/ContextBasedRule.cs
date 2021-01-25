using System;
using System.Collections.Generic;

namespace MLinter.Rules.Base
{
    public abstract class ContextBasedRule : IRule
    {
        private string[] _linesCache;

        public IEnumerable<Violation> Apply(string[] data)
        {
            List<Violation> violations = new List<Violation>();
            this._linesCache = data;

            for (int lineNumber = 0; lineNumber < data.Length; lineNumber++)
            {
                var line = data[lineNumber];
                if (ParseTriggerLine(line))
                {
                    Violation violation = ParserContext(line, lineNumber);
                    if (violation != null)
                    {
                        violation.Message = Message;
                        violation.Severity = Severity.Warning;
                        violation.Line = lineNumber;
                        violations.Add(violation);
                    }
                }
            }

            return violations;
        }

        protected string GetLine(int lineNumber)
        {
            return lineNumber >= 0 && lineNumber < this._linesCache.Length ? this._linesCache[lineNumber] : throw new ArgumentOutOfRangeException($"{lineNumber} is not a valid Line Number");
        }

        public abstract string Name { get; }
        public abstract string Message { get; }

        protected abstract bool ParseTriggerLine(string line);
        protected abstract Violation ParserContext(string trigger, in int lineNumber);
    }
}
