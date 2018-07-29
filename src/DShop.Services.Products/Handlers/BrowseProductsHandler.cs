﻿using DShop.Common.Handlers;
using DShop.Common.Types;
using DShop.Services.Products.Dtos;
using DShop.Services.Products.Queries;
using DShop.Services.Products.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace DShop.Services.Products.Handlers
{
    public sealed class BrowseProductsHandler : IQueryHandler<BrowseProducts, PagedResult<ProductDto>>
    {
        private readonly IProductsRepository _productsRepository;

        public BrowseProductsHandler(IProductsRepository productsRepository)
            => _productsRepository = productsRepository;

        public async Task<PagedResult<ProductDto>> HandleAsync(BrowseProducts query)
        {
            var pagedResult = await _productsRepository.BrowseAsync(query);

            var productDtos = pagedResult.Items.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Descirption = p.Descirption,
                Vendor = p.Vendor,
                Price = p.Price
            }).ToList();

            return PagedResult<ProductDto>.From(pagedResult, productDtos);
        }
    }
}
