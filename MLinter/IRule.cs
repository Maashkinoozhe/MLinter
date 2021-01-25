namespace MLinter
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IRule
    {
        IEnumerable<Violation> Apply(string[] data);
        public string Name { get; }
        public string Message { get; }
    }
}
