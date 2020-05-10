using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteDemo.Models;

namespace SQLiteDemo.Repository
{
    class ProductRepository
    {
        private readonly SQLite.SQLiteAsyncConnection _database;

        public ProductRepository()
        {
            string DbFilePath = Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                "NorthWind.db");
            _database = new SQLiteAsyncConnection(DbFilePath);
            _database.CreateTableAsync<Product>().Wait();
        }

        public Task<int> CreateProductAsync(Product product)
        {
            return _database.InsertAsync(product);
        }

        public Task<List<Product>> GetProducts()
        {
            return _database.Table<Product>().ToListAsync();
        }

        public Task<Product> GetProductByIdAsync(int id)
        {
            return _database.Table<Product>().Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> UpdateProductAsync(Product product)
        {
            return _database.UpdateAsync(product);
        }

        public Task<int> DeleteProductAsync(Product product)
        {
            return _database.DeleteAsync(product);
        }
        public Task<List<Product>> GetProductsAsync()
        {
            return _database.Table<Product>().ToListAsync();
        }

        public Task<List<Product>> GetProductsByCategoryIdAsync(int ID)
        {
            return _database.QueryAsync<Product>(
                $"SELECT * FROM[Product] WHERE [CategoryID] = { ID}");
        }
    }
}
