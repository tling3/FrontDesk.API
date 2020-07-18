using FrontDesk.API.Models.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace FrontDesk.API.Data.Base
{
    public class BaseRepo<T> where T : DbContext
    {
        private readonly T _context;

        public BaseRepo(T context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            var entries = _context.ChangeTracker.Entries().Where(
                e => e.Entity is BaseDomain &&
                (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseDomain)entityEntry.Entity).ModifiedDate = DateTime.Now;
                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseDomain)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }

            return (_context.SaveChanges() > 0);
        }
    }
}
