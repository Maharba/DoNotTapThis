using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using MenuItem = DoNotTapThis.Pages.Items.MenuItem;

namespace DoNotTapThis.Pages
{
    public partial class MainMenuPage : MasterDetailPage
    {
        public MainMenuPage()
        {
            InitializeComponent();

            Detail = new NavigationPage(new MainPage());
            lstMenu.ItemSelected += (sender, args) => { ((ListView) sender).SelectedItem = null; };
            lstMenu.ItemTapped += LstMenuOnItemTapped;
            lstMenu.ItemsSource = new List<MenuItem>()
            {
                new MenuItem() { Image = "gamepad_variant.png", Text = "Home" },
                new MenuItem() { Image = "crown.png", Text = "Leaderboard" }
            };

            lstSeccionInferior.ItemSelected += (sender, args) => { ((ListView)sender).SelectedItem = null; };
            lstSeccionInferior.ItemTapped += LstMenuOnItemTapped;
            lstSeccionInferior.ItemsSource = new List<MenuItem>()
            {
                new MenuItem() { Image = "settings.png", Text = "Settings" },
                new MenuItem() { Image = "information.png", Text = "About" }
            };
        }

        private async void LstMenuOnItemTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
        {
            var menuItem = itemTappedEventArgs.Item as MenuItem;
            if (menuItem != null)
            {
                switch (menuItem.Text)
                {
                    case "Home":
                        IsPresented = false;
                        await Detail.Navigation.PopToRootAsync();
                        break;
                    case "Leaderboard":
                        //TODO: Add a Leaderboard Page
                    case "Settings":
                        //TODO: Add a Settings Page
                        break;
                    case "About":
                        IsPresented = false;
                        await Detail.Navigation.PushAsync(new AboutPage());
                        break;
                }
            }
        }
    }
}
