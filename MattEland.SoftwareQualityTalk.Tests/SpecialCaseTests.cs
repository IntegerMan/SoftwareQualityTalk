using System;
using Xunit;
using FluentAssertions;
using Snapper;
using Snapper.Attributes;

namespace MattEland.SoftwareQualityTalk.Tests
{
    public class SpecialCaseTests : ResumeTestsBase
    {
        [Fact]
        public void MattElandShouldAlwaysScoreWell()
        {
            // Arrange
            var resume = new ResumeInfo("Matt Eland");

            // Act
            var result = Analyze(resume);

            // Assert
            result.Score.Should().Be(int.MaxValue);
            result.Resume.Should().NotBeNull();
            result.Resume.Should().Be(resume);
            result.Resume.Jobs.Count.Should().BeLessThan(1);
        }

        [Fact]
        // [UpdateSnapshots] // This should only be active the first run or if snapshot needs updating
        public void SamplePinningTest()
        {
            // Arrange
            var resume = new ResumeInfo("Some Body");
            resume.Jobs.Add(new JobInfo("Test Engineer", "Universal Exports", 42));

            // Act
            var result = Analyze(resume);

            // Assert
            result.ShouldMatchSnapshot();
        }
    }
}
