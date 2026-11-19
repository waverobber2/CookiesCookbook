GameDataParserApp gameDataParser = new(new ConsoleUi());

public class GameDataParserApp
{
    private readonly IConsoleUi _consoleUi;

    public GameDataParserApp(IConsoleUi consoleUi)
    {
        _consoleUi = consoleUi;
    }

    public void Run()
    {
        var fileContents = _consoleUi.PromptForFileName();
    }
}

public interface IConsoleUi
{
    string? PromptForFileName();
}

public class ConsoleUi : IConsoleUi
{
    public string? PromptForFileName()
    {
        Console.WriteLine("Enter the name of the file you want to read: ");
        var fileContents = Console.ReadLine();

        return fileContents;
    }
}

/*
games.json
gamesInvalidFormat.json
*/
