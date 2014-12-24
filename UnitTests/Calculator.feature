Feature: Making Calculations


Scenario: Add two numbers
	Given I enter 3 plus 4
	When I press add
	Then the result should be 7 on the screen
