@echo off
for /f "delims=" %%i in ("%cd%") do set folder=%%~ni
dotnet publish -r win-x64 -c Release -o %folder%-Publish

pause