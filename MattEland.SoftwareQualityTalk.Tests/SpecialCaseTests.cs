using System;
using Xunit;

namespace MattEland.SoftwareQualityTalk.Tests
{
    public class SpecialCaseTests
    {
        [Fact]
        public void MattElandShouldAlwaysScoreWell()
        {
            // Arrange
            var analyzer = new ResumeAnalyzer();
            var resume = new ResumeInfo("Matt Eland");

            // Act
            var result = analyzer.Analyze(resume);

            // Assert
            Assert.Equal(int.MaxValue, result.Score);
        }
    }
}
