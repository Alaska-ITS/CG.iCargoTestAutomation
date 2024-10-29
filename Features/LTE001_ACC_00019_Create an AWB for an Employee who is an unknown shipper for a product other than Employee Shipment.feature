Feature: LTE001_ACC_00019_Create an AWB for an Employee who is an unknown shipper for a product other than Employee Shipment

Create a New Shipment, Acceptance of that new shipment & screening as a CGO or CGODG user

@LTE001 @LTE001_ACC_00019
Scenario Outline: Create an AWB for an Employee who is an unknown shipper for a product other than Employee Shipment
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
	And User enters the Screening details for row 1 with screeingMethod as 'Transfer Manifest Verified' and ScreeningResult as 'Pass'
	And User clicks on the ContinueScreeningDetails button
	And User clicks on the save button
	And User saves the shipment details validate error message as "No credit account exists." and capture AWB number
	#And User captures the checksheet
	#And User checks the AWB_Verified checkbox
	#And User clicks on the save button
	#And User handles the error popups with errorType as ''
	#And User saves all the details & handles all the popups
	

Examples:
	| AgentCode | UnknownShipperCode | ConsigneeCode | Origin | Destination | ProductCode | SCC | Commodity | ShipmentDescription | ServiceCargoClass | Piece | Weight | ChargeType | ModeOfPayment | cartType | Execute |
	| ASQXGUEST | C1001              | 10763         | SEA    | ANC         | GOLDSTREAK  | SAL | NONSCR    | None                | None              | 3     | 87     | PP         | CREDIT        | CART     | Yes     |
