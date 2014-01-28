using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;

namespace SmallSharpTools.EmbeddedScripts.Controls
{
    /// <summary>
    /// </summary>
    public class EmbeddedScriptsActionList : DesignerActionList
    {

        #region "  Variables  "

        private EmbeddedScriptsManager _ctrl = null;

        #endregion

        #region "  Methods  "
        
        /// <summary>
        /// </summary>
        public EmbeddedScriptsActionList(IComponent component) : base(component)
        {
             _ctrl = component as EmbeddedScriptsManager;
        }

        /// <summary>
        /// </summary>
        public void LaunchWebsite()
        {
            Process.Start("http://www.smallsharptools.com/");
        }
        
        /// <summary>
        /// </summary>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection actionItems = new DesignerActionItemCollection();

            actionItems.Add(new DesignerActionHeaderItem("Scripts"));
            actionItems.Add(new DesignerActionHeaderItem("Support"));

            actionItems.Add(new DesignerActionPropertyItem("IsVerbose", "Verbose", "Scripts"));
            actionItems.Add(new DesignerActionPropertyItem("UseJQueryScript", "Use jQuery", "Scripts"));
            actionItems.Add(new DesignerActionPropertyItem("UseJQueryJsonScript", "Use jQuery JSON", "Scripts"));
            actionItems.Add(new DesignerActionPropertyItem("UseJQueryInterfaceScript", "Use jQuery Interface", "Scripts"));
            actionItems.Add(new DesignerActionPropertyItem("UseJQueryDimensionsScript", "Use jQuery Dimensions", "Scripts"));
            actionItems.Add(new DesignerActionPropertyItem("UseJQueryTooltipScript", "Use jQuery Tooltip", "Scripts"));
            actionItems.Add(new DesignerActionPropertyItem("UseJQueryContextMenuScript", "Use jQuery Context Menu", "Scripts"));
            actionItems.Add(new DesignerActionPropertyItem("UsePrototypeScript", "Use Prototype", "Scripts"));
            actionItems.Add(new DesignerActionPropertyItem("UseScriptaculousScript", "Use Scriptaculous", "Scripts"));

            actionItems.Add(new DesignerActionMethodItem(this, "LaunchWebsite", "SmallSharpTools.com", "Support"));

            return actionItems;
        }

        private PropertyDescriptor GetControlProperty(string propertyName)
        {
            PropertyDescriptor pd = TypeDescriptor.GetProperties(_ctrl)[propertyName];
            if (pd != null)
            {
                return pd;
            }
            else
            {
                throw new ArgumentException("Invalid property", propertyName);
            }
            
        }

        #endregion

        #region "  Properties  "

         /// <summary>
        /// </summary>
        public bool IsVerbose
        {
            get
            {
                return _ctrl.IsVerbose;
            }
            set
            {
                GetControlProperty("IsVerbose").SetValue(_ctrl, value);
            }
        }
        
        /// <summary>
        /// </summary>
        public bool UseJQueryScript
        {
            get
            {
                return _ctrl.UseJQueryScript;
            }
            set
            {
                GetControlProperty("UseJQueryScript").SetValue(_ctrl, value);
            }
        }
        
        /// <summary>
        /// </summary>
        public bool UseJQueryJsonScript
        {
            get
            {
                return _ctrl.UseJQueryJsonScript;
            }
            set
            {
                GetControlProperty("UseJQueryJsonScript").SetValue(_ctrl, value);
            }
        }
        
        /// <summary>
        /// </summary>
        public bool UseJQueryInterfaceScript
        {
            get
            {
                return _ctrl.UseJQueryInterfaceScript;
            }
            set
            {
                GetControlProperty("UseJQueryInterfaceScript").SetValue(_ctrl, value);
            }
        }
        
        /// <summary>
        /// </summary>
        public bool UseJQueryDimensionsScript
        {
            get
            {
                return _ctrl.UseJQueryDimensionsScript;
            }
            set
            {
                GetControlProperty("UseJQueryDimensionsScript").SetValue(_ctrl, value);
            }
        }
        
        /// <summary>
        /// </summary>
        public bool UseJQueryTooltipScript
        {
            get
            {
                return _ctrl.UseJQueryTooltipScript;
            }
            set
            {
                GetControlProperty("UseJQueryTooltipScript").SetValue(_ctrl, value);
            }
        }
        
        /// <summary>
        /// </summary>
        public bool UseJQueryContextMenuScript
        {
            get
            {
                return _ctrl.UseJQueryContextMenuScript;
            }
            set
            {
                GetControlProperty("UseJQueryContextMenuScript").SetValue(_ctrl, value);
            }
        }
        
        /// <summary>
        /// </summary>
        public bool UsePrototypeScript
         {
             get
             {
                 return _ctrl.UsePrototypeScript;
             }
             set
             {
                 GetControlProperty("UsePrototypeScript").SetValue(_ctrl, value);
             }
         }
        
        /// <summary>
        /// </summary>
        public bool UseScriptaculousScript
        {
            get
            {
                return _ctrl.UseScriptaculousScript;
            }
            set
            {
                GetControlProperty("UseScriptaculousScript").SetValue(_ctrl, value);
            }
        }

        #endregion

    }
}
