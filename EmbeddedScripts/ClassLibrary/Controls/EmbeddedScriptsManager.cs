using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmallSharpTools.EmbeddedScripts.Controls
{
    
    /// <summary>
    /// Includes several of the most popular Javascript libraries.
    /// </summary>
    [ToolboxData("<{0}:EmbeddedScriptsManager runat=server></{0}:EmbeddedScriptsManager>"),
       Designer(typeof(EmbeddedScriptsDesigner))]
    public class EmbeddedScriptsManager : CompositeControl
    {
        #region "  Constants  "

        private readonly string scriptTemplate = "<script type=\"text/javascript\" src=\"{0}\"></script>\n";
        private readonly string Prefix = "SmallSharpTools.EmbeddedScripts.Resources.";
        private readonly string PrototypeScript = "prototype-1.5.0.js";
        
        private readonly string RicoScript = "rico-1.1.2.js";
        private readonly string ScriptaculousBuilderScript = "scriptaculous-js-1.7.0-builder.js";
        private readonly string ScriptaculousControlsScript = "scriptaculous-js-1.7.0-controls.js";
        private readonly string ScriptaculousDragDropScript = "scriptaculous-js-1.7.0-dragdrop.js";
        private readonly string ScriptaculousEffectsScript = "scriptaculous-js-1.7.0-effects.js";
        private readonly string ScriptaculousScript = "scriptaculous-js-1.7.0-scriptaculous.js";
        private readonly string ScriptaculousSliderScript = "scriptaculous-js-1.7.0-slider.js";
        private readonly string ScriptaculousUnitTestScript = "scriptaculous-js-1.7.0-unittest.js";
        private readonly string DojoScript = "dojo-0.2.2-ajax.js";
        private readonly string OverLibScript = "overlib-2.42.js";
        private readonly string MochiKitScript = "MochiKit-1.3.1.js";
        private readonly string LibertyScript = "liberty-01.js";
        private readonly string JsonScript = "json-01102007.js";
        private readonly string BehaviourScript = "behaviour-1.1.js";

        private readonly string JQueryScript = "jquery-1.1.4.pack.js";
        private readonly string JQueryJsonScript = "jquery.json.js";
        private readonly string JQueryInterfaceScript = "interface-1.2.js";
        private readonly string JQueryDimensionsScript = "jquery.dimensions.pack.js";
        private readonly string JQueryTooltipScript = "jtip.js";
        private readonly string JQueryContextMenuScript = "jquery.contextmenu.r2.packed.js";

        #endregion
        
        #region "  Variables  "
        private Label label;
        private List<String> registeredScripts = new List<String>(); 
        #endregion
        
        #region "  Constructors  "
        
        /// <summary>
        /// </summary>
        public EmbeddedScriptsManager()
        {
            Load += new EventHandler(EmbeddedScriptsManager_Load);
        }

        #endregion

        #region "  Events  "

        private void EmbeddedScriptsManager_Load(object sender, EventArgs e)
        {
            label.Visible = DesignMode;
            if (UsePrototypeScript)
            {
                RegisterScriptInclude(PrototypeScript);
            }
            if (UseJQueryScript)
            {
                RegisterScriptInclude(JQueryScript);
            }
            if (UseJQueryJsonScript)
            {
                RegisterScriptInclude(JQueryJsonScript);
            }
            if (UseJQueryInterfaceScript)
            {
                RegisterScriptInclude(JQueryInterfaceScript);
            }
            if (UseJQueryDimensionsScript)
            {
                RegisterScriptInclude(JQueryDimensionsScript);
            }
            if (UseJQueryTooltipScript)
            {
                RegisterScriptInclude(JQueryTooltipScript);
            }
            if (UseJQueryContextMenuScript)
            {
                RegisterScriptInclude(JQueryContextMenuScript);
            }
            if (UseRicoScript)
            {
                RegisterScriptInclude(RicoScript);
            }
            if (UseDojoScript)
            {
                RegisterScriptInclude(DojoScript);
            }
            if (UseOverLibScript)
            {
                RegisterScriptInclude(OverLibScript);
            }
            if (UseMochiKitScript)
            {
                RegisterScriptInclude(MochiKitScript);
            }
            if (UseLibertyScript)
            {
                RegisterScriptInclude(LibertyScript);
            }
            if (UseJsonScript)
            {
                RegisterScriptInclude(JsonScript);
            }
            if (UseBehaviourScript)
            {
                RegisterScriptInclude(BehaviourScript);
            }
            if (UseScriptaculousScript)
            {
                RegisterScriptInclude(ScriptaculousScript);
            }
            if (UseScriptaculousBuilderScript)
            {
                RegisterScriptInclude(ScriptaculousBuilderScript);
            }
            if (UseScriptaculousEffectsScript)
            {
                RegisterScriptInclude(ScriptaculousEffectsScript);
            }
            if (UseScriptaculousDragDropScript)
            {
                RegisterScriptInclude(ScriptaculousDragDropScript);
            }
            if (UseScriptaculousSliderScript)
            {
                RegisterScriptInclude(ScriptaculousSliderScript);
            }
            if (UseScriptaculousControlsScript)
            {
                RegisterScriptInclude(ScriptaculousControlsScript);
            }
            if (UseScriptaculousUnitTestScript)
            {
                RegisterScriptInclude(ScriptaculousUnitTestScript);
            }
        }

        #endregion

        #region "  Rendering  "
        
        /// <summary>
        /// </summary>
        protected override void CreateChildControls()
        {
            // Clear child controls
            Controls.Clear();

            // Build the control tree
            CreateControlHierarchy();

            // Clear the viewstate of child controls
            ClearChildViewState();
        }

        /// <summary>
        /// Creates control hierarchy
        /// </summary>
        protected void CreateControlHierarchy()
        {
            if (label == null)
            {
                label = new Label();
                label.Text = ClientID;
            }
        }

        #endregion
        
        #region "  Properties  "

        private bool _isVerbose = true;

        /// <summary>
        /// Shows comments with script include references
        /// </summary>
        [Browsable(true), Category("Scripts"), DefaultValue(true),
         Description("Shows comments with script include references")]
        public bool IsVerbose
        {
            get
            {
                EnsureChildControls();
                return _isVerbose;
            }
            set
            {
                EnsureChildControls();
                _isVerbose = value;
            }
        }

        private bool _usePrototypeScript = false;

        /// <summary>
        /// Prototype Library (http://www.prototypejs.org/)
        /// </summary>
        [Browsable(true), Category("Scripts"), DefaultValue(false),
         Description("Prototype Library (http://www.prototypejs.org/)")]
        public bool UsePrototypeScript
        {
            get
            {
                EnsureChildControls();
                return _usePrototypeScript;
            }
            set
            {
                EnsureChildControls();
                _usePrototypeScript = value;
            }
        }

        private bool _useJQueryScript = false;

        /// <summary>
        /// jQuery Library (http://jquery.com/)
        /// </summary>
        [Browsable(true), Category("Scripts"), DefaultValue(false),
         Description("jQuery Library (http://jquery.com/)")]
        public bool UseJQueryScript
        {
            get
            {
                EnsureChildControls();
                return _useJQueryScript;
            }
            set
            {
                EnsureChildControls();
                _useJQueryScript = value;
            }
        }

        private bool _useJQueryJsonScript = false;

        /// <summary>
        /// jQuery JSON Plugin (http://mg.to/2006/01/25/json-for-jquery)
        /// </summary>
        [Browsable(true), Category("Scripts"), DefaultValue(false),
         Description("jQuery JSON Plugin (http://mg.to/2006/01/25/json-for-jquery)")]
        public bool UseJQueryJsonScript
        {
            get
            {
                EnsureChildControls();
                return _useJQueryJsonScript;
            }
            set
            {
                EnsureChildControls();
                if (value)
                {
                    UseJQueryScript = true;
                }
                _useJQueryJsonScript = value;
            }
        }

        private bool _useJQueryInterfaceScript = false;

        /// <summary>
        /// jQuery Interface Plugin (http://interface.eyecon.ro/)
        /// </summary>
        [Browsable(true), Category("Scripts"), DefaultValue(false),
         Description("jQuery Interface Plugin (http://interface.eyecon.ro/)")]
        public bool UseJQueryInterfaceScript
        {
            get
            {
                EnsureChildControls();
                return _useJQueryInterfaceScript;
            }
            set
            {
                EnsureChildControls();
                if (value)
                {
                    UseJQueryScript = true;
                }
                _useJQueryInterfaceScript = value;
            }
        }

        private bool _useJQueryDimensionsScript = false;

        /// <summary>
        /// jQuery Dimensions Plugin (http://jquery.com/plugins/project/dimensions)
        /// </summary>
        [Browsable(true), Category("Scripts"), DefaultValue(false),
         Description("jQuery Dimensions Plugin (http://jquery.com/plugins/project/dimensions)")]
        public bool UseJQueryDimensionsScript
        {
            get
            {
                EnsureChildControls();
                return _useJQueryDimensionsScript;
            }
            set
            {
                EnsureChildControls();
                if (value)
                {
                    UseJQueryScript = true;
                }
                _useJQueryDimensionsScript = value;
            }
        }

        private bool _useJQueryTooltipScript = false;

        /// <summary>
        /// jQuery Tooltip Plugin (http://www.codylindley.com/blogstuff/js/jtip/)
        /// </summary>
        [Browsable(true), Category("Scripts"), DefaultValue(false),
         Description("jQuery Tooltip Plugin (http://www.codylindley.com/blogstuff/js/jtip/)")]
        public bool UseJQueryTooltipScript
        {
            get
            {
                EnsureChildControls();
                return _useJQueryTooltipScript;
            }
            set
            {
                EnsureChildControls();
                if (value)
                {
                    UseJQueryScript = true;
                }
                _useJQueryTooltipScript = value;
            }
        }

        private bool _useJQueryContextMenuScript = false;

        /// <summary>
        /// jQuery Context Menu Plugin (http://www.trendskitchens.co.nz/jquery/contextmenu/)
        /// </summary>
        [Browsable(true), Category("Scripts"), DefaultValue(false),
         Description("jQuery Context Menu (http://www.trendskitchens.co.nz/jquery/contextmenu/)")]
        public bool UseJQueryContextMenuScript
        {
            get
            {
                EnsureChildControls();
                return _useJQueryContextMenuScript;
            }
            set
            {
                EnsureChildControls();
                if (value)
                {
                    UseJQueryScript = true;
                }
                _useJQueryContextMenuScript = value;
            }
        }

        private bool _useRicoScript = false;

        /// <summary>
        /// Rico (http://openrico.org/)
        /// </summary>
        [Browsable(true), Category("Scripts"), DefaultValue(false),
         Description("Rico (http://openrico.org/)")]
        public bool UseRicoScript
        {
            get
            {
                EnsureChildControls();
                return _useRicoScript;
            }
            set
            {
                EnsureChildControls();
                _useRicoScript = value;
                if (value)
                {
                    UsePrototypeScript = true;
                }
            }
        }

        private bool _useOverLibScript = false;

        /// <summary>
        /// OverLib (http://www.bosrup.com/web/overlib/)
        /// </summary>
        [Browsable(true), Category("Scripts"), DefaultValue(false),
         Description("OverLib (http://www.bosrup.com/web/overlib/)")]
        public bool UseOverLibScript
        {
            get
            {
                EnsureChildControls();
                return _useOverLibScript;
            }
            set
            {
                EnsureChildControls();
                _useOverLibScript = value;
            }
        }

        private bool _useMochiKitScript = false;

        /// <summary>
        /// MochiKit (http://mochikit.com/)
        /// </summary>
        [Browsable(true), Category("Scripts"), DefaultValue(false),
         Description("MochiKit (http://mochikit.com/)")]
        public bool UseMochiKitScript
        {
            get
            {
                EnsureChildControls();
                return _useMochiKitScript;
            }
            set
            {
                EnsureChildControls();
                _useMochiKitScript = value;
            }
        }

        private bool _useLibertyScript = false;

        /// <summary>
        /// Liberty (http://aka-fotos.de/web?javascript/liberty)
        /// </summary>
        [Browsable(true), Category("Scripts"), DefaultValue(false),
         Description("Liberty (http://aka-fotos.de/web?javascript/liberty)")]
        public bool UseLibertyScript
        {
            get
            {
                EnsureChildControls();
                return _useLibertyScript;
            }
            set
            {
                EnsureChildControls();
                _useLibertyScript = value;
            }
        }

        private bool _useJsonScript = false;

        /// <summary>
        /// JSON (http://www.json.org/js.html)
        /// </summary>
        [Browsable(true), Category("Scripts"), DefaultValue(false),
         Description("JSON (http://www.json.org/js.html)")]
        public bool UseJsonScript
        {
            get
            {
                EnsureChildControls();
                return _useJsonScript;
            }
            set
            {
                EnsureChildControls();
                _useJsonScript = value;
            }
        }

        private bool _useBehaviourScript = false;

        /// <summary>
        /// Behaviour Library (http://www.bennolan.com/behaviour/)
        /// </summary>
        [Browsable(true), Category("Scripts"), DefaultValue(false),
         Description("Behaviour Library (http://www.bennolan.com/behaviour/)")]
        public bool UseBehaviourScript
        {
            get
            {
                EnsureChildControls();
                return _useBehaviourScript;
            }
            set
            {
                EnsureChildControls();
                _useBehaviourScript = value;
            }
        }

        private bool _useScriptaculousScript = false;

        /// <summary>
        /// Scriptaculous Library (http://script.aculo.us/)
        /// </summary>
        [Browsable(true), Category("Scripts"), DefaultValue(false),
         Description("Scriptaculous Library (http://script.aculo.us/)")]
        public bool UseScriptaculousScript
        {
            get
            {
                EnsureChildControls();
                return _useScriptaculousScript;
            }
            set
            {
                EnsureChildControls();
                _useScriptaculousScript = value;
                if (value)
                {
                    UsePrototypeScript = true;
                }
            }
        }

        private bool _useScriptaculousBuilderScript = false;

        /// <summary>
        /// Scriptaculous Library (Builder)
        /// </summary>
        [Browsable(true), Category("Scripts"), DefaultValue(false),
         Description("Scriptaculous Library (Builder)")]
        public bool UseScriptaculousBuilderScript
        {
            get
            {
                EnsureChildControls();
                return _useScriptaculousBuilderScript;
            }
            set
            {
                EnsureChildControls();
                _useScriptaculousBuilderScript = value;
                if (value)
                {
                    UseScriptaculousScript = true;
                }
            }
        }

        private bool _useScriptaculousControlsScript = false;

        /// <summary>
        /// Scriptaculous Library (Controls)
        /// </summary>
        [Browsable(true), Category("Scripts"), DefaultValue(false),
         Description("Scriptaculous Library (Controls)")]
        public bool UseScriptaculousControlsScript
        {
            get
            {
                EnsureChildControls();
                return _useScriptaculousControlsScript;
            }
            set
            {
                EnsureChildControls();
                _useScriptaculousControlsScript = value;
                if (value)
                {
                    UseScriptaculousScript = true;
                }
            }
        }

        private bool _useScriptaculousDragDropScript = false;

        /// <summary>
        /// Scriptaculous Library (DragDrop)
        /// </summary>
        [Browsable(true), Category("Scripts"), DefaultValue(false),
         Description("Scriptaculous Library (DragDrop)")]
        public bool UseScriptaculousDragDropScript
        {
            get
            {
                EnsureChildControls();
                return _useScriptaculousDragDropScript;
            }
            set
            {
                EnsureChildControls();
                _useScriptaculousDragDropScript = value;
                if (value)
                {
                    UseScriptaculousScript = true;
                    UseScriptaculousEffectsScript = true;
                }
            }
        }

        private bool _useScriptaculousEffectsScript = false;

        /// <summary>
        /// Scriptaculous Library (Effects)
        /// </summary>
        [Browsable(true), Category("Scripts"), DefaultValue(false),
         Description("Scriptaculous Library (Effects)")]
        public bool UseScriptaculousEffectsScript
        {
            get
            {
                EnsureChildControls();
                return _useScriptaculousEffectsScript;
            }
            set
            {
                EnsureChildControls();
                _useScriptaculousEffectsScript = value;
                if (value)
                {
                    UseScriptaculousScript = true;
                }
            }
        }

        private bool _useScriptaculousSliderScript = false;

        /// <summary>
        /// Scriptaculous Library (Slider)
        /// </summary>
        [Browsable(true), Category("Scripts"), DefaultValue(false),
         Description("Scriptaculous Library (Slider)")]
        public bool UseScriptaculousSliderScript
        {
            get
            {
                EnsureChildControls();
                return _useScriptaculousSliderScript;
            }
            set
            {
                EnsureChildControls();
                _useScriptaculousSliderScript = value;
                if (value)
                {
                    UseScriptaculousScript = true;
                }
            }
        }

        private bool _useScriptaculousUnitTestScript = false;

        /// <summary>
        /// Scriptaculous Library (UnitTests)
        /// </summary>
        [Browsable(true), Category("Scripts"), DefaultValue(false),
         Description("Scriptaculous Library (UnitTests)")]
        public bool UseScriptaculousUnitTestScript
        {
            get
            {
                EnsureChildControls();
                return _useScriptaculousUnitTestScript;
            }
            set
            {
                EnsureChildControls();
                _useScriptaculousUnitTestScript = value;
                if (value)
                {
                    UseScriptaculousScript = true;
                }
            }
        }

        private bool _useDojoScript = false;

        /// <summary>
        /// Dojo Toolkit (http://dojotoolkit.org/)
        /// </summary>
        [Browsable(true), Category("Scripts"), DefaultValue(false),
         Description("Dojo Toolkit (http://dojotoolkit.org/)")]
        public bool UseDojoScript
        {
            get
            {
                EnsureChildControls();
                return _useDojoScript;
            }
            set
            {
                EnsureChildControls();
                _useDojoScript = value;
            }
        }

        #endregion

        #region "  Methods  "

        private void RegisterScriptInclude(string key)
        {
            if (!registeredScripts.Contains(key))
            {
                registeredScripts.Add(key);
                string url = Page.ClientScript.GetWebResourceUrl(GetType(), Prefix + key);
                if (IsVerbose)
                {
                    LiteralControl comment = new LiteralControl(String.Format("<!-- {0} -->\n", key));
                    Page.Header.Controls.Add(comment);
                }
                Page.Header.Controls.Add(new LiteralControl(String.Format(scriptTemplate, url)));
            }
        }

        #endregion
    }
}
