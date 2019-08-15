using System.Collections.Generic;
using System.Data.SqlClient;
using MattEland.SoftwareQualityTalk.Properties;

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

            // Identify keywords and their weight
            IDictionary<string, int> keywordBonuses = new Dictionary<string, int>();

            // Grab keywords from the local database
            using (var conn = new SqlConnection(Resources.DbConnStr))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "Select Keyword, Modifier from ResumeKeywords";
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string keyword = (string) reader[0];
                        int modifier = (int) reader[1];

                        keywordBonuses[keyword.ToLowerInvariant()] = modifier;
                    }
                }
            }

            // Score each job
            foreach (var job in resume.Jobs)
            {
                var jobScore = job.MonthsInJob;

                // Give a bump for various words in the title
                foreach (var word in job.Title.Split())
                {
                    var key = word.ToLowerInvariant();
                    if (keywordBonuses.ContainsKey(key))
                    {
                        jobScore += keywordBonuses[key];
                    }
                }

                // Look at description in an a very repetitive way. Because code duplication is great!
                foreach (var word in job.Description.Split())
                {
                    var key = word.ToLowerInvariant();
                    if (keywordBonuses.ContainsKey(key))
                    {
                        jobScore += keywordBonuses[key];
                    }
                }

                score += jobScore;
            }

            // TODO: Penalize Job Hoppers, just for spite

            // TODO: Penalize gaps in resumes (please don't really do this)

            return score;
        }
    }
}
