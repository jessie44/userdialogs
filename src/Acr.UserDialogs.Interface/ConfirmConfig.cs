using System;


namespace Acr.UserDialogs
{

    public class ConfirmConfig : AbstractDialogConfig
    {
        public static string DefaultYes { get; set; } = "Yes";
        public static string DefaultNo { get; set; } = "No";
        public static string DefaultOkText { get; set; } = "Ok";
        public static string DefaultCancelText { get; set; } = "Cancel";
        //public static int? DefaultAndroidStyleId { get; set; }

        //public int? AndroidStyleId { get; set; } = DefaultAndroidStyleId;
        public Action<bool> OnAction { get; set; }

        public string OkText { get; set; } = DefaultOkText;
        public string CancelText { get; set; } = DefaultCancelText;


        public ConfirmConfig UseYesNo()
        {
            this.OkText = DefaultYes;
            this.CancelText = DefaultNo;
            return this;
        }
    }
}
