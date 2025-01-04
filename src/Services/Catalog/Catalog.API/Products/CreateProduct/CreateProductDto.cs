namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductDto(string Name,
                                   List<string> Category,
                                   string Description,
                                   string ImageFile,
                                   decimal Price);
}
