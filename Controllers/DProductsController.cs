using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThirdTryAPI.Data;
using ThirdTryAPI.Entities;

namespace ThirdTryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DProductsController : ControllerBase
    {
        private readonly StoreContext storeContext;

        public DProductsController(StoreContext storeContext)
        {
            this.storeContext = storeContext;
        }

        //pirdapir contextidan gashveba infosi
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await storeContext.Products.ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await storeContext.Products.FindAsync(id);
        }
    }
}
