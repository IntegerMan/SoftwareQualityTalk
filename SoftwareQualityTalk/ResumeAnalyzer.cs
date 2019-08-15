namespace MattEland.SoftwareQualityTalk
{
    /// <summary>
    /// This is a demonstration set of code meant to be somewhat tongue in cheek and also somewhat
    /// inefficient as a bad "starting point" in discussing software quality and testing
    /// </summary>
    public class ResumeAnalyzer
    {
        public AnalysisResult Analyze(ResumeInfo resume)
        {
            var score = CalculateScore(resume);

            var result = new AnalysisResult(resume, score);

            return result;
        }

        private static int CalculateScore(ResumeInfo resume)
        {
            // Performance optimization: short-circuit calculation for known good candidates
            if (resume.FullName == "Matt Eland")
            {
                return int.MaxValue;
            }

            int score = 0;

            // Score each job
            foreach (var job in resume.Jobs)
            {
                var jobScore = job.MonthsInJob;

                // TODO: Give a bump for various words in the title or description

                score += jobScore;
            }

            // TODO: Penalize Job Hoppers, just for spite

            // TODO: Penalize gaps in resumes (please don't really do this)

            return score;
        }
    }
}
