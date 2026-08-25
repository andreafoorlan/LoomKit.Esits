namespace LoomKit.Esits;

public class Error
{
    //
    private readonly string _code;
    private readonly string _subCode;
    private readonly string _message;
    private readonly Dictionary<string, List<string>> _details;
    private readonly Exception? _exception;

    // props
    public string Code => _code;
    public string SubCode => _subCode;
    public string Message => _message;
    public Dictionary<string, List<string>> Details => _details;
    public Exception? Exception => _exception;

    public Error(string code, string subCode, string message)
    {
        // deps
        _code = code;
        _subCode = subCode;
        _message = message;
        _details = new();
    }

    public Error(string code, string subCode, string message, Dictionary<string, List<string>>? details = null)
    {
        // deps
        _code = code;
        _subCode = subCode;
        _message = message;
        _details = details ?? new();
    }

    public Error(string code, string subCode, string message, Exception? exception = null)
    {
        // deps
        _code = code;
        _subCode = subCode;
        _message = message;
        _details = new();
        _exception = exception;
    }
}
