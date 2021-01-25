using MLinter.Rules.Base;

namespace MLinter.Rules
{
    using System.Text.RegularExpressions;

    public class XMLDocReturnStatementEmpty : RegExLineBasedRule
    {

        public override string Name { get; } = nameof(XMLDocReturnStatementEmpty);
        public override string Message { get; } = "<returns> statement in xml doc is empty";
        protected override string Pattern { get; } = "<returns></returns>";
    }
}
