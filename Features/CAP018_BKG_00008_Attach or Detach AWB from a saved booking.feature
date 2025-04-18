﻿Feature: CAP018_BKG_00008_Attach or Detach AWB from a saved booking

@CAP018 @CAP018_BKG_00008
Scenario Outline: iCargo Login and Create New Shipment
	Given User wants to execute the example "<Execute>"
	When User switches station if BaseStation other than "<Origin>"
	When User enters screen name as 'CAP018'
	Then User enters into the  iCargo 'Maintain Booking' page successfully
	And User clicks on New/List button
	And User enters shipment details with Origin "<Origin>", Destination "<Destination>", Product Code "<ProductCode>" and Agent code "<Agent Code>"
	And User enters Shipper "<Shipper Code>" and Consignee "<Consignee Code>" details
	And User enters commodity details with Commodity "<Commodity>", Pieces "<Piece>", Weight "<Weight>"
	And User selects flight for "<ProductCode>"
	And User clicks on Save button
	And User enters the AWB number
	And User clicks on New/List button
	And User clicks on Attach/Detach button
	And User enters new Agent Code "<New Agent Code>"
	And User clicks on Save button
Examples:
	| Origin | Destination | ProductCode | Commodity | Piece | Weight | Execute | Agent Code | Shipper Code | Consignee Code | New Agent Code |
	| ANC    | SEA         | PRIORITY    | 0316      | 2     | 20     | Yes     | 10763      | 10763        | 10763          | ASQXGUEST      |																