
using System;
using System.Collections.Generic;
using System.Linq;

namespace PublicSuffix.Rules {

    /// <summary>
    /// An abstract Rule class that the specific Rule Types inherit from.
    /// </summary>
    public abstract class Rule
    {
        /// <summary>
        /// Gets or sets the normalized rule name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the raw rule value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rule"/> class. 
        /// </summary>
        /// <param name="name">
        /// A line from a <see cref="RulesList"/>
        /// </param>
        protected Rule(string name)
        {
            Name   = name.ToLowerInvariant();
            Value  = Name;
        }

        /// <summary>
        /// Gets an array of parts from splitting the rule along the dots.
        /// </summary>
        public string[] Parts
        {
            get
            {
                return Name.Split('.').Reverse().ToArray();
            }
        }

        /// <summary>
        /// Gets the number of <see cref="Parts" />
        /// </summary>
        public int Length
        {
            get
            {
                return Parts.Length;
            }
        }

        /// <summary>
        /// Convert this rule instance to a string.
        /// </summary>
        /// <returns>The <see cref="Name" /></returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// A domain is said to match a rule if, when the domain and rule are both split, and one compares the labels from the rule to the labels from the domain, beginning at the right hand end, one finds that for every pair either they are identical, or that the label from the rule is "*" (star).
        /// The domain may legitimately have labels remaining at the end of this matching process.
        /// </summary>
        /// <param name="url">A valid url, example: http://www.google.com</param>
        /// <returns>true if the rule matches; otherwise, false.</returns>
        public virtual bool IsMatch(string url)
        {
            var host = Canonicalize(url);
            return IsMatch(host);
        }

        /// <summary>
        /// A domain is said to match a rule if, when the domain and rule are both split, and one compares the labels from the rule to the labels from the domain, beginning at the right hand end, one finds that for every pair either they are identical, or that the label from the rule is "*" (star).
        /// The domain may legitimately have labels remaining at the end of this matching process.
        /// </summary>
        /// <param name="url">A valid url, example: http://www.google.com</param>
        /// <returns>true if the rule matches; otherwise, false.</returns>
        public virtual bool IsMatch(Uri url)
        {
            var host = Canonicalize(url);
            return IsMatch(host);
        }

        /// <summary>
        /// Parses a domain name from the supplied url and current <see cref="Rule" /> instance.
        /// Gets the Top, Second and Third level domains populated (if present.)
        /// </summary>
        /// <param name="url">A valid url, example: http://www.google.com</param>
        /// <returns>A valid <see cref="Domain" /> instance.</returns>
        public virtual Domain Parse(string url)
        {
            var host = Canonicalize(url);
            return Parse(host);
        }

        /// <summary>
        /// Parses a domain name from the supplied url and current <see cref="Rule" /> instance.
        /// Gets the Top, Second and Third level domains populated (if present.)
        /// </summary>
        /// <param name="url">A valid url, example: http://www.google.com</param>
        /// <returns>A valid <see cref="Domain" /> instance.</returns>
        public virtual Domain Parse(Uri url)
        {
            var host = Canonicalize(url);
            return Parse(host);
        }

        /// <summary>
        /// Converts a valid uri to a canonicalized uri, example: com.google.maps
        /// </summary>
        /// <param name="url">A valid url, example: http://www.google.com</param>
        /// <returns>A string array in reverse order.</returns>
        protected string[] Canonicalize(string url)
        {
            return Canonicalize(new UriBuilder(url).Uri);
        }

        /// <summary>
        /// Converts a valid uri to a canonicalized uri, example: com.google.maps
        /// </summary>
        /// <param name="uri"> A valid url, example: http://www.google.com </param>
        /// <returns> A string array in reverse order. </returns>
        protected string[] Canonicalize(Uri uri)
        {
            return uri.DnsSafeHost.Split('.').Reverse().ToArray();
        }

        private bool IsMatch(IList<string> host)
        {
            var match = true;

            var parts = Parts;
            var partsLen = parts.Length;

            for (var h = 0; h < host.Count(); h++)
            {
                if (h >= partsLen) continue;
                var part = parts[h];
                if (part != host[h] && part != "*") match = false;
            }

            return match;
        }

        private Domain Parse(string[] host)
        {
            var len = Length;
            var reverseTld = host.Take(len).Reverse();
            var reverseSub = host.Skip(len + 1).Reverse();

            var domain = new Domain
            {
                TLD = string.Join(".", reverseTld),
                MainDomain = host.Skip(len).First(),
                SubDomain = string.Join(".", reverseSub)
            };

            return domain;
        }
    }
}
