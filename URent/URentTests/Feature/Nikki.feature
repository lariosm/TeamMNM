Feature: Nikki
	In order to test search functionality on URent
	As a developer I was to ensure functionality is working end to end


@tag1
Scenario: URent search should navigate to search page
	Given I have naigated to the URent website
	When I press the search button
	Then I should navigate to search result page
