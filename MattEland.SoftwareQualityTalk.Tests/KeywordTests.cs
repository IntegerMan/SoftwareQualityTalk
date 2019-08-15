using Xunit;

namespace MattEland.SoftwareQualityTalk.Tests
{
    public class KeywordTests
    {
        [Theory]
        [InlineData("XUnit Testing Guru", 11)]
        [InlineData("NUnit Engineer", 6)]
        [InlineData("MSTest Developer", 3)]
        [InlineData("Intern", 1)]
        public void ShouldScoreJobsBasedOnKeywords(string title, int expectedScore)
        {
            // Arrange
            var analyzer = new ResumeAnalyzer();
            var resume = new ResumeInfo("Someone Awesome");
            resume.Jobs.Add(new JobInfo(title, "Universal Exports", 1));

            // Act
            var result = analyzer.Analyze(resume);

            // Assert
            Assert.Equal(expectedScore, result.Score);
        }
    }
}