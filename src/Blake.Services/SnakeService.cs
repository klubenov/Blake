using Blake.Services.Contracts;
using Blake.Shared.Entities.Snake;
using System;

namespace Blake.Services
{
    public class SnakeService : ISnakeService
    {
        public SnakeEngine GetEngine(SnakeDifficulty snakeDifficulty)
        {
            return new SnakeEngine(snakeDifficulty);
        }
    }
}
