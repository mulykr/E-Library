using LiBook.Data.Entities;
using LiBook.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LiBook.Data.Repositories
{
    public class UserRepository : IRepository<UserProfile>
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<UserProfile> GetList()
        {
            return _context.UserProfiles
                .Include(i => i.Comments)
                .Include(i => i.WishListItems);
        }

        public UserProfile Get(string id)
        {
            return _context.UserProfiles
                .Include(i => i.Comments)
                .Include(i => i.WishListItems)
                .FirstOrDefault(a => a.Id==id);
        }


        public IEnumerable<UserProfile> Get(Expression<Func<UserProfile, bool>> filter = null, Func<IQueryable<UserProfile>, IOrderedQueryable<UserProfile>> orderBy = null, string includeProperties = "")
        {
            IQueryable<UserProfile> query = _context.UserProfiles
                .Include(i => i.Comments)
                .Include(i => i.WishListItems);
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

        public void Create(UserProfile item)
        {
            _context.UserProfiles.Add(item);

        }

        public void Update(UserProfile item)
        {
            var user = _context.UserProfiles.Find(item.Id);
            user.FirstName = item.FirstName;
            user.LastName = item.LastName;
            _context.UserProfiles.Update(user);
        }

        public void Delete(string id)
        {
            var user = _context.UserProfiles.Find(id);
            if (user != null)
                _context.UserProfiles.Remove(user);

        }

        public void Save()
        {
            _context.SaveChanges();
        }

        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UserRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
