SmallSharpTools.BrowseThisFolder

This application launches a mini web server to view a directory as a website.  Once installed,
right-click on a folder and select the "Browse This Folder" option in the context menu.  It
will start the web server and open it in your default web browser.

This project is a result of the following blog entry.  The purpose of this project was to make
it a bit easier by providing an installer which handles the registry settings and puts the
assemblies in place.

http://weblogs.asp.net/rmclaws/archive/2005/10/25/428422.aspx

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
