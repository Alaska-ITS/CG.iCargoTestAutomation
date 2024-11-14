Feature: CAP018_BKG_00010_Create a booking from a saved template

@CAP018_BKG_00010 @CAP018
Scenario: Save a template and create a booking
	Given User wants to execute the example "<Execute>"	
	When User enters screen name as 'CAP018'
	Then User enters into the  iCargo 'Maintain Booking' page successfully
	And User clicks on New/List button
	And User clicks on Select/Save Template	
	And User clicks on Save button
