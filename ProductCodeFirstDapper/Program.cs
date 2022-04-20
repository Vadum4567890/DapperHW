using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCodeFirstDapper
{
    class Program
    {
        static void Main(string[] args)
        {
            string conStr1 = ConfigurationManager.ConnectionStrings["conStr1"].ConnectionString;
            using (IDbConnection db = new SqlConnection(conStr1))
            {
                string scriptp1 = File.ReadAllText(@"D:\VS_Projects\spu011Dapper\ProductCodeFirstDapper\Query1.txt");
                db.Query(scriptp1);
            }

            string conStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            using (IDbConnection db = new SqlConnection(conStr))
            {
                string scriptp2 = File.ReadAllText(@"D:\VS_Projects\spu011Dapper\ProductCodeFirstDapper\Query2.txt");
                db.Query(scriptp2);
            }



        }
    }
}
