Feature: CAP018_BKG_00007_Create a booking for an AVI

@CAP018 @CAP018_BKG_00007
Scenario Outline: iCargo Login and Create New AVI Shipment
	Given User wants to execute the example "<Execute>"
	When User switches station if BaseStation other than "<Origin>"
	When User enters screen name as 'CAP018'
	Then User enters into the  iCargo 'Maintain Booking' page successfully
	And User clicks on New/List button
	And User enters shipment details with Origin "<Origin>", Destination "<Destination>", Product Code "<ProductCode>" and Agent code "<Agent Code>"
	And User enters Shipper "<Shipper Code>" and Consignee "<Consignee Code>" details
	And User enters commodity details with Commodity "<Commodity>", Pieces "<Piece>", Weight "<Weight>"
	And User selects flight for "<ProductCode>"
	And User clicks on Save button and fills the checksheet details to generate awb
Examples:
	| Origin | Destination | ProductCode | Commodity | Piece | Weight | Execute | Agent Code | Shipper Code | Consignee Code |
	| ANC    | FAI         | PET CONNECT | 9730      | 2     | 50     | No      | 10763      | 10763        | 10763          |
	| SEA    | HNL         | PET CONNECT | 9730      | 1     | 100    | Yes     | ASQXGUEST  | C1001        | C1001          |
	| BOI    | LAX         | PET CONNECT | 9730      | 1     | 35     | No      | 49990      | 49990        | C1001          |
