using System;
using System.Text;
using System.Web.UI;
using SmallSharpTools.Messenger.Web.UI.Controls;

public partial class _Default : Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void cf1_SendingMail(object sender, System.ComponentModel.CancelEventArgs e)
    {
        // log the message
        StringBuilder message = new StringBuilder();
        message.AppendLine(cf1.ToAddressText);
        message.AppendLine(cf1.ToNameText);
        message.AppendLine(cf1.FromAddressText);
        message.AppendLine(cf1.FromNameText);
        message.AppendLine(cf1.SubjectText);
        message.AppendLine(cf1.MessageText);
        Utility.GetLogger(GetType()).Info(message.ToString());

        // cancel sending mail and show thank you template
        e.Cancel = true;
        cf1.DisplayMode = ContactFormDisplayMode.ThankYou;
    }

}
