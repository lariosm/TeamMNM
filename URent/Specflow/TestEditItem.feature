Feature: TestEditItem
	When a logged in owner of an item is on that item's details page and clicks on the edit button,
	they should be taken to the item's edit page and should be able to upload a new photo to be shown for that item.

@mytag
Scenario: Add two numbers
	Given I have navigated to my item's details page that I want to edit
	And I am the owner of the item
	When I press the edit button
	Then I should be taken to that item's edit page to upload a new photo for that item
