namespace CookiesCookbook.Recipes.Ingredients;

public abstract class Sweetener : Ingredient
{
    public override int Id { get; }
    public override string Name { get; } = null!;
    public override string PrepInstructions => $"Add to desired taste. {base.PrepInstructions}";
}


