using System;
using System.Threading;
using System.Threading.Tasks;
using Splat;


namespace Acr.UserDialogs
{
    public abstract class AbstractUserDialogs : IUserDialogs
    {
        const string NO_ONACTION = "OnAction should not be set as async will not use it";
        const string NO_ONACTION_SET = "You must set the OnAction of your config object";

        protected abstract IProgressDialog CreateDialogInstance(ProgressDialogConfig config);
        public abstract IAlertDialog CreateDialog();
        public abstract IDisposable DatePrompt(DatePromptConfig config);
        public abstract IDisposable TimePrompt(TimePromptConfig config);
        public abstract IDisposable Toast(ToastConfig config);
        public abstract void ShowImage(IBitmap image, string message, int timeoutMillis);


        #region Internal Helpers

        protected virtual IAlertDialog CreateDialogFromConfig(IDialogConfig config)
        {
            var dlg = this.CreateDialog();
            dlg.Title = config.Title;
            dlg.Message = config.Message;
            dlg.MessageTextColor = config.MessageTextColor;
            dlg.BackgroundColor = config.BackgroundColor;
            dlg.IsCancellable = config.IsCancellable;

            return dlg;
        }


        protected static void Cancel<TResult>(IDisposable disp, TaskCompletionSource<TResult> tcs)
        {
            disp.Dispose();
            tcs.TrySetCanceled();
        }

        #endregion

        #region Toasts/Snackbars

        public virtual IDisposable Toast(string message, TimeSpan? dismissTimer, ScreenPosition screenPosition)
        {
            return this.Toast(new ToastConfig(message)
            {
                Duration = dismissTimer ?? ToastConfig.DefaultDuration,
                Position = screenPosition
            });
        }


        public virtual IDisposable Snackbar(SnackbarConfig config)
        {
            // Transform to toast as UWP, tvOS, and macOS won't have a snackbar?
            return this.Toast(new ToastConfig(config.Message)
            {
                Duration = config.Duration,
                Action = () => config.ActionButton?.Command?.Execute(null)
            });
        }


        public IDisposable Snackbar(string message, TimeSpan? dismissTimer = null, ScreenPosition position = ScreenPosition.Bottom)
        {
            return this.Snackbar(new SnackbarConfig(message)
            {
                Duration = dismissTimer ?? SnackbarConfig.DefaultDuration,
                Position = position
            });
        }


        #endregion

        #region Progress Dialogs

        IProgressDialog loading;
        public virtual void ShowLoading(string title, MaskType? maskType)
        {
            if (this.loading != null)
                this.HideLoading();

            this.loading = this.Loading(title, null, null, true, maskType);
        }


        public virtual void HideLoading()
        {
            this.loading?.Dispose();
            this.loading = null;
        }


        public virtual IProgressDialog Loading(string title, Action onCancel, string cancelText, bool show, MaskType? maskType)
        {
            return this.Progress(new ProgressDialogConfig
            {
                Title = title ?? ProgressDialogConfig.DefaultTitle,
                AutoShow = show,
                CancelText = cancelText ?? ProgressDialogConfig.DefaultCancelText,
                MaskType = maskType ?? ProgressDialogConfig.DefaultMaskType,
                IsDeterministic = false,
                OnCancel = onCancel
            });
        }


        public virtual IProgressDialog Progress(string title, Action onCancel, string cancelText, bool show, MaskType? maskType)
        {
            return this.Progress(new ProgressDialogConfig
            {
                Title = title ?? ProgressDialogConfig.DefaultTitle,
                AutoShow = show,
                CancelText = cancelText ?? ProgressDialogConfig.DefaultCancelText,
                MaskType = maskType ?? ProgressDialogConfig.DefaultMaskType,
                IsDeterministic = true,
                OnCancel = onCancel
            });
        }


        public virtual IProgressDialog Progress(ProgressDialogConfig config)
        {
            var dlg = this.CreateDialogInstance(config);
            dlg.Title = config.Title;

            if (config.AutoShow)
                dlg.Show();

            return dlg;
        }

