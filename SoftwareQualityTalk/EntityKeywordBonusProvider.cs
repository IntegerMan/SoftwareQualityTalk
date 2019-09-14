using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace MattEland.SoftwareQualityTalk
{
    public class EntityKeywordBonusProvider : IKeywordBonusProvider
    {
        private readonly EntityKeywordContext _context;

        public EntityKeywordBonusProvider([NotNull] EntityKeywordContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IDictionary<string, ResumeKeyword> LoadKeywordBonuses()
        {

            var keywordBonuses = new Dictionary<string, ResumeKeyword>();

            foreach (var keyword in _context.Keywords)
            {
                keywordBonuses[keyword.Keyword] = new ResumeKeyword(keyword.Keyword, keyword.Modifier);
            }

            return keywordBonuses;
        }

    }
}