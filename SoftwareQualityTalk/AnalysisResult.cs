using System;
using JetBrains.Annotations;

namespace MattEland.SoftwareQualityTalk
{
    public class AnalysisResult
    {
        public AnalysisResult([NotNull] ResumeInfo resume, int score)
        {
            Resume = resume ?? throw new ArgumentNullException(nameof(resume));
            Score = score;
        }

        [NotNull]
        public ResumeInfo Resume { get; }

        public int Score { get; }
    }
}