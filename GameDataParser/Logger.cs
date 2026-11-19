internal class Logger
{
    private readonly string _logFileName;

    public Logger(string logFileName)
    {
        _logFileName = logFileName;
        File.AppendAllText(_logFileName, string.Empty);
    }

    public void Log(Exception ex)
    {
        var entry = $@"[{DateTime.Now}]: 
Exception Message:{ex.Message}
Stack Trace: {ex.StackTrace}


";
        File.AppendAllText(_logFileName, entry);
    }
}