SmallSharpTools.UrlMapper

The Url Mapper supports virtual Url mappings using a module and a provider.  The included
static Url provider maps a single alias to a single virtual mapping.  The provider can be
implemented to support any number of virtual mappings.

The sample website included with the project shows how the Web.config can be configured
for the module and the available providers.

BUILD
-----

This project can be built using MSBuild.  To build, run one of the the following scripts.

* Package Debug.cmd
* Package Release.cmd

Following the build a Zip archive will be created and placed in the root directory.
 
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
brennan@smallsharptoos.com
