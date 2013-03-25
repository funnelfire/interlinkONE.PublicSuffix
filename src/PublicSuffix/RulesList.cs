using System.IO;
using System.Linq;
using System.Text;

using PublicSuffix.Rules;
using System.Net;
using System;
using System.Collections.Generic;

namespace PublicSuffix
{
    /// <summary>
    /// From: http://publicsuffix.org/format/
    /// - The Public Suffix List consists of a series of lines, separated by \n.
    /// - Each line is only read up to the first whitespace; entire lines can also be commented using //.
    /// - Each line which is not entirely whitespace or begins with a comment contains a rule.
    /// See http://mxr.mozilla.org/mozilla-central/source/netwerk/dns/effective_tld_names.dat?raw=1 for the latest file.
    /// </summary>
    public class RulesList : List<Rule>
    {
        /// <summary>
        /// Creates a new RulesList
        /// <param name="fileName">The rules list file to load</param>
        /// </summary>
        RulesList(string fileName)
            : base()
        {
            // set the filename for posterity, 
            // Note: Must be set BEFORE update is called.
            FileName = fileName;

            // update the rules list file to the latest version.
            Update();

            // using the hopefully updated version load it.
            this.AddRange(FromFile());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RulesList"/> class. 
        /// </summary>
        /// <param name="stream"> The rules list stream to load.  </param>
        public RulesList(StreamReader stream)
            : base()
        {
            this.AddRange(FromStream(stream));
        }

        /// <summary>
        /// The file that this rules list is persisted in.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// IsValidRule, returns true when a rule string is valid.
        /// </summary>
        /// <param name="strRule">A rule string.</param>
        /// <returns><c>true</c> when the rule string is valid.</returns>
        private static bool IsValidRule(string strRule)
        {
            return !string.IsNullOrEmpty(strRule) && !strRule.StartsWith("//");
        }

        /// <summary>
        /// Updates the rule list from the original source.
        /// <param name="fileName">The file name that will be updated</param>
        /// </summary>
        public void Update()
        {
            UriBuilder tmpUriBuilder = new UriBuilder(RuleListFullPath);

            tmpUriBuilder.Query = "raw=1";

            using (WebClient client = new WebClient())
            using (Stream rawDataStream = client.OpenRead(tmpUriBuilder.Uri))
            using (TextReader rawDataReader = new StreamReader(rawDataStream))
                File.WriteAllText(FileName, rawDataReader.ReadToEnd());
        }

        /// <summary>
        /// Reads a PublixSuffix formatted file.
        /// </summary>
        /// <param name="file">The a text file.</param>
        /// <param name="update">Should the file be updated using latest. (default's to false)</param>
        /// <returns>An array of <see cref="Rule" />s.</returns>
        Rule[] FromFile()
        {
            var lines = (from line in File.ReadAllLines(FileName, Encoding.UTF8)
                         where IsValidRule(line)
                         select RuleFactory.Get(line)).ToArray();

            return this.ToArray();
        }

        /// <summary>
        /// Reads a PublicSuffix from a stream.
        /// </summary>
        /// <param name="stream"> The stream. </param>
        /// <returns> An array of <see cref="Rule"/>s. </returns>
        private IEnumerable<Rule> FromStream(StreamReader stream)
        {
            var rules = new List<Rule>();
            string line;
            while ((line = stream.ReadLine()) != null)
            {
                if (IsValidRule(line))
                {
                    rules.Add(RuleFactory.Get(line));
                }
            }
            return rules.ToArray();
        }

        /// <summary>
        /// The web location where the rules list may be found.
        /// <remarks>We may decide to change where and how this is stored</remarks>
        /// </summary>
        const string RuleListFullPath = "http://mxr.mozilla.org/mozilla-central/source/netwerk/dns/effective_tld_names.dat";
    }

    /// <summary>
    /// From: http://publicsuffix.org/format/
    /// - The Public Suffix List consists of a series of lines, separated by \n.
    /// - Each line is only read up to the first whitespace; entire lines can also be commented using //.
    /// - Each line which is not entirely whitespace or begins with a comment contains a rule.
    /// See http://mxr.mozilla.org/mozilla-central/source/netwerk/dns/effective_tld_names.dat?raw=1 for the latest file.
    /// </summary>
    [Obsolete("This version of rules list has been deprecated")]
    public static class RulesListDeprecated
    {
        /// <summary>
        /// Reads a PublixSuffix formatted file.
        /// </summary>
        /// <param name="file">The a text file.</param>
        /// <param name="update">Should the file be updated using latest. (default's to false)</param>
        /// <returns>An array of <see cref="Rule" />s.</returns>
        public static Rule[] FromFile(string file)
        {
            var lines = (from line in File.ReadAllLines(file, Encoding.UTF8)
                         where IsValidRule(line)
                         select RuleFactory.Get(line)).ToArray();

            return lines;
        }

        /// <summary>
        /// IsValidRule, returns true when a rule string is valid.
        /// </summary>
        /// <param name="strRule">A rule string.</param>
        /// <returns><c>true</c> when the rule string is valid.</returns>
        private static bool IsValidRule(string strRule)
        {
            return !string.IsNullOrEmpty(strRule) && !strRule.StartsWith("//");
        }

        /// <summary>
        /// Updates the rule list from the original source.
        /// <param name="fileName">The file name that will be updated</param>
        /// </summary>
        public static void Update(string fileName)
        {
            UriBuilder tmpUriBuilder = new UriBuilder(RuleListFullPath);

            tmpUriBuilder.Query = "raw=1";

            using (WebClient client = new WebClient())
            using (Stream rawDataStream = client.OpenRead(tmpUriBuilder.Uri))
            using (TextReader rawDataReader = new StreamReader(rawDataStream))
                File.WriteAllText(fileName, rawDataReader.ReadToEnd());
        }

        /// <summary>
        /// The web location where the rules list may be found.
        /// <remarks>We may decide to change where and how this is stored</remarks>
        /// </summary>
        const string RuleListFullPath = "http://mxr.mozilla.org/mozilla-central/source/netwerk/dns/effective_tld_names.dat";
    }
}
