using System;
using System.Collections.Generic;

namespace QBoxCore.Api.Models
{
    public partial class Question
    {
        public Question()
        {
            Answer = new HashSet<Answer>();
            GameQuestion = new HashSet<GameQuestion>();
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Text { get; set; }

        public Category Category { get; set; }
        public ICollection<Answer> Answer { get; set; }
        public ICollection<GameQuestion> GameQuestion { get; set; }
    }
}
