@echo off
echo Cleaning up any existing processes...
taskkill /F /IM bookSansar.exe 2>nul
timeout /t 2 /nobreak >nul

echo Building the application...
dotnet build

if %ERRORLEVEL% NEQ 0 (
    echo Build failed! Please check the errors above.
    pause
    exit /b %ERRORLEVEL%
)

echo Starting the application...
echo.
echo The application will be available at:
echo - Swagger UI: http://localhost:5067/swagger
echo - API: http://localhost:5067/api
echo.
echo Press Ctrl+C to stop the application
echo.

dotnet run 