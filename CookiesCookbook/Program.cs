using CookiesCookbook.Recipes.Ingredients;

const string FileName = "recipes";
const FileFormat Format = FileFormat.Json;

var fileMetaData = new FileMetaData(FileName, Format);

IStringsRepository stringsRepository = Format switch
{
    FileFormat.Json => new StringsJsonRepository(),
    FileFormat.Txt => new StringsTextualRepository(),
    _ => throw new NotSupportedException($"File format '{Format}' is not supported.")
};

IngredientRegister ingredientRegister = new();
RecipesConsoleUI recipesConsoleUI = new(ingredientRegister);
RecipesRepository recipesRepository = new(fileMetaData.ToPath, stringsRepository, ingredientRegister);

var cookieRecipesApp = new CookieRecipesApp(recipesConsoleUI, recipesRepository);

try
{
    cookieRecipesApp.Run();
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Application encountered an unexpected error: {ex.Message}");
    Environment.ExitCode = 1;
}
