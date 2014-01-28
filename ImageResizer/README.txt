SmallSharpTools.ImageResizer

This utility resizes adds a context menu option to Windows Explorer when
a folder is selected.  When it is run it will look for images and create 
smaller versions of those images.  By default it creates images with the 
dimensions of 300x225 and 160x120.  These values can be changed in the 
application config file.

BUILD
-----

This project can be built using MSBuild.  To build, run one of the the following scripts.

* Build Debug.cmd
* Build Release.cmd
* Package Installer.cmd

Following build a Zip archive will be created and placed in the root directory.
 
The MSBuild script, build.proj, relies on the following dependencies.

* Visual Web Developer (or Visual Studio 2005)
http://msdn.microsoft.com/express/vwd/

* .NET 2.0
http://www.microsoft.com/downloads/details.aspx?FamilyID=0856eacb-4362-4b0d-8edd-aab15c5e04f5

* MSBuild Community Tasks
http://msbuildtasks.tigris.org/

CONTACT
-------
Brennan Stehling
brennan@smallsharptools.com
http://www.smallsharptools.com/
