using System;
using System.Threading;
using System.Threading.Tasks;
using Splat;


namespace Acr.UserDialogs
{

    public interface IUserDialogs
    {
        IAlertDialog CreateDialog();

        IAlertDialog Alert(string message, string title = null, string okText = null, Action<DialogChoice> action = null);
        IAlertDialog Alert(AlertConfig config);
        Task AlertAsync(string message, string title = null, string okText = null, CancellationToken? cancelToken = null);
        Task AlertAsync(AlertConfig config, CancellationToken? cancelToken = null);

        IAlertDialog ActionSheet(ActionSheetConfig config);
        Task<string> ActionSheetAsync(string title, string cancel, string destructive, CancellationToken? cancelToken = null, params string[] buttons);

        // TODO: take out of actionsheet, can switch to grids and other things
        //IAlertDialog BottomSheet()

        IAlertDialog Confirm(ConfirmConfig config);
        Task<bool> ConfirmAsync(string message, string title = null, string okText = null, string cancelText = null, CancellationToken? cancelToken = null);
        Task<bool> ConfirmAsync(ConfirmConfig config, CancellationToken? cancelToken = null);

        IDisposable DatePrompt(DatePromptConfig config);
        Task<DialogResult<DateTime>> DatePromptAsync(DatePromptConfig config, CancellationToken? cancelToken = null);
        Task<DialogResult<DateTime>> DatePromptAsync(string title = null, DateTime? selectedDate = null, CancellationToken? cancelToken = null);

        IDisposable TimePrompt(TimePromptConfig config);
        Task<DialogResult<TimeSpan>> TimePromptAsync(TimePromptConfig config, CancellationToken? cancelToken = null);
        Task<DialogResult<TimeSpan>> TimePromptAsync(string title = null, TimeSpan? selectedTime = null, CancellationToken? cancelToken = null);

        IAlertDialog Prompt(PromptConfig config);
        Task<DialogResult<string>> PromptAsync(string message, string title = null, string okText = null, string cancelText = null, string placeholder = "", KeyboardType inputType = KeyboardType.Default, CancellationToken? cancelToken = null);
        Task<DialogResult<string>> PromptAsync(PromptConfig config, CancellationToken? cancelToken = null);

        IAlertDialog Login(LoginConfig config);
        Task<DialogResult<Credentials>> LoginAsync(string title = null, string message = null, CancellationToken? cancelToken = null);
        Task<DialogResult<Credentials>> LoginAsync(LoginConfig config, CancellationToken? cancelToken = null);

        IProgressDialog Progress(ProgressDialogConfig config);
        IProgressDialog Loading(string title = null, Action onCancel = null, string cancelText = null, bool show = true, MaskType? maskType = null);
        IProgressDialog Progress(string title = null, Action onCancel = null, string cancelText = null, bool show = true, MaskType? maskType = null);

        void ShowImage(IBitmap image, string message, int timeoutMillis = 2000);

        IDisposable Snackbar(string message, TimeSpan? dismissTimer = null, ScreenPosition position = ScreenPosition.Bottom);
        IDisposable Snackbar(SnackbarConfig config);

        IDisposable Toast(string message, TimeSpan? dismissTimer = null, ScreenPosition position = ScreenPosition.Bottom);
        IDisposable Toast(ToastConfig cfg);
    }
}