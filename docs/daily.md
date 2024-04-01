### 1.04.2024 - day 31

Today I mainly focused on learning about Identity approach which I want to use. Plan for this week is to add Users module and add authentication to the application.

### 23.03.2023 - day 23

Plan for the day:
- [x] Do the user story mapping of the 'Item owner'

Today's work:

![img.png](images/user_story_mapping.jpg)

### 21.03.2023 - day 21

Plan for the day:
- [x] Test in code concept of claim selection policy. It should be used from the level above the domain.
- [x] Prepare plan for next week.
- [ ] Start using Github project && issues.

Plan for next week:
- [ ] Finish the concept of claim selection policy.
- [ ] Build application, presentation layer of auction.
- [ ] Add concept of user and authentication.
- [ ] Update presentation endpoints, not to start from `/users/{userId}` - user should come from the token.

Today's work:

I'm still not able to finish the concept of claim selection policy. I'm not sure if I'm going in the right direction. My questions are:
- Should I use the concept of the policy in the Auction aggregate?
- What I'm missing in the domain to make it properly working for automatic selection of the claims?

![img.png](images/2024_03_21_auction_aggregate.png)

### 20.03.2024 - day 20

Plan for the day:
- [x] Decide the namings of the modules. Now there is Auction/Adveritisement and more.
- [x] Continuation of restructure the Readme.md and create docs folder.

### 19.03.2024 - day 19

I need to restructure the Readme.md and create a `docs` folder. Unfortunately, I see that my plan was not enough detailed. Here I want to plan work on the daily basis.

Plan for the day:
- [x] Restructure the Readme.md and create docs folder.
- [ ] Decide the namings of the modules. Now there is Auction/Adveritisement and more.

#### Decisions
- As I'm exploring the domain I want to fully focus on backend and leave the frontend part for now.

### 14.03.2024 - day 14

After the 13.03.2024 analysis, the miro storming was recreated. Based on that, more precise requirements were created:

- User can create offers of items which she/he wants to give for free to other platform members.
- User can specify item details (photo, description, title, category) of the item.
- User can specify quantity, if offer is about more than one same item.
- User can specify type of the offer - which will trigger the way, the claims will be considered.
- User can mark offer as published - after this action, the item auction will be created.
- Interesants can claims item(s) from auction.
- Even if all items are already reserved, interesants can still claim item(s).
- Item is reserved until the interesant will receive them. After receiving the item, reservation is ended, available quantity on the auction§ is updated. Quantity is also updated in the offer.
- User can modify the offer quantity.
    - If any reservations are made, they will be cancelled accordingly.
- User can cancel the offer.
    - If any reservations are made, they will be cancelled accordingly.

![img_1.png](images/miro_modules_offers_auctions.png)

Example lifecycle othe the offer was created:

![img_2.png](images/miro_manual_process.png)

### 13.03.2024 - day 13

Today's work was focused on exploring and analyzing the domain, as the initial analysis did not do it with needed deepnest. Result of today's work is a image of the board with was created. Next days I will try to materialize it on Miro and base domain model.

![img.png](images/2024_03_13_board.png)