namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
    
    public record DeleteBasketResult(bool IsSuccess);

    public class DeleteBasketCommandHandler(IDocumentSession session) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            var basket = (await session
                .Query<ShoppingCart>()
                .FirstOrDefaultAsync(x => x.UserName == command.UserName, cancellationToken))
                ??
                throw new NotFoundException(nameof(ShoppingCart), command.UserName);

            session.Delete<ShoppingCart>(command.UserName);

            await session.SaveChangesAsync(cancellationToken);

            return new DeleteBasketResult(true);
        }
    }
}
