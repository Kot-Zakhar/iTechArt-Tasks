using Microsoft.EntityFrameworkCore;
using ShareMe.DataAccessLayer.Entity;
using System;

namespace ShareMe.DataAccessLayer.Context
{
    public class ShareMeContext : DbContext
    {
        public ShareMeContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ShareMeDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Comment>().ToTable("Comment");
            modelBuilder.Entity<Post>().ToTable("Post");
            modelBuilder.Entity<Tag>().ToTable("Tag");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<PostTag>().ToTable("PostTag").HasKey(pt => new { pt.PostId, pt.TagId });

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
    }
}
