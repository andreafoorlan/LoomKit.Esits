namespace LoomKit.Esits.Extensions;

public static class EsitExtensions
{
    public static void Switch<TValue>(this Esit<TValue> esit, Action<TValue> success, Action<Error> failure, Action<Esit<TValue>>? then = null)
    {
        if (esit.IsSuccess)
            success(esit.Value!);
        else
            failure(esit.Error!);

        if (then is not null)
            then(esit);
    }

    public static Esit<TOut> Map<TIn, TOut>(this Esit<TIn> esit, Func<TIn, TOut> mapper)
    {
        if (esit.IsFailure)
            return Esit<TOut>.Failure(esit.Error!);

        var outValue = mapper(esit.Value!);

        return Esit<TOut>.Success(outValue);
    }

    public static TOut Match<TValue, TOut>(this Esit<TValue> esit, Func<TValue, TOut> success, Func<Error, TOut> failure)
    {
        if (esit.IsSuccess)
            return success(esit.Value!);
        else
            return failure(esit.Error!);
    }

    public static Task<Esit> AsTask(this Esit esit) => Task.FromResult(esit);

    public static Task<Esit<TValue>> AsTask<TValue>(this Esit<TValue> esit) => Task.FromResult(esit);

    // transform any object in successful esit
    public static Esit<TValue> ToEsit<TValue>(this TValue value) => Esit<TValue>.Success(value);

    public static Esit EnsureAllSucceeded(this Esit esit, params ReadOnlySpan<Esit> otherEsits)
    {
        if (esit.IsFailure)
            return esit;

        foreach (var otherEsit in otherEsits)
        {
            if (otherEsit.IsFailure)
            {
                return Esit.Failure(otherEsit.Error!);
            }
        }

        return esit;
    }

    public static Esit<TValue> EnsureAllSucceeded<TValue>(this Esit<TValue> esit, params ReadOnlySpan<Esit> otherEsits)
    {
        if (esit.IsFailure)
            return Esit<TValue>.Failure(esit.Error!);

        foreach (var otherEsit in otherEsits)
        {
            if (otherEsit.IsFailure)
            {
                return Esit<TValue>.Failure(otherEsit.Error!);
            }
        }

        return esit;
    }
}
