using Bogus;
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

        public YearsInJobTests()
        {
            _bogus = new Faker();
        }

        private readonly Faker _bogus;

        [Fact]
        public void SingleJobScoreShouldMatchExpectedUsingBogus()
        {
            // Arrange
            int numMonths = _bogus.Random.Int(0, 40 * 12);
            var resume = new ResumeInfo(_bogus.Name.FullName());

            resume.Jobs.Add(new JobInfo(_bogus.Hacker.Phrase(),
                _bogus.Company.CompanyName(),
                numMonths));

            var analyzer = new ResumeAnalyzer();

            // Act
            var result = analyzer.Analyze(resume);

            // Assert
            Assert.Equal(numMonths, result.Score);
        }
    }
}



