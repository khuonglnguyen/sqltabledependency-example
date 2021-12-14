using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            var InfoMapper = new ModelToTableMapper<Stock>();
            InfoMapper.AddMapping(s => s.price, "price");

            var connectionString = "data source=KHUONGNGUYEN;initial catalog=Stock;User Id=sa;Password=123456";
            using (var tableDependency = new SqlTableDependency<Stock>(connectionString, tableName: "tbStock", schemaName: "dbo", mapper: InfoMapper, executeUserPermissionCheck: false, includeOldValues: true))
            {
                tableDependency.OnChanged += TableDependency_Changed;
                tableDependency.OnError += TableDependency_OnError;

                tableDependency.Start();
                Console.WriteLine("Cho thay doi de thong bao");
                Console.WriteLine("Nhan phim bat ky de thoat");
                Console.ReadKey();
                tableDependency.Stop();
            }
        }

        private static void TableDependency_OnError(object sender, ErrorEventArgs e)
        {
            throw e.Error;
        }

        private static void TableDependency_Changed(object sender, RecordChangedEventArgs<Stock> e)
        {
            Console.WriteLine(Environment.NewLine);
            if (e.ChangeType != ChangeType.None)
            {
                var changedEntity = e.Entity;
                Console.WriteLine("Id: " + changedEntity.StockId);
                Console.WriteLine("Quantity: " + changedEntity.quantity);
                Console.WriteLine("Price: " + changedEntity.price);
                Console.WriteLine("Product Name: " + changedEntity.productName);
                Console.WriteLine("Update On: " + changedEntity.updateOn);
            }
        }
    }
}
