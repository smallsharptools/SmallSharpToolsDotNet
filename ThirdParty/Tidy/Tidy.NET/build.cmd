call %VSCOMNTOOLS%\vsvars32.bat
TlbImp ..\tidy.atl\ReleaseMinDependency\TidyATL.dll /out:Tidy.dll
csc /o+ /out:TestTidy.exe /r:Tidy.dll TestTidy\TestIt.cs
