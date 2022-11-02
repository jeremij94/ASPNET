using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Testing.Models;

namespace Testing
{
    public class ProductRepo : IProductRepository
    {
        private readonly IDbConnection _conn;

        public ProductRepo(IDbConnection connection)
        {
            _conn = connection;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _conn.Query<Product>("SELECT * FROM products;");
        }

        public Product GetProduct(int id)
        {
           return _conn.QuerySingle<Product>("SELECT * FROM products WHERE ProductID = @id", new {id = id});
        }
    }
}

