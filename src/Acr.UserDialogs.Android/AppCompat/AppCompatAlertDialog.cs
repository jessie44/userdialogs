using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using AlertDialog = Android.Support.V7.App.AlertDialog;


namespace Acr.UserDialogs.AppCompat
{
    public class AppCompatAlertDialog : AppCompatDialogFragment, IAlertDialog
    {
        public const string FragmentTag = "acr";

        readonly AppCompatActivity activity;
        readonly IEditTextBuilder editTextBuilder;
        readonly IList<TextEntry> textEntries;
        readonly IList<DialogAction> actions;

        AlertDialog.Builder builder;
        AlertDialog dialog;


        public AppCompatAlertDialog(AppCompatActivity activity, IEditTextBuilder editTextBuilder)
        {
            this.activity = activity;
            this.editTextBuilder = editTextBuilder;

            this.textEntries = new List<TextEntry>();
            this.TextEntries = new ReadOnlyCollection<TextEntry>(this.textEntries);

            this.actions = new List<DialogAction>();
            this.Actions = new ReadOnlyCollection<DialogAction>(this.actions);
        }


        public override Dialog OnCreateDialog(Bundle bundle)
        {
            return base.OnCreateDialog(bundle);
            // TODO: state restore?
        }


        public override void OnDetach()
        {
            base.OnDetach();
            // TODO: state grab
        }


        public Color? BackgroundColor { get; set; }
        public Color? MessageTextColor { get; set; }

        public string Message { get; set; }
        public string Title { get; set; }
        public bool IsCancellable { get; set; }
        public DialogAction Positive { get; set; }
        public DialogAction Neutral { get; set; }
        public DialogAction Negative { get; set; }
        public IReadOnlyList<DialogAction> Actions { get; }
        public IReadOnlyList<TextEntry> TextEntries { get; }


        public IAlertDialog Add(TextEntry instance)
        {
            this.textEntries.Add(instance);
            return this;
        }


        public IAlertDialog Add(DialogAction action)
        {
            this.actions.Add(action);
            return this;
        }


        public void Show()
        {
            this.activity.RunOnUiThread(() =>
            {
                //var layout = new LinearLayout(context) {
                //    Orientation = Orientation.Vertical,
                //    OverScrollMode = OverScrollMode.IfContentScrolls
                //};
                //var txt = new TextView(context);
                this.builder = new AlertDialog.Builder(this.activity)
                    .SetCancelable(this.IsCancellable)
                    .SetMessage(this.Message)
                    .SetTitle(this.Title);

                //if (this.Config.Cancel == null)
                //{
                //    dialog.SetCancelable(false);
                //    dialog.SetCanceledOnTouchOutside(false);
                //}
                //else
                //{
                //    dialog.SetCancelable(true);
                //    dialog.SetCanceledOnTouchOutside(true);
                //    dialog.CancelEvent += (sender, args) => this.Config.Cancel.Action.Invoke();
                //}
                if (this.Positive != null)
                {
                    this.builder.SetPositiveButton(
                        this.Positive.Label,
                        (sender, args) => this.Positive.Command.TryExecute(null)
                    );
                }
                if (this.Neutral != null)
                {
                    this.builder.SetNeutralButton(
                        this.Neutral.Label,
                        (sender, args) => this.Neutral.Command.TryExecute(null)
                    );
                }
                if (this.Negative != null)
                {
                    this.builder.SetNegativeButton(
                        this.Negative.Label,
                        (sender, args) => this.Negative.Command.TryExecute(null)
                    );
                }
                if (this.TextEntries.Any())
                {
                    var layout = new LinearLayout(this.activity)
                    {
                        Orientation = Orientation.Vertical
                    };
                    foreach (var txt in this.TextEntries)
                    {
                        var editText = this.editTextBuilder.Create(this.activity, txt);
                        layout.AddView(editText, ViewGroup.LayoutParams.MatchParent);
                    }
                    this.builder.SetView(layout);
                }


                // TODO: show on the fragment first, then move to everything else
                //this.Show(this.activity.FragmentManager, FragmentTag);


                this.dialog = this.builder.Create();
                this.dialog.ShowEvent += (sender, args) =>
                {
                    //onChange(promptArgs);
                    //this.GetButton(dialog, buttonId).Enabled = promptArgs.IsValid;
                };
                // TODO: catch dismiss method so we can call our dismiss event
                this.dialog.Window.SetSoftInputMode(SoftInput.StateVisible);
                this.dialog.KeyPress += this.OnKeyPress;
            });
        }


        public void Hide()
        {
            // TODO: I have to call the fragment's dismiss, not the alertdialog here that is wrapping it
            this.Activity.RunOnUiThread(this.Dismiss);
        }


        public Action Dismissed { get; set; }


        protected virtual void OnKeyPress(object sender, DialogKeyEventArgs args)
        {
            args.Handled = false;

            switch (args.KeyCode)
            {
                case Keycode.Back:
                    args.Handled = true;
                    if (this.IsCancellable)
                        this.Hide();
                    break;

                case Keycode.Enter:
                    args.Handled = true;
                    this.Hide();  // TODO: should I call positive if available?
                    break;
            }
        }
    }
}


