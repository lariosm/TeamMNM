2018-19 Class Project Inception: Discussion Hub
=====================================

## Summary of Our Approach to Software Development

We are following the Agile SCRUM process, and also processes explained in the Discipline Agile Delivery book.


## Initial Vision Discussion with Stakeholders

Vision Statement: For people who like to discuss their thoughts on current events, this centralized discussion site is a hub for all civil conversations. This centralized discussion site will provide a single location for hosting communications amongst its users. Unlike mainstream social media, our product will foster cordial and productive discussions, and potentially be a meaningful distribution of ideas.


People like to talk; people like to discuss and argue.  People like to state their opinions and read thoughts of others.  But there are real problems with how it is happening on the Web.  Just take a look at YouTube or Twitter comments.  That's not a discussion and it's not often useful, productive or even civil.  Social media is not the place to talk about things.  The Internet should be a place where people can *communicate*.  Let's make a site where people can do that!

Places like Reddit and Kialo already have a handle on general topic discussions, so we won't try to do that.  Enthusiasts often have their own sites with discussion and comment sections (e.g. Slashdot) and have formed close-knit communities that work well.  We don't want to do that either.  Here's our idea, using an example:

Let's say I just read this article on CNN, [American endurance athlete becomes the first person to cross Antarctica solo](https://www.cnn.com/2018/12/27/world/colin-obrady-antarctica-solo-trip-trnd/index.html), about a guy from Oregon who skied *across* Antarctica.  The thing is, is that he didn't and he wasn't the first.  His claims should be challenged and the article/journalist is disingenuous by not at least bringing this up.  People reading this article should know there are serious questions about it.  Someone should comment on this to point it out and spark further discussion.   There is no way to do this.  CNN does not have a comment section.  Even if it did, do I want to create an account there just so I can make a quick comment?  I'd have to do that everywhere on the web where I had a question or wanted to comment.  How would I know if my question was answered?  

We want a centralized discussion site that can be found easily and where an individual can maintain an account, build a history, expertise, level of trust, etc.  It should make it easy to create or find a discussion page about any news article, post or web site.  It should provide features for the user to follow their discussions without ever going back to the original website.  It should allow them to create and maintain their own identity that is separate from any social media identity.

## Initial Requirements Elaboration and Elicitation

### Questions

1. How do we link a discussion on the site to one or more articles/pages?

    We were thinking via URL.

2. How will users find out that there is a discussion on the site for the article/page they're currently viewing?
   
    How about a browser plug-in?  It could send the URL of the current page to our API to see if a discussion page exists and provide an easy way for them to navigate to our page.

    Or the user can copy the URL and paste it into a search bar on our site.

3. Clearly we need accounts and logins.  Our own, or do we allow logging in via 3rd party, i.e. "Log in with Google" or ...?  

    Both approaches are good ones, but using a 3rd party would be more convenient for the users.

4. Do we allow people to comment anonymously?  Read anonymously? 

    We should allow people to read anonymously, but commenting would require an account.

5. Do we allow people to sign up with a pseudonym or will we demand/enforce real names?

    It's almost impossible to verify that users are using their real names, so a pseudonym would be fine.

6. What is it important to know about our users?  What data should we collect?

    As much information as possible to sell.

7. If there are news articles on multiple sites that are about the same topic should we have separate discussion pages or just one?

    It shouldnt matter, because there can be different types of conversations on one topic.

8. What kind of discussion do we want to create? Linear traditional, chronological, ranked, or ?

    We should give users the ability to choose how to view comments. Examples are sorting chronologically or ranking.

9. Should we allow image/video uploads and host them ourselves?

    No, only links to outside sources.

10. Should users be able to comment on other user's comments?

    Yes, because they can start threads with other small topics within the discussion topic.

11. Should users be able to tag other users in their comments?

    Yes, dont @ me.

### Interviews


## List of Needs and Features

1. A great looking landing page with info to tell the user what our site is all about and how to use it.  Include a link to and a page with more info.  Needs a page describing our company and our philosophy.
2. The ability to create a new discussion page about a given article/URL.  This discussion page needs to allow users to write comments.
3. The ability to find a discussion page.
4. User accounts
5. A user needs to be able to keep track of things they've commented on and easily go back to those discussion pages.  If someone rates or responds to their comment we need to alert them.
6. Allow users to identify fundamental questions and potential answers about the topic under discussion.  Users can then vote on answers.
7. The ability to easily respond to another user's comments, and the option to tag the other user
8. The ability to sort content within a discussion
9. The ability to receive notifications as discussions progress

## Initial Modeling

### Use Case Diagrams

### Other Modeling

## Identify Non-Functional Requirements

1. User accounts and data must be stored indefinitely.
2. Site and data must be backed up regularly and have failover redundancy that will allow the site to remain functional in the event of loss of primary web server or primary database.  We can live with 1 minute of complete downtime per event and up to 1 hour of read-only functionality before full capacity is restored.
3. Site should never return debug error pages.  Web server must never return 404's.  All server errors must be logged.  Users should receive a custom error page in that case telling them what to do.
4. Must work in all languages and countries.  English will be the default language but users can comment in their own language and we may translate it.
5. Site should have light and dark modes
6. Discussions should stay rolling live

## Identify Functional Requirements (User Stories)

E: Epic  
F: Feature  
U: User Story  
T: Task

1. [U] As a visitor to the site I would like to see a fantastic and modern homepage that tells me how to use the site so I can decide if I want to use this service in the future.
   1. [T] Create starter ASP dot NET MVC 5 Web Application with Individual User Accounts and no unit test project
   2. [T] Switch it over to Bootstrap 4
   3. [T] Create nice homepage: write content, customize navbar
   4. [T] Create SQL Server database on Azure and configure web app to use it. Hide credentials.
2. [U] Fully enable Individual User Accounts
   1. [T] Copy SQL schema from an existing ASP.NET Identity database and integrate it into our UP script
   2. [T] Configure web app to use our db with Identity tables in it
   3. [T] Create a user table and customize user pages to display additional data
3. [F] Allow logged in user to create new discussion page
4. [F] Allow any user to search for and find an existing discussion page
5. [E] Allow a logged in user to write a comment on an article in an existing discussion page
   1. [F] Implement commenting
      1. [U] As a user I would like be able to comment on discussions so that I can talk to others about a given topic.
         1. [T] Create a text field under discussion topics
         2. [T] Create a table to store comment data
         3. [T] Use AJAX to show most recent comments without refreshing
   2. [F] Tagging
      1. [U] As a user I would like to be able to tag, so that I can direct my comment to another user
   3. [F] Hashtagging
      1. [U] As a user I would like to be able to hashtag a comment so that I can see other comments with the same hashtag.
6. [U] As a robot I would like to be prevented from creating an account on your website so I don't ask millions of my friends to join your website and add comments about male enhancement drugs.
7. [F] 3rd party logging in
   1. [U] As a user it is easier for me to use Google to make accounts.
8. [F] Filtering/Removing unnecessary comments
   1. [U] As a moderator I would like to be able to filter and even remove if need be, through comments. To avoid mallicious conversations.
   2. [U] As an administrator I would like to ensure good camaraderie amongst users.
9. [F] Voting on comments
   1. [U] As a user I would like to be able to vote on a comment showing wether I thought it was good or bad.
11. [U] As a troll I would like to be prevented from spamming comment sections with unrelated ideas.
12. [U] As a visitor I would like to be able to atleast view comments without having to log in.

## Initial Architecture Envisioning

## Agile Data Modeling

## Timeline and Release Plan