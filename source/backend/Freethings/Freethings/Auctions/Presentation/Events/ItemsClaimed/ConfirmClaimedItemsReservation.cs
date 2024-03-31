using Freethings.Auctions.Application.Commands;
using Freethings.Contracts.Events;
using Freethings.Shared.Abstractions.Domain.BusinessOperations;
using Freethings.Shared.Abstractions.Messaging;

namespace Freethings.Auctions.Presentation.Events.ItemsClaimed;

internal sealed class ConfirmClaimedItemsReservation : IEventConsumer<AuctionEvent.ItemsClaimed>
{
    private readonly ISender _sender;
    private readonly ILogger<ConfirmClaimedItemsReservation> _logger;
    
    public ConfirmClaimedItemsReservation(ISender sender, ILogger<ConfirmClaimedItemsReservation> logger)
    {
        _sender = sender;
        _logger = logger;
    }
    
    public async Task Handle(AuctionEvent.ItemsClaimed notification, CancellationToken cancellationToken)
    {
        BusinessResult result = await _sender.Send(new ReserveClaimedItemsCommand(
            notification.AuctionId,
            notification.ClaimedById,
            notification.ClaimedQuantity,
            false),
            cancellationToken);

        if (!result.IsSuccess)
        {
            _logger.LogWarning("Automatic reservation of claimed items failed. {ErrorMessage}", result.ErrorMessage);
        }
    }
}