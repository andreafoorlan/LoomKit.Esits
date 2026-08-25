using LoomKit.Esits.Errors;
using LoomKit.Esits.Extensions;

namespace LoomKit.Esits.Tests;

public class ErrorExtensionsTests
{
    [Fact]
    public void ToEsit_ReturnsFailedEsit_WithTheError()
    {
        var error = GenericErrors.GenericError("boom");

        var esit = error.ToEsit();

        Assert.True(esit.IsFailure);
        Assert.Same(error, esit.Error);
    }

    [Fact]
    public void ToEsit_Generic_ReturnsFailedEsit_WithTheError()
    {
        var error = GenericErrors.GenericError("boom");

        var esit = error.ToEsit<int>();

        Assert.True(esit.IsFailure);
        Assert.Same(error, esit.Error);
    }

    [Fact]
    public async Task AsTask_ReturnsCompletedTask_WrappingFailedEsit()
    {
        var error = GenericErrors.GenericError("boom");

        var esit = await error.AsTask();

        Assert.True(esit.IsFailure);
    }

    [Fact]
    public void AddDetail_AddsValue_WhenKeyDoesNotExist()
    {
        var error = GenericErrors.GenericError("boom");

        error.AddDetail("field", "required");

        Assert.Equal(["required"], error.Details["field"]);
    }

    [Fact]
    public void AddDetail_AppendsValue_WhenKeyAlreadyExists()
    {
        var error = GenericErrors.GenericError("boom");

        error.AddDetail("field", "required");
        error.AddDetail("field", "too long");

        Assert.Equal(["required", "too long"], error.Details["field"]);
    }
}
