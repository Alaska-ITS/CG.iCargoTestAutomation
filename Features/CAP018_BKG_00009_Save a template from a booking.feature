Feature: CAP018_BKG_00009_Save a template from a booking

@CAP018_BKG_00009
Scenario: Save a template from a booking
	Given User wants to execute the example "<Execute>"
	When User switches station if BaseStation other than "<Origin>"
	When User enters screen name as 'CAP018'
	Then User enters into the  iCargo 'Maintain Booking' page successfully
	And User clicks on New/List button
	And User enters shipment details with Origin "<Origin>", Destination "<Destination>", Product Code "<ProductCode>" and Agent code "<Agent Code>"
	And User enters Shipper "<Shipper Code>" and Consignee "<Consignee Code>" details
	And User enters commodity details with Commodity "<Commodity>", Pieces "<Piece>", Weight "<Weight>"
	And User selects flight for "<ProductCode>"
	And User clicks on Save button
	And User enters the AWB number
	And User clicks on New/List button
	And User clicks on Select/Save Template to save the template
	
Examples:

	| Origin | Destination | ProductCode | Commodity | Piece | Weight | Execute | Agent Code | Shipper Code | Consignee Code |
	| ANC    | HNL         | PRIORITY    | 2199      | 10     | 360   | No     | 10763      | 10763        | 10763          |
	| SEA    | BOI         | GENERAL     | NONSCR    | 13    | 775    | Yes     | 10763      | 10763        | 10763          |
	| SAN    | JFK         | GOLDSTREAK  | NONSCR    | 2     | 56     | Yes     | 10763      | 10763        | 10763          |
	| SEA    | MCO         | PRIORITY    | NONSCR    | 4     | 180    | No     | 10763      | 10763        | 10763          |
	| SEA    | DFW         | GENERAL     | NONSCR    | 31    | 4340   | No     | 10763      | 10763        | 10763          |
