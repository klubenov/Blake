using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blake.Shared.Entities.Snake;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Blake.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultyController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public DifficultyController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet("{difficultyLevel}")]
        public ActionResult<SnakeDifficulty> Get(string difficultyLevel)
        {
            var difficultyValues = this.configuration.GetSection("DifficultySettings").GetSection(difficultyLevel);
            var snakeDifficulty = difficultyValues.Get<SnakeDifficulty>();
            return snakeDifficulty;
        }
    }
}