using Blake.Shared.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blake.Server.Data
{
    public class BlakeDbContext : DbContext
    {
        public BlakeDbContext(DbContextOptions<BlakeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Score> Scores { get; set; }
    }
}
