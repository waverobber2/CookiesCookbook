using CookiesCookbook.Recipes;
/// <summary>
/// /////////////////////////////////////  RESIPES REPOSITORY   //////////////////////////
/// </summary>
public interface IRecipesRepository
{
    List<Recipe> Read();
    void Write(IEnumerable<Recipe> allRecipes);
}
