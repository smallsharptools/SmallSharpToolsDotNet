using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace DelegationPlugIn
{
    public partial class DelegationHelperPlugIn : StandardPlugIn
    {

        private void actExpandProperties_Execute(ExecuteEventArgs ea)
        {
            //TypeDeclaration typeDeclaration = CodeRush.Source.ActiveType as TypeDeclaration;
            //if (typeDeclaration == null)
            //    return;
            //TextDocument textDocument = CodeRush.Documents.ActiveTextDocument;
            //if (textDocument == null)
            //    return;
            //foreach(Member member in typeDeclaration.AllMembers)
            //    Filter(textDocument, member);

            //StringBuilder code = new StringBuilder();
            //foreach(Filter filter in _Filters)
            //    filter.GenerateCode(textDocument, code);
			
            //textDocument.QueueInsert(typeDeclaration.BlockCodeRange.Start, code.ToString());

            //textDocument.ApplyQueuedEdits("Organize Members");
        }

        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();

            //
            // TODO: Add your initialization code here.
            //
        }
        #endregion

        #region FinalizePlugIn
        public override void FinalizePlugIn()
        {
            //
            // TODO: Add your finalization code here.
            //

            base.FinalizePlugIn();
        }
        #endregion

    }
}