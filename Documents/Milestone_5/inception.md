#### Team: MNM Solutions
#### Milestone: 5

# Team Project Inception Phase (Week 3)

## Summary of Our Approach to Software Development
We are following the Agile SCRUM process, and also processes explained in the Disciplined Agile Delivery book.

## Initial Vision Discussion with Stakeholders
For people who like to make a little money on the side or rent rather than purchase, this sharing econemy rental website will be clean looking, easy to find local items that you are intrested in and even create a competitive market for affordable transactions. Unlike other rental websites, our website will have distinguished features that are extremely user friendly making it an enjoyable experience rather than a stressful one.

People like to save money; people like to make money. People like to rent items instead of buying them, or rent their property out to make a little extra money. The problem with this is that there are not any good places to do this on the Web. Not only have businesses taken over the rental marketplace, but it is also very flawed.

By flooding rental websites or mobile apps with business products, it deprecates the idea of a sharing economy. Some of these businesses include Rent-A-Center and Aaron’s. A sharing economy and peer to peer transactions are main priorities for this web application. This is because by taking away the businesses, we can sustain a host that will give users, both owners and renters, a more personal and enjoyable renting experience.

We have investigated other attempts at a rental website, and they are poor attempts. Just look at services like lendr and BeOmni which are mobile apps, and Zilok and PeerRenters are poorly designed websites. These services are not quite as simple as an Amazon or Craigslist when it comes to renting.

We want a site that is simple, easy to use, and sharing economy friendly for people to find what they want or need to rent. Visitors should be able to search and view rentable products without having to log in, users should be able to make an account to both rent and rent out items, and it should be done easily. It should provide features for the user to sign in using a 3rd-party log in, manage their account, view their renting history, and even rate their rental experience.

## Initial Requirements Elaboration and Elicitation

### Questions
1. What makes this project different from other rental websites?
    * Its overall appreal to the eye and its user/visitor friendliness.
2. What is it important to know about our users? What data should we collect?
    * Their email, first and last name and payment information.
3. Clearly, we need accounts and logins. Our own, or do we allow logging in via 3rd party, i.e. "Log in with Google" or...?
    * We'll give users the option to sign up using either approach.
4. How will users know who is trustable?
    * There will be a rating system for owners, renters and items.
5. How will I know about other peoples' experiences?
    * There will be a review section where users can leave reviews on owners, renters and items. Vice versa people can leave reviews as well.
6. We need to save payment information. What info is important?
    * We need to save credit card info and if we end up implementing the PayPal API, we can save that infomation in place of the credit card info.

### Mind Map/Brainstorming:
_**Diagram separate_

### Needs & Features (Requirements):
* A great-looking landing page with featured items around the user’s area.
* The ability to create/update/delete rental listings as an owner
* The ability to create/delete a reservation for a rental item.
* User accounts
* A feedback/rating system on transactions
* Payment system
* Search bar to search for items
    1. Prioritize individual listings over business listings in search results
* Recommendations related to the item user is looking at
* Multi-language support
* 3rd party login
* Google Recaptcha
* Rental history log

## Initial Modeling

### Use-case Diagrams
_**Diagram separate_

### Other modeling
_**Diagram separate_

## Identify Non-functional Requirements:
* User accounts and data must be stored indefintely.
* Site must never return debug errors. Web server must never return 404's. All server errors must be logged. Users should receive custom error pages, telling them what to do.
* Site must work with modern browsers across all platforms.
* Site must comply with current privacy laws.
* Site must be legally compliant and not infringe upon patented code.
Site and data must be backed up regularly and have failover redundacy that will allow the site to remain functional in the event our primary equipment goes down. Doing so will minimize downtime and maintain access to the website, even if reduced to read-only functionality.
* Server equipment must be capable of handling mass volumes of web traffic without crashing.
* Steps must be taken to prevent or reduce cyber attacks, such as DDoS, SQL injection and several other exploitable vulnerabilities.
* Sensitive information (i.e. user name, password, payment info, etc.) sent to and from the server must be encrypted.
* Code must be readable and reusable. Database string connectioins, API keys and other sensitive information must never be present in code, but rather concealed.
* Site should be adapted to work in all languages.

## Identify Functional Requirements (User Stories):

