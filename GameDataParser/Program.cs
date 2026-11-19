using System.Text.Json;

GameDataParserApp gameDataParser = new(new ConsoleUI(), new GameRepository());
gameDataParser.Run();

public class GameDataParserApp
{
    private readonly IConsoleUI _consoleUI;
    private readonly IGameRepository _gameRepository;

    public GameDataParserApp(IConsoleUI consoleUI, IGameRepository gameRepository)
    {
        _consoleUI = consoleUI;
        _gameRepository = gameRepository;
    }

    public void Run()
    {
        while (true)
        {
            var fileName = _consoleUI.PromptForFileName();
            if (fileName is null)
            {
                return;
            }

            List<VideoGame> videoGames;
            try
            {
                videoGames = _gameRepository.Read(fileName);
            }
            catch (Exception ex) when (ex is IOException or JsonException or UnauthorizedAccessException or ArgumentException or NotSupportedException)
            {
                Console.Error.WriteLine($"Error reading '{fileName}': {ex.Message}");
                continue;
            }

            if (videoGames.Count > 0)
            {
                foreach (var game in videoGames)
                {
                    Console.WriteLine(game);
                }
            }
            else
            {
                Console.WriteLine("No games have been recorded...");
            }
        }
    }
}

public interface IConsoleUI
{
    string? PromptForFileName();
}

public class ConsoleUI : IConsoleUI
{
    public string? PromptForFileName()
    {
        Console.Write("Enter the name of the file you want to read: ");
        return Console.ReadLine();
    }
}

public interface IGameRepository
{
    List<VideoGame> Read(string fileName);
}

public class GameRepository : IGameRepository
{
    public List<VideoGame> Read(string fileName)
    {
        using var fileStream = File.OpenRead(fileName);
        return NewMethod(fileStream);
    }

    private static List<VideoGame> NewMethod(FileStream fileStream)
    {
        return JsonSerializer.Deserialize<List<VideoGame>>(fileStream) ??
            throw new JsonException("The file did not contain a video game list.");
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
