using MasterChef.Mobile.Interface;
using MasterChef.Mobile.Services;
using SimpleInjector;

namespace MasterChef.Mobile.Initializer
{
    public class ContainerInitializer
    {
        public  IConnectionService Service { get; set; }
        public  IRecipeService RecipeService { get; set; }
        public  IImageService ImageService { get; set; }
        public  IIngredientService IngredientsService { get; set; }
        public IUserService UserService { get; set; }

        public ContainerInitializer()
        {
            InitializeContainers();
        }

        public void InitializeContainers()
        {
            var container = new Container();
            container.Register<IConnectionService, ConnectionService>(Lifestyle.Transient);
            container.Register<IRecipeService, RecipeService>(Lifestyle.Transient);
            container.Register<IImageService, ImageService>(Lifestyle.Transient);
            container.Register<IIngredientService, IngredientService>(Lifestyle.Transient);
            container.Register<IUserService, UserService>(Lifestyle.Transient);
            container.Verify();
            Service = container.GetInstance<IConnectionService>();
            RecipeService = container.GetInstance<IRecipeService>();
            ImageService = container.GetInstance<IImageService>();
            IngredientsService = container.GetInstance<IIngredientService>();
            UserService = container.GetInstance<IUserService>();
        }
    }
}
