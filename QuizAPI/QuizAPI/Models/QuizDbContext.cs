using Microsoft.EntityFrameworkCore;

namespace QuizAPI.Models
{
    //DbContext is the primary class in EF Core that manages database connections and operations.
    //and acts as a bridge between C# model classes  and the database tables.
    public class QuizDbContext : DbContext
    {
        //constructor passes database settings to EF Core
        public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options)
        { }
        //DbSet<T> : A collection of all rows in the database table that matches model T.
        //Questions is a table in database

        public DbSet<Question> Questions { get; set; }
        public DbSet<Participant> Participants { get; set; }
    }
}
   