        #endregion

        #region ActionSheets

        public virtual IAlertDialog ActionSheet(ActionSheetConfig config)
        {
            var dlg = this.CreateDialogFromConfig(config);
            foreach (var action in config.Actions)
            {
                dlg.Add(action);
            }
            return dlg;
        }


        public virtual async Task<string> ActionSheetAsync(string title, string cancel, string destructive, CancellationToken? cancelToken = null, params string[] buttons)
        {
            var tcs = new TaskCompletionSource<string>();
            var cfg = new ActionSheetConfig();
            if (title != null)
                cfg.Title = title;

            var disp = this.ActionSheet(cfg);
            using (cancelToken?.Register(() => Cancel(disp, tcs)))
            {
                return await tcs.Task;
            }
        }

        #endregion

        #region Alerts

        public virtual IAlertDialog Alert(AlertConfig config)
        {
            if (config.OnAction == null)
                throw new NullReferenceException(NO_ONACTION_SET);

            var dlg = this.CreateDialogFromConfig(config);
            if (!config.DisableImmediateShow)
                dlg.Show();

            return dlg;
        }


        protected virtual AlertConfig ToAlertConfig(string message, string title, string okText, Action onOk)
        {
            return new AlertConfig
            {
                Title = title ?? String.Empty,
                Message = message ?? String.Empty,
                OkText = okText ?? AlertConfig.DefaultOkText,
                OnAction = onOk
            };
        }


        public virtual IAlertDialog Alert(string message, string title, string okText, Action onOk)
        {
            return this.Alert(this.ToAlertConfig(message, title, okText, onOk));
        }


        public virtual async Task AlertAsync(AlertConfig config, CancellationToken? cancelToken = null)
        {
            if (config.OnAction != null)
                throw new ArgumentException(NO_ONACTION);

            var tcs = new TaskCompletionSource<object>();
            config.OnAction = () => tcs.TrySetResult(null);

            var disp = this.Alert(config);
            using (cancelToken?.Register(() => Cancel(disp, tcs)))
            {
                await tcs.Task;
            }
        }


        public virtual Task AlertAsync(string message, string title, string okText, CancellationToken? cancelToken = null)
        {
            return this.AlertAsync(this.ToAlertConfig(message, title, okText, null), cancelToken);
        }

        #endregion

        #region Confirm

        protected virtual ConfirmConfig ToConfirmConfig(string message, Action<bool> onAction, string title, string okText, string cancelText)
        {
            return new ConfirmConfig
            {
                Message = message,
                Title = title,
                OnAction = onAction,
                OkText = okText ?? ConfirmConfig.DefaultOkText,
                CancelText = cancelText ?? ConfirmConfig.DefaultCancelText
            };
        }


        public virtual IAlertDialog Confirm(ConfirmConfig config)
        {
            if (config.OnAction == null)
                throw new ArgumentNullException(NO_ONACTION_SET);

            var dlg = this.CreateDialogFromConfig(config);
            dlg.IsCancellable = false; // TODO: allow config?

            if (!config.DisableImmediateShow)
                dlg.Show();

            return dlg;
        }


        public virtual IAlertDialog Confirm(string message, Action<bool> onAction, string title, string okText, string cancelText)
        {
            return this.Confirm(this.ToConfirmConfig(message, onAction, title, okText, cancelText));
        }


        public virtual async Task<bool> ConfirmAsync(string message, string title, string okText, string cancelText, CancellationToken? cancelToken = null)
        {
            var tcs = new TaskCompletionSource<bool>();
            var dlg = this.Confirm(message, x => tcs.TrySetResult(x), title, okText, cancelText);
            using (cancelToken?.Register(dlg.Hide))
            {
                return await tcs.Task;
            }
        }


