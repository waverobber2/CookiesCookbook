using CookiesCookbook.Recipes.Ingredients;

namespace CookiesCookbook.Recipes;

public class Recipe
{
    public IEnumerable<Ingredient> Ingredients { get; }

    public Recipe(IEnumerable<Ingredient> ingredients)
    {
        Ingredients = ingredients;
    }

    public override string ToString()
    {
        var steps = Ingredients.Select(ingredient => $"{ingredient.Name} - {ingredient.PrepInstructions}");
        return string.Join(Environment.NewLine, steps);
    }
}
