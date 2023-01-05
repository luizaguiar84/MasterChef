using System;
using Xamarin.Forms;

namespace MasterChef.Mobile.View
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new LoginView());
        }
    }
}
