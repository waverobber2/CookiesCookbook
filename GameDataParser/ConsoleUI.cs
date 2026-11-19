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

/*
games.json
gamesInvalid.json
*/
