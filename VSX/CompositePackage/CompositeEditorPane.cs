using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Shell;
using EnvDTE;
using tom;

using ISysServiceProvider = System.IServiceProvider;
using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;
using VSStd97CmdID = Microsoft.VisualStudio.VSConstants.VSStd97CmdID;

namespace SmallSharpTools.VSX.Composite.VSPackage
{
    public class CompositeEditorPane : Microsoft.VisualStudio.Shell.WindowPane, 
        IPersistFileFormat, //to enable the programmatic loading or saving of an object 
        //in a format specified by the user.
        IExtensibleObject
    {
        private CompositeEditor editorControl = null;

        //private CompositePackage myPackage = null;
        private const uint MyFormat = 0;
        private const string MyExtension = ".xcp";
        private string fileName = string.Empty;
        private bool isDirty;
        //private bool loading = false;
        //private Microsoft.VisualStudio.Shell.SelectionContainer selContainer = null;

        private IExtensibleObjectSite extensibleObjectSite;

        #region "Window.Pane Overrides"
        /// <summary>
        /// Constructor that calls the Microsoft.VisualStudio.Shell.WindowPane constructor then
        /// our initialization functions.
        /// </summary>
        /// <param name="package">Our Package instance.</param>
        public CompositeEditorPane(CompositePackage package)
            : base(null)
        {
            //PrivateInit(package);
        }

        protected override void OnClose()
        {
            //editorControl.StopRecorder();
            base.OnClose();
        }

        /// <summary>
        /// This is a required override from the Microsoft.VisualStudio.Shell.WindowPane class.
        /// It returns the extended rich text box that we host.
        /// </summary>
        public override IWin32Window Window
        {
            get
            {
                return this.editorControl;
            }
        }
        #endregion

        #region IExtensibleObject Members

        public void GetAutomationObject(string Name, IExtensibleObjectSite pParent, out object ppDisp)
        {
            // null or empty string just means the default object, but if a specific string
            // is specified, then make sure it's the correct one, but don't enforce case
            if (!string.IsNullOrEmpty(Name) && !Name.Equals("Document", StringComparison.CurrentCultureIgnoreCase))
            {
                ppDisp = null;
                return;
            }

            // Set the out value to this
            //ppDisp = (IEditor)this;

            // TODO check if this really needs to be defined!
            ppDisp = null;

            // Store the IExtensibleObjectSite object, it will be used in the Dispose method
            extensibleObjectSite = pParent;
        }

        #endregion

        #region IPersistFileFormat Members

        int IPersistFileFormat.GetClassID(out Guid pClassID)
        {
            return GetClassID(out pClassID);
        }

        int IPersistFileFormat.GetCurFile(out string ppszFilename, out uint pnFormatIndex)
        {
            // We only support 1 format so return its index
            pnFormatIndex = MyFormat;
            ppszFilename = fileName;
            return VSConstants.S_OK;
        }

        int IPersistFileFormat.GetFormatList(out string ppszFormatList)
        {
            char Endline = (char)'\n';
            string FormatList = string.Format(CultureInfo.InvariantCulture, "Composite Editor (*{0}){1}*{0}{1}{1}", MyExtension, Endline);
            ppszFormatList = FormatList;
            return VSConstants.S_OK;
        }

        int IPersistFileFormat.InitNew(uint nFormatIndex)
        {
            if (nFormatIndex != MyFormat)
            {
                return VSConstants.E_INVALIDARG;
            }
            // until someone change the file, we can consider it not dirty as
            // the user would be annoyed if we prompt him to save an empty file
            isDirty = false;
            return VSConstants.S_OK;
        }

        int IPersistFileFormat.IsDirty(out int pfIsDirty)
        {
            if (isDirty)
            {
                pfIsDirty = 1;
            }
            else
            {
                pfIsDirty = 0;
            }
            return VSConstants.S_OK;
        }

        int IPersistFileFormat.Load(string pszFilename, uint grfMode, int fReadOnly)
        {
            if (pszFilename == null)
            {
                return VSConstants.E_INVALIDARG;
            }

            //loading = true;
            int hr = VSConstants.S_OK;
            try
            {
                // Show the wait cursor while loading the file
                IVsUIShell VsUiShell = (IVsUIShell)GetService(typeof(SVsUIShell));
                if (VsUiShell != null)
                {
                    // Note: we don't want to throw or exit if this call fails, so
                    // don't check the return code.
                    hr = VsUiShell.SetWaitCursor();
                }

                editorControl.LoadFile(pszFilename);
                isDirty = false;

                //Determine if the file is read only on the file system
                FileAttributes fileAttrs = File.GetAttributes(pszFilename);

                int isReadOnly = (int)fileAttrs & (int)FileAttributes.ReadOnly;

                //Set readonly if either the file is readonly for the user or on the file system
                // TODO finish
                //if (0 == isReadOnly && 0 == fReadOnly)
                //    SetReadOnly(false);
                //else
                //    SetReadOnly(true);

                // Notify to the property window that some of the selected objects are changed
                // TODO finish
                //ITrackSelection track = TrackSelection;
                //if (null != track)
                //{
                //    hr = track.OnSelectChange((ISelectionContainer)selContainer);
                //    if (ErrorHandler.Failed(hr))
                //        return hr;
                //}

                // Hook up to file change notifications
                if (String.IsNullOrEmpty(fileName) || 0 != String.Compare(fileName, pszFilename, true, CultureInfo.CurrentCulture))
                {
                    fileName = pszFilename;
                    // TODO finish
                    //SetFileChangeNotification(pszFilename, true);

                    // Notify the load or reload
                    // TODO finish
                    //NotifyDocChanged();
                }
            }
            finally
            {
                //loading = false;
            }
            return VSConstants.S_OK;
        }

        int IPersistFileFormat.Save(string pszFilename, int fRemember, uint nFormatIndex)
        {
            // TODO:  Add Editor.SaveCompleted implementation
            return VSConstants.S_OK;
        }

        int IPersistFileFormat.SaveCompleted(string pszFilename)
        {
            // TODO:  Add Editor.SaveCompleted implementation
            return VSConstants.S_OK;
        }

        #endregion

        #region IPersist Members

        int IPersist.GetClassID(out Guid pClassID)
        {
            return GetClassID(out pClassID);
        }

        int GetClassID(out Guid pClassID)
        {
            pClassID = GuidList.guidCompositePackageEditorFactory;
            return VSConstants.S_OK;
        }

        #endregion

    }
}
