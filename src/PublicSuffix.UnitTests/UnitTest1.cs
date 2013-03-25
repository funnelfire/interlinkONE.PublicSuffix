using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PublicSuffix.UnitTests
{
    [TestClass]
    public class RulesListTests
    {
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
    }
}
