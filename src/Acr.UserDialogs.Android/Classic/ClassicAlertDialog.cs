using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;


namespace Acr.UserDialogs.Classic
{
    public class ClassicAlertDialog : AbstractAlertDialog
    {
        readonly Activity activity;
        readonly IEditTextBuilder editTextBuilder;
        AlertDialog.Builder builder;
        AlertDialog dialog;


        public ClassicAlertDialog(Activity activity, IEditTextBuilder editTextBuilder)
        {
            this.activity = activity;
            this.editTextBuilder = editTextBuilder;
        }


        public override void Show()
        {
            // TODO: everything below here has to happen on the main thread
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


                // TODO: show on the fragment first, then move to everything else
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


        public override void Hide()
        {
            this.activity.RunOnUiThread(this.dialog.Dismiss);
        }



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
                    this.Hide(); // TODO: should I call positive if available?
                    break;
            }
        }
    }
}