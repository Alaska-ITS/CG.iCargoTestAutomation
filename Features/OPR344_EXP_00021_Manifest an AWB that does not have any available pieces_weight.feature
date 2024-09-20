Feature: OPR344_EXP_00021_Manifest an AWB that does not have any available pieces_weight

Manifest a Shipment as a CGO or CGODG user

@OPR344 @OPR344_EXP_00021
Scenario Outline: Manifest an AWB that does not have any available pieces_weight
	Given User wants to execute the example "<Execute>"
	When User switches station if BaseStation other than "<Origin>"
	And User enters the screen name as 'LTE001'
	Then User enters into the  iCargo 'Create Shipment' page successfully
	When user clicks on the List button
	And User enters the Participant details with AgentCode "<AgentCode>", ShipperCode "<ShipperCode>", ConsigneeCode "<ConsigneeCode>"
	And User clicks on the ContinueParticipant button
	And User enters the Certificate details
	And User clicks on the ContinueCertificate button
	And User enters the Shipment details with Origin "<Origin>", Destination "<Destination>", ProductCode "<ProductCode>", SCCCode "<SCC>", Commodity "<Commodity>", ShipmentDescription"<ShipmentDescription>", ServiceCargoClass "<ServiceCargoClass>", Piece "<Piece>", Weight "<Weight>"
	And User clicks on the ContinueShipment button
	And User clicks on the Select Flight Button
	And User selects an "Combination" flight
	And User clicks on the ContinueFlightDetails button
	And User enters the Charge details with ChargeType "<ChargeType>" and ModeOfPayment "<ModeOfPayment>"
	And User clicks on the CalculateCharges button
	And User clicks on the ContinueChargeDetails button	
	And User checks the AWB_Verified checkbox
	And User saves all the details with ChargeType "<ChargeType>"
	And User enters the screen name as 'OPR344'
	Then User enters into the  iCargo 'Export Manifest' page successfully
	When User enters the Booked FlightNumber with ""
	And User enters Booked ShipmentDate
	And User clicks on the List button to fetch the Booked Shipment
	And User creates new ULD/Cart in Assigned Shipment with cartType "<cartType>" and pou "<Destination>"
	And User filterouts the Booked AWB from '<AWBSectionName>' and Created ULD_Cart
	And User validates the error popover message as "The assigned pieces cannot be greater than the available pieces for the AWB"
	Then User closes the Export Manifest screen

Examples:
	| AgentCode | ShipperCode | ConsigneeCode | Origin | Destination | ProductCode | SCC  | Commodity | ShipmentDescription | ServiceCargoClass | Piece | Weight | ChargeType | ModeOfPayment | AWBSectionName  | cartType | Execute |
	| 11377     | 11377       | 11377         | SEA    | LAX         | GENERAL     | None | NONSCR    | None                | None              | 2     | 59     | CC         | None          | PlannedShipment | CART     | Yes     |


