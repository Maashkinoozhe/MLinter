using System.Text.RegularExpressions;

namespace MLinter.Rules.Base
{
    public abstract class RegExLineBasedRule : LineBasedRule
    {

        protected override Violation ParseLine(string line)
        {
            Regex pattern = new Regex(Pattern);
            if (pattern.IsMatch(line))
            {
                return new Violation() { Column = 0 };
            }

            return null;
        }

        protected abstract string Pattern { get; }
    }
}
