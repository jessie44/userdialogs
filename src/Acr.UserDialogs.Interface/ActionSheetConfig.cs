using System;
using System.Collections.Generic;


namespace Acr.UserDialogs
{
    public class ActionSheetConfig : AbstractDialogConfig
    {
        public DialogAction PositiveButton { get; set; }
        public DialogAction NeutralButton { get; set; }
        public DialogAction NegativeButton { get; set; }
        public IList<DialogAction> Actions { get; set; } = new List<DialogAction>();
    }
}
