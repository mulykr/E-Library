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
        public DbSet<E_Library.Models.Author> Author { get; set; }

        public DbSet<E_Library.Models.Book> Book { get; set; }

        public DbSet<AuthorBook> AuthorBooks { get; set; }
    }
}
