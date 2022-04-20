using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCategoryDapper
{
    class Program
    {
        static void Main(string[] args)
        {
            string conStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            using (IDbConnection db = new SqlConnection(conStr))
            {
                //insert
                //var query = "insert into Category(name) values(@name)";
                //string name = "Phone";
                //db.Execute(query, new { name });

                var queryCategory = "select id from Category where name=@name";
                string name = "Phone";
              var item=  db.Query<Category>(queryCategory, new { name }).Select(x=>new { id=x.Id}).FirstOrDefault();

                var queryProduct = "insert into Product(title,price,idCategory) values(@title,@price,@idCategory)";
                db.Execute(queryProduct, new { Title = "p1", Price = 55, IdCategory = item.id });

            }
        }
    }
}
