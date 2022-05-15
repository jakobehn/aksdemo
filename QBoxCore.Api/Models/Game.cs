using System;
using System.Collections.Generic;

namespace QBoxCore.Api.Models
{
    public partial class Game
    {
        public Game()
        {
            GameQuestion = new HashSet<GameQuestion>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public ICollection<GameQuestion> GameQuestion { get; set; }
    }
}
