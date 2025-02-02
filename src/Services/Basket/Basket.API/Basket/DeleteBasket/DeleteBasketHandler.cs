namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string UserName): ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool IsSuccess);
    public class DeleteBasketCommandVlidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandVlidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("userName is Required");
        }
    }

    internal class DeleteBasketCommandHandler(IBasketRepository repository)
        : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteBasket(request.UserName);

            return new DeleteBasketResult(result);
        }
    }
}
