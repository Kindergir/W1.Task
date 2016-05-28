using System;
using System.Linq;
using W1.Domain.Entities;

namespace W1.Domain.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        void SaveProduct(Product product);
        Product DeleteProduct(int productID); 
    }
}
