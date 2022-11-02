﻿using System;
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

        public void UpdateProduct(Product product)
        {
            _conn.Execute("UPDATE products SET Name = @name, Price = @price WHERE ProductID = @id",
        new { name = product.Name, price = product.Price, id = product.ProductID });
        }

        public IEnumerable<Category> GetCategories()
        {
            return _conn.Query<Category>("SELECT * FROM categories;");
        }

        public Product AssignCategory()
        {
            var categoryList = GetCategories();
            var product = new Product();
            product.Categories = categoryList;
            return product;
        }

         void IProductRepository.InsertProduct(Product productToInsert)
        {
            _conn.Execute("INSERT INTO products (NAME, PRICE, CATEGORYID) VALUES (@name, @price, @categoryID);",
        new { name = productToInsert.Name, price = productToInsert.Price, categoryID = productToInsert.CategoryID });
        }
    }
}

