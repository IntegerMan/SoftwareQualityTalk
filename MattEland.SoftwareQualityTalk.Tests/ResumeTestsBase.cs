using System;
using System.Runtime.CompilerServices;
using Autofac;
using Bogus;
using GitHub;
using JetBrains.Annotations;
using MattEland.SoftwareQualityTalk.Tests.Helpers;
using Microsoft.EntityFrameworkCore;

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

        protected EntityKeywordContext BuildContext([CallerMemberName] string caller = "")
        {
            // Specify that this should be an in-memory database instance
            var options = new DbContextOptionsBuilder<EntityKeywordContext>()
                .UseInMemoryDatabase(caller)
                .Options;

            var context = new EntityKeywordContext(options);

            // Seed with sample data
            context.Keywords.AddRange(
   new KeywordData {Id = 1, Keyword = "testing", Modifier = 5}, 
                new KeywordData {Id = 2, Keyword = "nunit", Modifier = 5}, 
                new KeywordData {Id = 3, Keyword = "xunit", Modifier = 5}, 
                new KeywordData {Id = 4, Keyword = "mstest", Modifier = 2});

            // Commit the in-memory data so we can pull it in tests
            context.SaveChanges();

            return context;
        }

        protected EntityKeywordBonusProvider BuildEntityKeywordBonusProvider()
        {
            var context = BuildContext();
            var provider = new EntityKeywordBonusProvider(context);
            return provider;
        }
    }
}