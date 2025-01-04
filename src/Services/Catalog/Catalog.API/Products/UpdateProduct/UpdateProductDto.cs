namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductDto(Guid Id,
                                   string Name,
                                   List<string> Category,
                                   string Description,
                                   string ImageFile,
                                   decimal Price);
}
