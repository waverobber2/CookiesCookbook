namespace CookiesCookbook.Recipes.Ingredients;

public abstract class Ingredient
{
    public abstract int Id { get; }
    public abstract string Name { get; }
    public virtual string PrepInstructions => "Add to and mix with other ingredients";
    public override string ToString() => $"Id: {Id} - Ingredient: {Name}";
}


