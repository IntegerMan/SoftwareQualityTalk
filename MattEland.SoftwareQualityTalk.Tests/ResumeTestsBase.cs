using System;
using System.Runtime.CompilerServices;
using Autofac;
using Bogus;
using GitHub;
using JetBrains.Annotations;
using MattEland.SoftwareQualityTalk.Tests.Helpers;

namespace MattEland.SoftwareQualityTalk.Tests
{
    public abstract class ResumeTestsBase
    {
        private static bool _scientistInitialized;
        protected readonly Faker _bogus;

        protected ResumeTestsBase()
        {
            _bogus = new Faker();
        }

        protected AnalysisResult Analyze(ResumeInfo resume,
            IKeywordBonusProvider bonusProvider = null,
            [CallerMemberName] string caller = "")
        {
            InitializeResultPublisherAsNeeded();


            var legacyAnalyzer = new ResumeAnalyzer();
            var newAnalyzer = new RewrittenAnalyzer();

            bonusProvider ??= BuildBonusProvider();

            IContainer container = BuildTestContainer(bonusProvider);

            try
            {
                return Scientist.Science<AnalysisResult>(caller,
                    experiment =>
                    {
                        experiment.Use(() => legacyAnalyzer.Analyze(resume, container));
                        // experiment.Try(() => newAnalyzer.Analyze(resume, container));
                        experiment.Compare((x, y) => x.Score == y.Score);
                    });
            }
            catch (AggregateException agEx) // Scientist wraps exceptions into an aggregate
            {
                var opEx = agEx.InnerException; // Scientist wraps exceptions into an OperationException
                throw opEx.InnerException; // This is the actual exception that we threw on comparing assertions
            }
        }

        private static IContainer BuildTestContainer(IKeywordBonusProvider bonusProvider)
        {
            // We need a container builder in order to add container instances
            var builder = new ContainerBuilder();

            // Our bonus provider will be used any time an IKeywordBonusProvider is requested
            builder.RegisterInstance(bonusProvider).As<IKeywordBonusProvider>();

            return builder.Build();
        }

        protected virtual IKeywordBonusProvider BuildBonusProvider()
        {
            return new FakeKeywordProvider();
        }

        private static void InitializeResultPublisherAsNeeded()
        {
            if (_scientistInitialized) return;

            _scientistInitialized = true;
            Scientist.ResultPublisher = new ThrowIfMismatchedPublisher();
        }

        [NotNull]
        protected JobInfo BuildJobEntry() 
            => new JobInfo(
                _bogus.Hacker.Phrase(),
                _bogus.Company.CompanyName(),
                _bogus.Random.Int(1, 60 * 12));
    }
}