        public virtual async Task<bool> ConfirmAsync(ConfirmConfig config, CancellationToken? cancelToken)
        {
            if (config.OnAction != null)
                throw new ArgumentNullException(NO_ONACTION);

            var tcs = new TaskCompletionSource<bool>();
            config.OnAction = x => tcs.TrySetResult(x);
            var dlg = this.Confirm(config);

            using (cancelToken?.Register(() => Cancel(dlg, tcs)))
            {
                return await tcs.Task;
            }
        }

        #endregion

        #region Date/Time

        public virtual async Task<DialogResult<DateTime>> DatePromptAsync(DatePromptConfig config, CancellationToken? cancelToken = null)
        {
            if (config.OnAction != null)
                throw new ArgumentException(NO_ONACTION);

            var tcs = new TaskCompletionSource<DialogResult<DateTime>>();
            config.OnAction = x => tcs.TrySetResult(x);

            var disp = this.DatePrompt(config);
            using (cancelToken?.Register(() => Cancel(disp, tcs)))
            {
                return await tcs.Task;
            }
        }


        public virtual Task<DialogResult<DateTime>> DatePromptAsync(string title, DateTime? selectedDate, CancellationToken? cancelToken = null)
        {
            return this.DatePromptAsync(
                new DatePromptConfig
                {
                    Title = title,
                    SelectedDate = selectedDate
                },
                cancelToken
            );
        }


        public virtual async Task<DialogResult<TimeSpan>> TimePromptAsync(TimePromptConfig config, CancellationToken? cancelToken = null)
        {
            if (config.OnAction != null)
                throw new ArgumentException(NO_ONACTION);

            var tcs = new TaskCompletionSource<DialogResult<TimeSpan>>();
            config.OnAction = x => tcs.TrySetResult(x);

            var disp = this.TimePrompt(config);
            using (cancelToken?.Register(() => Cancel(disp, tcs)))
            {
                return await tcs.Task;
            }
        }


        public virtual Task<DialogResult<TimeSpan>> TimePromptAsync(string title, TimeSpan? selectedTime, CancellationToken? cancelToken = null)
        {
            return this.TimePromptAsync(
                new TimePromptConfig
                {
                    Title = title,
                    SelectedTime = selectedTime
                },
                cancelToken
            );
        }

        #endregion

        #region Login

        public virtual IAlertDialog Login(LoginConfig config)
        {
            if (config.OnAction == null)
                throw new ArgumentNullException(NO_ONACTION_SET);

            var dlg = this.CreateDialogFromConfig(config);
            dlg.IsCancellable = false; // TODO: configurable?

            var txtLogin = new TextEntry
            {
                Placeholder = config.LoginPlaceholder ?? String.Empty,
                Text = config.LoginValue
            };
            var txtPassword = new TextEntry
            {
                Placeholder = config.PasswordPlaceholder ?? String.Empty,
                Keyboard = KeyboardType.Password
            };

            var loginCmd = new Command(() =>
            {
                var creds = new Credentials(txtLogin.Text, txtPassword.Text);
                config.OnAction(new DialogResult<Credentials>(DialogChoice.Positive, creds));
            });
            txtLogin.PropertyChanged += (sender, args) => loginCmd.IsExecuteable = !Extensions.IsAnyEmpty(txtLogin, txtPassword);
            txtPassword.PropertyChanged += (sender, args) => loginCmd.IsExecuteable = !Extensions.IsAnyEmpty(txtLogin, txtPassword);

            if (config.CancelText != null)
            {
                dlg.Add(new DialogAction
                {
                    Label = config.CancelText,
                    Command = new Command(() =>
                    {
                        var creds = new Credentials(txtLogin.Text, txtPassword.Text);
                        config.OnAction(new DialogResult<Credentials>(DialogChoice.Neutral, creds));
                    })
                });
            }
            dlg.Add(new DialogAction
            {
                Label = config.OkText,
                Command = loginCmd
            });

            if (!config.DisableImmediateShow)
                dlg.Show();

            return dlg;
        }


