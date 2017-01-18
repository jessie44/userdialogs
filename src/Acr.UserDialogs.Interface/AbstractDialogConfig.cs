using System;
using System.Drawing;


namespace Acr.UserDialogs
{
    public abstract class AbstractDialogConfig : IDialogConfig
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsCancellable { get; set; }
        public Color? MessageTextColor { get; set; }
        public Color? BackgroundColor { get; set; }
    }
}
