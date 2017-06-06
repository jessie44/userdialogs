using System;
using System.Collections.Generic;
using System.Text;

namespace Acr.UserDialogs
{
    public class PickerPromptResult : AbstractStandardDialogResult<string>
    {
        public PickerPromptResult(bool ok, string value) : base(ok, value)
        {
        }

        public string SelectedValue => this.Value;
    }
}