1. [E] Website with functionality for users
    * [F] Homepage should have a modern and clean layout
        * [U] As a user, I would like to see a clean layout page that is user friendly and shows me how to use the site so I can decide if I want to use this service in the future.
            * [T] Create starter ASP.NET MVC 5 web app with individual user accounts
            * [T] Switch over to Bootstrap 4
            * [T] Make it mobile-friendly
    * [F] Navigation bar
        * [U] As a user, I would like to see a navigation bar to navigate my way around the site.
2. [E] Fully enable individual user accounts
    * [F] Creating user accounts
        * [U] As a visitor, I want to be able to make an account so that I can rent items I need or rent out my belongings.
            * [T] Make sure to enable user accounts when creating ASP.NET project.
            * [T] Create a web page with fields for user information input.
        * [U] As a visitor, I am more inclined to create an account when there are easier ways to make accounts, such as signing in through Google, Facebook or Twitter.
        * [U] As a registered user, I want to be able to update my account information so taht I can keep my personal info up-to-date.
            * [T] Allow user to change password.
            * [T] Allow user to update their payment info.
            * [T] Allow user to update their e-mail address.
            * [T] Allow user to enable tow-factor authentication.
        * [U] As a registered user, I want to be able to retrieve my account in case I forget my login credentials.
        * [U] As a registered user, I want to be able to close my account if I no longer want to rent items or rent out my belongings.
        * [U] As a robot, I would like to be prevented from creating an account and posting fake items to be rented so I can scam people.
            * [T] Implement Google Recaptcha on user registration page
3. [E] Feedback system
    * [F] Rating
        * [U] As a renter, I would like to rate an item to show how well that product worked out for me.
        * [U] As an owner, I would like to rate a rentee on my experience with them
        * [U] As a visitor, I would like to see which items and renters are top-rated, so that I can know who and what is trustworthy.
    * [F] Reviewing
        * [U] As a renter, I would like to leave a review for the renter.
        * [U] As an owner, I would like to leave a review on my experience with a rentee.
        * [U] As a renter, I would like to leave a review on my thoughts about an item.
        * [U] As a visitor, I would like to be able to read reviews so that I can see how others' experiences are going so far with this website.
4. [F] Listing items for rent
    * [U] As an owner, I want to be able to create an item listing so that I can make some money from my belongings I am not using now.
        * [T] Add a listing title
        * [T] Add a description
        * [T] Add conditions to the listing
        * [T] Set price rate(s)
    * [U] As an owner, I want to be able to revise a listing I previously created.
5. [F] Rental history log
    * [U] As an owner, I want to see all the items I had rented out and which ones didn't get rented so that I can remove items that aren't being rented a lot.
    * [U] As a renter, I wanto see a history of the items I've rented to see what would maybe be worth purchasing for myself.
6. [F] Renting an item
    * [U] As a logged in user, I want to be able to rent an item that I might need for a couple hours or days.
    * [U] As a renter, I want to be able to cancel a reservation that I have for an item.
7. [F] Multi-language support
    * [U] As an international user, I would like to be able to read contents of the website in my native language.
    * [U] As a user with Spanish as their first language, I would like to be able ot read the site in Spanish.
8. [F] Payment system
    * [U] As a renter, I would like to save my payment information to my account so that I don't have to keep re-entering it every time I rent something.
    * [U] As a user, it is earier for me to use a payment application, such as PayPal or Venmo.

## Initial Architecture Envisioning

_**Diagram Separate_

## Agile Data Modeling

_**Diagram Separate_

## Timeline and Release Plan

* Sprint 1 - _Start_: Monday, February 11th - _End_: Sunday, February 24th - **Release 1**
* Sprint 2 - _Start_: Monday, February 25th - _End_: Sunday, March 10th - **Release 2**
* Sprint 3 - _Start_: Monday, April 1st - _End_: Sunday, April 14th - **Release 3**
* Sprint 4 - _Start_: Monday, April 15th - _End_: Sunday, April 28th - **Release 4**
* Sprint 5 - _Start_: Monday, April 29th - _End_: Sunday, May 12th - **Release 5**
* Test: _TBD_
* Sprint 6 - _Start_: Monday, May 13th - _End_: Sunday, May 26th - **Release 6**

## Identification of Risks

* Legal issues when having a rental application
* One of us doesn't pass CS461
* All of our computer are plugged in while at school and ITC gets struck by lightning and it destroys all of our laptops before we have the change to push our work up to our remote repos.
* The API(s) we want to use is no longer available.