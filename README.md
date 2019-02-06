# MNM Solutions

This is the repository for **MNM Solutions**. It primarily houses work items for the 2018-19 Capstone project course sequence.

## Team members 
* Manuel Larios
* Nikki Ki
* Mason Stokes

## Description of Project:
**Vision Statement:** For people who like to make a little money on the side or rent rather than purchase, this sharing economy rental website is a host for all of these kinds of peer to peer transactions. This rental website will be clean looking, easy to find local items that you are interested in, and even create a competitive market for affordable transactions. Unlike other rental websites, our website will have distinguished features that are extremely user friendly making it an enjoyable experience rather than a stressful one.

People like to save money; people like to make money. People like to rent items instead of buying them, or rent their property out to make a little extra money. The problem with this is that there are not any good places to do this on the Web. Not only have businesses taken over the rental marketplace, but it is also very flawed.

By flooding rental websites or mobile apps with business products, it deprecates the idea of a sharing economy. Some of these businesses include Rent-A-Center and Aaron’s.  A sharing economy and peer to peer transactions are main priorities for this web application. This is because by taking away the businesses, we can sustain a host that will give users, both owners and renters, a more personal and enjoyable renting experience.

We have investigated other attempts at a rental website, and they are poor attempts. Just look at services like lendr and BeOmni which are mobile apps, and Zilok and PeerRenters are poorly designed websites. These services are not quite as simple as an Amazon or Craigslist when it comes to renting.

We want a site that is simple, easy to use, and sharing economy friendly for people to find what they want or need to rent. Visitors should be able to search and view rentable products without having to log in, users should be able to make an account to both rent and rent out items, and it should be done easily. It should provide features for the user to sign in using a 3rd-party log in, manage their account, view their renting history, and even rate their rental experience.

## Guidelines
#### C#
* Use standard C# naming conventions. (i.e. LogEvent)
* Use RESTful style when creating controllers. (i.e. create a new controller for each entity)
* All public methods should be commented with XML comments.

#### JavaScript
* Use external ```.js``` files.

#### CSS
* Use external ```.css``` files.

#### Database
* Pluralize table names.
* Table primary keys are ```ID```.
* Foreign keys are ```<Entity>ID```.
* Foreign keys are the last column in the table.

#### Git
* Use branching. Our naming scheme is ```ft_<yourname>_<PBI ID number>```
* Commit often with meaningful commit messages
* Don't commit code that doesn't compile
* Resolve your own merge conflicts by first merging dev into your feature branch and testing thoroughly

## Git Workflow
When working with this repository, remember to follow the git forking workflow outlined below. 

1. Do a ```git checkout``` to ```develop``` branch
2. Always fetch and merge upstream first, before working. Doing so will allow you to be up-to-date with the main repository.
    * To fetch, do ```git fetch upstream develop```
    * To merge, do ```git merge upstream/develop```
3. Update your remote fork repository by pushing.
4. Create a feature branch off of ```develop```. Be sure to follow the Git guidelines described above as you name your feature branch.
5. After completing your task, push your feature branch to your forked repository and create a pull request.
    * Before pushing, be sure to fetch and merge from upstream to stay up-to-date with the latest commit.
    * Make sure all merge conflicts are resolved before creating a pull request.
    * When creating a pull request, be sure to specify that you want to merge your feature branch into the ```develop``` branch on the main repo.
6. If your pull request is denied, resolve any conflicts then create another.
7. When pull request is merged, fetch and merge into your local develop branch. Update your remote repo.

## Software Construction Processes or Lifecycles:
We are following the Agile SCRUM process, and also processes explained in the Discipline Agile Delivery book. We follow a two-week sprint cycle that involves daily SCRUM meetings, weekly sprint planning sessions, and end of sprint review and retrospective. To be apart of MNM Solutions these processes and lifecycles are required.

## Team Rules:
* Respect
* Complete what you say you will complete
* Communication with team members

[Team Mascot](https://www.youtube.com/watch?v=j5a0jTc9S10)

## Tools:
* Visual Studio 2017 (v15.9.6)
* Bitbucket
* Azure Web Services
* Git
* Kudu

### NuGet Packages:
* Bootstrap (v4.2.1)
* jQuery (v3.3.1) 
* ASP.NET MVC 5
* EntityFramework - (v6.2.0)
* ASP.NET Razor (v3.2.7) 
* Modernizr (v2.8.3)