Feature: LTE001_ACC_00003_Create a DG AWB in LTE001

Create a New DG Shipment, Acceptance & screening of that as a CGODG user

@LTE001 @LTE001_ACC_00003
Scenario Outline: Create a DG AWB in LTE001
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
	And User enters the Screening details for row 1 with screeingMethod as 'ALT Dangerous Goods' and ScreeningResult as 'Pass'
	And User clicks on the ContinueScreeningDetails button
	And User Save Shipment with DG Details & Capture Checksheet with ChargeType "<ChargeType>",UNID "<UNID>", ProperShipmentName "<ProperShipmentName>", PackingInstruction "<PackingInstruction>",NoOfPkg "<Piece>", NetQtyPerPkg "<NetQtyPerPkg>", ReportableQnty "<ReportableQnty>"
	

Examples:
	| AgentCode | ShipperCode | ConsigneeCode | Origin | Destination | ProductCode | SCC | Commodity | ShipmentDescription | ServiceCargoClass | Piece | Weight | ChargeType | ModeOfPayment | cartType | UNID | ProperShipmentName               | PackingInstruction | NetQtyPerPkg | ReportableQnty | Execute |
	| 11377     | 11377       | 11377         | SEA    | ANC         | PRIORITY    | DGR | NONSCR    | UN8000              | None              | 1     | 30     | PP         | CREDIT        | CART     | 8000 | Consumer commodity               | Y963               | 0.5          | No             | Yes     |
	| 11377     | 11377       | 11377         | SEA    | ANC         | PRIORITY    | DGR | NONSCR    | UN1845              | None              | 2     | 65     | PP         | CREDIT        | CART     | 1845 | Dry ice                          | 954                | 0.5          | No             | Yes     |
	| 11377     | 11377       | 11377         | SEA    | ANC         | PRIORITY    | DGR | NONSCR    | UN3110              | None              | 3     | 99     | PP         | CREDIT        | CART     | 3110 | Organic peroxide type F, solid * | 570                | 0.5          | No             | Yes     |

