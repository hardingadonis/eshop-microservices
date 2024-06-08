﻿using Catalog.Application.Queries;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Domain.Repositories;
using MediatR;
using Catalog.Domain.Entities;
using Catalog.Application.Exceptions;

namespace Catalog.Application.Queries.Handlers
{
    public class GetProductByNameHandler : IRequestHandler<GetProductByNameQuery, IEnumerable<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByNameHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductResponse>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetByName(request.Name);

            if (products is not null)
            {
                return CatalogMapper.Mapper.Map<IEnumerable<Product>, IEnumerable<ProductResponse>>(products);
            }

            throw new ProductNotFoundException($"name '{request.Name}'");
        }
    }
}
