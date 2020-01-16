using System;
using System.ComponentModel.DataAnnotations;

namespace LinkedInPost.Models
{
    public class Keywords
    {
        [Key]
        public int ID { get; set; }
        //[Index(IsUnique = true)]
        public string Keyword { get; set; }
        public Int64 Count { get; set; }
    }
}
