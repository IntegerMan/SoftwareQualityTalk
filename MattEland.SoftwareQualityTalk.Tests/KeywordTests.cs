using System.Collections.Generic;
using Moq;
using Shouldly;
using Xunit;

namespace MattEland.SoftwareQualityTalk.Tests
{
    public class KeywordTests : ResumeTestsBase
    {
        [Theory]
        [InlineData("XUnit Testing Guru", 11)]
        [InlineData("NUnit Engineer", 6)]
        [InlineData("MSTest Developer", 3)]
        [InlineData("Intern", 1)]
        public void ShouldScoreJobsBasedOnKeywords(string title, int expectedScore)
        {
            // Arrange
            var resume = new ResumeInfo("Someone Awesome");
            resume.Jobs.Add(new JobInfo(title, "Universal Exports", 1));

            // Act
            var result = Analyze(resume);

            // Assert
            Assert.Equal(expectedScore, result.Score);
        }

        [Fact]
        public void CustomKeywordBonusesShouldCalculateCorrectly()
        {
            // Arrange
            var resume = new ResumeInfo("Someone Awesome");
            resume.Jobs.Add(new JobInfo("Attendee", "CODNG", 1));

            var mock = new Mock<IKeywordBonusProvider>();
            mock.Setup(provider => provider.LoadKeywordBonuses())
                .Returns(new Dictionary<string, ResumeKeyword>
                {
                    ["attendee"] = new ResumeKeyword("Attendee", 50)
                });

            // Act
            var result = Analyze(resume, mock.Object);

            // Assert
            result.Score.ShouldBe(51); // 50 bonus + 1 month in job

            mock.Verify(provider => provider.LoadKeywordBonuses(), Times.Exactly(1));
        }

        [Fact]
        public void EntityFrameworkKeywordTestsShouldFunction()
        {
            // Arrange
            var resume = new ResumeInfo("Someone Awesome");
            resume.Jobs.Add(new JobInfo("XUnit Tester", "Universal Exports", 1));

            var provider = BuildEntityKeywordBonusProvider();

            // Act
            var result = Analyze(resume, provider);

            // Assert
            result.Score.ShouldBe(6);
        }
    }

}