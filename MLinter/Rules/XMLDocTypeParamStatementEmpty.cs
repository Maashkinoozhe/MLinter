using MLinter.Rules.Base;

namespace MLinter.Rules
{
    using System.Text.RegularExpressions;

    public class XMLDocTypeParamStatementEmpty : RegExLineBasedRule
    {

        public override string Name { get; } = nameof(XMLDocTypeParamStatementEmpty);
        public override string Message { get; } = "<typeparam> statement in xml doc is empty";
        protected override string Pattern { get; } = "<typeparam\\s?name=\"[a-zA-Z0-9_]*\"><\\/typeparam>";
    }
}
