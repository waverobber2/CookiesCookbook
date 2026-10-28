namespace CookiesCookbook.Recipes.Ingredients;

public class IngredientRegister : IIngredientRegister
{
    public IEnumerable<Ingredient> All { get; } = new List<Ingredient>
    {
        new WhiteFlour(),
        new WheatFlour(),
        new Sugar(),
        new Honey(),
        new Cinnamon(),
        new Nutmeg(),
        new Butter(),
        new Egg()
    };

    public Ingredient? GetById(int id)
    {
        foreach (var ingredient in All)
        {
            if (ingredient.Id == id)
            {
                return ingredient;
            }
        }
        return null;
    }
}
