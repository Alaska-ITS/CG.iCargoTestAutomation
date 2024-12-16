Feature: OPR339_SCRN

@OPR339_SCRN_00002 @OPR339 @DataSource:../TestData/OPR339_SecurityScreening_TestData.xlsx @DataSet:OPR339_SCRN_00002
Scenario Outline: Create a PP AWB in LTE001 for a known shipper and screen the full slated pieces using OPR339 screen
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
	And User clicks on the ContinueScreeningDetails button
	And User checks the AWB_Verified checkbox
	And User saves all the details with ChargeType "<ChargeType>" and validates the popped up error message as "Blocked for screening"
	And User enters the screen name as 'OPR339'
	And User enters the AWB number
	And User clicks on List button
	And User enters the Screening details with screeingMethod as 'Transfer Manifest Verified' and ScreeningResult as 'Pass'
	And User enters the screen name as 'LTE001'
	When User enters the Executed and Screened AWB number
	And User verifies and Update the Screening Details
	And User clicks on the ContinueScreeningDetails button
	And User saves all the details & handles all the popups	

	@OPR339_SCRN_00003 @OPR339 @DataSource:../TestData/OPR339_SecurityScreening_TestData.xlsx @DataSet:OPR339_SCRN_00003
Scenario Outline: Create a PP AWB in LTE001 for a known shipper and screen the half slated pieces using OPR339 screen
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
	And User clicks on the ContinueScreeningDetails button
	And User checks the AWB_Verified checkbox
	And User saves all the details with ChargeType "<ChargeType>" and validates the popped up error message as "Blocked for screening"
	And User enters the screen name as 'OPR339'
	And User enters the AWB number
	And User clicks on List button
	And User enters the Screening details with screeingMethod as 'Transfer Manifest Verified' and ScreeningResult as 'Pass' and HalfSlatedPieces as "<SlatedPieces>"
	And User enters the screen name as 'LTE001'
	When User enters the Executed and Screened AWB number
	And User verifies and Update the Screening Details
	And User clicks on the ContinueScreeningDetails button
	And User saves all the details & handles all the popups	