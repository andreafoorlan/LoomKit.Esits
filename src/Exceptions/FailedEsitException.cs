namespace LoomKit.Esits.Exceptions;

public class FailedEsitException : Exception
{
    public Esit Esit { get; init; }

    public FailedEsitException(Esit esit)
        : base(esit.Error?.Message)
    {
        // deps
        Esit = esit;
    }
}
