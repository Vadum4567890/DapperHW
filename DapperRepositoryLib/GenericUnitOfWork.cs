using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperRepositoryLib
{
   public class GenericUnitOfWork
    {
        private string connectionString;

        public GenericUnitOfWork(string connectionString)
        {
            this.connectionString = connectionString;
        }
        private IDbConnection CreateConnection()
        {
            return new SqlConnection(connectionString);
        }
        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();
        public IGenericRepository<TEntity> Repository<TEntity>()where TEntity:class
        {
            if(repositories.Keys.Contains(typeof(TEntity))==true)
            {
                return repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
            }
            IGenericRepository<TEntity> repo = new DapperGenericRepository<TEntity>(typeof(TEntity).Name, CreateConnection());
            repositories.Add(typeof(TEntity), repo);
            return repo;
        }
    }
}
