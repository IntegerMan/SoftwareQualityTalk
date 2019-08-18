using System;

namespace MattEland.SoftwareQualityTalk.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            ResumeInfo resume = GetResumeFromArgs(args);

            IResumeAnalyzer analyzer = GetResumeAnalyzer();

            var result = analyzer.Analyze(resume, new KeywordBonusProvider());

            Console.WriteLine($"{resume.FullName}'s resume score is {result.Score}");
        }

        private static IResumeAnalyzer GetResumeAnalyzer()
        {
            if (new UseNewAnalyzer().FeatureEnabled) // Reads from UseNewAnalyzer setting
            {
                return new RewrittenAnalyzer();
            }
            else
            {
                return new ResumeAnalyzer();
            }
        }

        private static ResumeInfo GetResumeFromArgs(string[] args)
        {
            // We'll just pretend this actually does complex logic to grab the resume
            return new ResumeInfo("Bruce Wayne");
        }
    }
}
