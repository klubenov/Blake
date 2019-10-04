using Blake.Shared.Entities.Snake;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blake.Services.Contracts
{
    public interface ISnakeService
    {
        SnakeEngine GetEngine(SnakeDifficulty snakeDifficulty);
    }
}
