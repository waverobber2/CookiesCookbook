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
                var ingredients = _recipesConsoleUI.PromptForNewRecipe();
                if (ingredients != null && ingredients.Any())
                {
                    Recipe newRecipe = new Recipe(ingredients);
                    allRecipes.Add(newRecipe);
                    //_recipesRepository.WriteAllRecipes(allRecipes);
                    _recipesConsoleUI.DisplayNewRecipe(newRecipe);
                }
                else
                {
                    _recipesConsoleUI.DisplayMessage("No ingredients were selected. Recipe not created.");
                    _recipesConsoleUI.GetAnyKeyPress();
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
    void DisplayMessage(string msg);
    void GetAnyKeyPress();
    void DisplayNewRecipe(Recipe newRecipe);
    string GetMenuInput();
    IEnumerable<Ingredient>? PromptForNewRecipe();
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

    public void DisplayNewRecipe(Recipe newRecipe)
    {
        Console.WriteLine();
        Console.WriteLine($"***** New recipe added! *****");
        foreach (var ingredient in newRecipe.Ingredients)
        {
            Console.WriteLine(ingredient);
        }
        GetAnyKeyPress();

    }

    public void GetAnyKeyPress()
    {
        Console.WriteLine($"{Environment.NewLine}Press any key to continue...");
        Console.ReadKey();
    }

    public void DisplayMessage(string msg)
    {
        Console.Clear();
        Console.WriteLine(msg);
    }

    IEnumerable<Ingredient>? IRecipesConsoleUI.PromptForNewRecipe()
    {
        var ingredients = new List<Ingredient>();
        DisplayMessage($"Create a cookie recipe. Available ingrediients are...{Environment.NewLine}");
        foreach (var ingredient in _ingredientRegister.All)
        {
            Console.WriteLine(ingredient);
        }
        Console.WriteLine();
        do
        {
            Console.Write($"Enter ingredients by id, or 'Q' when done: ");
            var userInput = Console.ReadLine();
            if (userInput?.ToUpper() == "Q")
            {
                break;
            }

            if (int.TryParse(userInput, out int ingredientId))
            {
                var ingredient = _ingredientRegister.GetById(ingredientId);
                if (ingredient != null)
                {
                    ingredients.Add(ingredient);
                    Console.WriteLine($"{ingredient.Name} added.");
                }
                else
                {
                    Console.WriteLine($"Ingredient with id {ingredientId} not found.");
                }
            }
            else
            {
                Console.WriteLine($"{userInput} is not a valid id. Try again.");
            }
        } while (true);

        return ingredients;
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