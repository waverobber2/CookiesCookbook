ConsoleUI ui = new ConsoleUI();
var app = new GameDataParserApp(ui);
var logger = new Logger("log.txt");

try
{
    app.Run();
}
catch (Exception ex)
{
    logger.Log(ex);
    Console.WriteLine("Sorry, the application has encountered a unexpected error and will self destruct!");
}
ui.PressAnyKey();