using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MLinter.Rules.Tests
{
    public class XMLDocParamStatementEmptyTests
    {
        [Fact]
        public void RuleTriggers()
        {
            IRule rule = new XMLDocParamStatementEmpty();

            string[] data = new[] { "        /// <param name=\"name\"></param>" };
            IEnumerable<Violation> violations = rule.Apply(data);
            Assert.NotEmpty(violations);
        }
        
        [Fact]
        public void RuleTriggers2()
        {
            IRule rule = new XMLDocParamStatementEmpty();

            string[] data = new[] { "/// <param name=\"container\"></param>" };
            IEnumerable<Violation> violations = rule.Apply(data);
            Assert.NotEmpty(violations);
        }
    }
}
