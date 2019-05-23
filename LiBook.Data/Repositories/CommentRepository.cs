using LiBook.Data.Entities;
using LiBook.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LiBook.Data.Repositories
{
    public class CommentRepository:IRepository<Comment>
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IEnumerable<Comment> GetList()
        {
            return _context.Comments
                .Include(i => i.CommentLikes)
                .Include(i => i.Book)
                .Include(i => i.User);
        }

        public IEnumerable<Comment> GetListByBook(Book book)
        {
            _context.Comments
                .Include(i => i.CommentLikes)
                .Include(i => i.Book)
                .Where(i => i.BookId == book.Id);
            return book.Comments;
        }

        public Comment Get(string id)
        {
            return _context.Comments
                .Include(i => i.CommentLikes)
                .Include(i => i.Book)
                .Include(i => i.User)
                .FirstOrDefault(i => i.Id == id);
        }

        public IEnumerable<Comment> Get(Expression<Func<Comment, bool>> filter = null, Func<IQueryable<Comment>, IOrderedQueryable<Comment>> orderBy = null, string includeProperties = "")
        {
            IQueryable<Comment> query = _context.Comments
                .Include(i => i.CommentLikes)
                .Include(i => i.Book)
                .Include(i => i.User);
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return orderBy?.Invoke(query).ToList() ?? query.ToList();
        }

        public void Create(Comment item)
        {
            _context.Comments.Add(item);
        }

        public void Update(Comment item)
        {
            _context.Comments.Update(item);
        }

        public void Delete(string id)
        {
            var item = _context.Comments.Find(id);
            if (item != null)
            {
                _context.Comments.Remove(item);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
