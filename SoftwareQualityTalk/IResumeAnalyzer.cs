using Autofac;
using JetBrains.Annotations;

namespace MattEland.SoftwareQualityTalk
{
    public interface IResumeAnalyzer
    {
        [NotNull]
        AnalysisResult Analyze([NotNull] ResumeInfo resume, [NotNull] IContainer container);
    }
}