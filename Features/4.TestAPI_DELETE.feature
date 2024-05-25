Feature: 4.TestAPI_DELETE


@regression @delete
Scenario: Make PUT Request and verify Status Code and Content
	Given I am a client
	When I make a DELETE  request to 'api/users/2'
	Then the response status code is '204'