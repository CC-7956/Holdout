namespace PVE.Exceptions;

public class SaveFileCorruptedException(string exceptionMessage, string json, Exception? innerException = null) : Exception(exceptionMessage, innerException)
{
    public string Json { get; private set; } = json;
}