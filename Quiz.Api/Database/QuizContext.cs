using Microsoft.EntityFrameworkCore;
using Quiz.Entities.Entities;

namespace Quiz.Api.Database
{
    public class QuizContext : DbContext
    {
        public QuizContext(DbContextOptions<QuizContext> options) : base(options) { }

        public virtual DbSet<Question> Questions { get; set; }

        public virtual DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>().HasMany(q => q.Answers);
        }
    }
}
