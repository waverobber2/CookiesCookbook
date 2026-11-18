using CookiesCookbook.Recipes;
using CookiesCookbook.Recipes.Ingredients;
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
