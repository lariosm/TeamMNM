Feature: TestUserRating
	When a logged in user clicks to view the user profile of the owner from an item listing, 
	they will be able to rate the owner on a scale of 1 to 5 stars, with 1 being the worst and 5 being the best.

@scopedBinding
Scenario: Rate another user
	Given I have navigated to an item listing as a logged in user
	And click to view the user profile of the owner renting the item
	When I click on a star and leave a review
	Then I should be able to see my review on the owner on their user profile page
