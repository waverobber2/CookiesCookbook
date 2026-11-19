using System.Text.Json;

GameDataParserApp gameDataParser = new(new ConsoleUI());
gameDataParser.Run();

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

        do
        {
            try
            {
                string fileName = _consoleUI.PromptForFileName();
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

        var videoGames = JsonSerializer.Deserialize<List<VideoGame>>(fileContents);


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

public interface IConsoleUI
{
    void FileIsEmpty();
    string? PromptForFileName();
    void PressAnyKey();
    void GamesHeader();
}

public class ConsoleUI : IConsoleUI
{
    public void FileIsEmpty()
    {
        Console.WriteLine("The file does not have any saved games...");
    }

    public string? PromptForFileName()
    {
        Console.Write($"{Environment.NewLine}Enter the name of the file you want to read: ");
        return Console.ReadLine();
    }

    public void PressAnyKey()
    {
        Console.Write($"{Environment.NewLine}Press any key to continue");
        Console.ReadKey();
    }

    public void GamesHeader()
    {
        Console.WriteLine($"{Environment.NewLine}Loaded games are:{Environment.NewLine}");
    }
}

public class VideoGame
{
    public string Title { get; init; }
    public int ReleaseYear { get; init; }
    public decimal Rating { get; init; }
    public override string ToString() => $"{Title} was released {ReleaseYear} and is rated {Rating}";
}

/*
games.json
gamesInvalid.json
*/
