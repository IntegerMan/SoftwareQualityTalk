using System;
using Xunit;
using Shouldly;

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
            result.Score.ShouldBe(int.MaxValue);
            result.Resume.ShouldNotBeNull();
            result.Resume.ShouldBe(resume);
            result.Resume.Jobs.Count.ShouldBeLessThan(1);
        }
    }
}
