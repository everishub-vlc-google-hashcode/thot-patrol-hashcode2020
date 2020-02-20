@echo off
for /f %%f in ('dir /b *.in') do "HashCode2020.exe" "%%~ff" "%%~dpnf.out"