using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ThirdTryAPI.Entities;

namespace ThirdTryAPI.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification:BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams) : base(x=>
            (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
            (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
            (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
        )
        {

            //include products and brands too
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);

            //sort by name
            AddOrderBy(x => x.Name);

            //applyPaging
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);


            //asc desc switching
            if(!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case
                    "priceAsc": AddOrderBy(p => p.Price);
                        break;
                    case
                    "priceDesc":AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x=>x.Id == id)
        {
            //include products and brands too
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}
