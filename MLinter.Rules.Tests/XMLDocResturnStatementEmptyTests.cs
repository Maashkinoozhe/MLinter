using System;
using System.Collections.Generic;
using Xunit;

namespace MLinter.Rules.Tests
{
    public class XMLDocResturnStatementEmptyTests
    {
        [Fact]
        public void RuleTriggers()
        {
            IRule rule = new XMLDocReturnStatementEmpty();

            string[] data = new[] {"        /// <returns></returns>"};
            IEnumerable <Violation> violations = rule.Apply(data);
            Assert.NotEmpty(violations);
        }
    }
}
