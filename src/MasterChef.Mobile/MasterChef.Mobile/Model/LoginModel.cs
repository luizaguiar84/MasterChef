using MasterChef.Mobile.Initillizer;
using MasterChef.Mobile.Services;
using MasterChef.Mobile.View;
using System;
using System.ComponentModel;
using System.Windows.Input;
using MasterChef.Mobile.Interface;
using Xamarin.Forms;

namespace MasterChef.Mobile.Model
{
    public class LoginModel : INotifyPropertyChanged
    {
        private readonly IUserService _userService;

        public LoginModel()
        {
            var initializer = new ContainerInitializer();
            _userService = initializer.UserService;

            SubmitCommand = new Command(OnSubmit);
            RegisterCommand = new Command(OnRegister);
        }

        public Action InvalidLoginNotification;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public ICommand SubmitCommand { protected set; get; }
        public ICommand RegisterCommand { protected set; get; }


        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }
        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }

        public async void OnSubmit()
        {
            if (!_userService.VerifyLogin(_email, _password))
            {
                InvalidLoginNotification();
                this.Email = string.Empty;
                this.Password = string.Empty;

                return;
            }

            Application.Current.MainPage = new AppShell();
        }

        public async void OnRegister()
        {
            await App.Current.MainPage.Navigation.PushAsync(new RegisterView());
        }
    }
}
