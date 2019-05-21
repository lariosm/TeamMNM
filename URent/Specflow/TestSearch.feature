Feature: TestSearch
	When a user visits the site and types a word to search for items,
	after the user clicks the search button then it should take them to the search page.

@mytag
Scenario: Add two numbers
	Given I have navigated to the URent website
	And I have typed a search word into the search bar
	When I press the search button
	Then I should be taken to the search result page
