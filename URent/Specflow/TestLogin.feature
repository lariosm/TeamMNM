Feature: TestLogin
	When a user has registered a new account, whenever they visit our site they
	can login to their account.

@mytag
Scenario: Login to a user account
	Given I have registered as a user
	And I can input my email and password
	When I press login
	Then I am logged into my account and redirected to the home page.
