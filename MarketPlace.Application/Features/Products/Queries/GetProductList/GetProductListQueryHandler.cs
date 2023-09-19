﻿using AutoMapper;
using MarketPlace.Application.Contracts.Persistence;
using MarketPlace.Domain.Entitites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Features.Products;

public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery,
    List<ProductListVm>>
{
    private readonly IAsyncRepository<Product> productRepository;
    private readonly IMapper mapper;

    public GetProductListQueryHandler(IAsyncRepository<Product> productRepository, IMapper mapper)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
    }

    public async Task<List<ProductListVm>> Handle(GetProductListQuery request,
        CancellationToken cancellationToken)
    {
        var allProducts = (await productRepository.FindAllAsync()).OrderBy(x => x.Release);

        return mapper.Map<List<ProductListVm>>(allProducts);
    }
}