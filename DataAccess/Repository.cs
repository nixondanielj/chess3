using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataAccess
{
    public class Repository<T> where T : class
    {
        internal GameContext db;
        internal DbSet<T> set;

        public Repository(GameContext context)
        {
            this.db = context;
            this.set = db.Set<T>();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> whereExpr = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> order = null,
            IEnumerable<string> includes = null)
        {
            return BuildQuery(whereExpr, order, includes);
        }

        public IQueryable<TD> Get<TD>(Expression<Func<T,TD>> select, Expression<Func<T, bool>> whereExpr = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> order = null,
            IEnumerable<string> includes = null)
        {
            IQueryable<T> items = BuildQuery(whereExpr, order, includes);
            return items.Select(select);
        }

        private IQueryable<T> BuildQuery(Expression<Func<T, bool>> whereExpr, Func<IQueryable<T>, IOrderedQueryable<T>> order, IEnumerable<string> includes)
        {
            IQueryable<T> items = set;
            if (whereExpr != null)
            {
                items = items.Where(whereExpr);
            }
            if (includes != null)
            {
                foreach (string s in includes)
                {
                    items = items.Include(s);
                }
            }
            if (order != null)
            {
                items = order(items);
            }
            return items;
        }

        public void Delete(T entity)
        {
            set.Remove(entity);
        }

        public void Add(T entity)
        {
            set.Add(entity);
        }
    }
}
