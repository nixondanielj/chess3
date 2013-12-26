using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityAbstractions;

namespace DataAccess
{
    public class UnitOfWork : IDisposable
    {
        private GameContext db { get; set; }
        private ILogger log { get; set; }
        public UnitOfWork(ILogger log)
        {
            this.db = new GameContext();
            this.log = log;
        }
        public Repository<T> GetRepository<T>() where T: class
        {
            return new Repository<T>(db);
        }

        public void Dispose()
        {
            db.SaveChanges();
            db.Dispose();
        }
    }
}
