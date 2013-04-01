﻿using System.Linq;

namespace PublicSuffix.Rules
{
    /// <summary>
    /// An Exception Rule starts with a "!"
    /// If it does, it is labeled as a "exception rule" and then treated as if the exclamation mark is not present.
    /// Marks an exception to a previous wildcard rule.
    /// </summary>
    public class ExceptionRule : Rule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionRule"/> class. 
        /// </summary>
        /// <param name="name">
        /// The rule, example: !metro.tokyo.jp
        /// </param>
        public ExceptionRule(string name) : base(name) 
        {
            Name = Name.Substring(1); // strip "!" character
        }

        /// <summary>
        /// Parses a domain name from the supplied url and current <see cref="ExceptionRule" /> instance.
        /// Gets the Top, Second and Third level domains populated (if present.)
        /// Modify it by removing the leftmost label.
        /// </summary>
        /// <param name="url">A valid url, example: http://www.site.metro.tokyo.jp</param>
        /// <returns>A valid <see cref="Domain" /> instance.</returns>
        public override Domain Parse(string url)
        {
            var host = Canonicalize(url);
            var len = Parts.Length;

            var domain = new Domain()
            {
                TLD         = string.Join(".", Parts.Reverse().Skip(1).ToArray()),
                MainDomain  = Parts.Last(),
                SubDomain   = string.Join(".", host.Skip(len).Reverse().ToArray())
            };

            return domain;
        }
    }
}
