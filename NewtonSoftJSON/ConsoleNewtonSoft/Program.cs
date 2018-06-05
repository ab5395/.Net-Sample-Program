using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ConsoleNewtonSoft
{
    class Program
    {
        static void Main(string[] args)
        {

            //Simple Serializing and Deserializing JSON with JsonConvert
            Product product = new Product();
            product.Name = "Apple";
            product.ExpiryDate = new DateTime(2008, 12, 28);
            product.Price = 3.99;
            product.Sizes = new string[] { "Small", "Medium", "Large" }.ToList();

            var output = JsonConvert.SerializeObject(product);
            Console.WriteLine("Json Encoded Data");
            Console.WriteLine(output);

            Console.WriteLine();
            Product deserializedProduct = JsonConvert.DeserializeObject<Product>(output);
            Console.WriteLine("Json Decoded Data");
            Console.WriteLine("Product Name :: " + deserializedProduct.Name);
            Console.WriteLine("Product Expiry Date :: " + deserializedProduct.ExpiryDate);
            Console.WriteLine("Product Price :: " + deserializedProduct.Price);
            foreach (var item in deserializedProduct.Sizes)
            {
                Console.WriteLine("Product Sizes :: " + item);
            }

            /*//For Write the data in file
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            var path = "E:\\Start Up\\NewtonSoftJSON\\ConsoleNewtonSoft\\test.txt";
            using (StreamWriter sw = new StreamWriter(path))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, product);
                }
            }*/

            //Collection Serialization
            List<string> videogames = new List<string>
            {
                    "Starcraft",
                    "Halo",
                    "Legend of Zelda"
            };
            string json = JsonConvert.SerializeObject(videogames);
            Console.WriteLine();
            Console.WriteLine("Json Encoded Data Of Collection");
            Console.WriteLine(json);

            Console.WriteLine();


            //Dictionary Serialization
            Dictionary<string, int> points = new Dictionary<string, int>
            {
                { "James", 9001 },
                { "Jo", 3474 },
                { "Jess", 11926 }
            };
            json = JsonConvert.SerializeObject(points);
            Console.WriteLine();
            Console.WriteLine("Json Encoded Data Of Dictionary");
            Console.WriteLine(json);

            //DataSet Serialization
            DataSet ds = new DataSet("ds");
            ds.Namespace = "NetFrameWork";
            DataTable table = new DataTable();
            DataColumn idColumn = new DataColumn("id", typeof(int));
            idColumn.AutoIncrement = true;
            DataColumn itemColumn = new DataColumn("item");
            table.Columns.Add(idColumn);
            table.Columns.Add(itemColumn);
            ds.Tables.Add(table);

            for (int i = 0; i < 2; i++)
            {
                DataRow newRow = table.NewRow();
                newRow["item"] = "item " + i;
                table.Rows.Add(newRow);
            }
            ds.AcceptChanges();
            json = JsonConvert.SerializeObject(ds, Formatting.Indented);
            Console.WriteLine();
            Console.WriteLine("Json Encoded Data Of DataSet");
            Console.WriteLine(json);
            

            Console.ReadKey();
        }
    }
}
