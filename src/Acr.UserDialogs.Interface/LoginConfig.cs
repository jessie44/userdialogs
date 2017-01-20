using System;


namespace Acr.UserDialogs
{
    public class LoginConfig : AbstractDialogConfig
    {
        public LoginConfig()
        {
            this.Title = DefaultTitle;
        }


        public static string DefaultTitle { get; set; } = "Login";
        public static string DefaultOkText { get; set; } = "Ok";
        public static string DefaultCancelText { get; set; } = "Cancel";
        public static string DefaultLoginPlaceholder { get; set; } = "User Name";
        public static string DefaultPasswordPlaceholder { get; set; } = "Password";
        //public static int? DefaultAndroidStyleId { get; set; }

        public string OkText { get; set; } = DefaultOkText;
        public string CancelText { get; set; } = DefaultCancelText;
        public string LoginValue { get; set; }
        public string LoginPlaceholder { get; set; } = DefaultLoginPlaceholder;
        public string PasswordPlaceholder { get; set; } = DefaultPasswordPlaceholder;
        //public int? AndroidStyleId { get; set; } = DefaultAndroidStyleId;

        public Action<DialogResult<Credentials>> OnAction { get; set; }
    }
}