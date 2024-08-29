Feature: OPR344_EXP_00001_Manifest an AWB for an Unknown Shipper on a pax flight

Manifest a Shipment as a CGO or CGODG user

@OPR344 @OPR344_EXP_00001
Scenario Outline: Manifest an AWB for an Unknown Shipper on a pax flight
	Given User wants to execute the example "<Execute>"
	When User switches station if BaseStation other than "<Origin>"
	And User enters the screen name as 'LTE001'
	Then User enters into the  iCargo 'Create Shipment' page successfully
	When user clicks on the List button
	And User enters the Participant details with AgentCode "<AgentCode>",Unknown ShipperCode "<UnknownShipperCode>", ConsigneeCode "<ConsigneeCode>"
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
	And User enters the Acceptance details
	And User clicks on the ContinueAcceptanceDetails button
	And User clicks on the ContinueScreeningDetails button
	And User checks the AWB_Verified checkbox
	And User saves the shipment details validate error message as "The Shipper does not have a Valid Certificate Type" and capture AWB number
	When User enters the screen name as 'OPR344'
	Then User enters into the  iCargo 'Export Manifest' page successfully
	When User enters the Booked FlightNumber with ""
	And User enters Booked ShipmentDate
	And User clicks on the List button to fetch the Booked Shipment
	And User creates new ULD/Cart in Assigned Shipment with cartType "<cartType>" and pou "<Destination>"
	And User filterouts the Booked AWB from '<AWBSectionName>' and Created ULD_Cart
	And User validates the error popover message as "AWB is not accepted"
	Then User closes the Export Manifest screen
	

Examples:
	| AgentCode | UnknownShipperCode | ConsigneeCode | Origin | Destination | ProductCode | SCC  | Commodity | ShipmentDescription | ServiceCargoClass | Piece | Weight | ChargeType | ModeOfPayment | cartType | AWBSectionName  | Execute |
	| ASQXGUEST | C1001              | 10763         | SEA    | JFK         | GENERAL     | None | NONSCR    | None                | None              | 13    | 775    | CC         | None          | CART     | PlannedShipment | Yes     |
	| ASQXGUEST | C1001              | 10763         | SEA    | JFK         | PRIORITY    | None | 2199      | None                | None              | 8     | 360    | CC         | None          | CART     | PlannedShipment | Yes     |
	



