using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        editor1.ContentSaved += new EventHandler(editor1_ContentSaved);
        editor1.ContentSaving += new EventHandler(editor1_ContentSaving);
    }

    void editor1_ContentSaving(object sender, EventArgs e)
    {
        phSavedContent.Controls.Clear();
        phSavedContent.Controls.Add(new LiteralControl(editor1.Content));
    }

    void editor1_ContentSaved(object sender, EventArgs e)
    {
        phSavedContent2.Controls.Clear();
        phSavedContent2.Controls.Add(new LiteralControl(editor1.Content));
    }

}