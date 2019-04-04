using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using E_Library.Models;

namespace E_Library.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<AuthorBook> AuthorBooks { get; set; }
    }
}
