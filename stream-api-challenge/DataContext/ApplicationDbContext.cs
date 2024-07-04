using Microsoft.EntityFrameworkCore;
using stream_api_challenge.Models;

namespace stream_api_challenge.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<MovieModel> Movies { get; set; }
        public DbSet<RatingModel> Ratings { get; set; }
        public DbSet<StreamingModel> Streamings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // MovieModel and RatingModel relation
            modelBuilder.Entity<RatingModel>()
                .HasOne<MovieModel>()
                .WithMany(m => m.Ratings)
                .HasForeignKey(r => r.MovieId);

            // MovieModel and StreamingModel relation
            modelBuilder.Entity<StreamingModel>()
                .HasOne<MovieModel>()
                .WithMany(m => m.Streamings)
                .HasForeignKey(s => s.MovieId);

            // Ensure Title is unique
            modelBuilder.Entity<MovieModel>()
                .HasIndex(m => m.Title)
                .IsUnique();
        }
    }
}
