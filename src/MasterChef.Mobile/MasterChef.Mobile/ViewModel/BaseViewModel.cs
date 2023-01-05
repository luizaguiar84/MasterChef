using MasterChef.Mobile.Initillizer;
using MasterChef.Mobile.Interface;
using MasterChef.Mobile.Model;
using MasterChef.Mobile.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace MasterChef.Mobile.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IRecipeService recipeService;
        public IImagemService imageService;
        public IIngredientesService ingredientesService;
        public BaseViewModel()
        {
            var initializer = new ContainerInitializer();
            recipeService = initializer.recipeService;
            imageService = initializer.imagemService;
            ingredientesService= initializer.ingredientesService;
        }
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();
        public bool IsNotBusy { get => !isBusy; }

        private bool isVisible;
        public bool IsVisible
        {
            get { return isVisible; }

            set
            {
                SetProperty(ref isVisible, value);
            }
        }


        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
            {
                return;
            }

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
