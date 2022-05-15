using System;
using System.Collections.Generic;

namespace QBoxCore.Api.Models
{
    public partial class Highscore
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public double ScorePercent { get; set; }
        public int TimeElapsedSeconds { get; set; }
        public string UserId { get; set; }
        public DateTime EndTime { get; set; }
        public int? Age { get; set; }

        public Category Category { get; set; }
    }
}
