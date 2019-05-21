Feature: TestRealTimeNotifications
	When the owner, a logged-in user, visits the rental notifications page, 
	they should expect to see real-time notifications when a renter rents out an item from them.

@scopedBinding
Scenario: Notifications show up real-time
	Given I am a logged in user
	When I navigate to the rental notifications page
	Then I should see real-time notifications when a renter rents out an item from me
