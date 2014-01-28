// Guids.cs
// MUST match guids.h
using System;

namespace SmallSharpTools.VSX.Composite.VSPackage
{
    static class GuidList
    {
        public const string guidCompositePackagePkgString = "59fc2e65-cd61-40f9-be60-4a2518495c90";
        public const string guidCompositePackageCmdSetString = "dc84af0d-0c9a-450e-9fcf-ea2babdeb317";
        public const string guidCompositePackageEditorFactoryString = "45d6d206-b262-4338-9b63-8b2765d52a1c";

        public static readonly Guid guidCompositePackageCmdSet = new Guid(guidCompositePackageCmdSetString);
        public static readonly Guid guidCompositePackageEditorFactory = new Guid(guidCompositePackageEditorFactoryString);
    };
}