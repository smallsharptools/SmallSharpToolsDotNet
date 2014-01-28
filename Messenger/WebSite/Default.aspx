<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Messenger Website</title>
    
<style type="text/css" media="screen">

table tr td.lbl {
    font-weight: bold;
    text-align: right;
}

table tr td.lblTop {
    font-weight: bold;
}

table tr td.btn {
    text-align: center;
}

input.tb {
    width: 300px;
}

input.btn {
    font-weight: normal;
}

textarea.ta {
    width: 400px;
    height: 80px;
}

span.sendError {
    color: #c00;
    font-weight: bold;
    display: block;
}

</style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 550px;">
    <sstm:ContactForm ID="cf1" runat="server" 
            ToAddressText="brennan@offwhite.net" 
            ToNameText="Brennan Stehling" 
            MessageText="This is a test." 
            FromAddressLabelText="Email: " 
            FromNameLabelText="Name: " 
            FromAddressRequiredErrorMessage="Email is required" 
            FromNameRequiredErrorMessage="Name is required" 
            FromAddressInvalidFormatErrorMessage="Email is invalid" 
            SendFailureText="Unable to send message.  Please try again later." 
            SendButtonText="Send Message" 
            ToVisible="False" 
            SubjectText="Feedback Form"
            OnSendingMail="cf1_SendingMail">
        <GreetingTemplate>
        Thanks for coming.  Please leave a message.
        </GreetingTemplate>
        <ThankYouTemplate>
        Thank you.  We will contact you shortly.<br />
        </ThankYouTemplate>
    </sstm:ContactForm>
    </div>
    </form>
</body>
</html>
