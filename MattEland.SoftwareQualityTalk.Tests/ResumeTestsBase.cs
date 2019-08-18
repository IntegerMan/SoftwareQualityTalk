using System;
using System.Runtime.CompilerServices;
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

            try
            {
                return Scientist.Science<AnalysisResult>(caller,
                    experiment =>
                    {
                        experiment.Use(() => legacyAnalyzer.Analyze(resume, bonusProvider));
                        // experiment.Try(() => newAnalyzer.Analyze(resume, bonusProvider));
                        experiment.Compare((x, y) => x.Score == y.Score);
                    });
            }
            catch (AggregateException agEx) // Scientist wraps exceptions into an aggregate
            {
                var opEx = agEx.InnerException; // Scientist wraps exceptions into an OperationException
                throw opEx.InnerException; // This is the actual exception that we threw on comparing assertions
            }
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