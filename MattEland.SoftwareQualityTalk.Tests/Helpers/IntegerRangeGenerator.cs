using System.Collections;
using System.Collections.Generic;

namespace MattEland.SoftwareQualityTalk.Tests.Helpers
{
    public class IntegerRangeGenerator : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            for (int i = 1; i <= 5; i++)
            {
                yield return new object[] {i};
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}