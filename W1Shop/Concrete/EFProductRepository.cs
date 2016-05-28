using System;
using W1.Domain.Abstract;
using W1.Domain.Entities;
using System.Linq;

namespace W1.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Product> Products
        {
            get { return context.Products; }
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products.Find(product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            context.SaveChanges();
        }         
         public Product DeleteProduct(int productID) 
        { 
            Product dbEntry = context.Products.Find(productID); 
            if (dbEntry != null) 
            { 
                context.Products.Remove(dbEntry); 
                context.SaveChanges(); 
            } 
            return dbEntry; 
        }
    }
}
