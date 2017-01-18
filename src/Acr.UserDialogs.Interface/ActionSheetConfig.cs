using System;
using System.Collections.Generic;


namespace Acr.UserDialogs
{
    public class ActionSheetConfig : AbstractMultiActionDialogConfig
    {
        public IList<DialogAction> Actions { get; set; } = new List<DialogAction>();
    }
}
