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

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<CommentLike> CommentLikes { get; set; }

        public virtual DbSet<Genre> Genres { get; set; }

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
            
            // WishListItem many-to-many
            modelBuilder.Entity<WishListItem>()
                .HasOne(i => i.Book)
                .WithMany(i => i.WishListItems)
                .HasForeignKey(i => i.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WishListItem>()
                .HasOne(p => p.User)
                .WithMany(p => p.WishListItems)
                .HasForeignKey(p => p.UserId);

            //Comment many-to-many
            modelBuilder.Entity<Comment>()
                .HasOne(v => v.Book)
                .WithMany(v => v.Comments)
                .HasForeignKey(i => i.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(p => p.User)
                .WithMany(p => p.Comments)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CommentLike>()
                .HasOne(i => i.Comment)
                .WithMany(i => i.CommentLikes)
                .HasForeignKey(i => i.CommentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CommentLike>()
                .HasOne(i => i.UserProfile)
                .WithMany(i => i.CommentLikes)
                .HasForeignKey(i => i.UserProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            // Genre many-to-many
            modelBuilder.Entity<BookGenre>()
                .HasKey(x => new { x.BookId, x.GenreId});

            modelBuilder.Entity<BookGenre>()
                .HasOne(p => p.Book)
                .WithMany(p => p.BooksGenres)
                .HasForeignKey(p => p.BookId);

            modelBuilder.Entity<BookGenre>()
                .HasOne(p => p.Genre)
                .WithMany(p => p.BooksGenres)
                .HasForeignKey(p => p.GenreId);
        }
    }
}
