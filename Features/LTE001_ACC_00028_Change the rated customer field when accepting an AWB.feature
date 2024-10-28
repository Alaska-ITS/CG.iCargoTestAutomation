Feature: LTE001_ACC_00028_Change the rated customer field when accepting an AWB
 
Create a New Shipment, Acceptance of that new shipment & screening as a CGO or CGODG user
 
@LTE001 @LTE001_ACC_00028
Scenario Outline: Change the rated customer field when accepting an AWB
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
	And User checks the ThirdParty checkbox and enters the RatedCustomer "<RatedCustomer>"
	And User clicks on the CalculateCharges button
	And User clicks on the ContinueChargeDetails button
	And User enters the Acceptance details
	And User clicks on the ContinueAcceptanceDetails button
	And User enters the Screening details for row 1 with screeingMethod as 'Transfer Manifest Verified' and ScreeningResult as 'Pass'
	And User clicks on the ContinueScreeningDetails button
	And User checks the AWB_Verified checkbox
	And User saves all the details & handles all the popups
	And User validates the CID under Account Info in paymentportal with the RatedCustomer "<RatedCustomer>"
	
 
Examples:
	| AgentCode | ShipperCode | ConsigneeCode | Origin | Destination | ProductCode | SCC  | Commodity | ShipmentDescription | ServiceCargoClass | Piece | Weight | ChargeType | RatedCustomer | ModeOfPayment | cartType | Execute |
	| 11377     | 11377       | 11377         | SEA    | DEN         | GENERAL     | None | NONSCR    | None                | None              | 13    | 775    | PP         | 10763         | CREDIT        | CART     | Yes     |
	| 11029     | 11029       | 10757         | SEA    | ANC         | PRIORITY    | None | 2300      | None                | None              | 5     | 250    | PP         | 10763         | CREDIT        | CART     | Yes     |
	
