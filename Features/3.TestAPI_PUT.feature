Feature: 3.TestAPI_PUT

@regression @put
Scenario: Make PUT Request and verify Status Code and Content
	Given I am a client
	When I make a PUT  request to 'api/users/2' with following data
		| Name  | Job                    |
		| Jason | Automation QA Engineer |

	Then the response status code is '200'
	And the response content should be following
		| Name  | Job                    |
		| Jason | Automation QA Engineer |