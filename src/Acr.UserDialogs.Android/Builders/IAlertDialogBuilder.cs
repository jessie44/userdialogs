using System;
using Android.App;
using Android.Support.V7.App;
using AlertDialog = Android.Support.V7.App.AlertDialog;

namespace Acr.UserDialogs.Builders
{
    public interface IAlertDialogBuilder<in TConfig>
    {
        AlertDialog Build(AppCompatActivity activity, TConfig config);

        Dialog Build(Activity activity, TConfig config);
    }
}