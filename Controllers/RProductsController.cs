using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThirdTryAPI.Entities;
using ThirdTryAPI.Interfaces;

namespace ThirdTryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RProductsController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public RProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        //repositoridan camogeba infosi

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await productRepository.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await productRepository.GerProductByIdAsync(id);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await productRepository.GetProductBrandsAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await productRepository.GetProductTypesAsync());
        }
    }
}
