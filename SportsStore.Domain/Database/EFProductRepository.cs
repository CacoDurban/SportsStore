using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Database
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Entities.Product> Products
        {
            get 
            {
                return context.Products;
            }
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
                context.Products.Add(product);

            context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            context.Products.Remove(product);

            context.SaveChanges();
        }
    }
}
