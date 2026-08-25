using LoomKit.Esits;
using LoomKit.Esits.Errors;

namespace LoomKit.Esits.Tests;

public class EsitTests
{
    [Fact]
    public void Success_ReturnsSuccessfulEsit_WithNoError()
    {
        var esit = Esit.Success();

        Assert.True(esit.IsSuccess);
        Assert.False(esit.IsFailure);
        Assert.Null(esit.Error);
    }

    [Fact]
    public void Failure_ReturnsFailedEsit_WithError()
    {
        var error = GenericErrors.GenericError("boom");

        var esit = Esit.Failure(error);

        Assert.False(esit.IsSuccess);
        Assert.True(esit.IsFailure);
        Assert.Same(error, esit.Error);
    }

    [Fact]
    public void Success_Generic_ReturnsSuccessfulEsitWithValue()
    {
        var esit = Esit.Success(42);

        Assert.True(esit.IsSuccess);
        Assert.Equal(42, esit.Value);
        Assert.Null(esit.Error);
    }

    [Fact]
    public void SuccessOfTValue_ReturnsSuccessfulEsitWithValue()
    {
        var esit = Esit<int>.Success(42);

        Assert.True(esit.IsSuccess);
        Assert.Equal(42, esit.Value);
    }

    [Fact]
    public void FailureOfTValue_ReturnsFailedEsit_WithDefaultValue()
    {
        var error = GenericErrors.GenericError("boom");

        var esit = Esit<int>.Failure(error);

        Assert.True(esit.IsFailure);
        Assert.Equal(default, esit.Value);
        Assert.Same(error, esit.Error);
    }

    [Fact]
    public void AllSucceeded_ReturnsTrue_WhenEveryEsitSucceeded()
    {
        var esits = new[] { Esit.Success(), Esit.Success() };

        Assert.True(Esit.AllSucceeded(esits));
    }

    [Fact]
    public void AllSucceeded_ReturnsFalse_WhenAnyEsitFailed()
    {
        var esits = new[] { Esit.Success(), Esit.Failure(GenericErrors.GenericError("boom")) };

        Assert.False(Esit.AllSucceeded(esits));
    }

    [Fact]
    public void AllSucceeded_TreatsNullEntriesAsSucceeded()
    {
        Assert.True(Esit.AllSucceeded(null, Esit.Success()));
    }
}
