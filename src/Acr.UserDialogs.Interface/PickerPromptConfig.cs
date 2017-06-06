using System;
using System.Collections.Generic;
using System.Text;

namespace Acr.UserDialogs
{
    public class PickerPromptConfig
    {
        public static string DefaultTitle { get; set; } = "Picker";
        public static string DefaultOkText { get; set; } = "Ok";
        public static string DefaultCancelText { get; set; } = "Cancel";
        public IList<string> PickerList { get; set; }
        public Action<PickerPromptResult> OnAction { get; set; }
        public string Title { get; set; } = DefaultTitle;
        public string OkText { get; set; } = DefaultOkText;
        public string CancelText { get; set; } = DefaultCancelText;
    }
}