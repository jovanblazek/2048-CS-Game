using System;
using System.Collections.Generic;
using System.Text;
using game1024Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace game1024Core.Services
{
    public class Game1024DbContext : DbContext
    {
        public DbSet<Score> Scores{ get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=game1024;Trusted_Connection=True;");
        }
    }
}
