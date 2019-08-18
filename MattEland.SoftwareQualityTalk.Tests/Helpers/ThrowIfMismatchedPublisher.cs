using System.Text;
using System.Threading.Tasks;
using GitHub;
using Shouldly;

namespace MattEland.SoftwareQualityTalk.Tests.Helpers
{
    public class ThrowIfMismatchedPublisher : IResultPublisher
    {
        public Task Publish<T, TClean>(Result<T, TClean> result)
        {
            if (result.Mismatched)
            {
                var sb = new StringBuilder();
                sb.AppendLine($"{result.ExperimentName} had mismatched results: ");

                foreach (var observation in result.MismatchedObservations)
                {
                    sb.AppendLine($"Value: {observation.Value}");
                }

                result.Mismatched.ShouldBeFalse(sb.ToString());
            }

            return Task.CompletedTask;
        }
    }
}