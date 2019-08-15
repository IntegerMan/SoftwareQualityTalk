using Xunit;

namespace MattEland.SoftwareQualityTalk.Tests
{
    public class YearsInJobTests
    {
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
    }
}



