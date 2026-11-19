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

    public string? ReadValidFilePathFromUser()
    {
        bool isFilePthValid = false;
        var fileName = default(string);

        do
        {
            fileName = PromptForFileName();

            if (fileName is null)
            {
                Console.WriteLine("The filename cannot be null...");
            }
            else if (fileName.Length == 0)
            {
                Console.WriteLine("The filename cannot be empty...");
            }
            else if (!File.Exists(fileName))
            {
                Console.WriteLine("The filename does not exist...");
            }
            else
            {
                isFilePthValid = true;
            }
        } while (!isFilePthValid);

        return fileName;
    }
}

/*
games.json
gamesInvalid.json
*/
