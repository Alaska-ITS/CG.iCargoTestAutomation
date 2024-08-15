Feature: LTE001_ACC_00007_Create an AWB in LTE001 that has pieces that fail screening

Create a New Shipment, Acceptance of that new shipment & screening as a CGO or CGODG user

@LTE001 @LTE001_ACC_00007
Scenario Outline: Create an AWB in LTE001 that has pieces that fail screening
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
	And USer adds another screening line
	And User enters the Screening details for row 2 with screeingMethod as 'Transfer Manifest Verified' and ScreeningResult as 'Fail'
	And User clicks on the ContinueScreeningDetails button
	And User checks the AWB_Verified checkbox	
	And User saves all the details with ChargeType "<ChargeType>" and validates the popped up error message as "Blocked for screening"	
	

Examples:
	| AgentCode | ShipperCode | ConsigneeCode | Origin | Destination | ProductCode | SCC  | Commodity | ShipmentDescription | ServiceCargoClass | Piece | Weight | ChargeType | ModeOfPayment | cartType | Execute |
	| 11377     | 11377       | 11377         | SEA    | LAX         | GENERAL     | None | NONSCR    | None                | None              | 13    | 775    | CC         | None          | CART     | Yes     |
	| 11377     | 11377       | 11377         | SEA    | JFK         | PRIORITY    | None | 2199      | None                | None              | 8     | 360    | CC         | None          | CART     | Yes     |
	| 11377     | 11377       | 11377         | SAN    | JFK         | GOLDSTREAK  | None | 0316      | None                | None              | 2     | 55     | CC         | None          | CART     | Yes     |
