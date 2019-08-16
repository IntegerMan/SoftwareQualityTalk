using System.Linq;
using AutoFixture;
using AutoFixture.Kernel;
using Bogus;
using Shouldly;
using Xunit;

namespace MattEland.SoftwareQualityTalk.Tests
{
    public class YearsInJobTests
    {
        private readonly Faker _bogus;

        [Theory]
        [InlineData(1, 1)]
        [InlineData(3, 3)]
        [InlineData(10, 10)]
        public void SingleJobShouldScoreForNumberOfMonthsInJob(int numMonths, int expectedScore)
        {
            // Arrange
            var analyzer = new ResumeAnalyzer();
            var resume = new ResumeInfo("John Doe");
            resume.Jobs.Add(new JobInfo("Organ Donor", "State of Ohio", numMonths));

            // Act
            var result = analyzer.Analyze(resume);

            // Assert
            Assert.Equal(expectedScore, result.Score);
        }

        public YearsInJobTests()
        {
            _bogus = new Faker();
        }

        [Fact]
        public void SingleJobScoreShouldMatchExpectedUsingBogus()
        {
            // Arrange
            var resume = new ResumeInfo(_bogus.Name.FullName());
            var job = BuildJobEntry();
            resume.Jobs.Add(job);
            var analyzer = new ResumeAnalyzer();

            // Act
            var result = analyzer.Analyze(resume);

            // Assert
            result.Score.ShouldBe(job.MonthsInJob);
        }

        private JobInfo BuildJobEntry() 
            => new JobInfo(
                _bogus.Hacker.Phrase(),
                _bogus.Company.CompanyName(),
                _bogus.Random.Int(1, 60 * 12));

        [Fact]
        public void SingleJobScoreShouldMatchExpectedUsingAutoFixture()
        {
            // Arrange
            var fixture = new Fixture();
            var resume = fixture.Create<ResumeInfo>();
            int totalMonths = resume.Jobs.Sum(j => j.MonthsInJob);

            var analyzer = new ResumeAnalyzer();
            var bonusProvider = new FakeKeywordProvider();

            // Act
            var result = analyzer.Analyze(resume, bonusProvider);

            // Assert
            result.Score.ShouldBe(totalMonths);
        }
    }
}



