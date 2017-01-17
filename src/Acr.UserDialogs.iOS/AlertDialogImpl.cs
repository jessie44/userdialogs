using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;


namespace Acr.UserDialogs
{
    public class AlertDialogImpl : AbstractAlertDialog
    {
        readonly Func<UIViewController> viewControllerFunc;
        UIAlertController alert;


        public AlertDialogImpl(Func<UIViewController> viewControllerFunc)
        {
            this.viewControllerFunc = viewControllerFunc;
        }


        public override void Show()
        {
            var style = this.Actions.Any() ? UIAlertControllerStyle.ActionSheet : UIAlertControllerStyle.Alert;
            this.alert = UIAlertController.Create(this.Title, this.Message, style);

            foreach (var action in this.Actions)
            {
                //this.alert.AddAction(action.Create());
            }
            foreach (var txt in this.TextEntries)
            {
                //this.alert.AddTextField(x => txt.Hook(x));
            }
            //this.AddNativeMainAction(this.alert, this.Positive);
            //this.AddNativeMainAction(this.alert, this.Negative);
            //this.AddNativeMainAction(this.alert, this.Neutral);
            //var vc = this.viewControllerFunc();
        }


        public override void Dismiss()
        {
            this.alert?.DismissViewController(true, null);
        }


        protected virtual void Hook(UITextView tb, TextEntry txt)
        {
            txt.PropertyChanged += (sender, args) =>
            {

            };
        }

        protected virtual IAlertDialog SetMainAction(DialogChoice choice, Action<DialogAction> action)
        {
            //var obj = new DialogAction { Choice = choice };
            //action(obj);
            //switch (choice)
            //{
            //    case DialogChoice.Positive:
            //        this.Positive = obj;
            //        break;

            //    case DialogChoice.Negative:
            //        this.Negative = obj;
            //        break;

            //    case DialogChoice.Neutral:
            //        this.Neutral = obj;
            //        break;
            //}
            return this;
        }


        //void AddNativeMainAction(UIAlertController ctrl, DialogAction action)
        //{
        //    var impl = action as DialogAction;
        //    if (impl != null)
        //        ctrl.AddAction(impl.Create());
        //}
        /*
            UIAlertController alert = null;
            var app = UIApplication.SharedApplication;
            app.InvokeOnMainThread(() =>
            {
                alert = alertFunc();
                var top = this.viewControllerFunc();
                if (alert.PreferredStyle == UIAlertControllerStyle.ActionSheet && UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
                {
                    var x = top.View.Bounds.Width / 2;
                    var y = top.View.Bounds.Bottom;
                    var rect = new CGRect(x, y, 0, 0);

                    alert.PopoverPresentationController.SourceView = top.View;
                    alert.PopoverPresentationController.SourceRect = rect;
                    alert.PopoverPresentationController.PermittedArrowDirections = UIPopoverArrowDirection.Unknown;
                }
                top.PresentViewController(alert, true, null);
            });
            return new DisposableAction(() =>
            {
                try
                {
                    app.InvokeOnMainThread(() => alert.DismissViewController(true, null));
                }
                catch { }
            });


public class DialogAction : DialogAction
    {
        UIAlertAction alertAction;

        public string Label { get; set; }


        bool enabled = true;
        public bool Enabled
        {
            get { return this.enabled; }
            set
            {
                this.enabled = value;
                if (this.alertAction != null)
                    this.alertAction.Enabled = value;
            }
        }


        public Color? TextColor { get; set; }
        public Color? BackgroundColor { get; set; }
        public IBitmap Icon { get; set; }
        public Action<DialogAction> Tap { get; set; }


        public UIAlertAction Create()
        {
            this.alertAction = UIAlertAction.Create(this.Label, this.ToStyle(), x => this.Tap?.Invoke(this));
            if (this.Icon != null)
                this.alertAction.SetValueForKey(this.Icon.ToNative(), new NSString("image"));

            this.alertAction.Enabled = this.Enabled;
            return this.alertAction;
        }


        UIAlertActionStyle ToStyle()
        {
            switch (this.Choice)
            {
                case DialogChoice.Negative:
                    return UIAlertActionStyle.Destructive;

                case DialogChoice.Neutral:
                    return UIAlertActionStyle.Cancel;

                default:
                    return UIAlertActionStyle.Default;
            }
        }



 public class TextEntry : TextEntry
    {
        UITextField native;


        string text;
        public string Text
        {
            get { return this.text; }
            set
            {
                this.text = value;
                if (this.native != null)
                    this.native.Text = value;
            }
        }


        private string placeholder;
        public string Placeholder
        {
            get { return this.placeholder; }
            set
            {
                this.placeholder = value;
                if (this.native != null)
                    this.native.Placeholder = value;
            }
        }


        public int? MaxLength { get; set; }
        public KeyboardType Keyboard { get; set; }
        public Color? TextColor { get; set; }
        public Color? BackgroundColor { get; set; }
        public Action<TextEntry> TextChanged { get; set; }


        public void Hook(UITextField txt)
        {
            this.native = txt;

            txt.Placeholder = this.Placeholder ?? String.Empty;
            txt.Ended += (sender, e) => this.TextChanged?.Invoke(this);
            txt.ShouldChangeCharacters = (field, replacePosition, replacement) =>
            {
                if (this.MaxLength == null)
                    return true;

                var updatedText = new StringBuilder(field.Text);
                updatedText.Remove((int)replacePosition.Location, (int)replacePosition.Length);
                updatedText.Insert((int)replacePosition.Location, replacement);
                return updatedText.ToString().Length <= this.MaxLength.Value;
            };

            switch (this.Keyboard)
            {
                case KeyboardType.DecimalNumber:
                    txt.KeyboardType = UIKeyboardType.DecimalPad;
                    break;

                case KeyboardType.Email:
                    txt.KeyboardType = UIKeyboardType.EmailAddress;
                    break;

                case KeyboardType.Name:
                    break;

                case KeyboardType.Number:
                    txt.KeyboardType = UIKeyboardType.NumberPad;
                    break;

                case KeyboardType.NumericPassword:
                    txt.SecureTextEntry = true;
                    txt.KeyboardType = UIKeyboardType.NumberPad;
                    break;

                case KeyboardType.Password:
                    txt.SecureTextEntry = true;
                    break;

                case KeyboardType.Phone:
                    txt.KeyboardType = UIKeyboardType.PhonePad;
                    break;

                case KeyboardType.Url:
                    txt.KeyboardType = UIKeyboardType.Url;
                    break;
            }
        }
    }
         */
    }
}
