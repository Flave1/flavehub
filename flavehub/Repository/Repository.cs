using flavehub.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace flavehub.Repository
{
    namespace Repository
    {
        public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
        {
            protected DataContext DataContext { get; set; }

            public RepositoryBase(DataContext dataContext)
            {
                this.DataContext = dataContext;
            }

            public IQueryable<T> FindAll()
            {
                return this.DataContext.Set<T>().AsNoTracking();
            }

            public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
            {
                return this.DataContext.Set<T>().Where(expression).AsNoTracking();
            }

            public void Create(T entity)
            {
                this.DataContext.Set<T>().Add(entity);
            }

            public void Update(T entity)
            {
                this.DataContext.Set<T>().Update(entity);
            }

            public void Delete(T entity)
            {
                this.DataContext.Set<T>().Remove(entity);
            }
        }
    }
}
