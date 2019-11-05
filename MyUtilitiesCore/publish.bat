@echo off
for /f "delims=" %%i in ("%cd%") do set folder=%%~ni
dotnet publish -c Release -o %folder%-Publish

pause