/*
if (config.ItemIcon != null || config.Options.Any(x => x.ItemIcon != null))
{
    var adapter = new ActionSheetListAdapter(activity, Android.Resource.Layout.SelectDialogItem,
        Android.Resource.Id.Text1, config);
    dlg.SetAdapter(adapter, (s, a) => config.Options[a.Which].Action?.Invoke());
}
else
{
    var array = config
        .Options
        .Select(x => x.Text)
        .ToArray();

    dlg.SetItems(array, (s, args) => config.Options[args.Which].Action?.Invoke());
}

protected virtual void HookTextChanged(Dialog dialog, EditText txt, Action<PromptTextChangedArgs> onChange)
{
if (onChange == null)
    return;

var buttonId = (int) Android.Content.DialogButtonType.Positive;
var promptArgs = new PromptTextChangedArgs { Value = String.Empty };

dialog.ShowEvent += (sender, args) =>
{
    onChange(promptArgs);
    ((AlertDialog)dialog).GetButton(buttonId).Enabled = promptArgs.IsValid;
};
txt.AfterTextChanged += (sender, args) =>
{
    promptArgs.IsValid = true;
    promptArgs.Value = txt.Text;
    onChange(promptArgs);
    ((AlertDialog)dialog).GetButton(buttonId).Enabled = promptArgs.IsValid;

    if (!txt.Text.Equals(promptArgs.Value))
    {
        txt.Text = promptArgs.Value;
        txt.SetSelection(txt.Text.Length);
    }
};

*/
//using System;
//using Android.App;
//using Android.OS;


//namespace Acr.UserDialogs.AppCompat
//{
//    public class AcrDialogFragment : DialogFragment
//    {
//        public override Dialog OnCreateDialog(Bundle savedInstanceState)
//        {
//            return base.OnCreateDialog(savedInstanceState);
//        }


//        public override void OnSaveInstanceState(Bundle bundle)
//        {
//            base.OnSaveInstanceState(bundle);
//        }


//        public override void OnDetach()
//        {
//            base.OnDetach();
//        }
//    }
//}
///*
// //PROMPT
// protected override void OnKeyPress(object sender, DialogKeyEventArgs args)
//        {
//            base.OnKeyPress(sender, args);
//            args.Handled = false;

//            switch (args.KeyCode)
//            {
//                case Keycode.Back:
//                    args.Handled = true;
//                    if (this.Config.IsCancellable)
//                        this.SetAction(false);
//                    break;

//                case Keycode.Enter:
//                    args.Handled = true;
//                    this.SetAction(true);
//                    break;
//            }
//        }

//        protected override Dialog CreateDialog(PromptConfig config)
//        {
//            return new PromptBuilder().Build(this.Activity, config);
//        }


//        protected virtual void SetAction(bool ok)
//        {
//            var txt = this.Dialog.FindViewById<TextView>(Int32.MaxValue);
//            this.Config?.OnAction(new PromptResult(ok, txt.Text.Trim()));
//            this.Dismiss();
//        }
// *
// //ACTIONSHEET
//            dialog.Window.SetSoftInputMode(SoftInput.StateVisible);
//            dialog.KeyPress += this.OnKeyPress;
//            if (this.Config.Cancel == null)
//            {
//                dialog.SetCancelable(false);
//                dialog.SetCanceledOnTouchOutside(false);
//            }
//            else
//            {
//                dialog.SetCancelable(true);
//                dialog.SetCanceledOnTouchOutside(true);
//                dialog.CancelEvent += (sender, args) => this.Config.Cancel.Action.Invoke();
//            }


// // ABSTRACT
//public T Config { get; set; }


//        public override void OnSaveInstanceState(Bundle bundle)
//        {
//            base.OnSaveInstanceState(bundle);
//            ConfigStore.Instance.Store(bundle, this.Config);
//        }


//        public override Dialog OnCreateDialog(Bundle bundle)
//        {
//            if (this.Config == null)
//                this.Config = ConfigStore.Instance.Pop<T>(bundle);

//            var dialog = this.CreateDialog(this.Config);
//            this.SetDialogDefaults(dialog);

//            return dialog;
//        }


//        public override void OnDetach()
//        {
//            base.OnDetach();
//            if (this.Dialog != null)
//                this.Dialog.KeyPress -= this.OnKeyPress;
//        }


//        protected virtual void SetDialogDefaults(Dialog dialog)
//        {
//            dialog.Window.SetSoftInputMode(SoftInput.StateVisible);
//            dialog.SetCancelable(false);
//            dialog.SetCanceledOnTouchOutside(false);
//            dialog.KeyPress += this.OnKeyPress;
//        }
// *
// *         protected override void OnKeyPress(object sender, DialogKeyEventArgs args)
//        {
//            base.OnKeyPress(sender, args);
//            if (args.KeyCode != Keycode.Back)
//                return;

//            args.Handled = true;
//            this.Config?.OnAction?.Invoke();
//            this.Dismiss();
//    }


//public string BundleKey { get; set; } = "UserDialogFragmentConfig";
//        long counter = 0;
//        readonly IDictionary<long, object> configStore = new Dictionary<long, object>();


//        public static ConfigStore Instance { get; } = new ConfigStore();


//        public void Store(Bundle bundle, object config)
//        {
//            this.counter++;
//            this.configStore[this.counter] = config;
//            bundle.PutLong(this.BundleKey, this.counter);
//        }


//        public T Pop<T>(Bundle bundle)
//        {
//            var id = bundle.GetLong(this.BundleKey);
//            var cfg = (T)this.configStore[id];
//            this.configStore.Remove(id);
//            return cfg;
//        }
//     */
