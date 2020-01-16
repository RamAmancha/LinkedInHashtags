using LinkedInPost.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkedInPost.Models
{
    public static class IKeywords
    {
        public static Keywords GetKeywordCount(this ApplicationDbContext context, string keyword)
        {
            return context.keywords
                .FirstOrDefault(c => c.Keyword.Equals(keyword));
        }

        public static IEnumerable<Keywords> GetAllKeywords(this ApplicationDbContext context)
        {
            return context.keywords;
        }
    }
}
