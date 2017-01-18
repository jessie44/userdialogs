using System;


namespace Acr.UserDialogs
{
    public class AlertConfig : AbstractDialogConfig
    {
        public DialogAction OkButton { get; set; }
        public Action OnAction { get; set; }
    }
}
