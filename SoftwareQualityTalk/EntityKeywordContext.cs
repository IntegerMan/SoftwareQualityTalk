using Microsoft.EntityFrameworkCore;

namespace MattEland.SoftwareQualityTalk
{
    public class EntityKeywordContext : DbContext
    {
        public EntityKeywordContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<KeywordData> Keywords { get; set; }

    }
}