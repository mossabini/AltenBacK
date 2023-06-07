using Back.Models;
using Newtonsoft.Json;
using System.Xml;

namespace Back.Data
{
    public static class ProductData
    {
        private static List<Product> products;

        public static List<Product> GetProducts()
        {
            if (products == null)
            {
                string json = File.ReadAllText("products.json");
                products = JsonConvert.DeserializeObject<List<Product>>(json);
            }

            return products;
        }

        public static Product GetProductById(int id)
        {
            return GetProducts().Find(p => p.Id == id);
        }

        public static void AddProduct(Product product)
        {
            product.Id = GetNextId();
            GetProducts().Add(product);
            SaveChanges();
        }

        public static void UpdateProduct(Product product)
        {
            int index = GetProducts().FindIndex(p => p.Id == product.Id);
            if (index >= 0)
            {
                GetProducts()[index] = product;
                SaveChanges();
            }
        }

        public static void DeleteProduct(int id)
        {
            GetProducts().RemoveAll(p => p.Id == id);
            SaveChanges();
        }

        private static int GetNextId()
        {
            int maxId = 0;
            foreach (var product in GetProducts())
            {
                if (product.Id > maxId)
                    maxId = product.Id;
            }
            return maxId + 1;
        }

        private static void SaveChanges()
        {
            string json = JsonConvert.SerializeObject(GetProducts(),Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("products.json", json);
        }
    }
}
