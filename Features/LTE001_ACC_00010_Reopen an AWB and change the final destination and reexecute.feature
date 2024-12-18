Feature: LTE001_ACC_00010_Reopen an AWB and change the final destination and reexecute
Create a New Shipment, Acceptance of that new shipment & screening as a CGO or CGODG user

@LTE001 @LTE001_ACC_00010
Scenario Outline: Reopen an AWB and change the final destination and reexecute
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
	And User selects an available flight
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
	When User enters the Executed AWB number
	And User Reopens the AWB
	And User verifies and Update the field 'destination' with updated value as "<UpdatedValue>" in the Shipment Details
	And User clicks on the ContinueShipment button
	And User verifies and Update the Flight Details with 'destination'
	And User clicks on the Select Flight Button
	And User selects an available flight
	And User clicks on the ContinueFlightDetails button
	And user opens the Charge Details
	And User clicks on the CalculateCharges button
	And User clicks on the ContinueChargeDetails button
	And User verifies and Update the Acceptance Details
	And User clicks on the ContinueAcceptanceDetails button
	And User verifies and Update the Screening Details
	And User clicks on the ContinueScreeningDetails button
	And User checks the AWB_Verified checkbox
	And User saves the details with capturing irregularity for flight destination change with ChargeType "<ChargeType>"
	And User validates the AWB is "EXECUTED"


Examples:
	| AgentCode | ShipperCode | ConsigneeCode | Origin | Destination | ProductCode | SCC  | Commodity | ShipmentDescription | ServiceCargoClass | Piece | Weight | ChargeType | ModeOfPayment | cartType | UpdatedValue | Execute |
	| 11377     | 11377       | 11377         | SEA    | JFK         | GENERAL     | None | NONSCR    | None                | None              | 2     | 59     | CC         | None          | CART     | PDX          | No      |
	| 11377     | 11377       | 11377         | SEA    | LAX         | PRIORITY    | None | 2199      | None                | None              | 36    | 259    | CC         | None          | CART     | JFK          | Yes     |

	
