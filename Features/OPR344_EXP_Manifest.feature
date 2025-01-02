Feature: OPR344_EXP_Manifest

@OPR344 @OPR344_EXP_00001 @DataSource:../TestData/OPR344_ExportManifest_TestData.xlsx @DataSet:OPR344_EXP_00001
Scenario Outline: OPR344_EXP_00001_Manifest an AWB for an Unknown Shipper on a pax flight
	Given User wants to execute the example "<Execute>"
	When User switches station if BaseStation other than "<Origin>"
	And User enters the screen name as 'LTE001'
	Then User enters into the  iCargo 'Create Shipment' page successfully
	When user clicks on the List button
	And User enters the Participant details with AgentCode "<AgentCode>",Unknown ShipperCode "<UnknownShipperCode>", ConsigneeCode "<ConsigneeCode>"
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
	And User saves the shipment details validate error message as "The Shipper does not have a Valid Certificate Type" and capture AWB number
	When User enters the screen name as 'OPR344'
	Then User enters into the  iCargo 'Export Manifest' page successfully
	When User enters the Booked FlightNumber with ""
	And User enters Booked ShipmentDate
	And User clicks on the List button to fetch the Booked Shipment
	And User creates new ULD/Cart in Assigned Shipment with cartType "<cartType>" and pou "<Destination>"
	And User filterouts the Booked AWB from '<AWBSectionName>' and Created ULD_Cart
	And User validates the error popover message as "AWB is not accepted"
	Then User closes the Export Manifest screen

