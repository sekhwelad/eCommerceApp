using AutoMapper;
using AutoMapper.Execution;
using eShopper.Core.Entities;
using eShopper.DTOs;

namespace eShopper.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductResponseDto, string>
    {
        private readonly IConfiguration _config;
        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(Product source, ProductResponseDto destination, 
            string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl;
            }
            return null;
        }
    }
}
