# Business requirements

## Project Plan

- [ ] Phase 1: Planning
- [ ] Phase 2: Initial Setup
    - [ ] Create structure of the Web API project
    - [ ] Create first endpoint and structure of the code
- [ ] Phase 3. MVP Development
    - [ ] Item Listings
    - [ ] Item Search
    - [ ] Interest Expression Mechanism
    - [ ] User registration and profiles
- [ ] Phase 3.1 Charity features
    - [ ] Bidding for Charity
- [ ] ~~Phase 4. Basic UI with Vue.js~~
- [ ] Phase 5. Testing and Bug-fixing

## Features - detailed plan
- Offers
    - User can create an item offer.
        - Item offer should include a title, description, category, quantity and form how the item can be claimed by interested.
        - Offer will be posted for 1 month.
    - Users can list items they want to give away.
    - User can modify the item offer.
        - User can extend once the offer time.
        - [Extra] User can extend the offer more than once if additional fee is paid.
        - User can edit offer details (title, description, category, quantity).
            - form how the item can be claimed by interested can be changed only if there are no interested parties. (does it make sense??)
        - User can delete the offer.
- Interests
    - User can express interest (claim) in the item offer.
        - To the interest user can add a comment.
    - User can retract interest in the item offer.
    - User/Owner can see the list of all interested parties.
        - Interested parties will be visible under the offer details (as comments, or sth like that).
    - Owner can select the new owner of the item. (if manual selection is chosen).
- Hand over
    - Owner and claimant can agree on the date and place of the item handover.
        - After that, the item should be visible as 'Reserved' for claimants.
    - User can mark the item as handed over.
        - After the item is marked as handed over, the offer will be closed (or quantity should be decreased by 1?).
    - User can mark the item as not handed over.
- User
    - User can create an account.
    - User can log in.
    - User can log out.
    - User can delete the account.
    - User can see the list of all offers he/she created.
    - User can see the list of all offers he/she claimed.
- Categories
    - User can see the list of all categories.
    - User can see the list of all offers in the category.
- Search
    - User can search for items they are interested in.
- Charity
    - TBD

## Notes (not included in project scope but I this is a good place to keep them)

- Social integration with Facebook. The project idea originates with FB group so this will be really nice to have feature linking the platform with the group.
- Integration with charity organizations. This is a really important feature, but I think it's out of scope for MVP. I will focus on building the platform and then I will try to find some charity organizations to cooperate with.
- Integration with payment systems. This is also a really important feature, but I think it's out of scope for MVP. I will focus on building the platform and then I will try to find some payment systems to cooperate with.
- Logging using social provider accounts like Google. It will be nice if I will be able to integrate with some AAD provider like Firebase or Google.
