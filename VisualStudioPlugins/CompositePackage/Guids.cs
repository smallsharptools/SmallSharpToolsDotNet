// Guids.cs
// MUST match guids.h
using System;

namespace SmallSharpToolscom.CompositePackage
{
    static class GuidList
    {
        public const string guidCompositePackagePkgString = "a04b9669-43c9-43bf-9726-3a232bdf9659";
        public const string guidCompositePackageCmdSetString = "59f69402-ee9e-4bef-a721-e4d82d679c1f";
        public const string guidCompositePackageEditorFactoryString = "27e43417-df6a-40f3-9ce9-f772b9048051";

        public static readonly Guid guidCompositePackagePkg = new Guid(guidCompositePackagePkgString);
        public static readonly Guid guidCompositePackageCmdSet = new Guid(guidCompositePackageCmdSetString);
        public static readonly Guid guidCompositePackageEditorFactory = new Guid(guidCompositePackageEditorFactoryString);
    };
}