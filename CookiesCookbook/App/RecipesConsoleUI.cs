using CookiesCookbook.Recipes;
using CookiesCookbook.Recipes.Ingredients;

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
