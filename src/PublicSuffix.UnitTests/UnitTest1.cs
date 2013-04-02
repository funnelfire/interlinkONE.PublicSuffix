using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PublicSuffix.UnitTests
{
    /// <summary>
    /// The rules list tests.
    /// </summary>
    [TestClass]
    public class RulesListTests
    {
        private static readonly RulesList Rules = new RulesList(Assembly.GetExecutingAssembly().GetManifestResourceStream("PublicSuffix.UnitTests." + "effective_tld_names.dat"));
        private static readonly Parser Parser = new Parser(Rules);

        private static readonly RulesList RulesMin = new RulesList(Assembly.GetExecutingAssembly().GetManifestResourceStream("PublicSuffix.UnitTests." + "effective_tld_names-min.dat"));
        private static readonly Parser ParserMin = new Parser(RulesMin);

        /// <summary>
        /// The test method 1.
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {

        }

        /// <summary>
        /// The test stream constructor test method.
        /// </summary>
        [TestMethod]
        public void TestStreamConstructor()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PublicSuffix.UnitTests." + "effective_tld_names.dat");
            var rules = new RulesList(stream);
            Console.WriteLine(rules);
        }

        /// <summary>
        /// The test parse speed.
        /// </summary>
        [TestMethod]
        public void TestParseSpeedFull()
        {
            var uris = new List<Uri>
                {
                    new UriBuilder("amazon.com").Uri,
                    new UriBuilder("amazon.co.uk").Uri,
                    new UriBuilder("amazon.fr").Uri,
                    new UriBuilder("amazon.it").Uri,
                    new UriBuilder("amazon.jp").Uri,
                    new UriBuilder("amazon.ca").Uri,
                    new UriBuilder("amazon.cn").Uri,
                    new UriBuilder("microsoft.com").Uri,
                    new UriBuilder("google.com").Uri,
                    new UriBuilder("facebook.com").Uri,
                    new UriBuilder("myspace.com").Uri,
                    new UriBuilder("java.com").Uri,
                    new UriBuilder("aol.com").Uri,
                    new UriBuilder("crunchbase.com").Uri,
                    new UriBuilder("apple.com").Uri,
                    new UriBuilder("amazon.com").Uri,
                    new UriBuilder("amazon.co.uk").Uri,
                    new UriBuilder("amazon.fr").Uri,
                    new UriBuilder("amazon.it").Uri,
                    new UriBuilder("amazon.jp").Uri,
                    new UriBuilder("amazon.ca").Uri,
                    new UriBuilder("amazon.cn").Uri,
                    new UriBuilder("microsoft.com").Uri,
                    new UriBuilder("google.com").Uri,
                    new UriBuilder("facebook.com").Uri,
                    new UriBuilder("myspace.com").Uri,
                    new UriBuilder("java.com").Uri,
                    new UriBuilder("aol.com").Uri,
                    new UriBuilder("crunchbase.com").Uri,
                    new UriBuilder("apple.com").Uri,
                    new UriBuilder("amazon.com").Uri,
                    new UriBuilder("amazon.co.uk").Uri,
                    new UriBuilder("amazon.fr").Uri,
                    new UriBuilder("amazon.it").Uri,
                    new UriBuilder("amazon.jp").Uri,
                    new UriBuilder("amazon.ca").Uri,
                    new UriBuilder("amazon.cn").Uri,
                    new UriBuilder("microsoft.com").Uri,
                    new UriBuilder("google.com").Uri,
                    new UriBuilder("facebook.com").Uri,
                    new UriBuilder("myspace.com").Uri,
                    new UriBuilder("java.com").Uri,
                    new UriBuilder("aol.com").Uri,
                    new UriBuilder("crunchbase.com").Uri,
                    new UriBuilder("apple.com").Uri,
                    new UriBuilder("amazon.com").Uri,
                    new UriBuilder("amazon.co.uk").Uri,
                    new UriBuilder("amazon.fr").Uri,
                    new UriBuilder("amazon.it").Uri,
                    new UriBuilder("amazon.jp").Uri,
                    new UriBuilder("amazon.ca").Uri,
                    new UriBuilder("amazon.cn").Uri,
                    new UriBuilder("microsoft.com").Uri,
                    new UriBuilder("google.com").Uri,
                    new UriBuilder("facebook.com").Uri,
                    new UriBuilder("myspace.com").Uri,
                    new UriBuilder("java.com").Uri,
                    new UriBuilder("aol.com").Uri,
                    new UriBuilder("crunchbase.com").Uri,
                    new UriBuilder("apple").Uri
                };
            var start = DateTime.UtcNow.Ticks;
            foreach (var uri in uris)
            {
                Console.WriteLine(Parser.Parse(uri).IsValid.ToString());
            }

            var end = DateTime.UtcNow.Ticks;
            Console.WriteLine("Parse took {0} milliseconds for {1} URLs", (end - start) / TimeSpan.TicksPerMillisecond, uris.Count);
        }

        /// <summary>
        /// The test parse speed.
        /// </summary>
        [TestMethod]
        public void TestParseSpeedMin()
        {
            var uris = new List<Uri>
                {
                    new UriBuilder("amazon.com").Uri,
                    new UriBuilder("amazon.co.uk").Uri,
                    new UriBuilder("amazon.fr").Uri,
                    new UriBuilder("amazon.it").Uri,
                    new UriBuilder("amazon.jp").Uri,
                    new UriBuilder("amazon.ca").Uri,
                    new UriBuilder("amazon.cn").Uri,
                    new UriBuilder("microsoft.com").Uri,
                    new UriBuilder("google.com").Uri,
                    new UriBuilder("facebook.com").Uri,
                    new UriBuilder("myspace.com").Uri,
                    new UriBuilder("java.com").Uri,
                    new UriBuilder("aol.com").Uri,
                    new UriBuilder("crunchbase.com").Uri,
                    new UriBuilder("apple.com").Uri,
                    new UriBuilder("amazon.com").Uri,
                    new UriBuilder("amazon.co.uk").Uri,
                    new UriBuilder("amazon.fr").Uri,
                    new UriBuilder("amazon.it").Uri,
                    new UriBuilder("amazon.jp").Uri,
                    new UriBuilder("amazon.ca").Uri,
                    new UriBuilder("amazon.cn").Uri,
                    new UriBuilder("microsoft.com").Uri,
                    new UriBuilder("google.com").Uri,
                    new UriBuilder("facebook.com").Uri,
                    new UriBuilder("myspace.com").Uri,
                    new UriBuilder("java.com").Uri,
                    new UriBuilder("aol.com").Uri,
                    new UriBuilder("crunchbase.com").Uri,
                    new UriBuilder("apple.com").Uri,
                    new UriBuilder("amazon.com").Uri,
                    new UriBuilder("amazon.co.uk").Uri,
                    new UriBuilder("amazon.fr").Uri,
                    new UriBuilder("amazon.it").Uri,
                    new UriBuilder("amazon.jp").Uri,
                    new UriBuilder("amazon.ca").Uri,
                    new UriBuilder("amazon.cn").Uri,
                    new UriBuilder("microsoft.com").Uri,
                    new UriBuilder("google.com").Uri,
                    new UriBuilder("facebook.com").Uri,
                    new UriBuilder("myspace.com").Uri,
                    new UriBuilder("java.com").Uri,
                    new UriBuilder("aol.com").Uri,
                    new UriBuilder("crunchbase.com").Uri,
                    new UriBuilder("apple.com").Uri,
                    new UriBuilder("amazon.com").Uri,
                    new UriBuilder("amazon.co.uk").Uri,
                    new UriBuilder("amazon.fr").Uri,
                    new UriBuilder("amazon.it").Uri,
                    new UriBuilder("amazon.jp").Uri,
                    new UriBuilder("amazon.ca").Uri,
                    new UriBuilder("amazon.cn").Uri,
                    new UriBuilder("microsoft.com").Uri,
                    new UriBuilder("google.com").Uri,
                    new UriBuilder("facebook.com").Uri,
                    new UriBuilder("myspace.com").Uri,
                    new UriBuilder("java.com").Uri,
                    new UriBuilder("aol.com").Uri,
                    new UriBuilder("crunchbase.com").Uri,
                    new UriBuilder("apple").Uri
                };
            var start = DateTime.UtcNow.Ticks;
            foreach (var uri in uris)
            {
                Console.WriteLine(ParserMin.Parse(uri).IsValid.ToString());
            }

            var end = DateTime.UtcNow.Ticks;
            Console.WriteLine("Parse took {0} milliseconds for {1} URLs", (end - start) / TimeSpan.TicksPerMillisecond, uris.Count);
        }

        /// <summary>
        /// The test parse speed.
        /// </summary>
        [TestMethod]
        public void TestIsValid()
        {
            var uris = new List<Uri>
                {
                    new UriBuilder("apple").Uri,
                    new UriBuilder("apple.co.uk").Uri
                };
            var start = DateTime.UtcNow.Ticks;
            foreach (var uri in uris)
            {
                Console.WriteLine(Parser.Parse(uri).IsValid.ToString());
            }

            var end = DateTime.UtcNow.Ticks;
            Console.WriteLine("Parse took {0} milliseconds for {1} URLs", (end - start) / TimeSpan.TicksPerMillisecond, uris.Count);
        }
    }
}
