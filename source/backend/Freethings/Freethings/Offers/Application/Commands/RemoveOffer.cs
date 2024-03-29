using Freethings.Offers.Application.Repositories;
using Freethings.Shared.Infrastructure;

namespace Freethings.Offers.Application.Commands;

public sealed record RemoveOfferCommand(
    Guid UserId,
    Guid OfferId) : IRequest<Result>;

internal sealed class RemoveOfferHandler : IRequestHandler<RemoveOfferCommand, Result>
{
    private readonly IOfferRepository _repository;
    public RemoveOfferHandler(IOfferRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(RemoveOfferCommand request, CancellationToken cancellationToken)
    {
        bool deleted = await _repository.DeleteAsync(request.OfferId, cancellationToken);
        
        if (deleted is false)
        {
            return Result.Failure("Offer not found");
        }
        
        return Result.Success();
    }
}