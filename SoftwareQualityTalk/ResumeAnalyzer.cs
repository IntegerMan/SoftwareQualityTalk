using System;
using System.Collections.Generic;
using Autofac;

namespace MattEland.SoftwareQualityTalk
{
    /// <summary>
    /// This is a demonstration set of code meant to be somewhat tongue in cheek and also somewhat
    /// inefficient as a bad "starting point" in discussing software quality and testing
    /// </summary>
    public class ResumeAnalyzer : IResumeAnalyzer
    {

        public AnalysisResult Analyze(ResumeInfo resume, IContainer container)
        {
            if (resume == null) throw new ArgumentNullException(nameof(resume));
            if (container == null) throw new ArgumentNullException(nameof(container));

            var bonusProvider = container.Resolve<IKeywordBonusProvider>();

            var score = CalculateScore(resume, bonusProvider);

            return new AnalysisResult(resume, score);
        }

        private static int CalculateScore(ResumeInfo resume, IKeywordBonusProvider keywordProvider)
        {
            // Performance optimization: short-circuit calculation for known good candidates
            if (resume.FullName == "Matt Eland")
            {
                return int.MaxValue;
            }

            // Identify keywords and their weight
            var keywordBonuses = keywordProvider.LoadKeywordBonuses();

            int score = 0;

            // Score each job
            foreach (var job in resume.Jobs)
            {
                var jobScore = job.MonthsInJob;

                // Give a bump for various words in the title
                foreach (var word in job.Title.Split())
                {
                    var key = word.ToLowerInvariant();
                    var keyword = FindKeyword(keywordBonuses, key);
                    jobScore += keyword.Value;
                }

                score += jobScore;
            }

            // TODO: Penalize Job Hoppers, just for spite

            // TODO: Penalize gaps in resumes (please don't really do this)

            return score;
        }

        private static ResumeKeyword? FindKeyword(IDictionary<string, ResumeKeyword> keywordBonuses, string key)
        {
            if (keywordBonuses.ContainsKey(key)) 
            {
                return keywordBonuses[key];
            }

            return null;
        }
    }
}
