﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThirdTryAPI.Entities;

namespace ThirdTryAPI.Specifications
{
    public class ProductWithFilteresForCountSpecification:BaseSpecification<Product>
    {
        public ProductWithFilteresForCountSpecification(ProductSpecParams productParams) : base(x =>
            (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
            (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
            (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId))
        {

        }
    }
}
