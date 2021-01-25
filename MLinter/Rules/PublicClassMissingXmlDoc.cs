using MLinter.Rules.Concept;

namespace MLinter.Rules
{
    public class PublicClassMissingXmlDoc : PublicObjectHasNoXmlDoc
    {
        public override string Name { get; } = nameof(PublicClassMissingXmlDoc);
        public override string Message { get; } = "Public class is missing Xml Docs";

        protected override bool ContainsTrigger(string line)
        {
            return line.Contains("class");
        }
    }
}