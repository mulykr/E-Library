using LiBook.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LiBook.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Author> Authors { get; set; }

        public virtual DbSet<UserProfile> UserProfiles { get; set; }

        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<AuthorBook> AuthorBooks { get; set; }

        public virtual DbSet<WishListItem> WishListItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // AuthorBook many-to-many relationship
            modelBuilder.Entity<AuthorBook>()
                .HasKey(x => new {x.AuthorId, x.BookId});

            modelBuilder.Entity<AuthorBook>()
                .HasOne(p => p.Book)
                .WithMany(p => p.AuthorsBooks)
                .HasForeignKey(p => p.BookId);

            modelBuilder.Entity<AuthorBook>()
                .HasOne(p => p.Author)
                .WithMany(p => p.AuthorsBooks)
                .HasForeignKey(p => p.AuthorId);

            // User & WishListItem one-to-many relationship
            modelBuilder.Entity<WishListItem>()
                .HasKey(x => new {x.UserId, x.BookId});

            modelBuilder.Entity<WishListItem>()
                .HasOne(p => p.User)
                .WithMany(p => p.WishListItems)
                .HasForeignKey(p => p.UserId);
        }
    }
}
