using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Bogus;
using Shouldly;
using TechTalk.SpecFlow;

namespace MattEland.SoftwareQualityTalk.SpecFlow
{
    [Binding]
    public sealed class StepDefinitions
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _context;

        private readonly Faker _bogus = new Faker();
        private ResumeInfo _resume;

        public StepDefinitions(ScenarioContext injectedContext)
        {
            _context = injectedContext;
        }

        [Given(@"A resume")]
        public void GivenAResume()
        {
            _resume = new ResumeInfo(_bogus.Person.FullName);
        }

        [When(@"the candidate has held a job for (.*) months")]
        public void WhenTheCandidateHasHeldAJobForMonths(int months)
        {
            var job = new JobInfo(_bogus.Company.CatchPhrase(), 
                _bogus.Company.CompanyName(), 
                months);

            _resume.Jobs.Add(job);
        }
        
        [Then(@"their score should be (.*)")]
        public void ThenMyScoreShouldBe(int expected)
        {
            var analyzer = new ResumeAnalyzer();
            var container = BuildContainer();
            var result = analyzer.Analyze(_resume, container);

            result.Score.ShouldBe(expected);
        }

        private IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<KeywordBonusProvider>().As<IKeywordBonusProvider>();

            return builder.Build();
        }
    }
}
