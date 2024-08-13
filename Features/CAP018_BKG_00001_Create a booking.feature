Feature: CAP018_BKG_00001_iCargo Login and Create New Shipment

@CAP018 @CAP018_BKG_00001
Scenario Outline: iCargo Login and Create New Shipment
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

Examples:
	| Origin | Destination | ProductCode | Commodity | Piece | Weight | Execute | Agent Code | Shipper Code | Consignee Code |
	| ANC    | HNL         | PRIORITY    | 2199      | 8     | 360    | No     | 10763      | 10763        | 10763          |
	| SEA    | LAX         | GENERAL     | NONSCR    | 13    | 775    | Yes     | 10763      | 10763        | 10763          |
	| SAN    | JFK         | GOLDSTREAK  | NONSCR    | 2     | 55     | Yes     | 10763      | 10763        | 10763          |
