using System.Collections.Generic;

namespace MLinter
{
    public class MResult
    {
        public List<Violation> Violations { get; } = new List<Violation>();
    }
}