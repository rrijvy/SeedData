using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using SeedDataToDatabase.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SeedDataToDatabase.Data
{
    public class SeedData
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _environment;

        public SeedData(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public void Seed()
        {
            _context.Database.EnsureCreated();

            if (!_context.Products.Any())
            {
                var filepath = Path.Combine(_environment.ContentRootPath, "Data/products.json");
                var json = File.ReadAllText(filepath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                _context.Products.AddRange(products);
                _context.SaveChanges();
            }
        }
    }
}
