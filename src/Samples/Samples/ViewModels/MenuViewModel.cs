using System;
using System.Collections.Generic;
using System.Windows.Input;
using Samples.Pages;
using Xamarin.Forms;


namespace Samples.ViewModels
{
    public class MenuViewModel
    {
        public List<CommandViewModel> Items { get; }

        public MenuViewModel()
        {
            this.Items = new List<CommandViewModel>(new[]
            {
                new CommandViewModel
                {
                    Text = "Standards",
                    Command = this.Nav<StandardPage>()
                }
            });
        }


        ICommand Nav<TPage>() where TPage : Page, new()
        {
            return new Command(() =>
            {
                var mp = ((NavigationPage)App.Current.MainPage).CurrentPage as MasterDetailPage;
                var page = new TPage();
                mp.Detail = page;
                mp.IsPresented = false;
            });
        }
    }
}
