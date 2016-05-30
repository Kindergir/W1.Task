using W1.Domain.Entities;

namespace W1.Domain.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails); 
    }
}