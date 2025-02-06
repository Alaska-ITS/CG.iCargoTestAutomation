
# Load the Excel COM object
$excel = New-Object -ComObject Excel.Application
$excel.Visible = $false
$workbook = $excel.Workbooks.Add()

# Path to the folder containing CSV files
$csvFolderPath = "C:\Users\v-asama\Github\Alaska-ITS\CG.iCargoTestAutomation\TestData\CSV\"

# Path to the log file
$logFilePath = "C:\Users\v-asama\Github\Alaska-ITS\CG.iCargoTestAutomation\TestData\logfile.txt"

# Function to log messages
function Log-Message {
    param (
        [string]$message
    )
    $timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    $logMessage = "$timestamp - $message"
    Add-Content -Path $logFilePath -Value $logMessage
}

# Start logging
Log-Message "Script started."

# Get all CSV files in the folder
$csvFiles = Get-ChildItem -Path $csvFolderPath -Filter *.csv

foreach ($csvFile in $csvFiles) {
    Log-Message "Processing file: $($csvFile.Name)"
    
    # Create a new worksheet for each CSV file
    $worksheet = $workbook.Worksheets.Add()
    $worksheet.Name = $csvFile.BaseName

    # Import CSV data into the worksheet
    $csvData = Import-Csv -Path $csvFile.FullName
    $row = 1
    foreach ($line in $csvData) {
        $col = 1
        foreach ($item in $line.PSObject.Properties) {
            if ($row -eq 1) {
                # Add header
                $worksheet.Cells.Item($row, $col) = $item.Name
            }
            $worksheet.Cells.Item($row + 1, $col) = $item.Value
            $col++
        }
        $row++
    }
    Log-Message "Finished processing file: $($csvFile.Name)"
}

# Save the workbook
$workbook.SaveAs("C:\Users\v-asama\Github\Alaska-ITS\CG.iCargoTestAutomation\TestData\Workbook.xlsx")
Log-Message "Workbook saved as C:\Users\v-asama\Github\Alaska-ITS\CG.iCargoTestAutomation\TestData\Workbook.xlsx"
$workbook.Close()
$excel.Quit()

# Release COM objects
[System.Runtime.InteropServices.Marshal]::ReleaseComObject($worksheet) | Out-Null
[System.Runtime.InteropServices.Marshal]::ReleaseComObject($workbook) | Out-Null
[System.Runtime.InteropServices.Marshal]::ReleaseComObject($excel) | Out-Null

Log-Message "Script finished."
