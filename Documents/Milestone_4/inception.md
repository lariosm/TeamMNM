#### Team: MNM Solutions
#### Milestone: 3

# Team Project Inception Phase (Week 1)

## Initial Description of Project Vision:
People like to save money; people like to make money. People like to rent items instead of buying them, or rent their property out to make a little extra money. The problem with this is that there are not any good places to do this on the Web. Not only have businesses taken over the rental marketplace, but it is also very flawed.

By flooding rental websites or mobile apps with business products, it deprecates the idea of a sharing economy. Some of these businesses include Rent-A-Center and Aaron’s.  A sharing economy and peer to peer transactions are main priorities for this web application. This is because by taking away the greedy and money hungry businesses, we can sustain a host that will give users, both owners and renters, a more personal and enjoyable renting experience.

We have investigated other attempts at a rental website, and they are poor attempts. Just look at services like lendr and BeOmni which are mobile apps, and Zilok and PeerRenters are poorly designed websites. These services are not quite as simple as an Amazon or Craigslist when it comes to renting.

We want a site that is simple, easy to use, and sharing economy friendly for people to find what they want or need to rent. Visitors should be able to search and view rentable products without having to log in, users should be able to make an account to both rent and rent out items, and it should be done easily. It should provide features for the user to sign in using a 3rd-party log in, manage their account, view their renting history, and even rate their rental experience.

## Mind Map/Brainstorming:
*Diagram separate...

## Needs & Features (Requirements):
* A great-looking landing page with featured items around the user’s area.
* The ability to create/update/delete rental listings as an owner
* User accounts
* A feedback/rating system on transactions
* Payment system
* Search bar to search for items
* Prioritize individual listings over business listings in search results
* Recommendations related to the item user is looking at
* Multi-language support
* 3rd party login
* Google Recaptcha
* Rental history log

## Identify Non-functional Requirements:

## Identify Functional Requirements (User Stories):

* [F] Fully enable individual user accounts
    1. [U] As a visitor, I want to be able to make an account so that I can rent items I need to orent out my belongings.
        * [T] Make sure to enable user accounts when creating ASP.NET project.
        * [T] Create a web apage with fields for user information input.
    2. [U] As a user, I want to be able to update my account information so that I can keep my personal info up-to-date.
        * [T] Allow user to change password
        * [T] Allow user to update their payment info
        * [T] Update email address
        * [T] Allow user to enable two-factor authentication
* [U] As a visitor to the site, I would like to see a sleek and modern homepage that is user-friendly and shows me how to use the site so I can decide if I want to use this service in the future.
    * [T] Create starter ASP.NET MVC 5  web app with individual user accounts
    * [T] Switch it over to Bootstrap 4
    * [T] Create nice landing page. Add preliminary contentand customize navbar.
* [E] Feedback system
    * [F] Rating
        1. [U] As a renter, I would like to rate an item to show how well that product worked out for me.
        2. [U] As an owner, I would like to rate a rentee on my experience with them
        3. [U] As a visitor, I would like to see which items and renters are top-rated, so that I can know who and what is trustworthy.
    * [F] Reviewing
        1. [U] As a renter, I would like to leave a review for the renter.
        2. [U] As an owner, I would like to leave a review on my experience with a rentee.
        3. [U] As a renter, I would like to leave a review on my thoughts about an item.
        3. [U] As a visitor, I would like to be able to read reviews so that I can see how others' experiences are going so far with this website.
* [F] Listing items for rent
    1. [U] As an owner, I want to be able to create an item listing so that I can make some money from my belongings I am not using now.
        * [T] Add a listing title
        * [T] Add a description
        * [T] Add conditions to the listing
        * [T] Set a price rate
    2. [U] As an owner, I want to be able to revise a listing I previously created.
* [E] Google API
    * [F] Google third-party login
        1. [U] As a visitor, I am more inclined to create an account when there are easier ways to make accounts, such as signing in through Google or Facebook.
            * [T] Google the Google API
        2. [U] As a user, I want to be able to sign in with Google.
    * [F] Google Recaptcha
        1. [U] As a robot, I would like to be prevented from creating an account on your website and posting fake items to be rented so that I can scam people.
        2. [U] As a robot, I would like to be prvented from flooding your postings with a spam of random items so that other renters' items will not be seen on the site.
* [F] Rental history log
    1. [U] As an owner, I want to see all the items I had rented out and which ones didn't get rented so that I can remove items that aren't being rented a lot.
    2. [U] As a renter, I wanto see a history of the items I've rented to see what would maybe be worth purchasing for myself.
* [F] Multi-language support
    1. [U] As a user/visitor I would like to be able to read the website in my desired language.
        * [T] Add a drop down menu to change to desired language
* [F] Payment system
    1. [U] As a renter, I would like to save my payment information to my account so that I don't have to keep re-entering it every time I rent something.
    2. [U] As a user, it is earier for me to use a payment application, such as PayPal or Venmo.

## Initial Architecture Envisioning

_**Diagram Separate_

## Agile Data Modeling

_**Diagram Separate_

## Timeline and Release Plan

* Sprint 1: Monday, February 11th - Sunday, February 24th
* Sprint 2: Monday February 25th - Sunday, March 10th
* Sprint 3: Monday April 1st - Sunday, April 14th
* Sprint 4: Monday, April 15th - Sunday, April 28th
* Sprint 5: Monday, April 29th - Sunday, May 12th
* Sprint 6: Monday, May 13th - Sunday, May 26th

## Identification of Risks

1. Legal issues when having a rental application
2. One of us doesn't pass CS461
3. All of our computer are plugged in while at school and ITC gets struck by lightning and it destroys all of our laptops before we have the change to push our work up to our remote repos.
4. The API we want to use is no longer available.