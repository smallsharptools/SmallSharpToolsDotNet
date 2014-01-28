Visual Studio Plugins

Delegation Helper

-------------------
SmallSharpTools.com


Debugging COM in the Visual Studio SDK
--------------------------------------
http://www.clariusconsulting.net/blogs/pga/archive/2007/07/03/28234.aspx

Carlos posted some time ago a really interesting article on how to find the real object 
type behind a System.__ComObject. The .Object property of many classes of the Visual Studio 
Automation model returns this System.__ComObject type.

This is really useful and can save a lot of time when you need to debug your VSX Package 
or GAT Package. As Carlos pointed out you will need to add a reference to the Microsoft.VisualBasic.dll 
assembly, if you develop using the C# language you can remove this reference after you finished with 
the development.


Introduction

This article explains how to get the actual type behind the System.__ComObject type returned by the 
.Object property of several classes of the Visual Studio extensibility model.

More Information

Several classes of the extensibility model provide a property named "Object" of the System.Object 
type. Among them are the following:

    * EnvDTE.Project.Object
    * EnvTDE.ProjectItem.Object
    * EnvDTE.Window.Object
    * EnvDTE.AddIn.Object
    * EnvDTE.UIHierarchyItem.Object

The type is declared as System.Object because depending on the instance, it can return one type or 
another. For example, the type returned by a toolwindow when calling Window.Object depends on the 
kind of toolwindow: while the Solution Explorer toolwindow will return an object implementing the 
EnvDTE.UIHierarchy interface, the Task List toolwindow will return an object implementing the 
EnvDTE.TaskList interface. However, when you make a call like this to guess its type:

MessageBox.Show(objWindow.Object.GetType().FullName)

You get "System.__ComObject" as result, which is not very helpful. Instead, to know the actual type 
behind the COM wrapper, you can use this other statement:

MessageBox.Show(Microsoft.VisualBasic.Information.TypeName(objWindow.Object))

The Information module resides in the Microsoft.VisualBasic namespace, inside the 
Microsoft.VisualBasic.dll assembly, which is added automatically to VB.NET projects but you will 
need to add it to your project if you are using C#, at least until your add-in is coded.

Once you know the actual type, you can cast the object to its actual type and work with it.
