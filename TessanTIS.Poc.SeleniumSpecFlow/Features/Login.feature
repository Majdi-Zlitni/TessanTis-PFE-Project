Feature: Login
	In order to access my account
  As a user of the website edx
  I want to log into the website edx


@mytag
Scenario: Login user as Sihem Jalleb
	Given I am on the Home page
    And I click the Sign In option
    When I log in login as 'Sihem2021' and password as 'Learning$2021'
    Then I am logged in as 'Sihem2021'
