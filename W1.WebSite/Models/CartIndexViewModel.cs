using W1.Domain.Entities;

namespace W1.WebUI.Models
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; } 
    }
}