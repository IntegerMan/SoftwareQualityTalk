using System.Linq;
using AutoFixture;
using Shouldly;
using Xunit;

namespace MattEland.SoftwareQualityTalk.Tests
{
    public class YearsInJobTests : ResumeTestsBase
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(3, 3)]
        [InlineData(10, 10)]
        public void SingleJobShouldScoreForNumberOfMonthsInJob(int numMonths, int expectedScore)
        {
            // Arrange
            var resume = new ResumeInfo("John Doe");
            resume.Jobs.Add(new JobInfo("Organ Donor", "State of Ohio", numMonths));

            // Act
            var result = Analyze(resume);

            // Assert
            Assert.Equal(expectedScore, result.Score);
        }

        [Fact]
        public void SingleJobScoreShouldMatchExpectedUsingBogus()
        {
            // Arrange
            var resume = new ResumeInfo(_bogus.Name.FullName());
            var job = BuildJobEntry();
            resume.Jobs.Add(job);

            // Act
            var result = Analyze(resume);

            // Assert
            result.Score.ShouldBe(job.MonthsInJob);
        }

        [Fact]
        public void SingleJobScoreShouldMatchExpectedUsingAutoFixture()
        {
            // Arrange
            var fixture = new Fixture();
            var resume = fixture.Create<ResumeInfo>();
            int totalMonths = resume.Jobs.Sum(j => j.MonthsInJob);

            // Act
            var result = Analyze(resume);

            // Assert
            result.Score.ShouldBe(totalMonths);
        }
    }
}



