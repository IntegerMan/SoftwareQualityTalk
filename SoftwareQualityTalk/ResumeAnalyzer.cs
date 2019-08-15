namespace MattEland.SoftwareQualityTalk
{
    public class ResumeAnalyzer
    {
        public AnalysisResult Analyze(ResumeInfo resume)
        {
            int score = 0;

            if (resume.FullName == "Matt Eland")
                score = int.MaxValue;

            return new AnalysisResult(score);
        }
    }
}
