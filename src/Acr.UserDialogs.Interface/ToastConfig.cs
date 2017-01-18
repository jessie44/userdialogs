using System;
using System.Drawing;


namespace Acr.UserDialogs
{
    public class ToastConfig
    {

        /// <summary>
        ///
        /// </summary>
        public static ScreenPosition DefaultPosition { get; set; } = ScreenPosition.Bottom;

        /// <summary>
        /// The default duration for how long the toast should remain on-screen.  Defaults to 2.5 seconds
        /// </summary>
        public static TimeSpan DefaultDuration { get; set; } = TimeSpan.FromSeconds(2.5);


        public ScreenPosition Position { get; set; }
        public string Message { get; set; }
        public TimeSpan Duration { get; set; } = DefaultDuration;
        public Action Action { get; set; }


        public ToastConfig(string message)
        {
            this.Message = message;
        }


        public ToastConfig SetDuration(int millis)
        {
            return this.SetDuration(TimeSpan.FromMilliseconds(millis));
        }


        public ToastConfig SetDuration(TimeSpan? duration = null)
        {
            this.Duration = duration ?? DefaultDuration;
            return this;
        }


        public ToastConfig SetAction(Action action)
        {
            this.Action = action;
            return this;
        }
    }
}
