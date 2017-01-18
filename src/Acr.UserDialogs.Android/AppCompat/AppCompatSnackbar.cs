using System;
using Android.App;
using Android.Support.Design.Widget;
using Android.Text;
using Android.Views;
using Android.Widget;
using Splat;


namespace Acr.UserDialogs
{
    public class AppCompatToast : IDisposable
    {
        readonly Activity activity;
        readonly SnackbarConfig config;
        Snackbar snackBar;


        public AppCompatToast(Activity activity, SnackbarConfig config)
        {
            this.activity = activity;
            this.config = config;
        }


        public void Show()
        {
            this.activity.RunOnUiThread(() =>
            {
                var view = this.activity.Window.DecorView.RootView.FindViewById(Android.Resource.Id.Content);
                this.snackBar = Snackbar.Make(
                    view,
                    Html.FromHtml(this.config.Message),
                    (int)this.config.Duration.TotalMilliseconds
                );
                this.TrySetToastTextColor();
                if (this.config.BackgroundColor != null)
                    this.snackBar.View.SetBackgroundColor(this.config.BackgroundColor.Value.ToNative());

                if (this.config.ActionButton != null)
                {
                    this.snackBar.SetAction(this.config.ActionButton.Label, x =>
                    {
                        var executed = this.config.ActionButton.Command?.TryExecute(null) ?? true;
                        if (executed)
                            this.snackBar.Dismiss();
                    });
                    //TODO
                    //var color = this.config.ActionButton.TextColor ?? SnackbarConfig.DefaultActionTextColor;
                    //if (color != null)
                    //    this.snackBar.SetActionTextColor(color.Value.ToNative());
                }
                this.snackBar.Show();
            });
        }


        protected virtual void TrySetToastTextColor()
        {
            var textColor = this.config.MessageTextColor ?? SnackbarConfig.DefaultMessageTextColor;
            if (textColor == null)
                return;

            var viewGroup = this.snackBar.View as ViewGroup;
            if (viewGroup != null)
            {
                for (var i = 0; i < viewGroup.ChildCount; i++)
                {
                    var child = viewGroup.GetChildAt(i);
                    var textView = child as TextView;
                    if (textView != null)
                    {
                        textView.SetTextColor(textColor.Value.ToNative());
                        break;
                    }
                }
            }
        }


        public void Dispose()
        {
            if (!this.snackBar.IsShown)
                return;

            this.activity.RunOnUiThread(() =>
            {
                try
                {
                    this.snackBar.Dismiss();
                }
                catch
                {
                    // catch and swallow
                }
            });
        }
    }
}