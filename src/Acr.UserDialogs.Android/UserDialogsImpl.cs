using System;
using Acr.UserDialogs.AppCompat;
using Acr.UserDialogs.Classic;
using Acr.UserDialogs.Internals;
using Android.App;
using Android.Support.V7.App;
using AndroidHUD;
using Splat;


namespace Acr.UserDialogs
{
    public class UserDialogsImpl : AbstractUserDialogs
    {
        public static string FragmentTag { get; set; } = "UserDialogs";
        protected internal Func<Activity> TopActivityFunc { get; set; }
        readonly IEditTextBuilder editTextBuilder;


        public UserDialogsImpl(Func<Activity> getTopActivity, EditTextBuilder editTextBuilder = null)
        {
            this.TopActivityFunc = getTopActivity;
            this.editTextBuilder = editTextBuilder ?? new EditTextBuilder();
        }


        public override IAlertDialog CreateDialog()
        {
            var activity = this.TopActivityFunc();
            var appcompat = activity as AppCompatActivity;

            if (appcompat != null)
                return new AppCompatAlertDialog(appcompat, this.editTextBuilder);

            return new ClassicAlertDialog(activity, this.editTextBuilder);
        }


        //public override IDisposable Alert(AlertConfig config)
        //{
        //    var activity = this.TopActivityFunc();
        //    if (activity is AppCompatActivity)
        //        return this.ShowDialog<AlertAppCompatDialogFragment, AlertConfig>((AppCompatActivity)activity, config);

        //    if (activity is FragmentActivity)
        //        return this.ShowDialog<AlertDialogFragment, AlertConfig>((FragmentActivity)activity, config);

        //    return this.Show(activity, () => new AlertBuilder().Build(activity, config));
        //}


        //public override IDisposable ActionSheet(ActionSheetConfig config)
        //{
        //    var activity = this.TopActivityFunc();
        //    if (activity is AppCompatActivity) {
        //        if (config.UseBottomSheet)
        //            return this.ShowDialog<Fragments.BottomSheetDialogFragment, ActionSheetConfig>((AppCompatActivity)activity, config);

        //        return this.ShowDialog<ActionSheetAppCompatDialogFragment, ActionSheetConfig>((AppCompatActivity)activity, config);
        //    }

        //    if (activity is FragmentActivity)
        //        return this.ShowDialog<ActionSheetDialogFragment, ActionSheetConfig>((FragmentActivity)activity, config);

        //    return this.Show(activity, () => new ActionSheetBuilder().Build(activity, config));
        //}


        //public override IDisposable Confirm(ConfirmConfig config)
        //{
        //    var activity = this.TopActivityFunc();
        //    if (activity is AppCompatActivity)
        //        return this.ShowDialog<ConfirmAppCompatDialogFragment, ConfirmConfig>((AppCompatActivity)activity, config);

        //    if (activity is FragmentActivity)
        //        return this.ShowDialog<ConfirmDialogFragment, ConfirmConfig>((FragmentActivity)activity, config);

        //    return this.Show(activity, () => new ConfirmBuilder().Build(activity, config));
        //}


        //public override IDisposable Login(LoginConfig config)
        //{
        //    var activity = this.TopActivityFunc();
        //    if (activity is AppCompatActivity)
        //        return this.ShowDialog<LoginAppCompatDialogFragment, LoginConfig>((AppCompatActivity)activity, config);

        //    if (activity is FragmentActivity)
        //        return this.ShowDialog<LoginDialogFragment, LoginConfig>((FragmentActivity)activity, config);

        //    return this.Show(activity, () => new LoginBuilder().Build(activity, config));
        //}


        //public override IDisposable Prompt(PromptConfig config)
        //{
        //    var activity = this.TopActivityFunc();
        //    if (activity is AppCompatActivity)
        //        return this.ShowDialog<PromptAppCompatDialogFragment, PromptConfig>((AppCompatActivity)activity, config);

        //    if (activity is FragmentActivity)
        //        return this.ShowDialog<PromptDialogFragment, PromptConfig>((FragmentActivity)activity, config);

        //    return this.Show(activity, () => new PromptBuilder().Build(activity, config));
        //}

        public override IDisposable DatePrompt(DatePromptConfig config)
        {
            //var activity = this.TopActivityFunc();
            //if (activity is AppCompatActivity)
            //    return this.ShowDialog<DateAppCompatDialogFragment, DatePromptConfig>((AppCompatActivity)activity, config);

            //if (activity is FragmentActivity)
            //    return this.ShowDialog<DateDialogFragment, DatePromptConfig>((FragmentActivity)activity, config);

            //return this.Show(activity, () => DatePromptBuilder.Build(activity, config));
            throw new NotImplementedException();
        }


        public override IDisposable TimePrompt(TimePromptConfig config)
        {
            //var activity = this.TopActivityFunc();
            //if (activity is AppCompatActivity)
            //    return this.ShowDialog<TimeAppCompatDialogFragment, TimePromptConfig>((AppCompatActivity)activity, config);

            //if (activity is FragmentActivity)
            //    return this.ShowDialog<TimeDialogFragment, TimePromptConfig>((FragmentActivity)activity, config);

            //return this.Show(activity, () => TimePromptBuilder.Build(activity, config));
            throw new NotImplementedException();
        }


        public override void ShowImage(IBitmap image, string message, int timeoutMillis)
        {
            var activity = this.TopActivityFunc();
            activity.RunOnUiThread(() =>
                AndHUD.Shared.ShowImage(activity, image.ToNative(), message, AndroidHUD.MaskType.Black, TimeSpan.FromMilliseconds(timeoutMillis))
            );
        }


        public override IDisposable Toast(ToastConfig cfg)
        {
            var activity = this.TopActivityFunc();
            return new Toast(activity, cfg);
        }


        public override IDisposable Snackbar(SnackbarConfig config)
        {
            var activity = this.TopActivityFunc() as AppCompatActivity;
            if (activity == null)
                throw new ArgumentException("Snackbar on Android is only supported when your current activity inherit from AppCompatActivity");

            return new AppCompatSnackbar(activity, config);
        }


        protected override IProgressDialog CreateDialogInstance(ProgressDialogConfig config)
        {
            var activity = this.TopActivityFunc();
            var dialog = new ProgressDialog(config, activity);


            //if (activity != null)
            //{
            //    var frag = new LoadingFragment();
            //    activity.RunOnUiThread(() =>
            //    {
            //        frag.Config = dialog;
            //        frag.Show(activity.SupportFragmentManager, FragmentTag);
            //    });
            //}

            return dialog;
        }
    }
}