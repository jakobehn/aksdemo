using System;
using System.Collections.Generic;

namespace QBoxCore.Api.Models
{
    public partial class Answer
    {
        public Answer()
        {
            GameQuestion = new HashSet<GameQuestion>();
        }

        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        public Question Question { get; set; }
        public ICollection<GameQuestion> GameQuestion { get; set; }
    }
}
