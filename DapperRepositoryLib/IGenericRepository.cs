using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DapperRepositoryLib
{
    public interface IGenericRepository<TEntity> where TEntity:class
    {
        int Add(TEntity item);
        TEntity FindById(object id);

        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicateExpression);

        IEnumerable<TEntity> GetAll();
        int Remove(int id);
        int Update(TEntity item);
    }
}
