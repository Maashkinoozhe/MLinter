using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MLinter.Rules.Tests
{
	public class PublicFieldMissingXmlDocTests
	{
		[Fact]
		public void Test_Missing_Doc()
		{
			IRule rule = new PublicFieldMissingXmlDoc();

			string[] data = new[]
			{
				"        ",
				"public int variable { get; set; }"
			};
			IEnumerable<Violation> violations = rule.Apply(data);
			Assert.NotEmpty(violations);
		}

		[Theory]
		[InlineData("private")]
		[InlineData("protected")]
		[InlineData("internal")]
        public void Test_Missing_Doc_On_NonPublic(string modifier)
		{
			IRule rule = new PublicFieldMissingXmlDoc();

			string[] data = new[]
			{
				"        ",
				$"{modifier} int variable {{ get; set; }}"
			};
			IEnumerable<Violation> violations = rule.Apply(data);
			Assert.Empty(violations);
		}

        [Fact]
        public void Test_Has_Doc()
        {
            IRule rule = new PublicFieldMissingXmlDoc();

            string[] data = new[]
            {
                "        ",
                "/// something",
                "public string SomeName {"
            };
            IEnumerable<Violation> violations = rule.Apply(data);
            Assert.Empty(violations);
        }
	}
}
