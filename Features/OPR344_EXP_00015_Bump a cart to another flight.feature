﻿Feature: OPR344_EXP_00015_Bump a cart to another flight
Manifest a Shipment as a CGO or CGODG user

@OPR344 @OPR344_EXP_00015
Scenario Outline: Bump a cart to another flight
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
	And User cliks on the offload ULD button to open the offload popup
	And User enters the details to move to another NewFlightNumber "<NewFlightNumber>" and POU "<Destination>" in the 'ULD' offload popup
	And User validates the warning message "The shipment is not booked to the flight"
	And User validates the AWB is "Offloaded" in the Export Manifest screen
	And User clicks on the orange pencil to edit the manifest
	When User enters the new flight number "<NewFlightNumber>" to move the offloaded shipment
	And User enters Booked ShipmentDate
	And User clicks on the List button to fetch the Booked Shipment
	And User clicks on the Manifest button
	And User closes the PrintPDF window
	Then User closes the Export Manifest screen

Examples:
	| AgentCode | ShipperCode | ConsigneeCode | Origin | Destination | ProductCode | SCC  | Commodity | ShipmentDescription | ServiceCargoClass | Piece | Weight | ChargeType | ModeOfPayment | AWBSectionName  | NewFlightNumber | cartType | Execute |
	| 11377     | 11377       | 11377         | SEA    | JFK         | GENERAL     | None | 0316      | None                | None              | 2     | 59     | CC         | None          | PlannedShipment | 26              | CART     | Yes     |


