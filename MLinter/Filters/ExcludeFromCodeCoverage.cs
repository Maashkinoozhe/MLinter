using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MLinter.Filters
{
    public class ExcludeFromCodeCoverage : IContentFilter
    {
        public bool Matches(string[] data)
        {
            return data.Any(x => x.Contains(Pattern));
        }

        private string Pattern { get; } = "[ExcludeFromCodeCoverage]";
    }
}
