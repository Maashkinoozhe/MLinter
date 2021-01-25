using MLinter.Rules.Base;

namespace MLinter.Rules
{
    public class UnresolvedTodo : RegExLineBasedRule
    {
        public override string Name { get; } = nameof(UnresolvedTodo);
        public override string Message { get; } = "found TODO:";
        protected override string Pattern { get; } = "\\/\\/.*?TODO:";
    }
}
