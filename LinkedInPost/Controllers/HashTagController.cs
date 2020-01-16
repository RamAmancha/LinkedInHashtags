using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LinkedInPost.Data;
using LinkedInPost.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace LinkedInPost.Controllers
{
    /// <summary>
    /// Hashtag controller
    /// </summary>
    [Route("api/[controller]")]
    public class HashTagController : Controller
    {
        private readonly ApplicationDbContext _db;
        private IMemoryCache _cache;
        private IEnumerable<Keywords> Keywords;
        public HashTagController(ApplicationDbContext db, IMemoryCache memoryCache)
        {
            _db = db;
            _cache = memoryCache;
            Keywords = _db.GetAllKeywords();
        }

        /// <summary>
        /// Get Hash Tags
        /// </summary>
        /// <param name="inputtext"></param>
        /// <returns> List of Hash Tags</returns>
        //[HttpPost("[action]")]
        [HttpGet("[action]")]
        public string GetHashTag(string inputtext)
        {
            string hastag = string.Empty;
            string[] commonwords = { "a","about","all","an","and","are","as","at","be","been","but","by","call","can","come","could","day","did","do","down",
            "each","find","first","for","from","get","go","had","has","have","he","her","him","his","how","I","if","in","into","is","it","its","like","long","look","made",
            "make","many","may","more","my","no","not","now","number","of","on","one","or","other","out","part","people","said","see","she","so","some","than","that","the",
            "their","them","then","there","these","they","this","time","to","two","up","use","was","way","we","were","what","when","which","who","will","with","word",
            "would","write","you","your" };

            if (!string.IsNullOrEmpty(inputtext))
            {
                string[] inputstring = inputtext.Split(" ");

                foreach (string s in inputstring)
                {
                    Regex reg = new Regex("[*'\",_&#^@]");
                    string _s = reg.Replace(s, string.Empty);
                    if (!Array.Exists(commonwords, element => element == _s))
                    {
                        Keywords keywords = Keywords.FirstOrDefault(x => x.Keyword.ToLower().Contains(_s.ToLower()));
                        if (keywords != null)
                        {
                            if (keywords.Count > 100)
                            {
                                hastag = hastag + "#" + keywords.Keyword + " ";
                            }
                        }
                    }
                }
                hastag = string.Join(" ", hastag.Split(' ').Distinct());
               // hastag = result;
            }
            return hastag;
        }

        [HttpGet("[action]")]
        public IEnumerable<Keywords> GetAllTopHashTags()
        {
            return Keywords;
        }
    }
}
