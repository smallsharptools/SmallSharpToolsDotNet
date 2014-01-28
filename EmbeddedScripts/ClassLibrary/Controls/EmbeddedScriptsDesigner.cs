using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Web.UI.Design.WebControls;

namespace SmallSharpTools.EmbeddedScripts.Controls
{
    /// <summary>
    /// </summary>
    public class EmbeddedScriptsDesigner : CompositeControlDesigner
    {
        //string style = "style='color: #000; padding: 3px; font-size: 10px; font-family: arial, san-serif; " +
        //               "background: AntiqueWhite; width: auto; padding: 3px; border: 1px solid #999;'";

        private EmbeddedScriptsManager _ctrl = null;

        /// <summary>
        /// Override method
        /// </summary>
        public override void Initialize(IComponent component)
        {
            _ctrl = component as EmbeddedScriptsManager;
            base.Initialize(component);
        }

        /// <summary>
        /// Override method
        /// </summary>
        public override string GetDesignTimeHtml()
        {
            string markup = String.Empty;
            try
            {
                if (_ctrl != null)
                {
                    string style = "style='margin: 2px; padding: 3px; " +
                                   "background: transparent; width: auto; border: 1px solid #999;'";
                    string template = "<div " + style + ">{0}: {1}</div>{2}";
                    try
                    {
                        string name = _ctrl.GetType().Name;
                        string siteName = _ctrl.Site.Name;
                        markup = String.Format(template, name, siteName, base.GetDesignTimeHtml());
                    }
                    catch (Exception ex)
                    {
                        markup = "<p style='background: #ccc; color: #900; font-weight: bold;'>Error: " +
                         ex.Message + "</p>\n<div>" +
                         ex.StackTrace + "</div>";
                    }
                }
            }
            catch (Exception ex)
            {
                markup = "<p style='background: #ccc; color: #900; font-weight: bold;'>Error: " +
                         ex.Message + "</p>\n<div>" +
                         ex.StackTrace + "</div>";
            }
            return markup;
        }

        DesignerActionListCollection actionLists = null;
        
        /// <summary>
        /// </summary>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (actionLists == null)
                {
                    actionLists = new DesignerActionListCollection();
                    EmbeddedScriptsManager esm = Component as EmbeddedScriptsManager;
                    if (esm != null)
                    {
                        actionLists.Add(new EmbeddedScriptsActionList(esm));
                    }
                }
                return actionLists;
            }
        }
    }
}
