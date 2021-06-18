using Blog.DataAccess.Configurations;
using Blog.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess
{
    public class BlogContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new LogConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new SentEmailsConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<PostCategory>().HasKey(x => new { x.PostId,x.CategoryId});
            modelBuilder.Entity<PostComment>().HasKey(x => new { x.PostId,x.CommentId });
            modelBuilder.Entity<UserUseCase>().HasKey(x => new { x.UseCaseId, x.UserId });
            modelBuilder.Entity<PostRating>().HasKey(x => new { x.PostId, x.UserId });


            modelBuilder.Entity<Comment>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Post>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<SentEmails>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Log>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<PostRating>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<PostComment>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<PostCategory>().HasQueryFilter(x => !x.IsDeleted);









        }
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is BaseEntity e)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            e.IsActive = true;
                            e.CreatedAt = DateTime.Now;
                            e.IsDeleted = false;
                            e.ModifiedAt = null;
                            e.DeletedAt = null;
                            break;
                        case EntityState.Modified:
                            e.ModifiedAt = DateTime.Now;
                            break;
                    }
                }
               
            }

            return base.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=Blog_asp;Integrated Security=True");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<PostComment> PostComments { get; set; }

        public DbSet<SentEmails> SentEmails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserUseCase> UserUseCases { get; set; }
        public DbSet<PostRating> PostRatings { get; set; }



    }
}
