using System;
using System.Drawing;


namespace Acr.UserDialogs
{
    public interface IDialogConfig
    {
        string Title { get; set; }
        string Message { get; set; }
        bool IsCancellable { get; set; }
        bool DisableImmediateShow { get; set; }
        Color? BackgroundColor { get; set; }
        Color? MessageTextColor { get; set; }
    }
}
