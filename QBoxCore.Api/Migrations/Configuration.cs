using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using QBoxCore.Api.Models;

namespace QBox.Api.Migrations
{
    using System.Linq;

    public class DbInitializer
    {
        private QuizBoxContext _context;

        public DbInitializer(QuizBoxContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Category.Any())
                return;

            _context.Category.AddRange(
                new List<Category>
                {
                    new Category()
                    {
                        Name = "Sports",
                        Question = new List<Question>()
                        {
                            new Question()
                            {
                                Text = "Which country won world cup in soccer 2014",
                                Answer = new List<Answer>()
                                {
                                    new Answer()
                                    {
                                        Text = "Brazil",
                                        IsCorrect = false
                                    },
                                    new Answer()
                                    {
                                        Text = "Germany",
                                        IsCorrect = true
                                    },
                                    new Answer()
                                    {
                                        Text = "Sweden",
                                        IsCorrect = false
                                    },
                                    new Answer()
                                    {
                                        Text = "Spain",
                                        IsCorrect = false
                                    }
                                }
                            }
                        }
                    },
                    new Category()
                    {
                        Name = "Food"
                    },
                    new Category()
                    {
                        Name = "Entertainment"
                    },
                    new Category()
                    {
                        Name = "Music"
                    },
                    new Category()
                    {
                        Name = "American trivia",
                    },
                    new Category()
                    {
                        Name = "Geek",
                    },
                });

            _context.SaveChanges();
        }
    }
}
