using System;

namespace MattEland.SoftwareQualityTalk
{
    public class RewrittenAnalyzer : IResumeAnalyzer
    {
        public AnalysisResult Analyze(ResumeInfo resume, IKeywordBonusProvider bonusProvider)
        {
            return new AnalysisResult(resume, int.MaxValue);
        }
    }
}