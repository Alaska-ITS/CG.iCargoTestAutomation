Feature: LTE001_ACC_00015_Create an AWB for a CAO DG shipment and book on a pax flight

Create a New DG Shipment, Acceptance & screening of that as a CGODG user

@LTE001 @LTE001_ACC_00015
Scenario Outline: Create an AWB for a CAO DG shipment and book on a pax flight
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
	And User selects an "Combination" flight having Embargo error
	And User clicks on the ContinueFlightDetails button
	And User enters the Charge details with ChargeType "<ChargeType>" and ModeOfPayment "<ModeOfPayment>"
	And User clicks on the CalculateCharges button
	And User clicks on the ContinueChargeDetails button
	And User enters the Acceptance details
	And User clicks on the ContinueAcceptanceDetails button
	And User enters the Screening details for row 1 with screeingMethod as 'ALT Dangerous Goods' and ScreeningResult as 'Pass'
	And User clicks on the ContinueScreeningDetails button
	And User clicks on the save button	
	And User validates the error message "SPECIFIED SHIPMENT TYPE NOT SUITED FOR SELECTED AIRCRAFT TYPE" in the Embargo Details popup
	

Examples:
	| AgentCode | ShipperCode | ConsigneeCode | Origin | Destination | ProductCode | SCC     | Commodity | ShipmentDescription | ServiceCargoClass | Piece | Weight | ChargeType | ModeOfPayment | cartType | UNID | ProperShipmentName         | PackingInstruction | NetQtyPerPkg | ReportableQnty | Execute |
	| 11377     | 11377       | 11377         | SEA    | ANC         | PRIORITY    | DGR,CAO | NONSCR    | UN1170              | None              | 1     | 35     | CC         | None          | CART     | 1170 | Ethanol                    | 364                | 0.5          | No             | Yes     |
	| 11377     | 11377       | 11377         | SEA    | ANC         | PRIORITY    | DGR,CAO | NONSCR    | UN3480              | None              | 76    | 456    | CC         | None          | CART     | 3090 | Lithium metal batteries    | 968                | 0.5          | No             | No      |
	| 11377     | 11377       | 11377         | SEA    | ANC         | PRIORITY    | DGR,CAO | NONSCR    | UN3480              | None              | 56    | 230    | CC         | None          | CART     | 1075 | Petroleum gases, liquefied | 200                | 0.5          | No             | No      |

