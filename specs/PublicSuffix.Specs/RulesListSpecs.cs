
using System.Linq;

using PublicSuffix.Rules;

using Machine.Specifications;
using System.Collections.Generic;

namespace PublicSuffix.Specs
{
    [Subject(typeof(RulesList))]
    public class when_rules_come_from_text_file
    {
        static List<Rule> rules;

        // Establish context = () => list = new RulesList();
        const string tldNamesFile = @"data\effective_tld_names.dat";

        Because of = () => rules = new RulesList(tldNamesFile);

        It returns_an_array_of_rules = () => rules.Count.ShouldBeGreaterThan(0);
        It has_no_blank_lines = () => rules.ShouldNotContain("");
        It has_no_commented_lines = () => rules.ShouldEachConformTo((rule) => !rule.ToString().StartsWith("//"));
        It has_first_line = () => rules.First().ToString().ShouldEqual("ac");
        It has_last_line = () => rules.Last().ToString().ShouldEqual("*.zw");
    }
}
