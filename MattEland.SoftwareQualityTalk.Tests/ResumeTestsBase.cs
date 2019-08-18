using System;
using System.Runtime.CompilerServices;
using GitHub;
using MattEland.SoftwareQualityTalk.Tests.Helpers;

namespace MattEland.SoftwareQualityTalk.Tests
{
    public abstract class ResumeTestsBase
    {
        private static bool _scientistInitialized;

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
    }
}