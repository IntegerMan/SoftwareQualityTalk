using System;

namespace MattEland.SoftwareQualityTalk
{
    public class AnalysisResult
    {
        public AnalysisResult(ResumeInfo resume, int score)
        {
            Resume = resume ?? throw new ArgumentNullException(nameof(resume));
            Score = score;
        }

        public ResumeInfo Resume { get; }
        public int Score { get; }
    }
}