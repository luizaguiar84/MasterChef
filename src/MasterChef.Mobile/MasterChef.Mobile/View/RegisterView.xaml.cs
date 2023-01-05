using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterChef.Mobile.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MasterChef.Mobile.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterView : ContentPage
    {
        public RegisterView()
        {
            var model = new RegisterModel();
            this.BindingContext = model;

            model.InvalidPasswordNotification+= () => DisplayAlert("Erro", "Senhas não conferem!", "OK");
            
            model.ExistingUserNotification += () => DisplayAlert("Erro", "Usuário já cadastrado!", "OK");
            
            model.CreatedUserNotification += () => DisplayAlert("Sucesso!", "Usuário cadastrado com sucesso!", "OK");
            
            model.ErrorCreatingUserNotification += () => DisplayAlert("Erro", "Erro ao criar usuário, tente novamente!", "OK");

        InitializeComponent();

        }
    }
}