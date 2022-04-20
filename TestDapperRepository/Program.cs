using DapperRepositoryLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDapperRepository
{
    class Program
    {
        static void Main(string[] args)
        {
            string connection= ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            GenericUnitOfWork work = new GenericUnitOfWork(connection);
         IGenericRepository<Product> repositoryProduct=   work.Repository<Product>();
            //insert
            //repositoryProduct.Add(new Product() { Title = "P1", Price=11 });
            //update
            //Product product=  repositoryProduct.FindById(1);
            //  product.Title = "Sony";
            //  repositoryProduct.Update(product);

            //delete
            repositoryProduct.Remove(1);

        }
    }
}
