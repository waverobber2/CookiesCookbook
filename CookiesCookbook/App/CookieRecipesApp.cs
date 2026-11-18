using CookiesCookbook.Recipes;

public class CookieRecipesApp
{
    private readonly IRecipesConsoleUI _recipesConsoleUI;
    private readonly IRecipesRepository _recipesRepository;

    public CookieRecipesApp(IRecipesConsoleUI recipesConsoleUI, IRecipesRepository recipesRepository)
    {
        _recipesConsoleUI = recipesConsoleUI;
        _recipesRepository = recipesRepository;
    }

    public void Run()
    {
        List<Recipe> allRecipes = _recipesRepository.Read();

        bool isActive = true;
        do
        {
            _recipesConsoleUI.DisplayMainMenu();
            var userInput = _recipesConsoleUI.GetMenuInput();

            switch (userInput)
            {
                case "1":
                    _recipesConsoleUI.DisplayAllRecipes(allRecipes);
                    break;

                case "2":
                    _recipesConsoleUI.PromptForNewRecipe();
                    var ingredients = _recipesConsoleUI.PromptForIngredients();

                    if (ingredients != null && ingredients.Any())
                    {
                        Recipe newRecipe = new Recipe(ingredients);
                        allRecipes.Add(newRecipe);
                        _recipesRepository.Write(allRecipes);
                        _recipesConsoleUI.DisplayNewRecipe(newRecipe);
                    }
                    else
                    {
                        _recipesConsoleUI.DisplayMessage("No ingredients were selected. Recipe not created.");
                        _recipesConsoleUI.GetAnyKeyPress();
                    }
                    break;

                case "3":
                    isActive = false;
                    break;

                default:
                    _recipesConsoleUI.DisplayMessage("Invalid option. Please try again.");
                    _recipesConsoleUI.GetAnyKeyPress();
                    break;
            }

        } while (isActive);
    }
}
