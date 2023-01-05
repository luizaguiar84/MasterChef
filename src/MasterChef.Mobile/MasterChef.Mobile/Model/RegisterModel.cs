using MasterChef.Mobile.Initillizer;
using System;
using System.ComponentModel;
using System.Windows.Input;
using MasterChef.Mobile.Interface;
using Xamarin.Forms;

namespace MasterChef.Mobile.Model
{
    public class RegisterModel : INotifyPropertyChanged
    {
        private readonly IUserService _userService;

        public RegisterModel()
        {
            var initializer = new ContainerInitializer();
            _userService = initializer.UserService;
            SubmitCommand = new Command(OnSubmit);
            LoginCommand = new Command(OnLogin);
        }
        public Action InvalidPasswordNotification;
        public Action ExistingUserNotification;
        public Action CreatedUserNotification;
        public Action ErrorCreatingUserNotification;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public ICommand SubmitCommand { protected set; get; }
        public ICommand LoginCommand { protected set; get; }

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

        private string _confirmPassword;

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ConfirmPassword"));
            }
        }

        public async void OnSubmit()
        {
            if (!_password.Equals(_confirmPassword))
            {
                InvalidPasswordNotification();
                this.Email = string.Empty;
                this.Password = string.Empty;
                this.ConfirmPassword = string.Empty;
                return;
            }

            if (_userService.VerifyLogin(_email, _password))
            {
                ExistingUserNotification();
                this.Email = string.Empty;
                this.Password = string.Empty;
                this.ConfirmPassword = string.Empty;
                return;

            }

            var created = _userService.CreateNewUser(_email, _password);

            if (created)
            {
                CreatedUserNotification();
                await App.Current.MainPage.Navigation.PopAsync(true);
            }

            else
            {
                ErrorCreatingUserNotification();
                this.Email = string.Empty;
                this.Password = string.Empty;
                this.ConfirmPassword = string.Empty;
                return;
            }


        }

        public async void OnLogin()
        {
            await App.Current.MainPage.Navigation.PopAsync(true);

        }
    }
}
