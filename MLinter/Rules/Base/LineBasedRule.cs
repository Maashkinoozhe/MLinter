using System.Collections.Generic;
using System.Linq;

namespace MLinter.Rules.Base
{
    public abstract class LineBasedRule : IRule
    {
        private List<IContentFilter> _filters = new List<IContentFilter>();

        public LineBasedRule AddContentFilter(IContentFilter filter)
        {
            _filters.Add(filter);
            return this;
        }

        public IEnumerable<Violation> Apply(string[] data)
        {
            if (_filters.Any(x => x.Matches(data)))
            {
                return Enumerable.Empty<Violation>();
            }

            int lineNumber = 1;
            List<Violation> violations = new List<Violation>();
            foreach (string line in data)
            {
                Violation violation = ParseLine(line);
                if (violation != null)
                {
                    violation.Message = Message;
                    violation.Severity = Severity.Warning;
                    violation.Line = lineNumber;
                    violations.Add(violation);
                }
                lineNumber++;
            }

            return violations;
        }

        public abstract string Name { get; }
        public abstract string Message { get; }

        protected abstract Violation ParseLine(string line);
    }
}
