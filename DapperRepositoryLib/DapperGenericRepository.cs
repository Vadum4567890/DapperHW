using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DapperRepositoryLib
{
    public class DapperGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private string tableName;
        private IDbConnection dbConnection;

        public DapperGenericRepository(string tableName, IDbConnection dbConnection)
        {
            this.tableName = tableName;
            this.dbConnection = dbConnection;
        }

        private IEnumerable<PropertyInfo> GetProperties => typeof(TEntity).GetProperties();
        private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listProperties)
        {
            return (from prop in listProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                    select prop.Name
                ).ToList();
        }

        private string GenerateInsertQuery()
        {
            //insert into Product([Name],[Price]) values(....)
            var insertQuery = new StringBuilder($"INSERT INTO {tableName}");
            insertQuery.Append("(");
            var properties = GenerateListOfProperties(GetProperties);
            properties.ForEach(prop =>
            {
                if (prop != "Id")
                    insertQuery.Append($"[{prop}],");

            }

                );

            insertQuery.Remove(insertQuery.Length - 1, 1);
            insertQuery.Append(") VALUES (");
            properties.ForEach(prop =>
            {
                if (prop != "Id")
                    insertQuery.Append($"@{prop},");
            }

                );
            insertQuery.Remove(insertQuery.Length - 1, 1);
            insertQuery.Append(")");
            return insertQuery.ToString();
        }

        private string GenerateUpdateQuery()
        {
            var updateQuery = new StringBuilder($"UPDATE {tableName} SET ");

            var properties = GenerateListOfProperties(GetProperties);

            properties.ForEach(property =>
            {
                if (!property.Equals("Id"))
                {
                    updateQuery.Append($"{property}=@{property},");
                }
            });

            updateQuery.Remove(updateQuery.Length - 1, 1); //remove last comma

            updateQuery.Append(" WHERE Id=@Id");

            return updateQuery.ToString();
        }
        public int Add(TEntity item)
        {
            var insertQuery = GenerateInsertQuery();
            return dbConnection.Execute(insertQuery, item);
        }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().AsQueryable().Where(predicate).ToList();
        }

        public TEntity FindById(object id)
        {
            return dbConnection.Query<TEntity>($"SELECT * FROM {tableName} WHERE Id=@Id", new { Id = id }).FirstOrDefault();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbConnection.Query<TEntity>($"SELECT * FROM {tableName}");
        }

        public int Remove(int id)
        {
            return dbConnection.Execute($"DELETE FROM {tableName} WHERE Id=@id", new { Id = id });
        }

        public int Update(TEntity item)
        {
            var insertQuery = GenerateUpdateQuery();
            return dbConnection.Execute(insertQuery, item);
        }
    }
}
