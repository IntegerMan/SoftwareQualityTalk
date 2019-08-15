using System.Collections.Generic;
using System.Data.SqlClient;
using MattEland.SoftwareQualityTalk.Properties;

namespace MattEland.SoftwareQualityTalk
{
    public interface IKeywordBonusProvider
    {
        IDictionary<string, int> LoadKeywordBonuses();
    }

    public class KeywordBonusProvider : IKeywordBonusProvider
    {
        public IDictionary<string, int> LoadKeywordBonuses()
        {
            IDictionary<string, int> keywordBonuses = new Dictionary<string, int>();

            // Grab keywords from the local database
            using (var conn = new SqlConnection(Resources.DbConnStr))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "Select Keyword, Modifier from ResumeKeywords";
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string keyword = (string) reader[0];
                        int modifier = (int) reader[1];

                        keywordBonuses[keyword.ToLowerInvariant()] = modifier;
                    }
                }
            }

            return keywordBonuses;
        }

    }
}