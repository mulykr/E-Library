using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LiBook.Data.Entities;
using LiBook.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LiBook.Data.Repositories
{
    public class WishItemsRepository : IRepository<WishListItem>
    {
        private readonly ApplicationDbContext _context;

        public WishItemsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IEnumerable<WishListItem> GetList()
        {
            return _context.WishListItems
                .Include(i => i.Book)
                .Include(i => i.User);
        }

        public WishListItem Get(string id)
        {
            return _context.WishListItems
                .Include(i => i.Book)
                .Include(i => i.User)
                .FirstOrDefault(i => i.Id ==id);
        }

        public IEnumerable<WishListItem> Get(Expression<Func<WishListItem, bool>> filter = null, Func<IQueryable<WishListItem>, IOrderedQueryable<WishListItem>> orderBy = null, string includeProperties = "")
        {
            IQueryable<WishListItem> query = _context.WishListItems
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

        public void Create(WishListItem item)
        {
            _context.WishListItems.Add(item);
        }

        public void Update(WishListItem item)
        {
            _context.WishListItems.Update(item);
        }

        public void Delete(string id)
        {
            var item = _context.WishListItems.Find(id);
            if (item != null)
            {
                _context.WishListItems.Remove(item);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
