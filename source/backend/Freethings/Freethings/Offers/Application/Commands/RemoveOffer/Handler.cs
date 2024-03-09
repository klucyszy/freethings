using Freethings.Offers.Domain.Repositories;
using Freethings.Shared;
using MediatR;

namespace Freethings.Offers.Application.Commands.RemoveOffer;

public sealed record RemoveOfferCommand(
    Guid UserId,
    Guid OfferId) : IRequest<Result>;

public sealed class Handler : IRequestHandler<RemoveOfferCommand, Result>
{
    private readonly IOfferRepository _repository;
    public Handler(IOfferRepository repository)
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