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

        private IRepository<User> _userRepository;
        private IRepository<File> _fileRepository;
        private IRepository<Role> _roleRepository;
        private IRepository<ExternalProvider> _externalProviderRepository;

        public IRepository<User> UserRepository => _userRepository ?? (_userRepository = new GenericRepository<User>(_dbContext));
        public IRepository<File> FileRepository => _fileRepository ?? (_fileRepository = new GenericRepository<File>(_dbContext));
        public IRepository<Role> RoleRepository => _roleRepository ?? (_roleRepository = new GenericRepository<Role>(_dbContext));
        public IRepository<ExternalProvider> ExternalProviderRepository => _externalProviderRepository ?? (_externalProviderRepository = new GenericRepository<ExternalProvider>(_dbContext));

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
