public interface IConsoleUI
{
    void FileIsEmpty();
    string? PromptForFileName();
    void PressAnyKey();
    void GamesHeader();
    string? ReadValidFilePathFromUser();
}

/*
games.json
gamesInvalid.json
*/
