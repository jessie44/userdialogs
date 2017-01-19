using System;
using Android.App;
using AndroidHUD;


namespace Acr.UserDialogs.Internals
{
    public class Toast : IDisposable
    {
        readonly Activity activity;
        readonly ToastConfig config;


        public Toast(Activity activity, ToastConfig config)
        {
            this.activity = activity;
            this.config = config;
        }


        public void Show()
        {
            // TODO: position?
            AndHUD.Shared.ShowToast(
                this.activity,
                this.config.Message,
                AndroidHUD.MaskType.None,
                this.config.Duration,
                false,
                () =>
                {
                    AndHUD.Shared.Dismiss();
                    //this.config.Action?.Action?.Invoke();
                }
            );
        }


        public void Dispose()
        {
            try
            {
                AndHUD.Shared.Dismiss(this.activity);
            }
            catch
            {
            }
        }
    }
}