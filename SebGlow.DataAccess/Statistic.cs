using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    public class Statistic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string owner_login { get; set; }
        public int owner_id { get; set; }
        public string owner_url { get; set; }

        [NotMapped]
        public KeyValuePair<char, int>[] letters { get; set; }

        public decimal avgStargazers { get; set; }
        public decimal avgWatchers { get; set; }
        public decimal avgForks { get; set; }
        public decimal avgSize { get; set; }

        public DateTime createdAt { get; set; }
    }
}