﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThirdTryAPI.Dtos;
using ThirdTryAPI.Entities;
using ThirdTryAPI.Interfaces;

namespace ThirdTryAPI.Controllers
{
    public class BasketController:BaseApiController
    {
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            this.basketRepository = basketRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await basketRepository.GetBasketAsync(id);

            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var customerBasket = mapper.Map<CustomerBasketDto, CustomerBasket>(basket);

            var updatedBasket = await basketRepository.UpdateBasketAsync(customerBasket);

            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasketAsync(string id)
        {
            await basketRepository.DeleteBasketAsync(id);
        }
    }
}
