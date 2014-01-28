using System;
using System.Data;
using System.ComponentModel;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SmallSharpTools.Tidy;
using SmallSharpTools.TidyWebite;

public partial class Controls_EditorControl : System.Web.UI.UserControl
{

    public event EventHandler ContentSaving;
    public event EventHandler ContentSaved;

    protected virtual void OnContentSaving(EventArgs e)
    {
        if (ContentSaving != null)
        {
            ContentSaving(this, e);
        }
    }

    protected virtual void OnContentSaved(EventArgs e)
    {
        if (ContentSaved != null)
        {
            ContentSaved(this, e);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RegisterTinyMCE();
    }

    private void RegisterTinyMCE()
    {
        if (!Page.ClientScript.IsClientScriptIncludeRegistered("TinyMCE"))
        {
            Page.ClientScript.RegisterClientScriptInclude("TinyMCE", Utility.GetRelativeSiteUrl("~/tiny_mce/tiny_mce.js"));
        }
        if (!Page.ClientScript.IsClientScriptBlockRegistered("TinyMCEInit"))
        {
            string script = "tinyMCE.init({" +
                "mode : 'exact', " +
                //"theme : 'advanced', " +
                "theme : 'custom', " +
                //"theme : 'intermediate', " +
                "content_css: '" + Utility.GetRelativeSiteUrl("~/style.css") + "', " +
                "elements : '" + tbContent.ClientID + 
                "'});";
            Page.ClientScript.RegisterClientScriptBlock(GetType(), "TinyMCEInit", script, true);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        MarkupCleaner cleaner = new MarkupCleaner();
        cleaner.OptionFile = Server.MapPath("~/Controls/tidy.config");
        OnContentSaving(EventArgs.Empty);
        Content = cleaner.CleanContent(Content);
        OnContentSaved(EventArgs.Empty);
    }

    [Description("Content to edit"), DefaultValue(""), Category("Editor")]
    public string Content
    {
        get { return tbContent.Text; }
        set { tbContent.Text = value; }
    }

}
