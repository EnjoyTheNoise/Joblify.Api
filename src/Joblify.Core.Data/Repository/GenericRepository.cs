using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Joblify.Core.Data.Context;
using Joblify.Core.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Joblify.Core.Data.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly JoblifyDbContext _dbContext;
        private DbSet<T> DbSet => _dbContext.Set<T>();
        public IQueryable<T> Entities => DbSet;

        public GenericRepository(JoblifyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            DbSet.Attach(entity);
        }

        public async Task AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
        }
    }
}
