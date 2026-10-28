using CookiesCookbook.Recipes;
using CookiesCookbook.Recipes.Ingredients;

IngredientRegister _ingredientRegister = new();
RecipesConsoleUI _recipesConsoleUI = new(_ingredientRegister);
RecipesRepository _recipesRepository = new("recipes.json");

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
        List<Recipe> allRecipes = _recipesRepository.ReadAllRecipes();

        bool isActive = true;
        do
        {
            _recipesConsoleUI.DisplayMainMenu();
            var userInput = _recipesConsoleUI.GetMenuInput();

            if (userInput == "1")
            {
                _recipesConsoleUI.DisplayAllRecipes(allRecipes);
            }
            else if (userInput == "2")
            {
                var ingredients = _recipesConsoleUI.PromptForIngredients();
                if (!string.IsNullOrEmpty(ingredients))
                {
                    string newRecipe = ingredients;
                    //allRecipes.Add(newRecipe);
                    //_recipesRepository.WriteAllRecipes(allRecipes);
                    //_recipesConsoleUI.DisplayNewRecipe(newRecipe);
                }
                else
                {
                    Console.WriteLine("No ingredients were provided. Recipe not added.");
                }
            }
            else if (userInput == "3")
            {
                isActive = false;
            }

        } while (isActive);
    }
}

public interface IRecipesConsoleUI
{
    void DisplayAllRecipes(IEnumerable<Recipe> recipes);
    void DisplayMainMenu();
    void DisplayNewRecipe(string newRecipe);
    string GetMenuInput();
    string? PromptForIngredients();
}

public class RecipesConsoleUI : IRecipesConsoleUI
{
    private readonly IIngredientRegister _ingredientRegister;

    public RecipesConsoleUI(IIngredientRegister ingredientRegister)
    {
        _ingredientRegister = ingredientRegister;
    }

    public void DisplayMainMenu()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Cookie Recipes App!");
        Console.WriteLine($"{Environment.NewLine}1. View all recipes");
        Console.WriteLine("2. Add a new recipe");
        Console.WriteLine("3. Exit");
    }

    public string GetMenuInput()
    {
        do
        {
            Console.Write($"{Environment.NewLine}Please select a menu option: ");
            var input = Console.ReadLine();
            if (input == "1" || input == "2" || input == "3")
            {
                return input;
            }

            Console.WriteLine($"{input} is not valid. Try again.");
        } while (true);
    }

    public void DisplayAllRecipes(IEnumerable<Recipe> recipes)
    {
        if (recipes is null)
        {
            DisplayMessage("There are no saved recipes.");
            GetAnyKeyPress();
        }
        else
        {
            DisplayMessage("Hers's your saved recipes...");

            int counter = 1;
            foreach (var recipe in recipes)
            {
                Console.WriteLine();
                Console.WriteLine($"***** {counter} *****");
                Console.WriteLine(recipe);
                counter++;
            }
            GetAnyKeyPress();
        }
    }

    public void DisplayNewRecipe(string newRecipe)
    {
        Console.WriteLine($"New recipe added: {newRecipe}");
        GetAnyKeyPress();
    }

    private void GetAnyKeyPress()
    {
        Console.WriteLine($"{Environment.NewLine}Press any key to continue...");
        Console.ReadKey();
    }

    private void DisplayMessage(string msg)
    {
        Console.Clear();
        Console.WriteLine(msg);
    }

    public string? PromptForIngredients()
    {
        Console.Write("Enter the ingredients for the new recipe: ");
        return Console.ReadLine();
    }
}

public interface IRecipesRepository
{
    List<Recipe> ReadAllRecipes();
    void WriteAllRecipes(string allRecipes);
}

public class RecipesRepository : IRecipesRepository
{
    private readonly string _filePath;

    public RecipesRepository(string filePath)
    {
        _filePath = filePath;
    }

    public List<Recipe> ReadAllRecipes()
    {
        return new List<Recipe>
        {
            new Recipe(new List<Ingredient>
            {
                new WhiteFlour(),
                new Sugar(),
                new Butter(),
                new Egg()
            }),
            new Recipe(new List<Ingredient>
            {
                new WheatFlour(),
                new Honey(),
                new Cinnamon(),
                new Nutmeg()
            })
        };
    }

    public void WriteAllRecipes(string allRecipes)
    {
        Console.WriteLine($"Writing all recipes: {allRecipes}");
    }
}