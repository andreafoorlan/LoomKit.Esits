namespace LoomKit.Esits.Extensions;

public static class ErrorExtensions
{
    public static Esit ToEsit(this Error error) => Esit.Failure(error);
    public static Esit<TValue> ToEsit<TValue>(this Error error) => Esit<TValue>.Failure(error);

    public static Task<Esit> AsTask(this Error error) => Task.FromResult(Esit.Failure(error));
    public static Task<Esit<TValue>> AsTask<TValue>(this Error error) => Task.FromResult(Esit<TValue>.Failure(error));

    public static void AddDetail(this Error error, string key, string value)
    {
        //
        if (!error.Details.ContainsKey(key))
            error.Details.Add(key, []);

        //
        error.Details[key].Add(value);
    }
}
