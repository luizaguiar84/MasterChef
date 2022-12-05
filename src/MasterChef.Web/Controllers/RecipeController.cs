using MasterChef.Application.Interfaces;
using MasterChef.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MasterChef.Web.Controllers
{
    public class RecipeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRecipeService _service;

        public RecipeController(
            ILogger<HomeController> logger,
            IRecipeService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var recipes = await _service.GetAll();
            return View(recipes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Recipe recipe)
        {
            _service.CreateNewRecipe(recipe);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Recipe recipe)
        {
            await _service.UpdateRecipe(recipe);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            Recipe recipe = await _service.GetById(id);

            return View(recipe);
        }

        public async Task<IActionResult> Details(int id)
        {
            var recipe = await _service.GetById(id);

            return View(recipe);
        }
    }
}
