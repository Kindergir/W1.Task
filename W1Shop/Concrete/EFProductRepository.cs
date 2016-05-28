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
    }
}
