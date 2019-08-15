using System;
using System.Collections.Generic;
using System.Text;

namespace MattEland.SoftwareQualityTalk
{
    public class ResumeInfo
    {
        public ResumeInfo(string fullName)
        {
            FullName = fullName;
        }

        public string FullName { get; set; }

        public IList<JobInfo> Jobs { get; set; } = new List<JobInfo>();
    }
}
