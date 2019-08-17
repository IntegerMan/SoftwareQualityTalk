using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using JetBrains.Annotations;
using MattEland.SoftwareQualityTalk.Properties;

namespace MattEland.SoftwareQualityTalk
{
    /// <summary>
    /// This is a demonstration set of code meant to be somewhat tongue in cheek and also somewhat
    /// inefficient as a bad "starting point" in discussing software quality and testing
    /// </summary>
    public class ResumeAnalyzer
    {
        public AnalysisResult Analyze([NotNull] ResumeInfo resume, [CanBeNull] IKeywordBonusProvider bonusProvider)
        {
            if (resume == null) throw new ArgumentNullException(nameof(resume));

            var score = CalculateScore(resume, bonusProvider);

            var result = new AnalysisResult(resume, score);

            return result;
        }

        private static int CalculateScore([NotNull] ResumeInfo resume, [NotNull] IKeywordBonusProvider keywordProvider)
        {
            // Performance optimization: short-circuit calculation for known good candidates
            if (resume.FullName == "Matt Eland")
            {
                return int.MaxValue;
            }

            int score = 0;

            // Identify keywords and their weight
            var keywordBonuses = keywordProvider.LoadKeywordBonuses();

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
