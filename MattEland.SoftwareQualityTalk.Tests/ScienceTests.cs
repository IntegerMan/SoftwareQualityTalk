using Snapper;
using Snapper.Attributes;
using Xunit;

namespace MattEland.SoftwareQualityTalk.Tests
{
    public class RefactoredTests : ResumeTestsBase
    {
        [Fact]
        public void MattElandSnapshotTest()
        {
            // Arrange
            var resume = new ResumeInfo("Matt Eland");
            
            // Act
            var result = Analyze(resume);

            // Assert
            result.ShouldMatchSnapshot();
        }

    }
}