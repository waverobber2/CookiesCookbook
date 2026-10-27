namespace CookiesCookbook.Recipes.Ingredients;

public class Egg : Ingredient
{
    public override int Id => 8;
    public override string Name => "Egg";
    public override string PrepInstructions => $"Crack and beat the meat. {base.PrepInstructions}";
}


