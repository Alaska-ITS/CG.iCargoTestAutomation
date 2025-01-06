Feature: CAP018 Test Cases

@CAP018 @CAP018_BKG_00001 @CAP @DataSource:../TestData/CAP018_MaintainBooking_TestData.xlsx @DataSet:CAP018_BKG_00001
Scenario Outline: CAP018_BKG_00001_iCargo Login and Create New Shipment
	Given User wants to execute the example "<Execute>" for the scenario "<Tags>"
	When User switches station if BaseStation other than "<Origin>"
	When User enters screen name as 'CAP018'
	Then User enters into the  iCargo 'Maintain Booking' page successfully
	And User clicks on New/List button
	And User enters shipment details with Origin "<Origin>", Destination "<Destination>", Product Code "<ProductCode>" and Agent code "<AgentCode>"
	And User enters Shipper "<ShipperCode>" and Consignee "<ConsigneeCode>" details
	And User enters commodity details with Commodity "<Commodity>", Pieces "<Piece>", Weight "<Weight>"
	And User selects flight for "<ProductCode>"
	And User clicks on Save button

#	@CAP018_BKG_00002 @CAP018
#Scenario Outline: CAP018_BKG_00002_iCargo Login and Rebook an already executed AWB
#	Given User wants to execute the example "<Execute>"
#	When User enters screen name as 'CAP018'
#	Then User enters into the  iCargo 'Maintain Booking' page successfully
#	And User enters the AWB number as "<AWB>"
#	And User clicks on New/List button
#	And User deletes the flight details and adds new flight details
#	And User selects new carrier details
#	And User clicks on Save button to save new flight details
#	And User captures the irregularity details
#	Then User logs out from the application

	@CAP018 @CAP @CAP018_BKG_00003 @DataSource:../TestData/CAP018_MaintainBooking_TestData.xlsx @DataSet:CAP018_BKG_00001
Scenario Outline: CAP018_BKG_00003_Create a booking for an unknown shipper on a pax flight
	Given User wants to execute the example "<Execute>" for the scenario "<Tags>"
	When User switches station if BaseStation other than "<Origin>"
	When User enters screen name as 'CAP018'
	Then User enters into the  iCargo 'Maintain Booking' page successfully
	And User clicks on New/List button
	And User enters shipment details with Origin "<Origin>", Destination "<Destination>",Agent Code "<AgentCode>", Product Code "<ProductCode>"
	And User enters Unknown Shipper "<ShipperCode>" and Consignee "<ConsigneeCode>" with all details
	And User enters commodity details with Commodity "<Commodity>", Pieces "<Piece>", Weight "<Weight>"
	And User selects flight for "<ProductCode>"
	And User clicks on Save button

#	@CAP018 @CAP018_BKG_00004 @DataSource:../TestData/CAP018_MaintainBooking_TestData.xlsx @DataSet:CAP018_BKG_00004
#Scenario Outline: CAP018_BKG_00004_Create a booking given an AWB from stock and system will create a new awb
#	Given User wants to execute the example "<Execute>"
#	When User switches station if BaseStation other than "<Origin>"
#	When User enters screen name as 'CAP018'
#	Then User enters into the  iCargo 'Maintain Booking' page successfully
#	And User enters the AWB number as "<AWB>"
#	And User clicks on New/List button
#	And a banner appears for the awb does not exist
#	And User enters shipment details with Origin "<Origin>", Destination "<Destination>", Product Code "<ProductCode>" and Agent code "<AgentCode>"
#	And User enters Unknown Shipper "<ShipperCode>" and Consignee "<ConsigneeCode>" details
#	And User enters commodity details with Commodity "<Commodity>", Pieces "<Piece>", Weight "<Weight>"
#	And User selects flight for "<ProductCode>"
#	And User clicks on Save button

#	@CAP018 @CAP018_BKG_00005 @DataSource:../TestData/CAP018_MaintainBooking_TestData.xlsx @DataSet:CAP018_BKG_00005
#Scenario Outline: CAP018_BKG_00005_Create a multi leg booking with flights that do not meet minimum connection time and system should display a warning message
#	Given User wants to execute the example "<Execute>"
#	When User switches station if BaseStation other than "<Origin>"
#	When User enters screen name as 'CAP018'
#	Then User enters into the  iCargo 'Maintain Booking' page successfully
#	And User clicks on New/List button
#	And User enters shipment details with Origin "<Origin>", Destination "<Destination>", Product Code "<ProductCode>" and Agent code "<AgentCode>"
#	And User enters Shipper "<ShipperCode>" and Consignee "<ConsigneeCode>" details
#	And User enters commodity details with Commodity "<Commodity>", Pieces "<Piece>", Weight "<Weight>"
#	And User searches for the multileg flight to verify RES bubble 'red' a warning message as 'Minimum Handling / Connection Time Fails' and product code as "<ProductCode>"

