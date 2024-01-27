namespace WebApi.Models
{
    /// <summary>
    /// Simple class to map entities to DTOs.
    /// TODO: Replace with an AutoMapper implementation.
    /// </summary>
    public static class Map
    {
        public static ProductDto ProductToDto(Product product)
        {
            return new ProductDto { Guid = product.Guid, 
                                    Name = product.Name, 
                                    Description = product.Description, 
                                    Price = product.Price, 
                                    DeliveryPrice = product.DeliveryPrice };
        }

        public static Product DtoToProduct(ProductDto productDto)
        {
            return new Product
            {
                Guid = productDto.Guid,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                DeliveryPrice = productDto.DeliveryPrice
            };
        }
        public static ProductOptionDto ProductOptionToDto(ProductOption option)
        {
            return new ProductOptionDto { ProductGuid = option.Guid, 
                                          Name = option.Name, 
                                          Guid = option.Guid, 
                                          Description = option.Description };
        }

        public static ProductOption CreateDtoToProductOption(Guid productId, ProductOptionCreateDto createDto) 
        {
            return new ProductOption
            { 
                ProductGuid = productId, 
                Guid = createDto.Guid, 
                Description = createDto.Description, 
                Name = createDto.Name 
            };
        }
    }
}
