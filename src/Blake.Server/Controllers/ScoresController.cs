using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blake.Server.Data;
using Blake.Shared.Models.DbModels;
using Blake.Shared.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blake.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private readonly BlakeDbContext context;

        public ScoresController(BlakeDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public HighScoresViewModel Get()
        {
            var scores = this.context.Scores.OrderByDescending(s => s.Points).Take(20).ToList();
            var model = new HighScoresViewModel
            {
                Scores = scores
            };

            return model;
        }

        [HttpPost]
        public void Post(Score score)
        {
            if (this.TryValidateModel(score))
            {
                this.context.Scores.Add(score);
                context.SaveChanges();
            }
        }
    }
}