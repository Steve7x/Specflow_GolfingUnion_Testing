Feature: GolfingUnionBookingFeature
	In order to test functionality on the GolfingUnion site
	As a tester
	I want to ensure functionality is working end to end

@mytag
Scenario: User should login, search for desired time and then book in 
	Given I have navigated to the GolfingUnion site
	And I have entered a Username + Password
	And I select the desired Date + Time
	When I press book-now
	Then the user should be booked in for the selected time and date
