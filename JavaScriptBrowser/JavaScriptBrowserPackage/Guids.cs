// Guids.cs
// MUST match guids.h
using System;

namespace SmallSharpTools.JavaScriptBrowser.VSPackage
{
    static class GuidList
    {
        public const string guidJavaScriptBrowserPkgString = "d57fed4f-f4ac-4e92-a60a-af36b2b421ac";
        public const string guidJavaScriptBrowserCmdSetString = "3c38801f-50b2-4e7a-b25a-af5bc7c56936";
        public const string guidToolWindowPersistanceString = "6fb0ff9c-da08-4d7e-a4de-fd8d2d388ec7";

        public static readonly Guid guidJavaScriptBrowserCmdSet = new Guid(guidJavaScriptBrowserCmdSetString);
    };
}