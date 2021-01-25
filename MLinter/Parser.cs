using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MLinter.Filters;
using MLinter.Rules;

namespace MLinter
{
    public class Parser
    {
        private readonly string[] _files;

        public Parser(string[] files)
        {
            _files = files;
        }

        public MResult Parse()
        {
            List<IRule> rules = new List<IRule>();
            MResult parseResult = new MResult();

            InstallRules(rules);

            foreach (var file in _files)
            {
                List<Violation> violations = new List<Violation>();

                string[] data = File.ReadAllLines(file);
                
                IEnumerable <Violation> result = Test(rules,data);

                foreach (var violation in result)
                {
                    violation.Path = file;
                    parseResult.Violations.Add(violation);
                }
            }

            return parseResult;
        }

        private void InstallRules(IList<IRule> rules)
        {
            bool useExcludeRules = false;
            if (useExcludeRules)
            {
                rules.Add(new XMLDocReturnStatementEmpty());
                rules.Add(new XMLDocParamStatementEmpty());
                rules.Add(new XMLDocTypeParamStatementEmpty());
            }
            else
            {
                rules.Add(new XMLDocReturnStatementEmpty().AddContentFilter(new ExcludeFromCodeCoverage()));
                rules.Add(new XMLDocParamStatementEmpty().AddContentFilter(new ExcludeFromCodeCoverage()));
                rules.Add(new XMLDocTypeParamStatementEmpty().AddContentFilter(new ExcludeFromCodeCoverage()));
            }

            rules.Add(new PublicClassMissingXmlDoc());
            rules.Add(new PublicFunctionMissingXmlDoc());
            rules.Add(new PublicFieldMissingXmlDoc());;
            rules.Add(new UnresolvedTodo());
            rules.Add(new EmptyComment());
        }

        private IEnumerable<Violation> Test(List<IRule> rules, string[] data)
        {
            List<Violation> violations = new List<Violation>();

            foreach (IRule rule in rules)
            {
                IEnumerable<Violation> result = ApplyRule(rule,data);
                violations.AddRange(result);
            }

            return violations;
        }

        private IEnumerable<Violation> ApplyRule(IRule rule, string[] data)
        {
            return rule.Apply(data);
        }
    }
}