        public virtual async Task<DialogResult<Credentials>> LoginAsync(LoginConfig config, CancellationToken? cancelToken = null)
        {
            if (config.OnAction != null)
                throw new ArgumentException(NO_ONACTION);

            var tcs = new TaskCompletionSource<DialogResult<Credentials>>();
            config.OnAction = x => tcs.TrySetResult(x);

            var disp = this.Login(config);
            using (cancelToken?.Register(() => Cancel(disp, tcs)))
            {
                return await tcs.Task;
            }
        }


        public virtual Task<DialogResult<Credentials>> LoginAsync(string title, string message, CancellationToken? cancelToken = null)
        {
            return this.LoginAsync(new LoginConfig
            {
                //Title = title ?? LoginConfig.DefaultTitle,
                Message = message
            }, cancelToken);
        }

        #endregion

        #region Prompt

        public virtual IAlertDialog Prompt(PromptConfig config)
        {
            if (config.OnAction == null)
                throw new ArgumentNullException(NO_ONACTION_SET);

            var dlg = this.CreateDialogFromConfig(config);
            var txt = new TextEntry
            {
                Placeholder = config.Placeholder ?? String.Empty,
                Text = config.Text,
                MaxLength = config.MaxLength,
                Keyboard = config.InputType
            };
            var acceptCmd = new Command(() =>
                config.OnAction(new DialogResult<string>(DialogChoice.Positive, txt.Text))
            );

            dlg.Add(new DialogAction
            {
                Label = config.OkText,
                Command = acceptCmd
            });
            if (config.CancelText != null)
            {
                dlg.Add(new DialogAction
                {
                    Label = config.CancelText,
                    Command = new Command(() =>
                        config.OnAction(new DialogResult<string>(DialogChoice.Neutral, txt.Text))
                    )
                });
            }
            if (config.OnTextChanged != null)
            {
                var changeArgs = new PromptTextChangedArgs
                {
                    IsValid = true,
                    Value = txt.Text
                };
                config.OnTextChanged(changeArgs); // fire first time
                acceptCmd.IsExecuteable = changeArgs.IsValid;

                txt.PropertyChanged += (sender, args) =>
                {
                    if (nameof(txt.Text) != args.PropertyName)
                        return;

                    changeArgs.Value = txt.Text;
                    config.OnTextChanged(changeArgs);
                    acceptCmd.IsExecuteable = changeArgs.IsValid;
                    if (!txt.Equals(changeArgs.Value))
                        txt.Text = changeArgs.Value;
                };
            }
            //dlg.Dismissed = () => { };
            // TODO: 3 buttons

            if (!config.DisableImmediateShow)
                dlg.Show();


            return dlg;
        }


        public virtual Task<DialogResult<string>> PromptAsync(PromptConfig config, CancellationToken? cancelToken = null)
        {
            if (config.OnAction != null)
                throw new ArgumentException(NO_ONACTION);

            var tcs = new TaskCompletionSource<DialogResult<string>>();
            config.OnAction = x => tcs.TrySetResult(x);
            var dlg = this.Prompt(config);

            using (cancelToken?.Register(() => Cancel(dlg, tcs)))
            {
                return tcs.Task;
            }
        }


        public virtual Task<DialogResult<string>> PromptAsync(string message, string title, string okText, string cancelText, string placeholder, KeyboardType inputType, CancellationToken? cancelToken = null)
        {
            //return this.PromptAsync(new PromptConfig()
            //    .SetMessage(message)
            //    .SetTitle(title)
            //    .SetText(DialogChoice.Neutral, cancelText ?? PromptConfig.DefaultNeutral.Text)
            //    .SetText(DialogChoice.Positive, okText ?? PromptConfig.DefaultPositive.Text)
            //    .SetPlaceholder(placeholder)
            //    .SetInputType(inputType),
            //    cancelToken
            //);
            return null;
        }

        #endregion
    }
}
