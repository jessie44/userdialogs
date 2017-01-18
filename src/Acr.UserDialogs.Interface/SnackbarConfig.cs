using System;
using System.Drawing;


namespace Acr.UserDialogs
{
    public class SnackbarConfig
    {
        /// <summary>
        ///
        /// </summary>
        public static ScreenPosition DefaultPosition { get; set; } = ScreenPosition.Bottom;

        /// <summary>
        /// The default duration for how long the toast should remain on-screen.  Defaults to 2.5 seconds
        /// </summary>
        public static TimeSpan DefaultDuration { get; set; } = TimeSpan.FromSeconds(2.5);

        /// <summary>
        /// The default message text color to use.  If not set, defaults very depending on platform.
        /// </summary>
        public static Color? DefaultMessageTextColor { get; set; }

        /// <summary>
        /// The default toast background color.  If not set, defaults very depending on platform.
        /// </summary>
        public static Color? DefaultBackgroundColor { get; set; }


        public ScreenPosition Position { get; set; }

        // TODO: consider label
        public string Message { get; set; }
        public Color? MessageTextColor { get; set; } = DefaultMessageTextColor;
        public Color? BackgroundColor { get; set; } = DefaultBackgroundColor;
        public TimeSpan Duration { get; set; } = DefaultDuration;
        public DialogAction ActionButton { get; set; }


        public SnackbarConfig(string message)
        {
            this.Message = message;
        }


        public SnackbarConfig SetBackgroundColor(Color color)
        {
            this.BackgroundColor = color;
            return this;
        }


        public SnackbarConfig SetDuration(int millis)
        {
            return this.SetDuration(TimeSpan.FromMilliseconds(millis));
        }


        public SnackbarConfig SetDuration(TimeSpan? duration = null)
        {
            this.Duration = duration ?? DefaultDuration;
            return this;
        }


        public SnackbarConfig SetAction(DialogAction action)
        {
            this.ActionButton = action;
            return this;
        }


        public SnackbarConfig SetMessageTextColor(Color color)
        {
            this.MessageTextColor = color;
            return this;
        }
    }
}
