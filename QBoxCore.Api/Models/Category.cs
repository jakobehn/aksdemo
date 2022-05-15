using System;
using System.Collections.Generic;

namespace QBoxCore.Api.Models
{
    public partial class Category
    {
        public Category()
        {
            Game = new HashSet<Game>();
            Highscore = new HashSet<Highscore>();
            Question = new HashSet<Question>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Game> Game { get; set; }
        public ICollection<Highscore> Highscore { get; set; }
        public ICollection<Question> Question { get; set; }
    }
}
