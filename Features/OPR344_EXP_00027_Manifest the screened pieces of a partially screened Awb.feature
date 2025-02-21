Feature: OPR344_EXP_00027_Manifest the screened pieces of a partially screened Awb

@OPR344_EXP_00026  @OPR344
Scenario: Manifest the screened pieces of a partially screened Awb
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
	And User clicks on the ContinueScreeningDetails button	
	And User checks the AWB_Verified checkbox
	And User saves all the details & handles all the popups
	When User enters the screen name as 'OPR344'
	Then User enters into the  iCargo 'Export Manifest' page successfully
	When User enters the Booked FlightNumber with "<FlightNumber>"
	And User enters Booked ShipmentDate
	And User clicks on the List button to fetch the Booked Shipment
	And User creates new ULD/Cart in Assigned Shipment with cartType "<cartType>" and pou "<Destination>"
	And User selects the Booked AWB from the Lying List, Split And assign with Pieces "<SplitPieces>"	
	And User validates the error message in Check Embargo as "SCREENING MUST BE COMPLETED FOR MOVEMENT ON PAX AIRCRAFT"	
	And User clicks on the Manifest button
	And User closes the PrintPDF window
	And User validates the AWB is "Manifested" in the Export Manifest screen
	Then User closes the Export Manifest screen

Examples:
	| AgentCode | ShipperCode | ConsigneeCode | Origin | Destination | ProductCode | SCC  | Commodity | ShipmentDescription | ServiceCargoClass | Piece | Weight | ChargeType | ModeOfPayment | FlightNumber | AWBSectionName | cartType | Execute | SplitPieces |
	| 11377     | 11377       | 11377         | SEA    | ANC         | GENERAL     | None | NONSCR    | None                | None              | 2     | 60     | CC         | None          | 173          | LyingList      | CART     | Yes     | 1           |

