# Project Contribution Guidelines

## Software Construction Process
We are following the Agile SCRUM process, and also processes explained in the Discipline Agile Delivery book. We follow a two-week sprint cycle that involves daily SCRUM meetings, weekly sprint planning sessions, and end of sprint review and retrospective. To be a part of MNM Solutions these processes and lifecycles are required.

## Coding Guidelines

#### C#
* Use standard C# naming [conventions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions). (i.e. LogEvent)
* Use RESTful style when creating controllers. (i.e. create a new controller for each entity)
* All public methods should be commented with XML comments.

#### JavaScript
* Use external ```.js``` files.
* Use camelCase when naming fields, variables and functions.
* Use single-line (```//```) or multi-line (```/**/```) comments on functions and other areas where appropriate.

#### CSS
* Use external ```.css``` files.
* Use ```/**/``` to comment where appropriate.

#### Database
* Pluralize table names.
* Table primary keys are ```ID```.
* Foreign keys are ```<Entity>ID```.
* Foreign keys are the last column in the table.
* Use ```--``` to comment where appropriate.

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

## Team Rules
* Respect others
* Complete what you say you will complete
* Communicate with team members