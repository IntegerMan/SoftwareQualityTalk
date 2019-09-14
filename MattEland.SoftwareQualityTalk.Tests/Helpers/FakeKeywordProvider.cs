using System.Collections.Generic;

namespace MattEland.SoftwareQualityTalk.Tests
{
    public class FakeKeywordProvider : IKeywordBonusProvider
    {
        public IDictionary<string, ResumeKeyword> LoadKeywordBonuses() 
            => new Dictionary<string, ResumeKeyword>
            {
                ["testing"] = new ResumeKeyword("Testing", 5), 
                ["xunit"] = new ResumeKeyword("XUnit", 5), 
                ["nunit"] = new ResumeKeyword("NUnit", 5), 
                ["mstest"] = new ResumeKeyword("MSTest", 1) // Sorry, MSTest
            };
    }
}

