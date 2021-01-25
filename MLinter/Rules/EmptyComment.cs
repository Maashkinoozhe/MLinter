using System;
using System.Collections.Generic;
using System.Text;
using MLinter.Rules.Base;

namespace MLinter.Rules
{
    public class EmptyComment : RegExLineBasedRule
    {
        public override string Name { get; } = nameof(EmptyComment);
        public override string Message { get; } = "Some empty Comment";
        protected override string Pattern { get; } = "//\\s$";
    }
}
