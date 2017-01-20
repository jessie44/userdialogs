using System;


namespace Acr.UserDialogs
{
    public class PromptConfig : AbstractDialogConfig
    {
        public Action<DialogResult<string>> OnAction { get; set; }
        public static string DefaultOkText { get; set; } = "Ok";
        public static string DefaultCancelText { get; set; } = "Cancel";
        //public static int? DefaultAndroidStyleId { get; set; }
        public static int? DefaultMaxLength { get; set; }

        public string Text { get; set; }
        public string OkText { get; set; } = DefaultOkText;
        public string CancelText { get; set; } = DefaultCancelText;
        //public string NegativeText { get; set; }
        public string Placeholder { get; set; }
        public int? MaxLength { get; set; } = DefaultMaxLength;
        //public int? AndroidStyleId { get; set; } = DefaultAndroidStyleId;
        public KeyboardType InputType { get; set; } = KeyboardType.Default;
        public Action<PromptTextChangedArgs> OnTextChanged { get; set; }
    }
}
