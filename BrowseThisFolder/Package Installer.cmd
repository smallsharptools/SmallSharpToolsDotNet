@echo off

rem C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\msbuild build.proj /t:BuildSetup /p:Configuration=Release

C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\msbuild build.proj /t:PackageInstaller /p:Configuration=Release

pause
