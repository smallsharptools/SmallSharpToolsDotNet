@echo off

set REGSQL="C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\aspnet_regsql.exe"
set DSN=Data Source=(local); Database=EventsBanner; Integrated Security=true;

rem Help?
rem %REGSQL% -?

rem REGSQL = %REGSQL%
rem  DSN = %DSN%

%REGSQL% -C "%DSN%" -ed
%REGSQL% -C "%DSN%" -et -t Events

echo Please add these settings to web.config in appSettings block
echo   SqlCacheDependencyEnabled = true
echo   SqlCacheDependencyTables = Events
echo   SqlCacheDependencyConnectionStringName = EventsBannerDB

pause
