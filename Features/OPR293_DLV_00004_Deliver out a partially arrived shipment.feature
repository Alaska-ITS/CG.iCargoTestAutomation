Feature: OPR293_DLV_00004_Deliver out a partially arrived shipment

Scenario Outline: Deliver out a partially arrived shipment
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
	And User enters the Acceptance details
	And User clicks on the ContinueAcceptanceDetails button
	And User enters the Screening details for row 1 with screeingMethod as 'Transfer Manifest Verified' and ScreeningResult as 'Pass'
	And User clicks on the ContinueScreeningDetails button
	And User checks the AWB_Verified checkbox
	And User saves all the details & handles all the popups
	When User enters the screen name as 'OPR344'
	Then User enters into the  iCargo 'Export Manifest' page successfully
	When User enters the Booked FlightNumber with ""
	And User enters Booked ShipmentDate
	And User clicks on the List button to fetch the Booked Shipment
	And User creates new ULD/Cart in Assigned Shipment with cartType "<cartType>" and pou "<Destination>"
	And User filterouts the Booked AWB from '<AWBSectionName>' and Created ULD_Cart
	And User clicks on the Manifest button
	And User closes the PrintPDF window
	And User validates the AWB is "Manifested" in the Export Manifest screen
	Then User closes the Export Manifest screen
	When User enters the screen name as 'FLT006'
	When User enters the flight details and movement details for 'departure' and clicks on save button
	When User enters the screen name as 'OPR344'
	Then User enters into the  iCargo 'Export Manifest' page successfully
	When User enters the Booked FlightNumber with ""
	And User enters Booked ShipmentDate
	And User clicks on the List button to fetch the Booked Shipment
	And User checks for the flight status to be finalized
	Then User closes the Export Manifest screen
	When User switches station if BaseStation other than "<Destination>"
	When User enters the screen name as 'FLT006'
	When User enters the flight details and movement details for 'arrival' and clicks on save button
	When User enters the screen name as 'OPR367'
	Then User enters into the  iCargo 'Import Manifest' page successfully
	When User enters the Flight details to fetch the uld details
	And User selects ULD and clicks on the breakdown button to breakdown
	And User enters the breakdown details with BreakdownLocation "<Bdn_Locn>", receivedPieces "<Bdn_RcvdPieces>", receivedWeight "<Bdn_RcvdWeight>"
	Then User Clicks on the save button and validates the popup message as 'Saved successfully. Do you want to list the saved details?'
	When User enters the screen name as 'OPR293'
	Then User enters into the  iCargo 'Delivery Documentation : OPR293' page successfully
	When User lists the AWB details for delivery
	And process the Delivery Note details
	And User saves the payment details for "<ChargeType>"
	And User clicks on Accept Payment button
	And User Captures the delivery details
	

Examples:
	| AgentCode | ShipperCode | ConsigneeCode | Origin | Destination | ProductCode | SCC  | Commodity | ShipmentDescription | ServiceCargoClass | Piece | Weight | ChargeType | ModeOfPayment | AWBSectionName  | cartType | Bdn_Locn | Bdn_RcvdPieces | Bdn_RcvdWeight | Execute |
	| 11377     | 11377       | 11377         | SFO    | SEA         | GOLDSTREAK  | None | NONSCR    | None                | None              | 2     | 60     | CC         | None          | PlannedShipment | CART     | IDEFLOC  | 1              | 30             | Yes     |


