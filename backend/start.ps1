# Stop any existing processes
Write-Host "Stopping any existing processes..." -ForegroundColor Yellow
Get-Process -Name "bookSansar" -ErrorAction SilentlyContinue | Stop-Process -Force
Start-Sleep -Seconds 2

# Clean the solution
Write-Host "Cleaning the solution..." -ForegroundColor Yellow
dotnet clean
Remove-Item -Path "bin" -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item -Path "obj" -Recurse -Force -ErrorAction SilentlyContinue

# Restore packages
Write-Host "Restoring packages..." -ForegroundColor Yellow
dotnet restore

# Build the solution
Write-Host "Building the solution..." -ForegroundColor Yellow
dotnet build

# Start the application
Write-Host "Starting the application..." -ForegroundColor Green
Write-Host "The application will be available at:" -ForegroundColor Cyan
Write-Host "- Swagger UI: http://localhost:5067/swagger" -ForegroundColor Cyan
Write-Host "- API: http://localhost:5067/api" -ForegroundColor Cyan
Write-Host "`nPress Ctrl+C to stop the application`n" -ForegroundColor Yellow

# Run the application
dotnet run 