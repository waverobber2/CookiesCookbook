namespace CookiesCookbook.Recipes.Ingredients;

public abstract class Spice : Ingredient
{
    public override int Id { get; }
    public override string Name { get; } = null!;
    public override string PrepInstructions => $"Measure 1 tablespoon. {base.PrepInstructions}";
}


