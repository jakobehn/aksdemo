using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QBox.Api.DTO;
using QBoxCore.Api.Models;

namespace QBox.Api.Controllers
{
    [Route("api/game")]
    public class GameController : Controller
    {

        [HttpPost]
        [Route("answer/{gameId}/{questionNr}/{selectedAnswer}")]
        public void SaveAnswer(int gameId, int questionNr, int selectedAnswer)
        {
            using (var ctx = new QuizBoxContext())
            {
                var game = ctx.Game.Include(g => g.GameQuestion).ThenInclude(g => g.Answer).First(g => g.Id == gameId);
                var question = game.GameQuestion.First(q => q.Order == questionNr);
                question.Answer = ctx.Answer.First(a => a.Id == selectedAnswer);
                ctx.SaveChanges();
            }
        }

        [HttpPost]
        [Route("start/{category}/{nrquestions}")]
        public GameDTO StartNewGame(string category, int nrQuestions)
        {
            int gameId;
            using (var ctx = new QuizBoxContext())
            {
                var selectedCategory = ctx.Category.FirstOrDefault(c => c.Name == category);
                var newGame = new Game()
                {
                    Category = selectedCategory,
                    Start = DateTime.Now,
                    UserId = "1234567890",
                };

                var questionsInGame = ctx.Question.Include(c => c.Category).Where(q => q.Category.Id == selectedCategory.Id).AsEnumerable().OrderBy(
                    q => Guid.NewGuid()).Take(5).ToList();
                newGame.GameQuestion = new List<GameQuestion>();
                for (int i = 1; i <= questionsInGame.Count(); i++)
                {
                    newGame.GameQuestion.Add(
                        new GameQuestion()
                        {
                            Game = newGame,
                            Question = questionsInGame[i-1],
                            Order = i
                        });
                }
                ctx.Game.Add(newGame);
                ctx.SaveChanges();
                gameId = newGame.Id;
            }

            using (var ctx = new QuizBoxContext())
            {
                var game = ctx.Game.First(g => g.Id == gameId);
                return new GameDTO
                {
                    Id = game.Id,
                    Start = game.Start
                };
            }
        }

        [HttpGet]
        [Route("{gameid}/{questionNr}")]
        public QuestionDTO GetQuestion(int gameId, int questionNr)
        {
            using (var ctx = new QuizBoxContext())
            {
                var game = ctx.Game
                    .Include(g => g.GameQuestion)
                        .ThenInclude(g => g.Question)
                            .ThenInclude(g => g.Category)
                    .Include(g => g.GameQuestion)
                        .ThenInclude(g => g.Question)
                            .ThenInclude(g => g.Answer)
                    .FirstOrDefault(g => g.Id == gameId);

                var question = game.GameQuestion.FirstOrDefault(q => q.Order == questionNr);
                if (question == null)
                    return null;

                return new QuestionDTO(question.Id, gameId, question.Question.Text, game.Category.Name, questionNr,
                    game.GameQuestion.Count(),
                    question.Question.Answer.Select(
                        a => new QuestionChoiceDTO(a.Id, a.Text)).ToList());
            }
        }

        [HttpPost]
        [Route("finish/{gameId}")]
        public GameResultDTO Finish(int gameId)
        {
            using (var ctx = new QuizBoxContext())
            {
                var game = ctx.Game.Include(g => g.GameQuestion).ThenInclude(g => g.Answer).First(g => g.Id == gameId);
                int totalNrQuestions = game.GameQuestion.Count();
                int nrCorrectAnswers = game.GameQuestion.Count(q => q.Answer.IsCorrect);

                var result = new GameResultDTO()
                {
                    GameId = gameId,
                    CategoryId = game.CategoryId,
                    TotalNrQuestions = totalNrQuestions,
                    CorrectNrAnswers = nrCorrectAnswers,
                    StartTime = game.Start
                };
                result.ScoreMessage = GetScoreMessage((float)result.CorrectNrAnswers / (float)result.TotalNrQuestions);
                return result;
            }
        }

        public string GetScoreMessage(float scorePercent)
        {
            if(scorePercent > 0.9 )
                return "Nailed it!";
            if (scorePercent > 0.7)
                return "Wow, not bad!";
            if (scorePercent > 0.51)
                return "Not too shabby";
            if (scorePercent > 0.3)
                return "Pretty lousy, I'd say";
            if (scorePercent > 0.1)
                return "That was really bad";

            return "Boy, you suck";
        }
    }
}