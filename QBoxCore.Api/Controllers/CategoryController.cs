using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Polly;
using QBox.Api.DTO;
using QBox.Api.Migrations;
using QBoxCore.Api.Models;


namespace QBox.Api.Controllers
{

    [Route("api/category")]
    public class CategoryController : Controller
    {
        private readonly DbInitializer initializer;

        public CategoryController(DbInitializer initializer)
        {
            this.initializer = initializer;

        }
        public IEnumerable<CategoryDTO> Get()
        {
            using (var context = new QuizBoxContext())
            {
                context.Database.Migrate();
            }

            initializer.Seed();

            IEnumerable<CategoryDTO> categories = new List<CategoryDTO>();

            using (var ctx = new QuizBoxContext())
            {
                categories = ctx.Category.Select(
                    c => new CategoryDTO()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description
                    }).ToList();
                return categories;
            }
        }

    }
    
}
