using System;


namespace Acr.UserDialogs
{
    public class AlertConfig : AbstractDialogConfig
    {
        public static string DefaultOkText { get; set; } = "Ok";
        //public static int? DefaultAndroidStyleId { get; set; }

        public string OkText { get; set; } = DefaultOkText;
        //public int? AndroidStyleId { get; set; } = DefaultAndroidStyleId;
        public Action OnAction { get; set; }
    }
}
