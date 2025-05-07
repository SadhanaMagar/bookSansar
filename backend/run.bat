@echo off
echo Stopping any running instances...
taskkill /F /IM bookSansar.exe 2>nul
timeout /t 2 /nobreak >nul

echo Starting the application...
echo.
echo The application will be available at:
echo - Swagger UI: http://localhost:5000/swagger
echo - API: http://localhost:5000/api
echo.
echo Press Ctrl+C to stop the application
echo.

dotnet run --no-build 