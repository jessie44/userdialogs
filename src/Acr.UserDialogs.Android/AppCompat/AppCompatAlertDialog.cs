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


        public override void Dismiss()
        {
            // TODO: I have to call the fragment's dismiss, not the alertdialog here that is wrapping it
            //this.Activity.RunOnUiThread(this.Dismiss);
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
                //frag.Config = config;
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
                //this.Show(this.activity.FragmentManager, FragmentTag);


                this.dialog = this.builder.Create();
                this.dialog.ShowEvent += (sender, args) =>
                {
                    //onChange(promptArgs);
                    //this.GetButton(dialog, buttonId).Enabled = promptArgs.IsValid;
                };
                this.dialog.Window.SetSoftInputMode(SoftInput.StateVisible);
                this.dialog.KeyPress += this.OnKeyPress;
            });
        }


        public Action Dismissed { get; set; }


        protected virtual void OnKeyPress(object sender, DialogKeyEventArgs args)
        {
            args.Handled = false;

            switch (args.KeyCode)
            {
                case Keycode.Back:
                    args.Handled = true;
                    //if (this.IsCancellable)
                    //    this.SetAction(false);
                    break;

                case Keycode.Enter:
                    args.Handled = true;
                    //this.SetAction(true);
                    break;
            }
        }

        //protected virtual void SetAction(bool ok)
        //{
        //    var txt = this.Dialog.FindViewById<TextView>(Int32.MaxValue);
        //    this.Config?.OnAction(new PromptResult(ok, txt.Text.Trim()));
        //    this.Dismiss();
        //}

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
