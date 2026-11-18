using CookiesCookbook.Recipes;
using CookiesCookbook.Recipes.Ingredients;

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