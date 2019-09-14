namespace MattEland.SoftwareQualityTalk
{
    public class ResumeKeyword    
    {
        public ResumeKeyword(string keyword, int value)
        {
            Keyword = keyword;
            Value = value;
        }

        public string Keyword { get; }
        public int Value { get;  }
    }
}