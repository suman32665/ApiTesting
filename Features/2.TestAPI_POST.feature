Feature: 2.TestAPI_POST


@regression @post
Scenario: Make POST Request and verify Status Code and Content
	Given I am a client
	When I make a POST request to 'api/users' with following data
		| Name  | Job         |
		| Suman | QA Engineer |

	Then the response status code is '201'
	And the response content should be following
		| Name  | Job         |
		| Suman | QA Engineer |

