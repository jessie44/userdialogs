using System;


namespace Acr.UserDialogs
{

    public class ConfirmConfig : AbstractDialogConfig
    {
        //public static string DefaultYes { get; set; } = "Yes";
        //public static string DefaultNo { get; set; } = "No";
        //public static string DefaultOkText { get; set; } = "Ok";
        //public static string DefaultCancelText { get; set; } = "Cancel";
        public DialogAction Accept { get; set; }
        public DialogAction Decline {  get; set; }
        public Action<bool> OnAction { get; set; }

        //public ConfirmConfig UseYesNo()
        //{
        //    this.OkText = DefaultYes;
        //    this.CancelText = DefaultNo;
        //    return this;
        //}
    }
}
