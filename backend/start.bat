@echo off
setlocal enabledelayedexpansion

echo ===================================
echo BookSansar Application Startup
echo ===================================
echo.

:: Check if PostgreSQL is running
echo [1/6] Checking PostgreSQL...
sc query postgresql >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo ERROR: PostgreSQL service is not running!
    echo Please start PostgreSQL and try again.
    pause
    exit /b 1
)

:: Clean up processes
echo [2/6] Cleaning up processes...
taskkill /F /IM bookSansar.exe 2>nul
taskkill /F /IM dotnet.exe 2>nul
timeout /t 2 /nobreak >nul

:: Clean build artifacts
echo [3/6] Cleaning build artifacts...
if exist bin rmdir /s /q bin
if exist obj rmdir /s /q obj

:: Restore packages
echo [4/6] Restoring packages...
dotnet restore
if %ERRORLEVEL% neq 0 (
    echo ERROR: Failed to restore packages
    pause
    exit /b 1
)

:: Build
echo [5/6] Building...
dotnet build
if %ERRORLEVEL% neq 0 (
    echo ERROR: Build failed
    pause
    exit /b 1
)

:: Start application
echo [6/6] Starting application...
echo.
echo ===================================
echo Application will be available at:
echo - Swagger UI: http://localhost:5000/swagger
echo - API: http://localhost:5000/api
echo ===================================
echo.
echo The browser should open automatically.
echo If it doesn't, please open: http://localhost:5000/swagger
echo.
echo Press Ctrl+C to stop
echo.

dotnet run
if %ERRORLEVEL% neq 0 (
    echo.
    echo ERROR: Application failed to start
    echo Please check the error messages above
    pause
    exit /b 1
) 