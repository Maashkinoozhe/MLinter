using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MLinter.Rules.Tests
{
	public class PublicClassMissingXmlDocTests
	{
		[Fact]
		public void Test_Missing_Doc()
		{
			IRule rule = new PublicClassMissingXmlDoc();

			string[] data = new[]
			{
				"        ",
				"[ExcludeFromCodeCoverage]",
				"public class SomeName{"
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
			IRule rule = new PublicClassMissingXmlDoc();

			string[] data = new[]
			{
				"        ",
				"[ExcludeFromCodeCoverage]",
				$"{modifier} class SomeName{{"
			};
			IEnumerable<Violation> violations = rule.Apply(data);
			Assert.Empty(violations);
		}

        [Fact]
        public void Test_Has_Doc()
        {
            IRule rule = new PublicClassMissingXmlDoc();

            string[] data = new[]
            {
                "        ",
                "/// something",
				"[ExcludeFromCodeCoverage]",
                "public class SomeName{"
			};
            IEnumerable<Violation> violations = rule.Apply(data);
            Assert.Empty(violations);
        }

        [Fact]
        public void Test_Has_Doc_With_Resharper_SuppressionComment()
        {
            IRule rule = new PublicClassMissingXmlDoc();

            string[] data = new[]
            {
                "        ",
                "/// something",
				"// Resharper",
                "public class SomeName{"
            };
            IEnumerable<Violation> violations = rule.Apply(data);
            Assert.Empty(violations);
        }
	}
}
