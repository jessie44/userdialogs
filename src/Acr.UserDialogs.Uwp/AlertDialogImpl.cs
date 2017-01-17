using System;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Splat;


namespace Acr.UserDialogs
{
    //https://maximelabelle.wordpress.com/2016/03/02/displaying-a-yes-no-cancel-message-dialog-in-a-universal-windows-platform-uwp-app/
    public class AlertDialogImpl : AbstractAlertDialog
    {
        ContentDialog dialog;


        public override void Show()
        {
            var layout = new StackPanel();
            var btns = new StackPanel();
            this.dialog = new ContentDialog();


            if (this.Positive != null)
            {

                //this.dialog.DefaultCommandIndex = 0;
            }
            if (this.Neutral != null)
            {
                //this.dialog.CancelCommandIndex = 1;
            }
            if (this.Message != null)
            {
                layout.Children.Add(new TextBlock { Text = this.Message });
            }
            foreach (var txt in this.TextEntries)
            {
                this.Add(layout, txt);
            }
            foreach (var action in this.Actions)
            {
                var btn = new Button
                {
                    Content = action.Label
                };
                layout.Children.Add(btn);
            }
            this.dialog.ShowAsync();
        }


        public override void Dismiss()
        {
        }


        protected virtual void Add(MessageDialog dlg, DialogAction action, DialogChoice choice = DialogChoice.Neutral)
        {
            var btn = new Button
            {
                Content = action.Label,
                Command = action.Command
            };
            action.PropertyChanged += (sender, args) =>
            {
                switch (args.PropertyName)
                {
                    case nameof(action.Label):
                        btn.Content = action.Label;
                        break;

                    case nameof(action.TextColor):
                        btn.Foreground = action.TextColor?.ToNativeBrush();
                        break;

                    case nameof(action.BackgroundColor):
                        btn.Background = action.BackgroundColor?.ToNativeBrush();
                        break;
                }
            };
        }


        protected virtual void Add(StackPanel panel, TextEntry txt)
        {
            //CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action());
            var tb = new TextBox
            {
                Text = txt.Text ?? String.Empty,
                PlaceholderText = txt.Placeholder ?? String.Empty,
                MaxLength = txt.MaxLength ?? 0,
                IsEnabled = txt.IsEnabled
                //InputScope =
            };
            txt.PropertyChanged += (sender, args) =>
            {
                switch (args.PropertyName)
                {
                    case nameof(txt.Keyboard):
                        break;

                    case nameof(txt.TextColor):
                        tb.Foreground = txt.TextColor?.ToNativeBrush();
                        break;

                    case nameof(txt.BackgroundColor):
                        tb.Background = txt.BackgroundColor?.ToNativeBrush();
                        break;

                    case nameof(txt.Placeholder):
                        tb.PlaceholderText = txt.Placeholder;
                        break;

                    case nameof(txt.IsEnabled):
                        tb.MaxLength = txt.MaxLength ?? 0;
                        break;
                }
            };
            tb.SetBinding(TextBox.TextProperty, new Binding
            {
                Source = txt,
                Path = new PropertyPath(txt.Text),
                Mode = BindingMode.TwoWay
            });
            /*
             * TODO: when Text actually changes back, I should be running original code potentially
             *
    txt.TextChanged += (sender, e) =>
    {
        args.IsValid = true; // reset
        args.Value = txt.Text;
        config.OnTextChanged(args);
        dialog.IsPrimaryButtonEnabled = args.IsValid;

        if (!args.Value.Equals(txt.Text))
        {
            txt.Text = args.Value;
            txt.SelectionStart = Math.Max(0, txt.Text.Length);
            txt.SelectionLength = 0;
        }
    };
             */
            panel.Children.Add(tb);
        }
    }
}
/*
var dialog = new MessageDialog(content, title);
dialog.Options = MessageDialogOptions.None;
dialog.Commands.Add(yesCommand);

dialog.DefaultCommandIndex = 0;
dialog.CancelCommandIndex = 0;

if (noCommand != null)
{
    dialog.Commands.Add(noCommand);
    dialog.CancelCommandIndex = (uint)dialog.Commands.Count - 1;
}

if (cancelCommand != null)
{
    // Devices with a hardware back button
    // use the hardware button for Cancel.
    // for other devices, show a third option

    var t_hardwareBackButton = "Windows.Phone.UI.Input.HardwareButtons";

    if (ApiInformation.IsTypePresent(t_hardwareBackButton))
    {
        // disable the default Cancel command index
        // so that dialog.ShowAsync() returns null
        // in that case

        dialog.CancelCommandIndex = UInt32.MaxValue;
    }
    else
    {
        dialog.Commands.Add(cancelCommand);
        dialog.CancelCommandIndex = (uint)dialog.Commands.Count - 1;
    }
}

var command = await dialog.ShowAsync();

if (command == null && cancelCommand != null)
{
    // back button was pressed
    // invoke the UICommand

    cancelCommand.Invoked(cancelCommand);
}

if (command == yesCommand)
{
    // handle yes command
}
else if (command == noCommand)
{
    // handle no command
}
else
{
    // handle cancel command
}
*/

/*
protected virtual void SetPasswordPrompt(ContentDialog dialog, StackPanel stack, PromptConfig config)
{
    var txt = new PasswordBox
    {
        PlaceholderText = config.Placeholder ?? String.Empty,
        Password = config.Text ?? String.Empty
    };
    if (config.MaxLength != null)
        txt.MaxLength = config.MaxLength.Value;

    stack.Children.Add(txt);
    dialog.PrimaryButtonCommand = new Command(() =>
    {
        config.OnAction?.Invoke(new PromptResult(true, txt.Password));
        dialog.Hide();
    });
    if (config.OnTextChanged == null)
        return;

    var args = new PromptTextChangedArgs { Value = String.Empty };
    config.OnTextChanged(args);
    dialog.IsPrimaryButtonEnabled = args.IsValid;

    txt.PasswordChanged += (sender, e) =>
    {
        args.IsValid = true; // reset
        args.Value = txt.Password;
        config.OnTextChanged(args);

        dialog.IsPrimaryButtonEnabled = args.IsValid;
        if (!args.Value.Equals(txt.Password))
        {
            txt.Password = args.Value;
        }
    };
}


protected virtual void SetDefaultPrompt(ContentDialog dialog, StackPanel stack, PromptConfig config)
{
    var txt = new TextBox
    {
        PlaceholderText = config.Placeholder ?? String.Empty,
        Text = config.Text ?? String.Empty
    };
    if (config.MaxLength != null)
        txt.MaxLength = config.MaxLength.Value;

    stack.Children.Add(txt);

    dialog.PrimaryButtonCommand = new Command(() =>
    {
        config.OnAction?.Invoke(new PromptResult(true, txt.Text.Trim()));
        dialog.Hide();
    });

    if (config.OnTextChanged == null)
        return;

    var args = new PromptTextChangedArgs { Value = String.Empty };
    config.OnTextChanged(args);
    dialog.IsPrimaryButtonEnabled = args.IsValid;

    txt.TextChanged += (sender, e) =>
    {
        args.IsValid = true; // reset
        args.Value = txt.Text;
        config.OnTextChanged(args);
        dialog.IsPrimaryButtonEnabled = args.IsValid;

        if (!args.Value.Equals(txt.Text))
        {
            txt.Text = args.Value;
            txt.SelectionStart = Math.Max(0, txt.Text.Length);
            txt.SelectionLength = 0;
        }
    };
}
*/
