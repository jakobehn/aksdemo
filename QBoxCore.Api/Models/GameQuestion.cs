using System;
using System.Collections.Generic;

namespace QBoxCore.Api.Models
{
    public partial class GameQuestion
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int QuestionId { get; set; }
        public int Order { get; set; }
        public int? AnswerId { get; set; }

        public Answer Answer { get; set; }
        public Game Game { get; set; }
        public Question Question { get; set; }
    }
}
