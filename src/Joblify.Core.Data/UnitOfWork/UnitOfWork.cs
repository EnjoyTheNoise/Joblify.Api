using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Joblify.Core.Data.Context;
using Joblify.Core.Data.Models;
using Joblify.Core.Data.Repository;
using Joblify.Core.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Joblify.Core.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly JoblifyDbContext _dbContext;
        public IRepository<User> UserRepository => new GenericRepository<User>(_dbContext);
        public IRepository<File> FileRepository => new GenericRepository<File>(_dbContext);
        public IRepository<Role> RoleRepository => new GenericRepository<Role>(_dbContext);
        public IRepository<ExternalProvider> ExternalProviderRepository => new GenericRepository<ExternalProvider>(_dbContext);

        public UnitOfWork(JoblifyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void RejectChanges()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }
    }
}
