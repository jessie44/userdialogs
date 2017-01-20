using System;
using System.Collections.Generic;
using System.Drawing;


namespace Acr.UserDialogs
{
    public interface IAlertDialog : IDisposable
    {
        // TODO: IsShown, AndroidStyleId

        string Title { get; set; }
        string Message { get; set; }
        bool IsCancellable { get; set; }

        Color? MessageTextColor { get; set; }
        Color? BackgroundColor { get; set; }

        DialogAction Positive { get; set; }
        DialogAction Neutral { get; set; }
        DialogAction Negative { get; set; }

        IReadOnlyList<DialogAction> Actions { get; }
        IReadOnlyList<TextEntry> TextEntries { get; }

        IAlertDialog Add(TextEntry instance);
        IAlertDialog Add(DialogAction action);

        bool IsVisible { get; }
        void Show();
        void Hide();

        // TODO: what about outside cancel?
        Action Dismissed { get; set; }
    }
}
