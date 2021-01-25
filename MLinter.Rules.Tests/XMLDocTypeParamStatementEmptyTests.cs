using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MLinter.Rules.Tests
{
    public class XMLDocTypeParamStatementEmptyTests
    {
        [Fact]
        public void RuleTriggers()
        {
            IRule rule = new XMLDocTypeParamStatementEmpty();

            string[] data = new[] { "        /// <typeparam name=\"name\"></typeparam>" };
            IEnumerable<Violation> violations = rule.Apply(data);
            Assert.NotEmpty(violations);
        }
    }
}
