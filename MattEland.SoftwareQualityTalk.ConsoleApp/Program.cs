using System;
using Autofac;

namespace MattEland.SoftwareQualityTalk.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            ResumeInfo resume = GetResumeFromArgs(args);

            IResumeAnalyzer analyzer = GetResumeAnalyzer();

            IContainer container = BuildContainer();

            var result = analyzer.Analyze(resume, container);

            Console.WriteLine($"{resume.FullName}'s resume score is {result.Score}");
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<KeywordBonusProvider>().As<IKeywordBonusProvider>();

            return builder.Build();
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
