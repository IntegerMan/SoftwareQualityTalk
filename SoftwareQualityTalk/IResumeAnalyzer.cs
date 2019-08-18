using JetBrains.Annotations;

namespace MattEland.SoftwareQualityTalk
{
    public interface IResumeAnalyzer
    {
        AnalysisResult Analyze([NotNull] ResumeInfo resume, [CanBeNull] IKeywordBonusProvider bonusProvider);
    }
}