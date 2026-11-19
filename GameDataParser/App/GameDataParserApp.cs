using System.Text.Json;

public class GameDataParserApp
{
    private readonly IConsoleUI _consoleUI;

    public GameDataParserApp(IConsoleUI consoleUI)
    {
        _consoleUI = consoleUI;
    }

    public void Run()
    {
        bool isFileRead = false;
        var fileContents = default(string);
        var fileName = default(string);

        do
        {
            try
            {
                fileName = _consoleUI.PromptForFileName();
                fileContents = File.ReadAllText(fileName);
                isFileRead = true;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("The filename cannot be null...");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("The filename cannot be empty...");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("The filename does not exist...");
            }
        } while (!isFileRead);

        List<VideoGame> videoGames = default;

        try
        {
            videoGames = JsonSerializer.Deserialize<List<VideoGame>>(fileContents);

        }
        catch (JsonException ex)
        {
            var originalColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"JSON in {fileName} file was not in a valid format. JSON body:");
            Console.WriteLine(fileContents);

            Console.ForegroundColor = originalColor;

            throw new JsonException($"{ex.Message} The file is {fileName}", ex);
        }

        if (videoGames.Count > 0)
        {
            _consoleUI.GamesHeader();
            foreach (var videoGame in videoGames)
            {
                Console.WriteLine(videoGame);
            }
            _consoleUI.PressAnyKey();
        }
        else
        {
            _consoleUI.FileIsEmpty();
            _consoleUI.PressAnyKey();
        }
    }
}

/*
games.json
gamesInvalid.json
*/
