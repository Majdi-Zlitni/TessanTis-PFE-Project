Feature: Search
	Search a course from edx
Scenario: Search for Automated software testing in edx
    Given Explore the courses
    And I enter 'Automated software testing' in input serach
    When I click to search buttom
    Then Search results should contains 'Automated software testing'