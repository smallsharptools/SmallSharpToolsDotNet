SmallSharpTools.Logger

The Logger works with a optional web module and a provider.  The included providers work
with Log4Net and the Windows Event Log.
 
The sample website included with the project shows how the Web.config can be configured
for the two providers.  Another command-line projects shows that the Logger can also be
used beyond a web application.

BUILD
-----

This project can be built using MSBuild.  To build, run one of the the following scripts.

* Package Debug.cmd
* Package Release.cmd

Following build a Zip archive will be created and placed in the root directory.
 
The MSBuild script, build.proj, relies on the following dependencies.

* .NET 2.0
http://www.microsoft.com/downloads/details.aspx?FamilyID=0856eacb-4362-4b0d-8edd-aab15c5e04f5

* Visual Studio 2005 Web Deployment Projects
http://msdn.microsoft.com/asp.net/reference/infrastructure/wdp/

* MSBuild Community Tasks
http://msbuildtasks.tigris.org/

CONTACT
-------
Brennan Stehling
brennan@smallsharptools.com
http://www.smallsharptools.com/
