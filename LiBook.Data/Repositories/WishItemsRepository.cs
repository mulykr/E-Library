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
            throw new NotImplementedException();
        }

        public WishListItem Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WishListItem> Get(Expression<Func<WishListItem, bool>> filter = null, Func<IQueryable<WishListItem>, IOrderedQueryable<WishListItem>> orderBy = null, string includeProperties = "")
        {
            IQueryable<WishListItem> query = _context.WishListItems;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }

        public void Create(WishListItem item)
        {
            _context.WishListItems.Add(item);
        }

        public void Update(WishListItem item)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
