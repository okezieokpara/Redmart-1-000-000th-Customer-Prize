using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedmartCustomer
{
    class Program
    {
        static void Main(string[] args)
        {
            var prdu = GetProductsFromFile();
            prdu = prdu.OrderByDescending(x => x.Price).ThenBy(x => x.Weight).ToList();

            var ourTote = new Tote(45, 30, 35);
            var ourProducts = new List<Product>();
            var i = 0m;
            Console.WriteLine($"Tote Area: {ourTote.Area}");

            while (prdu.Count > 0)
            {

                var product = prdu.ElementAt(0);
                var remainingArea = ourTote.Area - i;
                if (remainingArea >= product.Area)
                {
                    ourProducts.Add(product);
                    i = i + product.Area;
                }
                prdu.RemoveAt(0);

            }
            Console.WriteLine("Products in our Tote:");
            foreach (var prod in ourProducts)
            {
                Console.WriteLine(prod.ToString());
            }
            Console.WriteLine($"Total Tote Count: {ourProducts.Count}\t Sum of ProductIds : {ourProducts.Sum(x => x.Id)}\t Total Area of Products in Tote: {ourProducts.Sum(x => x.Area)}");
            Console.ReadKey();
        }
        private static List<Product> GetProductsFromFile()
        {
            var productList = new List<Product>();
            var fileStream = File.ReadAllLines(@"C:/products.csv");
            foreach (var s in fileStream)
            {
                var product = new Product();
                var line = s.Split(',');
                product.Id = int.Parse(line[0]);
                product.Price = decimal.Parse(line[1]);
                product.Length = decimal.Parse(line[2]);
                product.Width = decimal.Parse(line[3]);
                product.Height = decimal.Parse(line[4]);
                product.Weight = decimal.Parse(line[5]);

                productList.Add(product);

            }
            return productList;
        }
    }



    public class Tote
    {
        public Tote(decimal length, decimal width, decimal height)
        {
            Length = length;
            Width = width;
            Height = height;
        }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal Area
        {
            get
            {
                return Width * Height * Length;
            }
        }
    }
    public class Product
    {
        public int Id { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }

        public decimal Area
        {
            get
            {
                return Length * Width * Height;
            }
        }


        public override string ToString()
        {
            return $"Id: {Id},\t Price: {Price},\t Length: {Length},\t Width: {Width}, \t Height : {Height}\t Weight: {Weight}\t, Area: {Area}";
        }
    }
}
