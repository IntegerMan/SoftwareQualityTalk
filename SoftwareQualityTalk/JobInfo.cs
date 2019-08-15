using System;

namespace MattEland.SoftwareQualityTalk
{
    public class JobInfo
    {
        public JobInfo(string title, string organization, int monthsInJob)
        {
            if (monthsInJob <= 0) throw new ArgumentOutOfRangeException(nameof(monthsInJob));

            Title = title ?? throw new ArgumentNullException(nameof(title));
            Organization = organization ?? throw new ArgumentNullException(nameof(organization));
            MonthsInJob = monthsInJob;
        }

        public string Title { get; set; }
        public string Organization { get; set; }
        public int MonthsInJob { get; set; }
    }
}