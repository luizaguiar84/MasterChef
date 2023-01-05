using MasterChef.Mobile.Interface;
using MasterChef.Mobile.Services;
using SimpleInjector;

namespace MasterChef.Mobile.Initillizer
{
    public class ContainerInitializer
    {
        public  IConnectionService service { get; set; }
        public  IRecipeService recipeService { get; set; }
        public  IImagemService imagemService { get; set; }
        public  IIngredientesService ingredientesService { get; set; }
        public IUserService UserService { get; set; }

        public ContainerInitializer()
        {
            InicializarContainers();
        }

        public void InicializarContainers()
        {
            var container = new Container();
            container.Register<IConnectionService, ConnectionService>(Lifestyle.Transient);
            container.Register<IRecipeService, RecipeService>(Lifestyle.Transient);
            container.Register<IImagemService, ImagemService>(Lifestyle.Transient);
            container.Register<IIngredientesService, IngredienteService>(Lifestyle.Transient);
            container.Register<IUserService, UserService>(Lifestyle.Transient);
            container.Verify();
            service = container.GetInstance<IConnectionService>();
            recipeService = container.GetInstance<IRecipeService>();
            imagemService = container.GetInstance<IImagemService>();
            ingredientesService = container.GetInstance<IIngredientesService>();
            UserService = container.GetInstance<IUserService>();
        }
    }
}
