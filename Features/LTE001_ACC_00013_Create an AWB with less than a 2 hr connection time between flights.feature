Feature: LTE001_ACC_00013_Create an AWB with less than a 2 hr connection time between flights

Create a New Shipment, Acceptance of that new shipment & screening as a CGO or CGODG user

@LTE001 @LTE001_ACC_00013
Scenario Outline: Create an AWB with less than a 2 hr connection time between flights
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
	And User selects flights having Minimum Handling / Connection Time Fails restriction
	And User clicks on the ContinueFlightDetails button
	And User enters the Charge details with ChargeType "<ChargeType>" and ModeOfPayment "<ModeOfPayment>"
	And User clicks on the CalculateCharges button
	And User clicks on the ContinueChargeDetails button
	And User enters the Acceptance details
	And User clicks on the ContinueAcceptanceDetails button
	And User enters the Screening details for row 1 with screeingMethod as 'Transfer Manifest Verified' and ScreeningResult as 'Pass'
	And User clicks on the ContinueScreeningDetails button
	And User checks the AWB_Verified checkbox
	And User saves the shipment details validate error message as "Minimum connection time not satisfied" and capture AWB number
	#And User clicks on the save button
	#And User handles the error popups with errorType as 'Embargo'
	#And User validates the popped up error message as "Minimum connection time not satisfied"
	#And User closes the LTE screen
	#Then User logs out from the application

Examples:
	| AgentCode | ShipperCode | ConsigneeCode | Origin | Destination | ProductCode | SCC  | Commodity | ShipmentDescription | ServiceCargoClass | Piece | Weight | ChargeType | ModeOfPayment | cartType | Execute |
	| 11377     | 11377       | 11377         | ANC    | LAS         | GENERAL     | None | 0316      | None                | None              | 2     | 59     | PP         | CREDIT        | CART     | Yes     |
	