#	@CAP018 @CAP018_BKG_00006
#Scenario Outline: CAP018_BKG_00006_Create a booking for a single piece over 300lbs on an OO E175 and system generates an AWB with a warning.
#	Given User lauches the Url of iCargo Staging UI
#	Then User enters into the  iCargo 'Sign in to icargoas' page successfully
#	When User clicks on the oidc button
#	Then A new window is opened
#	And User enters into the  iCargo 'Home' page successfully
#	When User enters screen name as 'CAP018'
#	Then User enters into the  iCargo 'Maintain Booking' page successfully
#	And User clicks on New/List button
#	And User enters shipment details with Origin "<Origin>", Destination "<Destination>", Product Code "<ProductCode>" and Agent code "<AgentCode>"
#	And User enters Shipper "<ShipperCode>" and Consignee "<ConsigneeCode>" details
#	And User enters commodity details with Commodity "<Commodity>", Pieces "<Piece>", Weight "<Weight>"
#	And User selects flight for "<ProductCode>"
#	And User clicks on Save button
#	And An an Embargo pops up with a warning message to generate new AWB
#	Then User logs out from the application
#
	@CAP018 @CAP @CAP018_BKG_00007 @DataSource:../TestData/CAP018_MaintainBooking_TestData.xlsx @DataSet:CAP018_BKG_00001
Scenario Outline: CAP018_BKG_00007_iCargo Login and Create New AVI Shipment
	Given User wants to execute the example "<Execute>" for the scenario "<Tags>"
	When User switches station if BaseStation other than "<Origin>"
	When User enters screen name as 'CAP018'
	Then User enters into the  iCargo 'Maintain Booking' page successfully
	And User clicks on New/List button
	And User enters shipment details with Origin "<Origin>", Destination "<Destination>", Product Code "<ProductCode>" and Agent code "<AgentCode>"
	And User enters Shipper "<ShipperCode>" and Consignee "<ConsigneeCode>" details
	And User enters commodity details with Commodity "<Commodity>", Pieces "<Piece>", Weight "<Weight>"
	And User selects flight for "<ProductCode>"
	And User clicks on Save button and fills the checksheet details to generate awb

#	@CAP018 @CAP018_BKG_00008 @DataSource:../TestData/CAP018_MaintainBooking_TestData.xlsx @DataSet:CAP018_BKG_00008
#Scenario Outline: CAP018_BKG_00008_iCargo Login and Create New Shipment from saved booking
#	Given User wants to execute the example "<Execute>"
#	When User switches station if BaseStation other than "<Origin>"
#	When User enters screen name as 'CAP018'
#	Then User enters into the  iCargo 'Maintain Booking' page successfully
#	And User clicks on New/List button
#	And User enters shipment details with Origin "<Origin>", Destination "<Destination>", Product Code "<ProductCode>" and Agent code "<AgentCode>"
#	And User enters Shipper "<ShipperCode>" and Consignee "<ConsigneeCode>" details
#	And User enters commodity details with Commodity "<Commodity>", Pieces "<Piece>", Weight "<Weight>"
#	And User selects flight for "<ProductCode>"
#	And User clicks on Save button
#	And User enters the AWB number
#	And User clicks on New/List button
#	And User clicks on Attach/Detach button
#	And User enters new Agent Code 
#	And User clicks on Save button
#
	@CAP018 @CAP018_BKG_00009 @DataSource:../TestData/CAP018_MaintainBooking_TestData.xlsx @DataSet:CAP018_BKG_00001
Scenario Outline: CAP018_BKG_00009_Save a template from a booking
	Given User wants to execute the example "<Execute>" for the scenario "<Tags>"
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
	And User clicks on Select/Save Template to save the template
#
#	@CAP018_BKG_00010 @CAP018
#Scenario: CAP018_BKG_00010_Save a template and create a booking
#	Given User wants to execute the example "<Execute>"	
#	When User enters screen name as 'CAP018'
#	Then User enters into the  iCargo 'Maintain Booking' page successfully
#	And User clicks on New/List button
#	And User clicks on Select/Save Template	
#	And User clicks on Save button
