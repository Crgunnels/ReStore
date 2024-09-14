using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http.Features;

namespace API.Entities
{
    public class Basket
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public List<BasketItem> Items { get; set; } = new();

        public void AddItem(Product product, int quanity){
            if(Items.All(item => item.ProductId != product.Id))
            {
                Items.Add(new BasketItem{Product = product, Quanity = quanity});
            }
            var existingItem = Items.FirstOrDefault(item => item.ProductId == product.Id);
            if(existingItem != null) existingItem.Quanity += quanity;
        }     

        public void RemoveItem(int productId, int quanity)
        {
            var item = Items.FirstOrDefault(item => item.ProductId == productId);
            if(item == null) return;
            item.Quanity -= quanity;
            if(item.Quanity == 0) Items.Remove(item);

        }
    }
}