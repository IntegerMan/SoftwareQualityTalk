using System;
using Xunit;
using FluentAssertions;

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
            result.Score.Should().Be(int.MaxValue);
            result.Resume.Should().NotBeNull();
            result.Resume.Should().Be(resume);
            result.Resume.Jobs.Count.Should().BeLessThan(1);
        }
    }
}
