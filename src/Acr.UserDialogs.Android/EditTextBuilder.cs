using System;
using Android.App;
using Android.Text;
using Android.Text.Method;
using Android.Widget;
using Splat;


namespace Acr.UserDialogs
{
    public class EditTextBuilder : IEditTextBuilder
    {
        public EditText Create(Activity activity, TextEntry entry)
        {
            var txt = new EditText(activity);

            var stop = false;
            txt.TextChanged += (sender, args) =>
            {
                stop = true;
                entry.Text = args.Text.ToString();
                stop = false;
            };

            entry.PropertyChanged += (sender, args) =>
            {
                if (stop && args.PropertyName.Equals(nameof(entry.Text)))
                    return;

                switch (args.PropertyName)
                {
                    case nameof(entry.Text) :
                        txt.Text = entry.Text;
                        break;

                    case nameof(entry.MaxLength):
                        if (entry.MaxLength == null)
                            txt.SetFilters(null);
                        else
                            txt.SetFilters(new[] { new InputFilterLengthFilter(entry.MaxLength.Value) });
                        break;

                    case nameof(entry.Placeholder):
                        txt.Hint = entry.Placeholder;
                        break;

                    case nameof(entry.BackgroundColor):
                        // TODO: what about null?
                        txt.SetBackgroundColor(entry.BackgroundColor.Value.ToNative());
                        break;

                    case nameof(entry.Keyboard):
                        this.SetKeyboard(txt, entry.Keyboard);
                        break;

                    case nameof(entry.TextColor):
                        // TODO: null
                        txt.SetTextColor(entry.TextColor.Value.ToNative());
                        break;

                    case nameof(entry.IsEnabled):
                        txt.Focusable = entry.IsEnabled;
                        break;
                }
                txt.Text = entry.Text;
            };

            return txt;
        }


        protected virtual void SetKeyboard(EditText txt, KeyboardType keyboardType)
        {
            switch (keyboardType)
            {
                case KeyboardType.DecimalNumber:
                    txt.InputType = InputTypes.ClassNumber | InputTypes.NumberFlagDecimal | InputTypes.NumberFlagSigned;
                    break;

                case KeyboardType.Email:
                    txt.InputType = InputTypes.ClassText | InputTypes.TextVariationEmailAddress;
                    break;

                case KeyboardType.Name:
                    txt.InputType = InputTypes.TextVariationPersonName;
                    break;

                case KeyboardType.Number:
                    txt.InputType = InputTypes.ClassNumber;
                    break;

                case KeyboardType.NumericPassword:
                    txt.InputType = InputTypes.ClassNumber;
                    txt.TransformationMethod = PasswordTransformationMethod.Instance;
                    break;

                case KeyboardType.Password:
                    txt.TransformationMethod = PasswordTransformationMethod.Instance;
                    txt.InputType = InputTypes.ClassText | InputTypes.TextVariationPassword;
                    break;

                case KeyboardType.Phone:
                    txt.InputType = InputTypes.ClassPhone;
                    break;

                case KeyboardType.Url:
                    txt.InputType = InputTypes.TextVariationUri;
                    break;
            }
        }
    }
}
