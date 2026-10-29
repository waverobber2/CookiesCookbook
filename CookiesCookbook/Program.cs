using CookiesCookbook.Recipes;
using CookiesCookbook.Recipes.Ingredients;

const string filePath = "recipes.txt";

StringsTextualRepository stringsTextualRepository = new();
IngredientRegister ingredientRegister = new();
RecipesConsoleUI recipesConsoleUI = new(ingredientRegister);
RecipesRepository recipesRepository = new(filePath, stringsTextualRepository, ingredientRegister);

var cookieRecipesApp = new CookieRecipesApp(recipesConsoleUI, recipesRepository);

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

/// <summary>
/// /////////////////////////////////////   RECIPES CONSOLE UI   //////////////////////////
/// </summary>

public interface IRecipesConsoleUI
{
    void DisplayAllRecipes(IEnumerable<Recipe> recipes);
    void DisplayMainMenu();
    void DisplayMessage(string msg);
    void GetAnyKeyPress();
    void DisplayNewRecipe(Recipe newRecipe);
    string GetMenuInput();
    void PromptForNewRecipe();
    IEnumerable<Ingredient>? PromptForIngredients();
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
        if (recipes is null || !recipes.Any())
        {
            DisplayMessage("There are no saved recipes.");
            GetAnyKeyPress();
        }
        else
        {
            DisplayMessage("Here's your saved recipes...");

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

    public void PromptForNewRecipe()
    {
        DisplayMessage($"Create a cookie recipe. Available ingrediients are...{Environment.NewLine}");
        foreach (var ingredient in _ingredientRegister.All)
        {
            Console.WriteLine(ingredient);
        }
    }

    public IEnumerable<Ingredient>? PromptForIngredients()
    {
        var ingredients = new List<Ingredient>();

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

/// <summary>
/// /////////////////////////////////// STRINGS REPOSITORY  ////////////////////////////////
/// </summary>

public interface IStringsRepository
{
    List<string> Read(string filePath);
    void Write(string filePath, List<string> strings);
}

public class StringsTextualRepository : IStringsRepository
{
    private static readonly string Separator = Environment.NewLine;

    public List<string> Read(string filePath)
    {
        var fileContents = File.ReadAllText(filePath);
        return fileContents.Split(Separator).ToList();
    }

    public void Write(string filePath, List<string> strings)
    {
        File.WriteAllText(filePath, string.Join(Separator, strings));
    }
}

/// <summary>
/// /////////////////////////////////////  RESIPES REPOSITORY   //////////////////////////
/// </summary>
public interface IRecipesRepository
{
    List<Recipe> Read();
    void Write(IEnumerable<Recipe> allRecipes);
}

public class RecipesRepository : IRecipesRepository
{
    private readonly IStringsRepository _stringsRepository;
    private readonly IIngredientRegister _ingredientRegister;
    private readonly string _filePath;
    private const string Separator = ",";

    public RecipesRepository(string filePath, IStringsRepository stringsRepository, IIngredientRegister ingredientRegister)
    {
        _filePath = filePath;
        _stringsRepository = stringsRepository;
        _ingredientRegister = ingredientRegister;
    }

    public List<Recipe> Read()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Recipe>();
        }

        var recipesFromFile = _stringsRepository.Read(_filePath);
        var allRecipes = new List<Recipe>();

        foreach (var recipeFromFile in recipesFromFile)
        {
            var recipe = RecipeFromString(recipeFromFile);
            allRecipes.Add(recipe);
        }

        return allRecipes;
    }

    private Recipe RecipeFromString(string recipeFromFile)
    {
        var ingredientIds = recipeFromFile.Split(Separator);
        var ingredients = new List<Ingredient>();

        foreach (var ingredientId in ingredientIds)
        {
            var ingredient = int.Parse(ingredientId);
            ingredients.Add(_ingredientRegister.GetById(ingredient));
        }
        return new Recipe(ingredients);
    }

    public void Write(IEnumerable<Recipe> allRecipes)
    {
        var recipesAsStrings = new List<string>();
        foreach (var recipe in allRecipes)
        {
            var allIds = new List<int>();
            foreach (var ingredient in recipe.Ingredients)
            {
                allIds.Add(ingredient.Id);
            }
            recipesAsStrings.Add(string.Join(Separator, allIds));
        }
        _stringsRepository.Write(_filePath, recipesAsStrings);
    }
}