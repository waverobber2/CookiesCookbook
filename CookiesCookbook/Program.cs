RecipesConsoleUI _recipesConsoleUI = new RecipesConsoleUI();
RecipesRepository _recipesRepository = new RecipesRepository("recipes.json");

var cookieRecipesApp = new CookieRecipesApp(_recipesConsoleUI, _recipesRepository);
cookieRecipesApp.Run();

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
        string allRecipes = _recipesRepository.ReadAllRecipes();

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
                    var ingredients = _recipesConsoleUI.PromptForIngredients();
                    if (!string.IsNullOrEmpty(ingredients))
                    {
                        string newRecipe = ingredients;
                        //allRecipes.Add(newRecipe);
                        _recipesRepository.WriteAllRecipes(allRecipes);
                        _recipesConsoleUI.DisplayNewRecipe(newRecipe);
                    }
                    else
                    {
                        Console.WriteLine("No ingredients were provided. Recipe not added.");
                    }
                    break;
                case "3":
                    isActive = false;
                    break;
            }

        } while (isActive);
    }
}

public interface IRecipesConsoleUI
{
    void DisplayAllRecipes(string recipes);
    void DisplayMainMenu();
    void DisplayNewRecipe(string newRecipe);
    string GetMenuInput();
    string? PromptForIngredients();
}

public class RecipesConsoleUI : IRecipesConsoleUI
{
    public void DisplayMainMenu()
    {
        Console.WriteLine("Welcome to the Cookie Recipes App!");
        Console.WriteLine("1. View all recipes");
        Console.WriteLine("2. Add a new recipe");
        Console.WriteLine("3. Exit");
    }

    public string GetMenuInput()
    {
        do
        {
            Console.Write("Please select an option: ");
            var input = Console.ReadLine();
            if (input == "1" || input == "2" || input == "3")
            {
                return input;
            }

            Console.WriteLine($"{input} is not valid. Try again.");
        } while (true);
    }

    public void DisplayAllRecipes(string recipes)
    {
        Console.WriteLine($"Displaying all recipes: {recipes}");
        GetAnyKeyPress();
    }

    public void DisplayNewRecipe(string newRecipe)
    {
        Console.WriteLine($"New recipe added: {newRecipe}");
        GetAnyKeyPress();
    }

    private void GetAnyKeyPress()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public string? PromptForIngredients()
    {
        Console.Write("Please enter the ingredients for the new recipe: ");
        return Console.ReadLine();
    }
}

public interface IRecipesRepository
{
    string ReadAllRecipes();
    void WriteAllRecipes(string allRecipes);
}

public class RecipesRepository : IRecipesRepository
{
    private readonly string _filePath;

    public RecipesRepository(string filePath)
    {
        _filePath = filePath;
    }

    public string ReadAllRecipes()
    {
        var result = $"Reading all recipes from {_filePath}";
        return result;
    }

    public void WriteAllRecipes(string allRecipes)
    {
        Console.WriteLine($"Writing all recipes: {allRecipes}");
        GetAnyKeyPress();
    }

    private void GetAnyKeyPress()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}