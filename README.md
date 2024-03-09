
![freethings-logo](/images/freethings-logo-textonly.png "freethings-logo")

## Idea

The ideas of this project is to create a platform with same concept as "Uwaga Śmieciarka Jedzie" FB group, where people can share things they don't need anymore, but are still in good condition. The main goal is to reduce waste and give things a second life.

My idea is to improve the concept and extend this, that really valuable things can be shared for charity purposes.

## Features
- [ ] **User registration and profiles**: Allow users to create profiles, where they can list items, express interest in items, and track their donations or acquisitions.
- [ ] **Item Listings**: Users can list items they want to give away. Listings should include a title, description, and photo.
- [ ] **Item Search**: Users can search for items they are interested in.
- [ ] **Interest Expression Mechanism**: Possibility to select how you want to give away your item:
  - [ ] **First-Come, First-Served Option**: The first user to express interest is automatically granted the item. 
  - [ ] **New Owner Manual Selection**: The owner reviews interested parties and selects who receives the item.
  - [ ] **Bidding for Charity**: An auction-like feature where the highest pledge to a selected charity wins the item.pledge to a selected charity wins the item.

## Tech Stack & Ideology behing the project

Behind building the working MVP of the project, I want to to focus on delivering a high-quality code, that is easy to maintain and extend. I want to start from really basic Web API and using evolutionary-approach extend it finding new contexts and features. Initially I will start from simple monolith app, but I believe I will be able to modularize it during the development process.

### Tech stack
- **.NET 8.0, Minimal API** - this is my main technology stack. I want to use the newest version of .NET and the new Minimal API feature, which is a really interesting approach to building Web APIs. I feel comfortable with C# and .NET, so I think it's a good choice for me.
- **Vue.js + Nuxt.js + Typescript** - I have lack of experience in using frontend frameworks so this will be a great opportunity for me to finally build some working concept with Vue.js. I want to use Typescript to make the code more maintainable and to learn it better.

## Project Plan

- [ ] Phase 1: Planning (5 days)
- [ ] Phase 2: Initial Setup (5 days)
  - [ ] Create structure of the Web API project
  - [ ] Create first endpoint and structure of the code
- [ ] Phase 3. MVP Development (up to 20 days)
    - [ ] Item Listings
    - [ ] Item Search
    - [ ] Interest Expression Mechanism
    - [ ] User registration and profiles
- [ ] Phase 3.1 Charity features (up to 30 days)
    - [ ] Bidding for Charity
- [ ] Phase 4. Basic UI with Vue.js (up to 30 days)
- [ ] Phase 5. Testing and Bug-fixing (10-30 days)

### Features - detailed plan
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

## Analysis & exploration

You need to know that I want also to learn exploring the domain using Event Storming as I do not have to do it in practice on daily basis.

Here is link to Miro, where I'm exploring the domain: [freethings miro](https://miro.com/app/board/uXjVNl71hrg=/?share_link_id=521419360509)

![alt text](images/miro-storming.png)


### Notes (not included in project scope but I this is a good place to keep them)

- Social integration with Facebook. The project idea originates with FB group so this will be really nice to have feature linking the platform with the group.
- Integration with charity organizations. This is a really important feature, but I think it's out of scope for MVP. I will focus on building the platform and then I will try to find some charity organizations to cooperate with.
- Integration with payment systems. This is also a really important feature, but I think it's out of scope for MVP. I will focus on building the platform and then I will try to find some payment systems to cooperate with.
- Logging using social provider accounts like Google. It will be nice if I will be able to integrate with some AAD provider like Firebase or Google.

## How to run

1. Run `docker-compose.yml` located in the `source/backend/Freethings/Freethings`.
2. Using connection string `Server=localhost,1435;Database=master;User=sa;Password=FreethingsSql1!;` login to database and create database called `freethings`.

### Running migrations

`dotnet ef migrations add "Init" -o Infrastructure/Offers/Persistence/Migrations -p Freethings/Freethings.csproj -c OffersContext`
`dotnet ef database update -p Freethings/Freethings.csproj -c OffersContext`