#@OPR344 @OPR344_EXP_00002 @DataSource:../TestData/OPR344_ExportManifest_TestData.xlsx @DataSet:OPR344_EXP_00002
#Scenario Outline: OPR344_EXP_00002_Manifest an AWB from the lying list
#	Given User wants to execute the example "<Execute>"
#	When User switches station if BaseStation other than "<Origin>"
#	And User enters the screen name as 'LTE001'
#	Then User enters into the  iCargo 'Create Shipment' page successfully
#	When user clicks on the List button
#	And User enters the Participant details with AgentCode "<AgentCode>", ShipperCode "<ShipperCode>", ConsigneeCode "<ConsigneeCode>"
#	And User clicks on the ContinueParticipant button
#	And User enters the Certificate details
#	And User clicks on the ContinueCertificate button
#	And User enters the Shipment details with Origin "<Origin>", Destination "<Destination>", ProductCode "<ProductCode>", SCCCode "<SCC>", Commodity "<Commodity>", ShipmentDescription"<ShipmentDescription>", ServiceCargoClass "<ServiceCargoClass>", Piece "<Piece>", Weight "<Weight>"
#	And User clicks on the ContinueShipment button
#	And User clicks on the Select Flight Button
#	And User selects an "Combination" flight
#	And User clicks on the ContinueFlightDetails button
#	And User enters the Charge details with ChargeType "<ChargeType>" and ModeOfPayment "<ModeOfPayment>"
#	And User clicks on the CalculateCharges button
#	And User clicks on the ContinueChargeDetails button
#	And User enters the Acceptance details
#	And User clicks on the ContinueAcceptanceDetails button
#	And User enters the Screening details for row 1 with screeingMethod as 'Transfer Manifest Verified' and ScreeningResult as 'Pass'
#	And User clicks on the ContinueScreeningDetails button
#	And User checks the AWB_Verified checkbox
#	And User saves all the details & handles all the popups
#	When User enters the screen name as 'OPR344'
#	Then User enters into the  iCargo 'Export Manifest' page successfully
#	When User enters the Booked FlightNumber with "<FlightNumber>"
#	And User enters Booked ShipmentDate
#	And User clicks on the List button to fetch the Booked Shipment
#	And User creates new ULD/Cart in Assigned Shipment with cartType "<cartType>" and pou "<Destination>"
#	And User filterouts the Booked AWB from '<AWBSectionName>' and Created ULD_Cart
#	And User handles any pop up error message
#	And User validates the warning message "The shipment is not booked to the flight"
#	And User clicks on the Manifest button
#	And User closes the PrintPDF window
#	And User validates the AWB is "Manifested" in the Export Manifest screen
#	Then User closes the Export Manifest screen
#
#@OPR344 @OPR344_EXP_00003 @DataSource:../TestData/OPR344_ExportManifest_TestData.xlsx @DataSet:OPR344_EXP_00003
#Scenario Outline: OPR344_EXP_00003_Manifest an AWB onto its booked flight
#	Given User wants to execute the example "<Execute>"
#	When User switches station if BaseStation other than "<Origin>"
#	And User enters the screen name as 'LTE001'
#	Then User enters into the  iCargo 'Create Shipment' page successfully
#	When user clicks on the List button
#	And User enters the Participant details with AgentCode "<AgentCode>", ShipperCode "<ShipperCode>", ConsigneeCode "<ConsigneeCode>"
#	And User clicks on the ContinueParticipant button
#	And User enters the Certificate details
#	And User clicks on the ContinueCertificate button
#	And User enters the Shipment details with Origin "<Origin>", Destination "<Destination>", ProductCode "<ProductCode>", SCCCode "<SCC>", Commodity "<Commodity>", ShipmentDescription"<ShipmentDescription>", ServiceCargoClass "<ServiceCargoClass>", Piece "<Piece>", Weight "<Weight>"
#	And User clicks on the ContinueShipment button
#	And User clicks on the Select Flight Button
#	And User selects an "Combination" flight
#	And User clicks on the ContinueFlightDetails button
#	And User enters the Charge details with ChargeType "<ChargeType>" and ModeOfPayment "<ModeOfPayment>"
#	And User clicks on the CalculateCharges button
#	And User clicks on the ContinueChargeDetails button
#	And User enters the Acceptance details
#	And User clicks on the ContinueAcceptanceDetails button
#	And User enters the Screening details for row 1 with screeingMethod as 'Transfer Manifest Verified' and ScreeningResult as 'Pass'
#	And User clicks on the ContinueScreeningDetails button
#	And User checks the AWB_Verified checkbox
#	And User saves all the details & handles all the popups
#	When User enters the screen name as 'OPR344'
#	Then User enters into the  iCargo 'Export Manifest' page successfully
#	When User enters the Booked FlightNumber with ""
#	And User enters Booked ShipmentDate
#	And User clicks on the List button to fetch the Booked Shipment
#	And User creates new ULD/Cart in Assigned Shipment with cartType "<cartType>" and pou "<Destination>"
#	And User filterouts the Booked AWB from '<AWBSectionName>' and Created ULD_Cart
#	And User clicks on the Manifest button
#	And User closes the PrintPDF window
#	And User validates the AWB is "Manifested" in the Export Manifest screen
#	Then User closes the Export Manifest screen
#
#@OPR344 @OPR344_EXP_00004 @DataSource:../TestData/OPR344_ExportManifest_TestData.xlsx @DataSet:OPR344_EXP_00004 
#Scenario Outline: OPR344_EXP_00004_Split an AWB in OPR344 and manifest split pieces to a flight
#	Given User wants to execute the example "<Execute>"
#	When User switches station if BaseStation other than "<Origin>"
#	And User enters the screen name as 'LTE001'
#	Then User enters into the  iCargo 'Create Shipment' page successfully
#	When user clicks on the List button
#	And User enters the Participant details with AgentCode "<AgentCode>", ShipperCode "<ShipperCode>", ConsigneeCode "<ConsigneeCode>"
#	And User clicks on the ContinueParticipant button
#	And User enters the Certificate details
#	And User clicks on the ContinueCertificate button
#	And User enters the Shipment details with Origin "<Origin>", Destination "<Destination>", ProductCode "<ProductCode>", SCCCode "<SCC>", Commodity "<Commodity>", ShipmentDescription"<ShipmentDescription>", ServiceCargoClass "<ServiceCargoClass>", Piece "<Piece>", Weight "<Weight>"
#	And User clicks on the ContinueShipment button
#	And User clicks on the Select Flight Button
#	And User selects an "Combination" flight
#	And User clicks on the ContinueFlightDetails button
#	And User enters the Charge details with ChargeType "<ChargeType>" and ModeOfPayment "<ModeOfPayment>"
#	And User clicks on the CalculateCharges button
#	And User clicks on the ContinueChargeDetails button
#	And User enters the Acceptance details
#	And User clicks on the ContinueAcceptanceDetails button
#	And User enters the Screening details for row 1 with screeingMethod as 'Transfer Manifest Verified' and ScreeningResult as 'Pass'
#	And User clicks on the ContinueScreeningDetails button
#	And User checks the AWB_Verified checkbox
#	And User saves all the details & handles all the popups
#	When User enters the screen name as 'OPR344'
#	Then User enters into the  iCargo 'Export Manifest' page successfully
#	When User enters the Booked FlightNumber with ""
#	And User enters Booked ShipmentDate
#	And User clicks on the List button to fetch the Booked Shipment
#	And User creates new ULD/Cart in Assigned Shipment with cartType "<cartType>" and pou "<Destination>"
#	And User filterouts the Booked AWB from '<AWBSectionName>' Split And Assign with Pieces "<SplitPieces>"
#	And User clicks on the Manifest button
#	And User closes the PrintPDF window
#	And User validates the AWB is "Manifested" in the Export Manifest screen
#	Then User closes the Export Manifest screen
#
#@OPR344 @OPR344_EXP_00005 @DataSource:../TestData/OPR344_ExportManifest_TestData.xlsx @DataSet:OPR344_EXP_00005
#Scenario Outline: OPR344_EXP_00005_Offload manifested cargo to another flight
#	Given User wants to execute the example "<Execute>"
#	When User switches station if BaseStation other than "<Origin>"
#	And User enters the screen name as 'LTE001'
#	Then User enters into the  iCargo 'Create Shipment' page successfully
#	When user clicks on the List button
#	And User enters the Participant details with AgentCode "<AgentCode>", ShipperCode "<ShipperCode>", ConsigneeCode "<ConsigneeCode>"
#	And User clicks on the ContinueParticipant button
#	And User enters the Certificate details
#	And User clicks on the ContinueCertificate button
#	And User enters the Shipment details with Origin "<Origin>", Destination "<Destination>", ProductCode "<ProductCode>", SCCCode "<SCC>", Commodity "<Commodity>", ShipmentDescription"<ShipmentDescription>", ServiceCargoClass "<ServiceCargoClass>", Piece "<Piece>", Weight "<Weight>"
#	And User clicks on the ContinueShipment button
#	And User clicks on the Select Flight Button
#	And User selects an "Combination" flight
#	And User clicks on the ContinueFlightDetails button
#	And User enters the Charge details with ChargeType "<ChargeType>" and ModeOfPayment "<ModeOfPayment>"
#	And User clicks on the CalculateCharges button
#	And User clicks on the ContinueChargeDetails button
#	And User enters the Acceptance details
#	And User clicks on the ContinueAcceptanceDetails button
#	And User enters the Screening details for row 1 with screeingMethod as 'Transfer Manifest Verified' and ScreeningResult as 'Pass'
#	And User clicks on the ContinueScreeningDetails button
#	And User checks the AWB_Verified checkbox
#	And User saves all the details & handles all the popups
#	When User enters the screen name as 'OPR344'
#	Then User enters into the  iCargo 'Export Manifest' page successfully
#	When User enters the Booked FlightNumber with ""
#	And User enters Booked ShipmentDate
#	And User clicks on the List button to fetch the Booked Shipment
#	And User creates new ULD/Cart in Assigned Shipment with cartType "<cartType>" and pou "<Destination>"
#	And User filterouts the Booked AWB from '<AWBSectionName>' and Created ULD_Cart
#	And User clicks on the Manifest button
#	And User closes the PrintPDF window
#	And User expands the cart/uld to check the awb
#	And User cliks on the offload button to open the offload popup
#	And User enters the details to move to another NewFlightNumber "<NewFlightNumber>" and POU "<Destination>" in the 'AWB' offload popup
#	And User validates the warning message "The shipment is not booked to the flight"
#	And User validates the AWB is "Offloaded" in the Export Manifest screen
#	And User clicks on the orange pencil to edit the manifest
#	When User enters the new flight number "<NewFlightNumber>" to move the offloaded shipment
#	And User enters Booked ShipmentDate
#	And User clicks on the List button to fetch the Booked Shipment
#	And User clicks on the Manifest button
#	And User closes the PrintPDF window
#	Then User closes the Export Manifest screen
#
#@OPR344 @OPR344_EXP_00006 @DataSource:../TestData/OPR344_ExportManifest_TestData.xlsx @DataSet:OPR344_EXP_00006
#Scenario Outline: OPR344_EXP_00006_Manifest DG on a thru flight
#	Given User wants to execute the example "<Execute>"
#	When User switches station if BaseStation other than "<Origin>"
#	And User enters the screen name as 'LTE001'
#	Then User enters into the  iCargo 'Create Shipment' page successfully
#	When user clicks on the List button
#	And User enters the Participant details with AgentCode "<AgentCode>", ShipperCode "<ShipperCode>", ConsigneeCode "<ConsigneeCode>"
#	And User clicks on the ContinueParticipant button
#	And User enters the Certificate details
#	And User clicks on the ContinueCertificate button
#	And User enters the Shipment details with Origin "<Origin>", Destination "<Destination>", ProductCode "<ProductCode>", SCCCode "<SCC>", Commodity "<Commodity>", ShipmentDescription"<ShipmentDescription>", ServiceCargoClass "<ServiceCargoClass>", Piece "<Piece>", Weight "<Weight>"
#	And User clicks on the ContinueShipment button
#	And User clicks on the Select Flight Button
#	And User selects an available flight
#	And User clicks on the ContinueFlightDetails button
#	And User enters the Charge details with ChargeType "<ChargeType>" and ModeOfPayment "<ModeOfPayment>"
#	And User clicks on the CalculateCharges button
#	And User clicks on the ContinueChargeDetails button
#	And User enters the Acceptance details
#	And User clicks on the ContinueAcceptanceDetails button
#	And User enters the Screening details for row 1 with screeingMethod as 'ALT Dangerous Goods' and ScreeningResult as 'Pass'
#	And User clicks on the ContinueScreeningDetails button
#	And User Save Shipment with DG Details & Capture Checksheet with ChargeType "<ChargeType>",UNID "<UNID>", ProperShipmentName "<ProperShipmentName>", PackingInstruction "<PackingInstruction>",NoOfPkg "<Piece>", NetQtyPerPkg "<NetQtyPerPkg>", ReportableQnty "<ReportableQnty>"
#	When User enters the screen name as 'OPR344'
#	Then User enters into the  iCargo 'Export Manifest' page successfully
#	When User enters the Booked FlightNumber with ""
#	And User enters Booked ShipmentDate
#	And User clicks on the List button to fetch the Booked Shipment
#	And User creates new ULD/Cart in Assigned Shipment with cartType "<cartType>" and pou "<Destination>"
#	And User filterouts the Booked AWB from '<AWBSectionName>' and Created ULD_Cart
#	And User clicks on the Manifest button
#	And User closes the PrintPDF window
#	And User validates the AWB is "Manifested" in the Export Manifest screen
#	Then User closes the Export Manifest screen
#
#@OPR344 @OPR344_EXP_00010 @DataSource:../TestData/OPR344_ExportManifest_TestData.xlsx @DataSet:OPR344_EXP_00010
#Scenario Outline: OPR344_EXP_00010_Manifest an AWB with no screening details to a freighter
#	Given User wants to execute the example "<Execute>"
#	When User switches station if BaseStation other than "<Origin>"
#	And User enters the screen name as 'LTE001'
#	Then User enters into the  iCargo 'Create Shipment' page successfully
#	When user clicks on the List button
#	And User enters the Participant details with AgentCode "<AgentCode>", ShipperCode "<ShipperCode>", ConsigneeCode "<ConsigneeCode>"
#	And User clicks on the ContinueParticipant button
#	And User enters the Certificate details
#	And User clicks on the ContinueCertificate button
#	And User enters the Shipment details with Origin "<Origin>", Destination "<Destination>", ProductCode "<ProductCode>", SCCCode "<SCC>", Commodity "<Commodity>", ShipmentDescription"<ShipmentDescription>", ServiceCargoClass "<ServiceCargoClass>", Piece "<Piece>", Weight "<Weight>"
#	And User clicks on the ContinueShipment button
#	And User clicks on the Select Flight Button
#	And User selects an "Cargo-Only" flight
#	And User clicks on the ContinueFlightDetails button
#	And User enters the Charge details with ChargeType "<ChargeType>" and ModeOfPayment "<ModeOfPayment>"
#	And User clicks on the CalculateCharges button
#	And User clicks on the ContinueChargeDetails button
#	And User enters the Acceptance details
#	And User clicks on the ContinueAcceptanceDetails button
#	And User clicks on the ContinueScreeningDetails button
#	And User checks the AWB_Verified checkbox
#	And User saves all the details & handles all the popups
#	When User enters the screen name as 'OPR344'
#	Then User enters into the  iCargo 'Export Manifest' page successfully
#	When User enters the Booked FlightNumber with ""
#	And User enters Booked ShipmentDate
#	And User clicks on the List button to fetch the Booked Shipment
#	And User creates new ULD/Cart in Assigned Shipment with cartType "<cartType>" and pou "<Destination>"
#	And User filterouts the Booked AWB from '<AWBSectionName>' and Created ULD_Cart
#	And User clicks on the Manifest button
#	And User closes the PrintPDF window
#	And User validates the AWB is "Manifested" in the Export Manifest screen
#	Then User closes the Export Manifest screen
#
#@OPR344 @OPR344_EXP_00011 @DataSource:../TestData/OPR344_ExportManifest_TestData.xlsx @DataSet:OPR344_EXP_00011
#Scenario Outline: OPR344_EXP_00011_Manifest an AWB that has not been screened and came inbound via freighter to a freighter
#	Given User wants to execute the example "<Execute>"
#	When User switches station if BaseStation other than "<Origin>"
#	And User enters the screen name as 'LTE001'
#	Then User enters into the  iCargo 'Create Shipment' page successfully
#	When user clicks on the List button
#	And User enters the Participant details with AgentCode "<AgentCode>", ShipperCode "<ShipperCode>", ConsigneeCode "<ConsigneeCode>"
#	And User clicks on the ContinueParticipant button
#	And User enters the Certificate details
#	And User clicks on the ContinueCertificate button
#	And User enters the Shipment details with Origin "<Origin>", Destination "<Destination>", ProductCode "<ProductCode>", SCCCode "<SCC>", Commodity "<Commodity>", ShipmentDescription"<ShipmentDescription>", ServiceCargoClass "<ServiceCargoClass>", Piece "<Piece>", Weight "<Weight>"
#	And User clicks on the ContinueShipment button
#	And User clicks on the Select Flight Button
#	And User selects an "Cargo-Only" flight
#	And User clicks on the ContinueFlightDetails button
#	And User enters the Charge details with ChargeType "<ChargeType>" and ModeOfPayment "<ModeOfPayment>"
#	And User clicks on the CalculateCharges button
#	And User clicks on the ContinueChargeDetails button
#	And User enters the Acceptance details
#	And User clicks on the ContinueAcceptanceDetails button
#	And User clicks on the ContinueScreeningDetails button
#	And User checks the AWB_Verified checkbox
#	And User saves all the details & handles all the popups
#	When User enters the screen name as 'OPR344'
#	Then User enters into the  iCargo 'Export Manifest' page successfully
#	When User enters the Booked FlightNumber with ""
#	And User enters Booked ShipmentDate
#	And User clicks on the List button to fetch the Booked Shipment
#	And User creates new ULD/Cart in Assigned Shipment with cartType "<cartType>" and pou "<Destination>"
#	And User filterouts the Booked AWB from '<AWBSectionName>' and Created ULD_Cart
#	And User clicks on the Manifest button
#	And User closes the PrintPDF window
#	And User validates the AWB is "Manifested" in the Export Manifest screen
#	Then User closes the Export Manifest screen
#
#@OPR344 @OPR344_EXP_00012 @DataSource:../TestData/OPR344_ExportManifest_TestData.xlsx @DataSet:OPR344_EXP_00012
#Scenario Outline: OPR344_EXP_00012_Manifest an AWB that has not been screened and came inbound via freighter to a PAX flight
#	Given User wants to execute the example "<Execute>"
#	When User switches station if BaseStation other than "<Origin>"
#	And User enters the screen name as 'LTE001'
#	Then User enters into the  iCargo 'Create Shipment' page successfully
#	When user clicks on the List button
#	And User enters the Participant details with AgentCode "<AgentCode>", ShipperCode "<ShipperCode>", ConsigneeCode "<ConsigneeCode>"
#	And User clicks on the ContinueParticipant button
#	And User enters the Certificate details
#	And User clicks on the ContinueCertificate button
#	And User enters the Shipment details with Origin "<Origin>", Destination "<Destination>", ProductCode "<ProductCode>", SCCCode "<SCC>", Commodity "<Commodity>", ShipmentDescription"<ShipmentDescription>", ServiceCargoClass "<ServiceCargoClass>", Piece "<Piece>", Weight "<Weight>"
#	And User clicks on the ContinueShipment button
#	And User clicks on the Select Flight Button
#	And User selects an "Cargo-Only" flight
#	And User clicks on the ContinueFlightDetails button
#	And User enters the Charge details with ChargeType "<ChargeType>" and ModeOfPayment "<ModeOfPayment>"
#	And User clicks on the CalculateCharges button
#	And User clicks on the ContinueChargeDetails button
#	And User enters the Acceptance details
#	And User clicks on the ContinueAcceptanceDetails button
#	And User clicks on the ContinueScreeningDetails button
#	And User checks the AWB_Verified checkbox
#	And User saves all the details & handles all the popups
#	When User enters the screen name as 'OPR344'
#	Then User enters into the  iCargo 'Export Manifest' page successfully
#	When User enters the Booked FlightNumber with "<FlightNumber>"
#	And User enters Booked ShipmentDate
#	And User clicks on the List button to fetch the Booked Shipment
#	And User creates new ULD/Cart in Assigned Shipment with cartType "<cartType>" and pou "<Destination>"
#	And User filterouts the Booked AWB from '<AWBSectionName>' and Created ULD_Cart
#	And User validates the error message 'SCREENING MUST BE COMPLETED FOR MOVEMENT ON PAX AIRCRAFT'
#	Then User closes the Export Manifest screen
#
#@OPR344 @OPR344_EXP_00013 @DataSource:../TestData/OPR344_ExportManifest_TestData.xlsx @DataSet:OPR344_EXP_00013
#Scenario Outline: OPR344_EXP_00013_Assign an AWB to a cart by typing in the AWB number when building the cart
#	Given User wants to execute the example "<Execute>"
#	When User switches station if BaseStation other than "<Origin>"
#	And User enters the screen name as 'LTE001'
#	Then User enters into the  iCargo 'Create Shipment' page successfully
#	When user clicks on the List button
#	And User enters the Participant details with AgentCode "<AgentCode>", ShipperCode "<ShipperCode>", ConsigneeCode "<ConsigneeCode>"
#	And User clicks on the ContinueParticipant button
#	And User enters the Certificate details
#	And User clicks on the ContinueCertificate button
#	And User enters the Shipment details with Origin "<Origin>", Destination "<Destination>", ProductCode "<ProductCode>", SCCCode "<SCC>", Commodity "<Commodity>", ShipmentDescription"<ShipmentDescription>", ServiceCargoClass "<ServiceCargoClass>", Piece "<Piece>", Weight "<Weight>"
#	And User clicks on the ContinueShipment button
#	And User clicks on the Select Flight Button
#	And User selects an "Combination" flight
#	And User clicks on the ContinueFlightDetails button
#	And User enters the Charge details with ChargeType "<ChargeType>" and ModeOfPayment "<ModeOfPayment>"
#	And User clicks on the CalculateCharges button
#	And User clicks on the ContinueChargeDetails button
#	And User enters the Acceptance details
#	And User clicks on the ContinueAcceptanceDetails button
#	And User enters the Screening details for row 1 with screeingMethod as 'Transfer Manifest Verified' and ScreeningResult as 'Pass'
#	And User clicks on the ContinueScreeningDetails button
#	And User checks the AWB_Verified checkbox
#	And User saves all the details & handles all the popups
#	When User enters the screen name as 'OPR344'
#	Then User enters into the  iCargo 'Export Manifest' page successfully
#	When User enters the Booked FlightNumber with ""
#	And User enters Booked ShipmentDate
#	And User clicks on the List button to fetch the Booked Shipment
#	And User creates new ULD/Cart in Assigned Shipment with with cartType "<cartType>" and pou "<Destination>" and the AWB Number typed in with piece "<Piece>"
#	And User clicks on the Manifest button
#	And User closes the PrintPDF window
#	And User validates the AWB is "Manifested" in the Export Manifest screen
#	Then User closes the Export Manifest screen
#
#@OPR344 @OPR344_EXP_00014 @DataSource:../TestData/OPR344_ExportManifest_TestData.xlsx @DataSet:OPR344_EXP_00014
#Scenario Outline: OPR344_EXP_00014_Assign an AWB to a pre built cart by typing in the AWB number
#	Given User wants to execute the example "<Execute>"
#	When User switches station if BaseStation other than "<Origin>"
#	And User enters the screen name as 'LTE001'
#	Then User enters into the  iCargo 'Create Shipment' page successfully
#	When user clicks on the List button
#	And User enters the Participant details with AgentCode "<AgentCode>", ShipperCode "<ShipperCode>", ConsigneeCode "<ConsigneeCode>"
#	And User clicks on the ContinueParticipant button
#	And User enters the Certificate details
#	And User clicks on the ContinueCertificate button
#	And User enters the Shipment details with Origin "<Origin>", Destination "<Destination>", ProductCode "<ProductCode>", SCCCode "<SCC>", Commodity "<Commodity>", ShipmentDescription"<ShipmentDescription>", ServiceCargoClass "<ServiceCargoClass>", Piece "<Piece>", Weight "<Weight>"
#	And User clicks on the ContinueShipment button
#	And User clicks on the Select Flight Button
#	And User selects an "Combination" flight
#	And User clicks on the ContinueFlightDetails button
#	And User enters the Charge details with ChargeType "<ChargeType>" and ModeOfPayment "<ModeOfPayment>"
#	And User clicks on the CalculateCharges button
#	And User clicks on the ContinueChargeDetails button
#	And User enters the Acceptance details
#	And User clicks on the ContinueAcceptanceDetails button
#	And User enters the Screening details for row 1 with screeingMethod as 'Transfer Manifest Verified' and ScreeningResult as 'Pass'
#	And User clicks on the ContinueScreeningDetails button
#	And User checks the AWB_Verified checkbox
#	And User saves all the details & handles all the popups
#	When User enters the screen name as 'OPR344'
#	Then User enters into the  iCargo 'Export Manifest' page successfully
#	When User enters the Booked FlightNumber with ""
#	And User enters Booked ShipmentDate
#	And User clicks on the List button to fetch the Booked Shipment
#	And User creates new ULD/Cart in Assigned Shipment with cartType "<cartType>" and pou "<Destination>"
#	And User clicks on the Edit ULD button for the pre-build ULD/Cart
#	And User types in the AWB number and pieces "<Piece>" inside the pre-built ULD/Cart
#	And User clicks on the Manifest button
#	And User closes the PrintPDF window
#	And User validates the AWB is "Manifested" in the Export Manifest screen
#	Then User closes the Export Manifest screen
#
#@OPR344 @OPR344_EXP_00015 @DataSource:../TestData/OPR344_ExportManifest_TestData.xlsx @DataSet:OPR344_EXP_00015
#Scenario Outline: OPR344_EXP_00015_Bump a cart to another flight
#	Given User wants to execute the example "<Execute>"
#	When User switches station if BaseStation other than "<Origin>"
#	And User enters the screen name as 'LTE001'
#	Then User enters into the  iCargo 'Create Shipment' page successfully
#	When user clicks on the List button
#	And User enters the Participant details with AgentCode "<AgentCode>", ShipperCode "<ShipperCode>", ConsigneeCode "<ConsigneeCode>"
#	And User clicks on the ContinueParticipant button
#	And User enters the Certificate details
#	And User clicks on the ContinueCertificate button
#	And User enters the Shipment details with Origin "<Origin>", Destination "<Destination>", ProductCode "<ProductCode>", SCCCode "<SCC>", Commodity "<Commodity>", ShipmentDescription"<ShipmentDescription>", ServiceCargoClass "<ServiceCargoClass>", Piece "<Piece>", Weight "<Weight>"
#	And User clicks on the ContinueShipment button
#	And User clicks on the Select Flight Button
#	And User selects an "Combination" flight
#	And User clicks on the ContinueFlightDetails button
#	And User enters the Charge details with ChargeType "<ChargeType>" and ModeOfPayment "<ModeOfPayment>"
#	And User clicks on the CalculateCharges button
#	And User clicks on the ContinueChargeDetails button
#	And User enters the Acceptance details
#	And User clicks on the ContinueAcceptanceDetails button
#	And User enters the Screening details for row 1 with screeingMethod as 'Transfer Manifest Verified' and ScreeningResult as 'Pass'
#	And User clicks on the ContinueScreeningDetails button
#	And User checks the AWB_Verified checkbox
#	And User saves all the details & handles all the popups
#	When User enters the screen name as 'OPR344'
#	Then User enters into the  iCargo 'Export Manifest' page successfully
#	When User enters the Booked FlightNumber with ""
#	And User enters Booked ShipmentDate
#	And User clicks on the List button to fetch the Booked Shipment
#	And User creates new ULD/Cart in Assigned Shipment with cartType "<cartType>" and pou "<Destination>"
#	And User filterouts the Booked AWB from '<AWBSectionName>' and Created ULD_Cart
#	And User clicks on the Manifest button
#	And User closes the PrintPDF window
#	And User cliks on the offload ULD button to open the offload popup
#	And User enters the details to move to another NewFlightNumber "<NewFlightNumber>" and POU "<Destination>" in the 'ULD' offload popup
#	And User validates the warning message "The shipment is not booked to the flight"
#	And User validates the AWB is "Offloaded" in the Export Manifest screen
#	And User clicks on the orange pencil to edit the manifest
#	When User enters the new flight number "<NewFlightNumber>" to move the offloaded shipment
#	And User enters Booked ShipmentDate
#	And User clicks on the List button to fetch the Booked Shipment
#	And User clicks on the Manifest button
#	And User closes the PrintPDF window
#	Then User closes the Export Manifest screen
#
#@OPR344 @OPR344_EXP_00017 @DataSource:../TestData/OPR344_ExportManifest_TestData.xlsx @DataSet:OPR344_EXP_00017
#Scenario Outline: OPR344_EXP_00017_Manifest a CAO DG from lying list to a pax flight
#	Given User wants to execute the example "<Execute>"
#	When User switches station if BaseStation other than "<Origin>"
#	And User enters the screen name as 'LTE001'
#	Then User enters into the  iCargo 'Create Shipment' page successfully
#	When user clicks on the List button
#	And User enters the Participant details with AgentCode "<AgentCode>", ShipperCode "<ShipperCode>", ConsigneeCode "<ConsigneeCode>"
#	And User clicks on the ContinueParticipant button
#	And User enters the Certificate details
#	And User clicks on the ContinueCertificate button
#	And User enters the Shipment details with Origin "<Origin>", Destination "<Destination>", ProductCode "<ProductCode>", SCCCode "<SCC>", Commodity "<Commodity>", ShipmentDescription"<ShipmentDescription>", ServiceCargoClass "<ServiceCargoClass>", Piece "<Piece>", Weight "<Weight>"
#	And User clicks on the ContinueShipment button
#	And User clicks on the Select Flight Button
#	And User selects an "Cargo-Only" flight
#	And User clicks on the ContinueFlightDetails button
#	And User enters the Charge details with ChargeType "<ChargeType>" and ModeOfPayment "<ModeOfPayment>"
#	And User clicks on the CalculateCharges button
#	And User clicks on the ContinueChargeDetails button
#	And User enters the Acceptance details
#	And User clicks on the ContinueAcceptanceDetails button
#	And User enters the Screening details for row 1 with screeingMethod as 'ALT Dangerous Goods' and ScreeningResult as 'Pass'
#	And User clicks on the ContinueScreeningDetails button
#	And User enters details for CAO DG shipment with ChargeType "<ChargeType>",UNID "<UNID>", ProperShipmentName "<ProperShipmentName>", PackingInstruction "<PackingInstruction>",NoOfPkg "<Piece>", NetQtyPerPkg "<NetQtyPerPkg>", ReportableQnty "<ReportableQnty>"
#	And User saves the CAO DG shipment
#	And User captures the checksheet
#	And User checks the AWB_Verified checkbox
#	And User saves all the details & handles all the popups
#	When User enters the screen name as 'OPR344'
#	Then User enters into the  iCargo 'Export Manifest' page successfully
#	When User enters the Booked FlightNumber with "<FlightNumber>"
#	And User enters Booked ShipmentDate
#	And User clicks on the List button to fetch the Booked Shipment
#	And User creates new ULD/Cart in Assigned Shipment with cartType "<cartType>" and pou "<Destination>"
#	And User filterouts the Booked AWB from '<AWBSectionName>' and Created ULD_Cart
#	And User validates the warning message "The CAO validations failed" and Cancel the warning message
#
#	
#@OPR344 @OPR344_EXP_00018 @DataSource:../TestData/OPR344_ExportManifest_TestData.xlsx @DataSet:OPR344_EXP_00018
#Scenario Outline: OPR344_EXP_00018_Manifest an AWB for an unknown shipper from the lying list to a pax flight
#	Given User wants to execute the example "<Execute>"
#	When User switches station if BaseStation other than "<Origin>"
#	And User enters the screen name as 'LTE001'
#	Then User enters into the  iCargo 'Create Shipment' page successfully
#	When user clicks on the List button
#	And User enters the Participant details with AgentCode "<AgentCode>",Unknown ShipperCode "<UnknownShipperCode>", ConsigneeCode "<ConsigneeCode>"
#	And User clicks on the ContinueParticipant button
#	And User enters the Certificate details
#	And User clicks on the ContinueCertificate button
#	And User enters the Shipment details with Origin "<Origin>", Destination "<Destination>", ProductCode "<ProductCode>", SCCCode "<SCC>", Commodity "<Commodity>", ShipmentDescription"<ShipmentDescription>", ServiceCargoClass "<ServiceCargoClass>", Piece "<Piece>", Weight "<Weight>"
#	And User clicks on the ContinueShipment button
#	And User clicks on the Select Flight Button
#	And User selects an "Cargo-Only" flight
#	And User clicks on the ContinueFlightDetails button
#	And User enters the Charge details with ChargeType "<ChargeType>" and ModeOfPayment "<ModeOfPayment>"
#	And User clicks on the CalculateCharges button
#	And User clicks on the ContinueChargeDetails button
#	And User enters the Acceptance details
#	And User clicks on the ContinueAcceptanceDetails button
#	And User enters the Screening details for row 1 with screeingMethod as 'Transfer Manifest Verified' and ScreeningResult as 'Pass'
#	And User clicks on the ContinueScreeningDetails button
#	And User clicks on the save button
#	And User captures the checksheet
#	And User checks the AWB_Verified checkbox
#	And User saves all the details & handles all the popups
#	When User enters the screen name as 'OPR344'
#	Then User enters into the  iCargo 'Export Manifest' page successfully
#	When User enters the Booked FlightNumber with "<FlightNumber>"
#	And User enters Booked ShipmentDate
#	And User clicks on the List button to fetch the Booked Shipment
#	And User creates new ULD/Cart in Assigned Shipment with cartType "<cartType>" and pou "<Destination>"
#	And User filterouts the Booked AWB from '<AWBSectionName>' and Created ULD_Cart
#	And User validates the error message in Check Embargo as "UNKNOWN SHIPPERS RESTRICTED ON PAX FLIGHTS"
#	And User clicks on the Manifest button
#	And User closes the PrintPDF window
#	And User validates the AWB is "Manifested" in the Export Manifest screen
#	Then User closes the Export Manifest screen
#
#@OPR344 @OPR344_EXP_00020 @DataSource:../TestData/OPR344_ExportManifest_TestData.xlsx @DataSet:OPR344_EXP_00020
#Scenario Outline: OPR344_EXP_00020_Manifest a CAO DG on a freighter
#	Given User wants to execute the example "<Execute>"
#	When User switches station if BaseStation other than "<Origin>"
#	And User enters the screen name as 'LTE001'
#	Then User enters into the  iCargo 'Create Shipment' page successfully
#	When user clicks on the List button
#	And User enters the Participant details with AgentCode "<AgentCode>", ShipperCode "<ShipperCode>", ConsigneeCode "<ConsigneeCode>"
#	And User clicks on the ContinueParticipant button
#	And User enters the Certificate details
#	And User clicks on the ContinueCertificate button
#	And User enters the Shipment details with Origin "<Origin>", Destination "<Destination>", ProductCode "<ProductCode>", SCCCode "<SCC>", Commodity "<Commodity>", ShipmentDescription"<ShipmentDescription>", ServiceCargoClass "<ServiceCargoClass>", Piece "<Piece>", Weight "<Weight>"
#	And User clicks on the ContinueShipment button
#	And User clicks on the Select Flight Button
#	And User selects an "Cargo-Only" flight
#	And User clicks on the ContinueFlightDetails button
#	And User enters the Charge details with ChargeType "<ChargeType>" and ModeOfPayment "<ModeOfPayment>"
#	And User clicks on the CalculateCharges button
#	And User clicks on the ContinueChargeDetails button
#	And User enters the Acceptance details
#	And User clicks on the ContinueAcceptanceDetails button
#	And User enters the Screening details for row 1 with screeingMethod as 'ALT Dangerous Goods' and ScreeningResult as 'Pass'
#	And User clicks on the ContinueScreeningDetails button
#	And User enters details for CAO DG shipment with ChargeType "<ChargeType>",UNID "<UNID>", ProperShipmentName "<ProperShipmentName>", PackingInstruction "<PackingInstruction>",NoOfPkg "<Piece>", NetQtyPerPkg "<NetQtyPerPkg>", ReportableQnty "<ReportableQnty>"
#	And User saves the CAO DG shipment
#	And User captures the checksheet
#	And User checks the AWB_Verified checkbox
#	And User saves all the details & handles all the popups
#	When User enters the screen name as 'OPR344'
#	Then User enters into the  iCargo 'Export Manifest' page successfully
#	When User enters the Booked FlightNumber with ""
#	And User enters Booked ShipmentDate
#	And User clicks on the List button to fetch the Booked Shipment
#	And User creates new ULD/Cart in Assigned Shipment with cartType "<cartType>" and pou "<Destination>"
#	And User filterouts the Booked AWB from '<AWBSectionName>' and Created ULD_Cart
#	And User clicks on the Manifest button
#	And User closes the PrintPDF window
#	And User validates the AWB is "Manifested" in the Export Manifest screen
#	Then User closes the Export Manifest screen
#	Given User lauches the Url of Fogs QA Portal
#	Then User enters into the  Fogs QA Portal 'Cargo Manifest' page successfully
#	When User enters the station '<Station>' and flightNumber in Fogs QA Portal
#	And User clicks on the View button
#	And User should see the manifest details appeared
#	Then User validates the handling code '<HandlingCode>' under Manifest details
#
#@OPR344 @OPR344_EXP_00021 @DataSource:../TestData/OPR344_ExportManifest_TestData.xlsx @DataSet:OPR344_EXP_00021
#Scenario Outline: OPR344_EXP_00021_Manifest an AWB that does not have any available pieces_weight
#	Given User wants to execute the example "<Execute>"
#	When User switches station if BaseStation other than "<Origin>"
#	And User enters the screen name as 'LTE001'
#	Then User enters into the  iCargo 'Create Shipment' page successfully
#	When user clicks on the List button
#	And User enters the Participant details with AgentCode "<AgentCode>", ShipperCode "<ShipperCode>", ConsigneeCode "<ConsigneeCode>"
#	And User clicks on the ContinueParticipant button
#	And User enters the Certificate details
#	And User clicks on the ContinueCertificate button
#	And User enters the Shipment details with Origin "<Origin>", Destination "<Destination>", ProductCode "<ProductCode>", SCCCode "<SCC>", Commodity "<Commodity>", ShipmentDescription"<ShipmentDescription>", ServiceCargoClass "<ServiceCargoClass>", Piece "<Piece>", Weight "<Weight>"
#	And User clicks on the ContinueShipment button
#	And User clicks on the Select Flight Button
#	And User selects an "Combination" flight
#	And User clicks on the ContinueFlightDetails button
#	And User enters the Charge details with ChargeType "<ChargeType>" and ModeOfPayment "<ModeOfPayment>"
#	And User clicks on the CalculateCharges button
#	And User clicks on the ContinueChargeDetails button
#	And User checks the AWB_Verified checkbox
#	And User saves all the details with ChargeType "<ChargeType>"
#	And User enters the screen name as 'OPR344'
#	Then User enters into the  iCargo 'Export Manifest' page successfully
#	When User enters the Booked FlightNumber with ""
#	And User enters Booked ShipmentDate
#	And User clicks on the List button to fetch the Booked Shipment
#	And User creates new ULD/Cart in Assigned Shipment with cartType "<cartType>" and pou "<Destination>"
#	And User filterouts the Booked AWB from '<AWBSectionName>' and Created ULD_Cart
#	And User validates the error popover message as "The assigned pieces cannot be greater than the available pieces for the AWB"
#	Then User closes the Export Manifest screen
#
#@OPR344 @OPR344_EXP_00022 @DataSource:../TestData/OPR344_ExportManifest_TestData.xlsx @DataSet:OPR344_EXP_00022
#Scenario Outline: OPR344_EXP_00022_Manifest an AWB with no screening details to a pax flight
#	Given User wants to execute the example "<Execute>"
#	When User switches station if BaseStation other than "<Origin>"
#	And User enters the screen name as 'LTE001'
#	Then User enters into the  iCargo 'Create Shipment' page successfully
#	When user clicks on the List button
#	And User enters the Participant details with AgentCode "<AgentCode>", ShipperCode "<ShipperCode>", ConsigneeCode "<ConsigneeCode>"
#	And User clicks on the ContinueParticipant button
#	And User enters the Certificate details
#	And User clicks on the ContinueCertificate button
#	And User enters the Shipment details with Origin "<Origin>", Destination "<Destination>", ProductCode "<ProductCode>", SCCCode "<SCC>", Commodity "<Commodity>", ShipmentDescription"<ShipmentDescription>", ServiceCargoClass "<ServiceCargoClass>", Piece "<Piece>", Weight "<Weight>"
#	And User clicks on the ContinueShipment button
#	And User clicks on the Select Flight Button
#	And User selects an "Combination" flight
#	And User clicks on the ContinueFlightDetails button
#	And User enters the Charge details with ChargeType "<ChargeType>" and ModeOfPayment "<ModeOfPayment>"
#	And User clicks on the CalculateCharges button
#	And User clicks on the ContinueChargeDetails button
#	And User enters the Acceptance details
#	And User clicks on the ContinueAcceptanceDetails button
#	And User clicks on the ContinueScreeningDetails button
#	And User checks the AWB_Verified checkbox
#	And User saves all the details with ChargeType "<ChargeType>"
#	When User enters the screen name as 'OPR344'
#	Then User enters into the  iCargo 'Export Manifest' page successfully
#	When User enters the Booked FlightNumber with ""
#	And User enters Booked ShipmentDate
#	And User clicks on the List button to fetch the Booked Shipment
#	And User creates new ULD/Cart in Assigned Shipment with cartType "<cartType>" and pou "<Destination>"
#	And User filterouts the Booked AWB from '<AWBSectionName>' and Created ULD_Cart
#	And User validates the error popover message as "Blocked for screening"
#	Then User closes the Export Manifest screen