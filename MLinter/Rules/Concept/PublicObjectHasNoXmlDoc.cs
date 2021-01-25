using MLinter.Rules.Base;

namespace MLinter.Rules.Concept
{
    public abstract class PublicObjectHasNoXmlDoc : ContextBasedRule
    {

        protected override bool ParseTriggerLine(string line)
        {
            return line.Contains("public ") && ContainsTrigger(line);
        }

        protected abstract bool ContainsTrigger(string line);

        protected override Violation ParserContext(string trigger, in int lineNumber)
        {
            for (int i = lineNumber - 1; i >= 0; i--)
            {
                string line = GetLine(i);

                if (line.Contains("///"))
                {
                    return null;
                }
                if (line.Contains("[Fact]") || line.Contains("[Theory]"))
                {
                    return null;
                }
                else if (line.Contains("["))
                {
                    continue;
                }
                else if (line.Contains("// Resharper "))
                {
                    continue;
                }
                else if (string.IsNullOrEmpty(line.Trim()))
                {
                    // empty line
                    break;
                }
            }

            return new Violation() { Column = 0 };
        }
    }
}
