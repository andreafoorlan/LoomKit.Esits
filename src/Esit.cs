namespace LoomKit.Esits;

public class Esit
{
    // fields
    protected bool _isSuccess;
    protected Error? _error;

    // constructors
    protected Esit(Error error)
    {
        _isSuccess = false;
        _error = error;
    }

    protected Esit()
    {
        _isSuccess = true;
        _error = default;
    }

    // properties
    public bool IsSuccess => _isSuccess;
    public bool IsFailure => !_isSuccess;

    public Error? Error => !_isSuccess && _error is not null ? _error : default;

    // factories
    public static Esit Success() => new Esit();
    public static Esit<TValue> Success<TValue>(TValue value) => Esit<TValue>.Success(value);
    public static Esit Failure(Error error) => new Esit(error);

    // helpers
    public static bool AllSucceeded(params ReadOnlySpan<Esit?> esits)
    {
        foreach (var esit in esits)
        {
            if (esit is not null && esit.IsFailure)
                return false;
        }

        return true;
    }
}

public class Esit<TValue> : Esit
{
    // fields
    protected TValue? _value;

    // constructors
    protected Esit(Error error)
        : base(error)
    {
        _value = default;
    }

    protected Esit(TValue value)
        : base()
    {
        _value = value;
    }

    // properties
    public TValue? Value => _isSuccess ? _value : default;

    // factories
    protected new static Esit<TValue> Success() => throw new InvalidOperationException("Cannot create a success esit without a value");
    public static Esit<TValue> Success(TValue value) => new Esit<TValue>(value);
    public new static Esit<TValue> Failure(Error error) => new Esit<TValue>(error);
}
