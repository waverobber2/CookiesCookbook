namespace CookiesCookbook.Recipes.Ingredients;

public abstract class Flour : Ingredient
{
    public override int Id { get; }
    public override string Name { get; } = null!;
    public override string PrepInstructions => $"Sift. {base.PrepInstructions}";
}


