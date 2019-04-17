using LiBook.Data.Entities;
using LiBook.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LiBook.Data.Repositories
{
    class UserRepository:IRepository<UserProfile>
    {
        private readonly ApplicationDbContext _context;
        private DbSet<UserProfile> _users;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            _users = context.UserProfiles;
        }

        public IEnumerable<UserProfile> GetList()
        {
            return _users;
        }

        public UserProfile Get(string id)
        {
            return _users.Where(a=>a.Id==id).First();
        }


        public IEnumerable<UserProfile> Get(Expression<Func<UserProfile, bool>> filter = null, Func<IQueryable<UserProfile>, IOrderedQueryable<UserProfile>> orderBy = null, string includeProperties = "")
        {
            IQueryable<UserProfile> query = _users;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public void Create(UserProfile item)
        {
            _users.Add(item);
            _context.SaveChanges();

        }

        public void Update(UserProfile item)
        {
            _users.Update(item);
            _context.SaveChanges();

        }

        public void Delete(string id)
        {
            var user = _users.Find(id);
            if (user != null)
                _users.Remove(user);
            _context.SaveChanges();

        }

        public void Save()
        {
            _context.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
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
