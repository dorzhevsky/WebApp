namespace LoggerManager
{
    /*
        Trace - Very detailed log messages, potentially of a high frequency and volume
        Debug -Less detailed and/or less frequent debugging messages
        Info - Informational messages
        Warn - Warnings which don't appear to the user of the application
        Error - Error messages
        Fatal - Fatal error messages. After a fatal error, the application usually terminates. 
     */
    public interface ILoggingManager
    {
        void LogInfo(string message);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogError(string message);
    }
}
