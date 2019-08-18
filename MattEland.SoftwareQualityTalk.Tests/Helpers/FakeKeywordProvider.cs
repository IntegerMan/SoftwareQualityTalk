using System.Collections.Generic;

namespace MattEland.SoftwareQualityTalk.Tests
{
    public class FakeKeywordProvider : IKeywordBonusProvider
    {
        public IDictionary<string, int> LoadKeywordBonuses() 
            => new Dictionary<string, int>
            {
                ["testing"] = 5, 
                ["xunit"] = 5, 
                ["nunit"] = 5, 
                ["mstest"] = 2 // Sorry, MSTest
            };
    }
}

