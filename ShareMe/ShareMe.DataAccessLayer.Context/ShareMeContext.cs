using Microsoft.EntityFrameworkCore;
using ShareMe.DataAccessLayer.Entity;
using System;

namespace ShareMe.DataAccessLayer.Context
{
    public class ShareMeContext : DbContext
    {
        public ShareMeContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PostTag>().HasKey(pt => new { pt.PostId, pt.TagId });
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
    }
}
