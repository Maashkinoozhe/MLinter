using MLinter.Rules.Base;

namespace MLinter.Rules
{
    using System.Text.RegularExpressions;

    public class XMLDocParamStatementEmpty : RegExLineBasedRule
    {

        public override string Name { get; } = nameof(XMLDocParamStatementEmpty);
        public override string Message { get; } = "<param> statement in xml doc is empty";
        protected override string Pattern { get; } = "<param\\s?name=\"[a-zA-Z0-9_]*\"><\\/param>";
    }
}
