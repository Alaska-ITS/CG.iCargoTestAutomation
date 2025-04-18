﻿Feature: OPR344_EXP_00028 Manifest a split of a CAO AWB to a pax flight via the lying list

A short summary of the feature
@OPR344 @OPR344_EXP_00028
Scenario Outline: Manifest a CAO AWB from lying list to a pax flight
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
	And User selects an "Cargo-Only" flight
	And User clicks on the ContinueFlightDetails button
	And User enters the Charge details with ChargeType "<ChargeType>" and ModeOfPayment "<ModeOfPayment>"
	And User clicks on the CalculateCharges button
	And User clicks on the ContinueChargeDetails button
	And User enters the Acceptance details
	And User clicks on the ContinueAcceptanceDetails button
	And User enters the Screening details for row 1 with screeingMethod as 'ALT Dangerous Goods' and ScreeningResult as 'Pass'
	And User clicks on the ContinueScreeningDetails button
	And User enters details for CAO DG shipment with ChargeType "<ChargeType>",UNID "<UNID>", ProperShipmentName "<ProperShipmentName>", PackingInstruction "<PackingInstruction>",NoOfPkg "<Piece>", NetQtyPerPkg "<NetQtyPerPkg>", ReportableQnty "<ReportableQnty>"
	And User saves the CAO DG shipment
	And User captures the checksheet
	And User checks the AWB_Verified checkbox
	And User saves all the details & handles all the popups
	When User enters the screen name as 'OPR344'
	Then User enters into the  iCargo 'Export Manifest' page successfully
	When User enters the Booked FlightNumber with "<FlightNumber>"
	And User enters Booked ShipmentDate
	And User clicks on the List button to fetch the Booked Shipment
	And User creates new ULD/Cart in Assigned Shipment with cartType "<cartType>" and pou "<Destination>"
	And User filterouts the Booked AWB from the Lying List, Split And assign with Pieces "<SplitPieces>"
	And User validates the warning message "The CAO validations failed" and Cancel the warning message

	Examples:
	| AgentCode | ShipperCode | ConsigneeCode | Origin | Destination | ProductCode | SCC     | Commodity | ShipmentDescription | ServiceCargoClass | Piece | Weight | ChargeType | ModeOfPayment | UNID | ProperShipmentName    | PackingInstruction | NetQtyPerPkg | ReportableQnty | AWBSectionName | cartType | Execute | SplitPieces | FlightNumber |
	| 11377     | 11377       | 11377         | ANC    | OME         | PRIORITY    | DGR,CAO | NONSCR    | UN3480              | None              | 6     | 180    | CC         | None          | 3480 | Lithium ion batteries | 965                | 0.5          | No             | LyingList      | CART     | Yes     | 3           | 2002         |