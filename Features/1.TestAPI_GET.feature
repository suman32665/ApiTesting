Feature: 1.TestAPI_GET

@regression @get
Scenario Outline: Make GET Request and verify Status code
	Given I am a client
	When I make a GET request to '<Endpoint>'
	Then the response status code is '<StatusCode>'
	And the response time is less than '<ResponseTime>' milliseconds

	Examples: 
	| SN  | Endpoint          | StatusCode | ResponseTime |
	| 100 | api/users?page=2  | 200        | 1000         |
	| 101 | api/users/2       | 200        | 1000         |
	| 102 | api/users/23      | 404        | 1000         |
	| 103 | api/unknown       | 200        | 1000         |
	| 104 | api/unknown/2     | 200        | 1000         |
	| 105 | api/unknown/23    | 404        | 1000         |
	| 106 | api/users?delay=3 | 200        | 1000         |


