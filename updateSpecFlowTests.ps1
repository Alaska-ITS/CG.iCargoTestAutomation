param (
    [string]$jsonFile = "ExamplesTestData.json",
    [string]$csvFolder = "TestData\CSV",
    [string]$featureFolder = "Features"
)

Write-Host "Starting the update of SpecFlow tests..."

# Ensure the CSV folder exists
if (-not (Test-Path -Path $csvFolder)) {
    New-Item -ItemType Directory -Path $csvFolder
}

# Read JSON data
$jsonData = Get-Content $jsonFile | ConvertFrom-Json

# Get all feature files in the specified folder
$featureFiles = Get-ChildItem -Path $featureFolder -Filter *.feature

foreach ($featureFile in $featureFiles) {
    # Read feature file
    $featureContent = Get-Content $featureFile.FullName
    $fileUpdated = $false

    # Update feature file based on JSON data and Key Tag
    # Loop through each test case tag in the JSON data
    foreach ($testCaseTagName in $jsonData.PSObject.Properties.Name) {
        
        # Get the test case data
        $testCaseData = $jsonData.$testCaseTagName

        # Create a CSV file for each test case key
        $csvFile = "$csvFolder\$($testCaseTagName).csv"
        
        # Convert the test case data to CSV format and save it
        $testCaseData | ConvertTo-Csv -NoTypeInformation | Set-Content $csvFile

        # Find the scenario outline with the matching tag
        for ($i = 0; $i -lt $featureContent.Length; $i++) {
            # Check if the line contains the test case tag
            if ($featureContent[$i] -match "\@$testCaseTagName") {
                # Add the DataSource tag if not already present
                if ($featureContent[$i] -notmatch "@DataSource:") {
                    # Add the DataSource tag to the line
                    $featureContent[$i] += " @DataSource:$csvFile"
                    # Set the file updated flag to true
                    $fileUpdated = $true
                }
            }
        }
    }

    # Save the updated feature file
    if ($fileUpdated) {
        $featureContent | Set-Content $featureFile.FullName
    }
}

# Log the completion of the script
Write-Host "Completed the update of SpecFlow tests."
