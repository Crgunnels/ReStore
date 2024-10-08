using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly StoreContext _context;
        public BasketController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Basket>> GetBasket()
        {
            var basket = await RetrieveBasket();
            if (basket == null) return NotFound();
            return basket;
        }

       

        [HttpPost]
        public async Task<ActionResult> AddItemToBasket(int productId, int quanity)
        {
            var basket = await RetrieveBasket(); 

             if(basket == null) basket = CreateBasket();

            var product = await _context.Products.FindAsync(productId);

            if (product == null) return NotFound();

            basket.AddItem(product, quanity);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return StatusCode(201);
            
            return BadRequest(new ProblemDetails{Title = "Problem saving item to basket"});
             
        }

       

        [HttpDelete]
        public async Task<ActionResult> RemoveBasketItem(int productId, int quanity)
        {
            //get basket
            //remove item or reduce quanity
            //save changes
            return Ok();
        }
         private async Task<Basket> RetrieveBasket()
        {
            return await _context.Baskets
                .Include(i => i.Items)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(x => x.BuyerId == Request.Cookies["buyerId"]);
        }

         private Basket CreateBasket()
        {
            var buyerId = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions{IsEssential = true, Expires = DateTime.Now.AddDays(30)};
            Response.Cookies.Append("buyerId", buyerId, cookieOptions);
            var basket = new Basket{BuyerId = buyerId};
            _context.Baskets.Add(basket);
            return basket;
        }
    }
}