using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MLinter.Rules.Tests
{
	public class PublicFunctionMissingXmlDocTests
	{
		[Fact]
		public void Test_Missing_Doc()
		{
			IRule rule = new PublicFunctionMissingXmlDoc();

			string[] data = new[]
			{
				"        ",
				"[ExcludeFromCodeCoverage]",
				"public void SomeName(int banana)"
			};
			IEnumerable<Violation> violations = rule.Apply(data);
			Assert.NotEmpty(violations);
		}

		[Theory]
		[InlineData("Fact")]
		[InlineData("Theory")]
        public void Test_Missing_Doc_On_UnitTest(string argument)
		{
			IRule rule = new PublicFunctionMissingXmlDoc();

			string[] data = new[]
			{
				"        ",
				"[ExcludeFromCodeCoverage]",
				$"[{argument}]",
				"[InlineData(1,55)]",
				$"{argument} int SomeName(string apples)"
			};
			IEnumerable<Violation> violations = rule.Apply(data);
			Assert.Empty(violations);
		}

		[Theory]
		[InlineData("private")]
		[InlineData("protected")]
		[InlineData("internal")]
        public void Test_Missing_Doc_On_NonPublic(string modifier)
		{
			IRule rule = new PublicFunctionMissingXmlDoc();

			string[] data = new[]
			{
				"        ",
				"[ExcludeFromCodeCoverage]",
				"[Fact]",
				"[InlineData(1,55)]",
				$"{modifier} int SomeName(string apples)"
			};
			IEnumerable<Violation> violations = rule.Apply(data);
			Assert.Empty(violations);
		}

        [Fact]
        public void Test_Has_Doc()
        {
            IRule rule = new PublicFunctionMissingXmlDoc();

            string[] data = new[]
            {
                "        ",
                "/// something",
                "[Fact]",
                "[InlineData(1,55)]",
                "public string GetPlan(int complexity)"
            };
            IEnumerable<Violation> violations = rule.Apply(data);
            Assert.Empty(violations);
        }
	}
}
