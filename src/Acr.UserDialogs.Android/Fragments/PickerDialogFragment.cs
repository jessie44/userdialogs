using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acr.UserDialogs.Builders;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Acr.UserDialogs.Fragments
{
    public class PickerDialogFragment : AbstractDialogFragment<PickerPromptConfig>
    {
        protected override Dialog CreateDialog(PickerPromptConfig config)
        {
            return new PickerBuilder().Build(this.Activity, config);
        }
    }

    public class PickerAppCompatDialogFragment : AbstractAppCompatDialogFragment<PickerPromptConfig>
    {
        protected override Dialog CreateDialog(PickerPromptConfig config)
        {
            return new PickerBuilder().Build(this.AppCompatActivity, config);
        }
    }
}