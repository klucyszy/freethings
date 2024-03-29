### Context

The current implementation of the Auction aggregate was working fine, but there was no easy way to add and manage the Auction object. The Auction data model was hidden in the infra model.

### Decision

The decision was to move the data model from hidden Infra part to Domain itself and name it as AuctionAdvert. The AuctionAdvert is a domain object that represents the Auction in the system.

The system will be able to create instance of the new AuctionAdvert and this will be root entity of the aggregate. To manage clear understanding what the Auction aggregate is we will keep the Auction aggregate. The Auction aggreagte will be build from AuctionAdvert domain object model and will be responsible for operations on Auction like claim/reserve/handover. AuctionAdvert management will be done directly on the AuctionAdvert itself, without using any aggregate.