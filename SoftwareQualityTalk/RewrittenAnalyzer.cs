using System;
using Autofac;

namespace MattEland.SoftwareQualityTalk
{
    public class RewrittenAnalyzer : IResumeAnalyzer
    {
        public AnalysisResult Analyze(ResumeInfo resume, IContainer container)
        {
            return new AnalysisResult(resume, int.MaxValue);
        }
    }
}