using AutoMapper;
using eShopper.Core.Entities;
using eShopper.Core.Interfaces;
using eShopper.Core.Specifications;
using eShopper.DTOs;
using eShopper.Errors;
using eShopper.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace eShopper.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepository,
            IGenericRepository<ProductBrand> productBrandRepository,
            IGenericRepository<ProductType> productTypeRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _productBrandRepository = productBrandRepository;
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
        }
        [Cached(600)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductResponseDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);

            var countSpec = new ProductWithFiltersForCountSpecification(productParams);

            var totalItems = await _productRepository.CountAsync(countSpec);

            var products = await _productRepository.ListAsync(spec);

            var data = _mapper.
                Map<IReadOnlyList<Product>, IReadOnlyList<ProductResponseDto>>(products);

            return Ok(new Pagination<ProductResponseDto>(productParams.PageIndex,
                productParams.PageSize,totalItems,data));
        }
        [Cached(600)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async  Task<ActionResult<ProductResponseDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product =  await _productRepository.GetEntityWithSpec(spec);

            if(product == null) { return NotFound(new ApiResponse(404)); }
            return _mapper.Map<Product, ProductResponseDto>(product);
        }

        [Cached(600)]
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductsBrands()
        {
            return Ok(await _productBrandRepository.ListAllAsync());
        }

        [Cached(600)]
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductsTypes()
        {
            return Ok(await _productTypeRepository.ListAllAsync());
        }
    }
}
