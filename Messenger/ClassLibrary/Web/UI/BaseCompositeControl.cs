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
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace SmallSharpTools.Messenger.Web.UI
{
    /// <summary>
    /// Base class to hold common helper methods for CompositeControl
    /// </summary>
    [Browsable(false)]
    public class BaseCompositeControl : CompositeControl
    {

        /// <summary>
        /// Init routine
        /// </summary>
        protected virtual void InitializeLabel(ref Label lbl, string text)
        {
            if (lbl == null)
            {
                lbl = new Label();
            }
            if (String.IsNullOrEmpty(lbl.Text))
            {
                lbl.Text = text;
            }
        }

        /// <summary>
        /// Init routine
        /// </summary>
        protected virtual void InitializeTextBox(ref TextBox tb, string controlId, string validationGroup)
        {
            if (tb == null)
            {
                tb = new TextBox();
            }
            tb.ID = controlId;
            tb.ValidationGroup = validationGroup;
        }

        /// <summary>
        /// Init routine
        /// </summary>
        protected virtual void InitializeRequiredFieldValidator(ref RequiredFieldValidator rfv,
            string controlId,
            string controlToValidate,
            string validationGroup, 
            string text,
            string errMessage,
            bool enableClientScript,
            ValidatorDisplay validatorDisplay)
        {
            if (rfv == null)
            {
                rfv = new RequiredFieldValidator();
                rfv.ID = controlId;
                rfv.ControlToValidate = controlToValidate;
                rfv.ValidationGroup = validationGroup;
                rfv.Text = text;
                rfv.ErrorMessage = errMessage;
                rfv.EnableClientScript = enableClientScript;
                rfv.Display = ValidatorDisplay.Static;
            }
        }

        /// <summary>
        /// Init routine
        /// </summary>
        protected virtual void InitializeRegularExpressionValidator(ref RegularExpressionValidator rev,
            string controlId,
            string controlToValidate,
            string validationExpression,
            string validationGroup,
            string text, 
            string errMessage,
            bool enableClientScript,
            ValidatorDisplay validatorDisplay)
        {
            if (rev == null)
            {
                rev = new RegularExpressionValidator();
                rev.ID = controlId;
                rev.ControlToValidate = controlToValidate;
                rev.ValidationExpression = validationExpression;
                rev.ValidationGroup = validationGroup;
                rev.Text = text;
                rev.ErrorMessage = errMessage;
                rev.EnableClientScript = enableClientScript;
            }
        }

        /// <summary>
        /// Init routine
        /// </summary>
        protected virtual void InitalizerPlaceHolder(ref PlaceHolder ph)
        {
            if (ph == null)
            {
                ph = new PlaceHolder();
            }
            ph.Controls.Clear();
        }
        
    }
}
