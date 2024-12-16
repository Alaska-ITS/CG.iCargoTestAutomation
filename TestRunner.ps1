param (
    [Parameter(Mandatory = $true)]
    [string]$ProjectName,
 
    [Parameter(Mandatory = $false)]
    [string]$TestFilter = ""
)
 
# Set the default filter to exclude skipped tests
$DefaultFilter = "TestCategory!=Skip"
 
# Combine the default filter with the user-provided filter (if any)
$TestFilter = if ([string]::IsNullOrWhiteSpace($TestFilter)) {
    $DefaultFilter
} else {
    "$DefaultFilter & ($TestFilter)"
}
 
# Display the final filter for verification
Write-Host "Applying test filter: $TestFilter"
 
# Construct and display the full dotnet test command for debugging
$dotnetTestCommand = "dotnet test --filter `"$TestFilter`" --logger `"trx;LogFileName=TestResults.trx`""
Write-Host "Executing command: $dotnetTestCommand"
 
# Run tests with the combined filter
Invoke-Expression $dotnetTestCommand
 
# Check if tests failed
if ($LASTEXITCODE -ne 0) {
    Write-Host "Tests failed with exit code $LASTEXITCODE"
    exit $LASTEXITCODE
} else {
    Write-Host "Tests completed successfully"
}
 
# Check if livingdoc is installed
if (-not (Get-Command livingdoc -ErrorAction SilentlyContinue)) {
    Write-Host "Error: 'livingdoc' command not found. Please ensure that the SpecFlow.Plus.LivingDoc.CLI tool is installed and added to your PATH."
    exit 1
}

# Define paths and generate LivingDoc report
$projectRoot = (Get-Item $PSScriptRoot).Parent.FullName + "\" + $ProjectName
$assemblyPath = Get-ChildItem -Path "$projectRoot\bin" -Recurse -Filter "$ProjectName.dll" | Select-Object -First 1 -ExpandProperty FullName
$testResultsPath = Join-Path $projectRoot "TestResults"
 
if (-not $assemblyPath) {
    Write-Host "Error: $ProjectName.dll not found in the bin directory."
    exit 1
}
 
Write-Host "Found test assembly at: $assemblyPath"
 
# Prepare LivingDoc report
$timestamp = Get-Date -Format "yyyyMMdd_HHmmss"
$reportName = "TestReport_$timestamp.html"
$assemblyDir = Split-Path -Parent $assemblyPath
Push-Location $assemblyDir
 
try {
    $testExecutionPath = Get-ChildItem -Path "." -Recurse -Filter "TestExecution.json" | Select-Object -First 1 -ExpandProperty FullName
    if (-not $testExecutionPath) {
        Write-Host "Error: TestExecution.json not found."
        exit 1
    }
 
    Write-Host "Generating LivingDoc report..."
    livingdoc test-assembly (Split-Path -Leaf $assemblyPath) -t $testExecutionPath --output $reportName
 
    if (Test-Path $reportName) {
        Write-Host "LivingDoc report generated successfully."
 
        # Create TestResults folder if it doesn't exist
        if (!(Test-Path -Path $testResultsPath)) {
            New-Item -ItemType Directory -Force -Path $testResultsPath | Out-Null
            Write-Host "Created TestResults directory."
        }
 
        # Move the report to TestResults folder
        $destination = Join-Path $testResultsPath $reportName
        Move-Item -Path $reportName -Destination $destination -Force
        Write-Host "LivingDoc report saved to $destination"
    } else {
        Write-Host "Error: LivingDoc report was not generated. Check for errors in the livingdoc command output."
    }
}
finally {
    # Return to the original directory
    Pop-Location
}