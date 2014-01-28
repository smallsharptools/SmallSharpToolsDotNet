/*=======================================================================
  Copyright (C) SmallSharpTools.com.  All rights reserved.
 
  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
  PARTICULAR PURPOSE.
  
  Brennan Stehling
  brennan@smallsharptools.com
  http://www.smallsharptools.com/
=======================================================================*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Net.Mail;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.Design.WebControls;
using System.Web.UI.WebControls;

namespace SmallSharpTools.Messenger.Web.UI.Controls
{
        
    /// <summary>
    /// Display mode for Contact Form
    /// </summary>
    public enum ContactFormDisplayMode
    {
        /// <summary>
        /// Greeting Template
        /// </summary>
        Greeting,
        /// <summary>
        /// Thank You Template
        /// </summary>
        ThankYou
    }

    /// <summary>
    /// SmallSharpTools.Messager: Contact Form
    /// </summary>
    [
        AspNetHostingPermission(SecurityAction.Demand, 
        Level = AspNetHostingPermissionLevel.Minimal),
        AspNetHostingPermission(SecurityAction.InheritanceDemand, 
        Level = AspNetHostingPermissionLevel.Minimal), 
        ToolboxData("<{0}:ContactForm runat=server></{0}:ContactForm>"),
       Designer(typeof(ContactFormDesigner))
    ]
    public class ContactForm : BaseCompositeControl, IValidator
    {

        /// <summary>
        /// Sending Mail event with the option to cancel
        /// </summary>
        [Category("Mail")]
        public event CancelEventHandler SendingMail;

        #region  "  Control Variables  "

        private readonly string EmailExpression = @"[\w-]+(?:\.[\w-]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7}";
        
        private Table contactFormTable;

        private Label lblSendError;
        
        private Label lblToAddress;
        private Label lblFromAddress;
        private Label lblToName;
        private Label lblFromName;
        private Label lblSubject;
        private Label lblMessage;

        private TextBox tbToAddress;
        private TextBox tbFromAddress;
        private TextBox tbToName;
        private TextBox tbFromName;
        private TextBox tbSubject;
        private TextBox tbMessage;

        TableRow toNameRow;
        TableRow toAddressRow;
        TableRow fromNameRow;
        TableRow fromAddressRow;

        private Button btnSend;

        private PlaceHolder greetingPlaceHolder;
        private PlaceHolder thankYouPlaceHolder;
        private GreetingTemplateContainer greetingTemplateContainer;
        private ThankYouTemplateContainer thankYouTemplateContainer;
        private ITemplate greetingTemplate;
        private ITemplate thankYouTemplate;

        private ValidationSummary validationSummary;
        private RequiredFieldValidator rfvToName;
        private RequiredFieldValidator rfvToAddress;
        private RegularExpressionValidator revToAddress;
        private RequiredFieldValidator rfvFromName;
        private RequiredFieldValidator rfvFromAddress;
        private RegularExpressionValidator revFromAddress;
        private RequiredFieldValidator rfvSubject;
        private RequiredFieldValidator rfvMessage;
        private List<IValidator> validators;
        
        private string _errorMessage;
        private bool _isValid;

        private bool _toVisible = true;
        private bool _fromVisible = true;
        private ContactFormDisplayMode _displayMode = ContactFormDisplayMode.Greeting;
        
        #endregion

        #region "  Constructors  "
        #endregion

        #region "  Control Events  "

        /// <summary>
        /// Load even handler
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetVisiblity();
            if (!DesignMode && !Page.IsPostBack)
            {
                DisplayMode = ContactFormDisplayMode.Greeting;
            }
        }

        /// <summary>
        /// Button Click Handler
        /// </summary>
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                CancelEventArgs args = new CancelEventArgs();
                OnSendingMail(args);
                if (!args.Cancel)
                {
                    string to = String.Format("\"{0}\" <{1}>", ToNameText, ToAddressText);
                    string from = String.Format("\"{0}\" <{1}>", FromNameText, FromAddressText);
                    MailMessage message = new MailMessage(from, to);
                    message.Subject = SubjectText;
                    message.Body = MessageText;
                    try
                    {
                        SmtpClient client = new SmtpClient();
                        client.Send(message);
                    }
                    catch
                    {
                        throw;
                    }

                    // assume success
                    DisplayMode = ContactFormDisplayMode.ThankYou;
                }
            }
        }
        
        #endregion

        /// <summary>
        /// Raise the SendingMail event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSendingMail(CancelEventArgs e)
        {
            if (SendingMail != null)
            {
                SendingMail(this, e);
            }
        }

        #region "  Control Methods  "

        /// <summary>
        /// Validates control
        /// </summary>
        public void Validate()
        {
            _isValid = true;
            // check all validators
            foreach (IValidator validator in validators)
            {
                if (!validator.IsValid)
                {
                    _isValid = false;
                }
            }
        }

        /// <summary>
        /// Created child controls
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
            contactFormTable = new Table();
            contactFormTable.CssClass = "contactForm";

            if (DesignMode)
            {
                contactFormTable.BorderWidth = 1;
                contactFormTable.BorderStyle = BorderStyle.Dotted;
            }
            
            #region "  Control Init  "

            InitializeLabel(ref lblSendError, "Failed to send message");
            lblSendError.CssClass = "sendError";
            lblSendError.Visible = false;

            InitializeLabel(ref lblToName, "To Name: ");
            InitializeLabel(ref lblToAddress, "To Address: ");
            InitializeLabel(ref lblFromName, "From Name: ");
            InitializeLabel(ref lblFromAddress, "From Address: ");
            InitializeLabel(ref lblSubject, "Subject: ");
            InitializeLabel(ref lblMessage, "Message: ");

            InitializeTextBox(ref tbToName, "tbToName", UniqueID);
            InitializeTextBox(ref tbToAddress, "tbToAddress", UniqueID);
            InitializeTextBox(ref tbFromName, "tbFromName", UniqueID);
            InitializeTextBox(ref tbFromAddress, "tbFromAddress", UniqueID);
            InitializeTextBox(ref tbSubject, "tbSubject", UniqueID);
            InitializeTextBox(ref tbMessage, "tbMessage", UniqueID);

            if (btnSend == null)
            {
                btnSend = new Button();
                btnSend.Text = "Send";
            }
            btnSend.CssClass = "btn";
            btnSend.ValidationGroup = UniqueID;

            #endregion

            #region "  Validation Code  "

            validationSummary = new ValidationSummary();
            validationSummary.ValidationGroup = UniqueID;

            InitializeRequiredFieldValidator(ref rfvToName, "rfvToName", tbToAddress.ID,
                UniqueID, "*", "To name is required", true, ValidatorDisplay.Dynamic);

            InitializeRequiredFieldValidator(ref rfvToAddress, "rfvToAddress", tbToAddress.ID,
                UniqueID, "*", "To address is required", true, ValidatorDisplay.Dynamic);

            InitializeRegularExpressionValidator(ref revToAddress, "revToAddress", tbToAddress.ID, EmailExpression,
                UniqueID, "*", "To address must be an email address", false, ValidatorDisplay.Static);

            InitializeRequiredFieldValidator(ref rfvFromName, "rfvFromName", tbFromName.ID,
                UniqueID, "*", "From name is required", true, ValidatorDisplay.Dynamic);

            InitializeRequiredFieldValidator(ref rfvFromAddress, "rfvFromAddress", tbFromAddress.ID,
                UniqueID, "*", "From address is required", true, ValidatorDisplay.Dynamic);

            InitializeRegularExpressionValidator(ref revFromAddress, "revFromAddress", tbFromAddress.ID, EmailExpression,
                UniqueID, "*", "From address must be an email address", false, ValidatorDisplay.Static);

            InitializeRequiredFieldValidator(ref rfvSubject, "rfvSubject", tbSubject.ID,
                UniqueID, "*", "Subject is required", true, ValidatorDisplay.Dynamic);

            InitializeRequiredFieldValidator(ref rfvMessage, "rfvMessage", tbMessage.ID,
                UniqueID, "*", "Message is required", true, ValidatorDisplay.Dynamic);
            
            validators = new List<IValidator>();
            validators.Add(rfvToName);
            validators.Add(rfvToAddress);
            validators.Add(revToAddress);
            validators.Add(rfvFromName);
            validators.Add(rfvFromAddress);
            validators.Add(revFromAddress);
            validators.Add(rfvSubject);
            validators.Add(rfvMessage);

            #endregion
            
            #region "  Template Code  "

            if (GreetingTemplate != null)
            {
                greetingTemplateContainer = new GreetingTemplateContainer(this);
                GreetingTemplate.InstantiateIn(greetingTemplateContainer);
                InitalizerPlaceHolder(ref greetingPlaceHolder);
                greetingPlaceHolder.Controls.Add(greetingTemplateContainer);
            }
        
            if (ThankYouTemplate != null)
            {
                thankYouTemplateContainer = new ThankYouTemplateContainer(this);
                ThankYouTemplate.InstantiateIn(thankYouTemplateContainer);
                InitalizerPlaceHolder(ref thankYouPlaceHolder);
                thankYouPlaceHolder.Controls.Add(thankYouTemplateContainer);
            }

            #endregion

            #region "  Table Code  "

            // To name row
            toNameRow = CreateTableRow(lblToName, tbToName, rfvToName);
            contactFormTable.Rows.Add(toNameRow);

            // To address row
            toAddressRow = CreateTableRow(lblToAddress, tbToAddress, 
                rfvToAddress, revToAddress);
            toAddressRow.ID = "toAddressRow";
            contactFormTable.Rows.Add(toAddressRow);

            // From name row
            fromNameRow = CreateTableRow(lblFromName, tbFromName, rfvFromName);
            contactFormTable.Rows.Add(fromNameRow);

            // From address row
            fromAddressRow = CreateTableRow(lblFromAddress, tbFromAddress, 
                rfvFromAddress, revFromAddress);
            contactFormTable.Rows.Add(fromAddressRow);

            // Subject row
            TableRow subjectRow = CreateTableRow(lblSubject, tbSubject, rfvSubject);
            contactFormTable.Rows.Add(subjectRow);

            // Message header row
            TableRow messageLabelRow = new TableRow();
            TableCell messageLabelCell = new TableCell();
            messageLabelCell.ColumnSpan = 2;
            messageLabelCell.CssClass = "lblTop";
            messageLabelCell.Controls.Add(lblMessage);
            messageLabelCell.Controls.Add(rfvMessage);
            messageLabelRow.Cells.Add(messageLabelCell);
            contactFormTable.Rows.Add(messageLabelRow);

            // Message text row
            TableRow messageTextRow = new TableRow();
            TableCell messageTextCell = new TableCell();
            messageTextCell.ColumnSpan = 2;
            messageTextCell.CssClass = "ta";
            tbMessage.CssClass = "ta";
            tbMessage.Rows = 3;
            tbMessage.TextMode = TextBoxMode.MultiLine;
            messageTextCell.Controls.Add(tbMessage);
            messageTextRow.Cells.Add(messageTextCell);
            contactFormTable.Rows.Add(messageTextRow);

            // Button row
            btnSend.Click += new EventHandler(btnSend_Click);
            TableRow buttonRow = new TableRow();
            TableCell buttonCell = new TableCell();
            buttonCell.ColumnSpan = 2;
            buttonCell.CssClass = "btn";
            buttonCell.Controls.Add(btnSend);
            buttonRow.Cells.Add(buttonCell);
            contactFormTable.Rows.Add(buttonRow);

            #endregion
            
            if (!DesignMode)
            {
                _displayMode = ContactFormDisplayMode.Greeting;
            }

            // Add controls to hierarchy
            if (greetingPlaceHolder != null)
            {
                Controls.Add(greetingPlaceHolder);
            }
            Controls.Add(lblSendError);
            Controls.Add(validationSummary);
            Controls.Add(contactFormTable);
            if (thankYouPlaceHolder != null)
            {
                Controls.Add(thankYouPlaceHolder);
            }
            
            SetVisiblity();
            ChangeDisplayMode();
            
            ChildControlsCreated = true;
        }

        /// <summary>
        /// Creates a table row
        /// </summary>
        private TableRow CreateTableRow(Label label, TextBox textbox, params Control[] controls)
        {
            TableRow row = new TableRow();
            TableCell labelCell = new TableCell();
            labelCell.CssClass = "lbl";
            labelCell.Controls.Add(label);
            TableCell textBoxCell = new TableCell();
            textBoxCell.CssClass = "tb";
            textbox.CssClass = "tb";
            textBoxCell.Controls.Add(textbox);
            if (controls != null)
            {
                foreach (Control control in controls)
                {
                    if (control != null)
                    {
                        textBoxCell.Controls.Add(control);
                    }
                }
            }
            row.Cells.Add(labelCell);
            row.Cells.Add(textBoxCell);
            return row;
        }

        /// <summary>
        /// Sets visibility for various controls
        /// </summary>
        private void SetVisiblity()
        {
            if (toNameRow != null && toAddressRow != null)
            {
                toNameRow.Visible = ToVisible;
                toAddressRow.Visible = ToVisible;
            }
            
            if (fromNameRow != null && fromAddressRow != null)
            {
                fromNameRow.Visible = FromVisible;
                fromAddressRow.Visible = FromVisible;
            }
            
            if (Page != null && !Page.IsPostBack && !DesignMode)
            {
                if (thankYouPlaceHolder != null)
                {
                    thankYouPlaceHolder.Visible = false;
                }
            }
        }

        /// <summary>
        /// Updates display mode
        /// </summary>
        private void ChangeDisplayMode()
        {
            if (DisplayMode == ContactFormDisplayMode.Greeting)
            {
                contactFormTable.Visible = true;
                if (greetingPlaceHolder != null)
                {
                    greetingPlaceHolder.Visible = true;
                }
                if (thankYouPlaceHolder != null)
                {
                    thankYouPlaceHolder.Visible = false;
                }
                if (DesignMode && validationSummary != null)
                {
                    validationSummary.Visible = true;
                }
            }
            else if (DisplayMode == ContactFormDisplayMode.ThankYou)
            {
                contactFormTable.Visible = false;
                lblSendError.Visible = false;
                if (greetingPlaceHolder != null)
                {
                    greetingPlaceHolder.Visible = false;
                }
                if (thankYouPlaceHolder != null)
                {
                    thankYouPlaceHolder.Visible = true;
                }
                if (DesignMode && validationSummary != null)
                {
                    validationSummary.Visible = false;
                }
            }
        }
        
        #endregion

        #region "  Control Properties  "

        #region "  Input Properties  "

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Input"), Description("To name"), DefaultValue("")]
        public string ToNameText
        {
            get
            {
                EnsureChildControls();
                return tbToName.Text;
            }
            set
            {
                EnsureChildControls();
                tbToName.Text = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Input"), Description("To address"), DefaultValue("")]
        public string ToAddressText
        {
            get
            {
                EnsureChildControls();
                return tbToAddress.Text;
            }
            set
            {
                EnsureChildControls();
                tbToAddress.Text = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Input"), Description("From name"), DefaultValue("")]
        public string FromNameText
        {
            get
            {
                EnsureChildControls();
                return tbFromName.Text;
            }
            set
            {
                EnsureChildControls();
                tbFromName.Text = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Input"), Description("From address"), DefaultValue("")]
        public string FromAddressText
        {
            get
            {
                EnsureChildControls();
                return tbFromAddress.Text;
            }
            set
            {
                EnsureChildControls();
                tbFromAddress.Text = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Input"), Description("Subject"), DefaultValue("")]
        public string SubjectText
        {
            get
            {
                EnsureChildControls();
                return tbSubject.Text;
            }
            set
            {
                EnsureChildControls();
                tbSubject.Text = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Input"), Description("Message value"), DefaultValue("")]
        public string MessageText
        {
            get
            {
                EnsureChildControls();
                return tbMessage.Text;
            }
            set
            {
                EnsureChildControls();
                tbMessage.Text = value;
            }
        }
        
        #endregion

        #region "  Form Properties  "

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Form"), Description("Flag to show or hide To fields"), DefaultValue(true)]
        public bool ToVisible
        {
            get
            {
                EnsureChildControls();
                return _toVisible;
            }
            set
            {
                EnsureChildControls();
                _toVisible = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Form"), Description("Flag to show or hide From fields"), DefaultValue(true)]
        public bool FromVisible
        {
            get
            {
                EnsureChildControls();
                return _fromVisible;
            }
            set
            {
                EnsureChildControls();
                _fromVisible = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Form"), Description("Send button text"), DefaultValue("Send")]
        public string SendButtonText
        {
            get
            {
                EnsureChildControls();
                return btnSend.Text;
            }
            set
            {
                EnsureChildControls();
                btnSend.Text = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Form"), Description("Send failure text"), DefaultValue("Failed to send message")]
        public string SendFailureText
        {
            get
            {
                EnsureChildControls();
                return lblSendError.Text;
            }
            set
            {
                EnsureChildControls();
                lblSendError.Text = value;
            }
        }
        
        #endregion

        #region "  Label Properties  "

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Label"), Description("To name label"), DefaultValue("To Name: ")]
        public string ToNameLabelText
        {
            get
            {
                EnsureChildControls();
                return lblToName.Text;
            }
            set
            {
                EnsureChildControls();
                lblToName.Text = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Label"), Description("To address label"), DefaultValue("To Address: ")]
        public string ToAddressLabelText
        {
            get
            {
                EnsureChildControls();
                return lblToAddress.Text;
            }
            set
            {
                EnsureChildControls();
                lblToAddress.Text = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Label"), Description("From name label"), DefaultValue("From Name: ")]
        public string FromNameLabelText
        {
            get
            {
                EnsureChildControls();
                return lblFromName.Text;
            }
            set
            {
                EnsureChildControls();
                lblFromName.Text = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Label"), Description("From address label"), DefaultValue("From Address: ")]
        public string FromAddressLabelText
        {
            get
            {
                EnsureChildControls();
                return lblFromAddress.Text;
            }
            set
            {
                EnsureChildControls();
                lblFromAddress.Text = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Label"), Description("Subject label"), DefaultValue("Subject: ")]
        public string SubjectLabelText
        {
            get
            {
                EnsureChildControls();
                return lblSubject.Text;
            }
            set
            {
                EnsureChildControls();
                lblSubject.Text = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Label"), Description("Message label"), DefaultValue("Message: ")]
        public string MessageLabelText
        {
            get
            {
                EnsureChildControls();
                return lblMessage.Text;
            }
            set
            {
                EnsureChildControls();
                lblMessage.Text = value;
            }
        }

        #endregion

        #region "  Validator Properties  "

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Validation"), Description("To name required text"), DefaultValue("*")]
        public string ToNameRequiredText
        {
            get
            {
                EnsureChildControls();
                return rfvToName.Text;
            }
            set
            {
                EnsureChildControls();
                rfvToName.Text = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Validation"), Description("To address required text"), DefaultValue("*")]
        public string ToAddressRequiredText
        {
            get
            {
                EnsureChildControls();
                return rfvToAddress.Text;
            }
            set
            {
                EnsureChildControls();
                rfvToAddress.Text = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Validation"), Description("To address invalid format text"), DefaultValue("*")]
        public string ToAddressInvalidFormatText
        {
            get
            {
                EnsureChildControls();
                return revToAddress.Text;
            }
            set
            {
                EnsureChildControls();
                revToAddress.Text = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Validation"), Description("From name required text"), DefaultValue("*")]
        public string FromNameRequiredText
        {
            get
            {
                EnsureChildControls();
                return rfvFromName.Text;
            }
            set
            {
                EnsureChildControls();
                rfvFromName.Text = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Validation"), Description("From address required text"), DefaultValue("*")]
        public string FromAddressRequiredText
        {
            get
            {
                EnsureChildControls();
                return rfvFromAddress.Text;
            }
            set
            {
                EnsureChildControls();
                rfvFromAddress.Text = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Validation"), Description("From address invalid format text"), DefaultValue("*")]
        public string FromAddressInvalidFormatText
        {
            get
            {
                EnsureChildControls();
                return revFromAddress.Text;
            }
            set
            {
                EnsureChildControls();
                revFromAddress.Text = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Validation"), Description("Subject required text"), DefaultValue("*")]
        public string SubjectRequiredText
        {
            get
            {
                EnsureChildControls();
                return rfvSubject.Text;
            }
            set
            {
                EnsureChildControls();
                rfvSubject.Text = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Validation"), Description("Message required text"), DefaultValue("*")]
        public string MessageRequiredText
        {
            get
            {
                EnsureChildControls();
                return rfvMessage.Text;
            }
            set
            {
                EnsureChildControls();
                rfvMessage.Text = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Validation"), Description("To name required error message"), DefaultValue("To name is required")]
        public string ToNameRequiredErrorMessage
        {
            get
            {
                EnsureChildControls();
                return rfvToName.ErrorMessage;
            }
            set
            {
                EnsureChildControls();
                rfvToName.ErrorMessage = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Validation"), Description("To address required error message"), DefaultValue("To address is required")]
        public string ToAddressRequiredErrorMessage
        {
            get
            {
                EnsureChildControls();
                return rfvToAddress.ErrorMessage;
            }
            set
            {
                EnsureChildControls();
                rfvToAddress.ErrorMessage = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Validation"), Description("To address invalid format error message"), DefaultValue("To address must be an email address")]
        public string ToAddressInvalidFormatErrorMessage
        {
            get
            {
                EnsureChildControls();
                return revToAddress.ErrorMessage;
            }
            set
            {
                EnsureChildControls();
                revToAddress.ErrorMessage = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Validation"), Description("From name required error message"), DefaultValue("From name is required")]
        public string FromNameRequiredErrorMessage
        {
            get
            {
                EnsureChildControls();
                return rfvFromName.ErrorMessage;
            }
            set
            {
                EnsureChildControls();
                rfvFromName.ErrorMessage = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Validation"), Description("From address required error message"), DefaultValue("From address is required")]
        public string FromAddressRequiredErrorMessage
        {
            get
            {
                EnsureChildControls();
                return rfvFromAddress.ErrorMessage;
            }
            set
            {
                EnsureChildControls();
                rfvFromAddress.ErrorMessage = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Validation"), Description("From address invalid format error message"), DefaultValue("From address must be an email address")]
        public string FromAddressInvalidFormatErrorMessage
        {
            get
            {
                EnsureChildControls();
                return revFromAddress.ErrorMessage;
            }
            set
            {
                EnsureChildControls();
                revFromAddress.ErrorMessage = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Validation"), Description("Subject required error message"), DefaultValue("Subject is required")]
        public string SubjectRequiredErrorMessage
        {
            get
            {
                EnsureChildControls();
                return rfvSubject.ErrorMessage;
            }
            set
            {
                EnsureChildControls();
                rfvSubject.ErrorMessage = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Validation"), Description("Message required error message"), DefaultValue("Message is required")]
        public string MessageRequiredErrorMessage
        {
            get
            {
                EnsureChildControls();
                return rfvMessage.ErrorMessage;
            }
            set
            {
                EnsureChildControls();
                rfvMessage.ErrorMessage = value;
            }
        }
        
        #endregion

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(false), DefaultValue(ContactFormDisplayMode.Greeting)]
        public ContactFormDisplayMode DisplayMode
        {
            get
            {
                EnsureChildControls();
                return _displayMode;
            }
            set
            {
                EnsureChildControls();
                _displayMode = value;
                ChangeDisplayMode();
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(false),
        DefaultValue(null), 
        PersistenceMode(PersistenceMode.InnerProperty),
        TemplateContainer(typeof(GreetingTemplateContainer))]
        public virtual ITemplate GreetingTemplate
        {
            get
            {
                return greetingTemplate;
            }
            set
            {
                greetingTemplate = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(false),
        DefaultValue(null), 
        PersistenceMode(PersistenceMode.InnerProperty),
        TemplateContainer(typeof(ThankYouTemplateContainer))]
        public virtual ITemplate ThankYouTemplate
        {
            get
            {
                return thankYouTemplate;
            }
            set
            {
                thankYouTemplate = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        public bool IsValid
        {
            get
            {
                Validate();
                return _isValid;
            }
            set
            {
                _isValid = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        #endregion
    
    }

    #region "  Helper Classes  "
    
    internal class ContactFormDesigner : CompositeControlDesigner
    {
        DesignerActionListCollection actionLists = null;
        private ContactForm _cf = null;

        #region "  Designer Methods  "

        /// <summary>
        /// Override method
        /// </summary>
        public override void Initialize(IComponent component)
        {
            _cf = component as ContactForm;
            base.Initialize(component);
            SetViewFlags(ViewFlags.TemplateEditing, true);
        }

        private TemplateGroupCollection _templateGroups = null;
        /// <summary>
        /// Property
        /// </summary>
        public override TemplateGroupCollection TemplateGroups
        {
            get
            {
                if (_templateGroups == null)
                {
                    _templateGroups = new TemplateGroupCollection();
                    TemplateGroup templateGroup = new TemplateGroup("Surrounding Templates");

                    TemplateDefinition greetingDefinition =
                        new TemplateDefinition(this, "Greeting Template", _cf, "GreetingTemplate", false);
                    templateGroup.AddTemplateDefinition(greetingDefinition);
                    
                    TemplateDefinition thankYouDefinition =
                        new TemplateDefinition(this, "Thank You Template", _cf, "ThankYouTemplate", false);
                    templateGroup.AddTemplateDefinition(thankYouDefinition);
                    
                    _templateGroups.Add(templateGroup);
                }
                return _templateGroups;
            }
        }

        /// <summary>
        /// Override method
        /// </summary>
        public override string GetDesignTimeHtml()
        {
            string markup; 
            
            string originalToAddress = String.Empty;
            string originalFromAddress = String.Empty;
            string originalToName = String.Empty;
            string originalFromName = String.Empty;
            string originalSubject = String.Empty;
            string originalMessage = String.Empty;

            if (_cf != null)
            {
                originalToAddress = _cf.ToAddressText;
                originalFromAddress = _cf.FromAddressText;
                originalToName = _cf.ToNameText;
                originalFromName = _cf.FromNameText;
                originalSubject = _cf.SubjectText;
                originalMessage = _cf.MessageText;
            }
            
            try
            {
                if (_cf != null)
                {

                    // dress it up with some fake values

                    if (String.IsNullOrEmpty(_cf.ToNameText))
                    {
                        _cf.ToNameText = "John Doe";
                    }

                    if (String.IsNullOrEmpty(_cf.ToAddressText))
                    {
                        _cf.ToAddressText = "john.doe@someplace.com";
                    }

                    if (String.IsNullOrEmpty(_cf.FromNameText))
                    {
                        _cf.FromNameText = "Jane Doe";
                    }

                    if (String.IsNullOrEmpty(_cf.FromAddressText))
                    {
                        _cf.FromAddressText = "jane.doe@someplace.com";
                    }

                    if (String.IsNullOrEmpty(_cf.SubjectText))
                    {
                        _cf.SubjectText = "Information Request";
                    }

                    if (String.IsNullOrEmpty(_cf.MessageText))
                    {
                        _cf.MessageText = "Please contact me";
                    }

                    string style = "style='color: #000; padding: 3px; font-size: 10px; font-family: arial, san-serif; " +
                                   "background: AntiqueWhite; width: auto; padding: 3px; border: 1px solid #999;'";
                    string template = "<div " + style + ">{0}: {1}</div>{2}";

                    try
                    {
                        string name = _cf.GetType().Name;
                        string siteName = _cf.Site.Name;
                        markup = String.Format(template, name, siteName, base.GetDesignTimeHtml());
                    }
                    catch (Exception ex)
                    {
                        markup = "<p style='background: #ccc; color: #900; font-weight: bold;'>Error: " +
                         ex.Message + "</p>\n<div>" +
                         ex.StackTrace + "</div>";
                    }
                }
                else
                {
                    markup = base.GetDesignTimeHtml();
                }
            }
            catch (Exception ex)
            {
                markup = "<p style='background: #ccc; color: #900; font-weight: bold;'>Error: " +
                         ex.Message + "</p>\n<div>" +
                         ex.StackTrace + "</div>";
            }
            finally
            {
                _cf.ToNameText = originalToName;
                _cf.ToAddressText = originalToAddress;
                _cf.FromNameText = originalFromName;
                _cf.FromAddressText = originalFromAddress;
                _cf.SubjectText = originalSubject;
                _cf.MessageText = originalMessage;
            }

            return markup;
        }

        /// <summary>
        /// Property
        /// </summary>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (actionLists == null)
                {
                    actionLists = new DesignerActionListCollection();
                    ContactForm cf = Component as ContactForm;
                    if (cf != null)
                    {
                        actionLists.Add(new ContactFormActionList(cf));
                    }
                }
                return actionLists;
            }
        }
        
        #endregion
    }

    internal class ContactFormActionList : DesignerActionList
    {
        private ContactForm _cf = null;

        #region "  Action List Constructor  "

        /// <summary>
        /// Contructor
        /// </summary>
        public ContactFormActionList(IComponent component)
            : base(component)
        {
             _cf = component as ContactForm;
         }

        #endregion

        #region "  Action List Methods  "

         /// <summary>
         /// Launches support website
         /// </summary>
        public void LaunchWebsite()
        {
            Process.Start("http://www.smallsharptools.com/");
        }

        /// <summary>
        /// Override method
        /// </summary>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection actionItems = new DesignerActionItemCollection();

            actionItems.Add(new DesignerActionHeaderItem("Form"));
            actionItems.Add(new DesignerActionHeaderItem("Support"));

            actionItems.Add(new DesignerActionPropertyItem("DisplayMode", "Display Mode", "Form"));
            actionItems.Add(new DesignerActionPropertyItem("ToVisible", "To Visible", "Form"));
            actionItems.Add(new DesignerActionPropertyItem("FromVisible", "From Visible", "Form"));
            actionItems.Add(new DesignerActionPropertyItem("ToNameText", "To Name", "Form"));
            actionItems.Add(new DesignerActionPropertyItem("ToAddressText", "To Address", "Form"));
            actionItems.Add(new DesignerActionPropertyItem("FromNameText", "From Name", "Form"));
            actionItems.Add(new DesignerActionPropertyItem("FromAddressText", "From Address", "Form"));
            actionItems.Add(new DesignerActionPropertyItem("SubjectText", "Subject", "Form"));
            
            actionItems.Add(new DesignerActionMethodItem(this, "LaunchWebsite", "SmallSharpTools.com", "Support"));

            return actionItems;
        }

        /// <summary>
        /// Override method
        /// </summary>
        private PropertyDescriptor GetControlProperty(string propertyName)
        {
            PropertyDescriptor pd = TypeDescriptor.GetProperties(_cf)[propertyName];
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

        #region "  Action List Properties  "

        /// <summary>
        /// Property
        /// </summary>
        public string ToNameText
        {
            get
            {

                return _cf.ToNameText;
            }
            set
            {
                GetControlProperty("ToNameText").SetValue(_cf, value);
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        public string ToAddressText
        {
            get
            {

                return _cf.ToAddressText;
            }
            set
            {
                GetControlProperty("ToAddressText").SetValue(_cf, value);
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        public bool ToVisible
        {
            get
            {

                return _cf.ToVisible;
            }
            set
            {
                GetControlProperty("ToVisible").SetValue(_cf, value);
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        public string FromNameText
        {
            get
            {

                return _cf.FromNameText;
            }
            set
            {
                GetControlProperty("FromNameText").SetValue(_cf, value);
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        public string FromAddressText
        {
            get
            {

                return _cf.FromAddressText;
            }
            set
            {
                GetControlProperty("FromAddressText").SetValue(_cf, value);
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        public bool FromVisible
        {
            get
            {

                return _cf.FromVisible;
            }
            set
            {
                GetControlProperty("FromVisible").SetValue(_cf, value);
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        public string SubjectText
        {
            get
            {
                return _cf.SubjectText;
            }
            set
            {
                GetControlProperty("SubjectText").SetValue(_cf, value);
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        public ContactFormDisplayMode DisplayMode
        {
            get
            {
                return _cf.DisplayMode;
            }
            set
            {
                GetControlProperty("DisplayMode").SetValue(_cf, value);
            }
        }
        
        #endregion

    }
    
    internal class GreetingTemplateContainer : WebControl, INamingContainer
    {
        //private ContactForm _parent;

        /// <summary>
        /// Constructor
        /// </summary>
        public GreetingTemplateContainer(ContactForm parent)
        {
            //_parent = parent;
        }
    }
    
    internal class ThankYouTemplateContainer : WebControl, INamingContainer
    {
        //private ContactForm _parent;

        /// <summary>
        /// Constructor
        /// </summary>
        public ThankYouTemplateContainer(ContactForm parent)
        {
            //_parent = parent;
        }
    }

    #endregion
    
}
