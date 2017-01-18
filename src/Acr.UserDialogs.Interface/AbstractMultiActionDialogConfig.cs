using System;


namespace Acr.UserDialogs
{
    public abstract class AbstractMultiActionDialogConfig : AbstractDialogConfig
    {
        public DialogAction Positive { get; set; }
        public DialogAction Neutral { get; set; }
        public DialogAction Negative { get; set; }
    }
}
