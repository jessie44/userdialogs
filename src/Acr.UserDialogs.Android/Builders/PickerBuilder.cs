using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using AlertDialog = Android.Support.V7.App.AlertDialog;

using Object = Java.Lang.Object;

namespace Acr.UserDialogs.Builders
{
    public class PickerBuilder : IAlertDialogBuilder<PickerPromptConfig>
    {
        public AlertDialog Build(AppCompatActivity activity, PickerPromptConfig config)
        {
            var optionsList = new List<string>();
            optionsList.AddRange(config.PickerList);
            var layout = new LinearLayout(activity)
            {
                Orientation = Orientation.Vertical,
            };
            layout.SetBackgroundColor(Android.Graphics.Color.White);
            var newspinner = new AppCompatSpinner(activity);
            newspinner.SetBackgroundColor(Android.Graphics.Color.White);
            var adapt = new ArrayAdapter<string>(activity, Android.Resource.Layout.SimpleSpinnerDropDownItem, optionsList);

            newspinner.Adapter = adapt;
            layout.AddView(newspinner, ViewGroup.LayoutParams.MatchParent);

            return new AlertDialog.Builder(activity, 0)
                .SetCancelable(false)
                .SetTitle(config.Title)
                .SetView(layout)
                .SetPositiveButton(config.OkText,
                    (s, a) => config.OnAction(new PickerPromptResult(true, (string)newspinner.SelectedItem)))
                .SetNegativeButton(config.CancelText,
                    (s, a) => config.OnAction(new PickerPromptResult(false, string.Empty)))
                .Create();
        }

        public Dialog Build(Activity activity, PickerPromptConfig config)
        {
            var optionsList = new List<string>();
            optionsList.AddRange(config.PickerList);
            var layout = new LinearLayout(activity);
            var newspinner = new Spinner(activity);

            var adapt = new ArrayAdapter<string>(activity, Android.Resource.Layout.SimpleSpinnerDropDownItem, optionsList);

            newspinner.Adapter = adapt;
            layout.AddView(newspinner, ViewGroup.LayoutParams.MatchParent);

            return new AlertDialog.Builder(activity, 0)
                .SetCancelable(false)
                .SetTitle(config.Title)
                .SetView(layout)
                .SetPositiveButton(config.OkText,
                    (s, a) => config.OnAction(new PickerPromptResult(true, (string)newspinner.SelectedItem)))
                .SetNegativeButton(config.CancelText,
                    (s, a) => config.OnAction(new PickerPromptResult(false, string.Empty)))
                .Create();
        }
    }
}