@automated
Feature: Test

Automation project with Playwright Nunit and Specflow

@tag1
Scenario: Look for non existing postal code
	Given Enter worldpostalcode website
    When I Look for code'80700000'
    Then Postal code does not exist

@tag2
Scenario: Look for existing postal code
	Given Go back to worldpostalcode website
    When I Look for code'01013-001'
    Then Postal code does exist

@tag3
Scenario: Look for stock code
	Given Go to B3 website
    When I Look for code'Petr3'
    Then Stock code does exist