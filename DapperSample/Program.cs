using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperSample
{
    class Program
    {
        static void Main(string[] args)
        {
            string conStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            using (IDbConnection db=new SqlConnection(conStr))
            {
                //Insert
                //v1
                //var query = "insert into Product(title,price) values('P1',22)";
                //db.Query(query);
                //v2
                //Product p1 = new Product() { Title = "P2", Price = 33 };
                //var query = "insert into Product(title,price) values(@title,@price)";
                //db.Query(query, p1);
                //v3
                //string title = "P3";
                //double price = 44;
                //var query = "insert into Product(title,price) values(@title,@price)";
                //db.Execute(query, new { title, price });

                //select
                //  var query = "select * from Product";
                //var res=  db.Query<Product>(query).ToList();
                //  foreach (var item in res)
                //      Console.WriteLine($"Id={item.Id} Title={item.Title} Price={item.Price}");

                //var query = "select * from Product where price>@price";
                //int price = 30;
                //var res = db.Query<Product>(query,new {price}).ToList();
                //foreach (var item in res)
                //    Console.WriteLine($"Id={item.Id} Title={item.Title} Price={item.Price}");

                ////update
                //int id = 1;
                //double price = 88;
                //var query = "update Product set price=@price where id=@id";
                //db.Execute(query, new { id, price });

                //delete
                int id = 1;
                var query = "delete from Product where id=@id";
                db.Execute(query, new { id});
            }
        }
    }
}
