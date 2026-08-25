namespace LoomKit.Esits.Tests;

public class ErrorTests
{
    [Fact]
    public void Constructor_WithCodeSubCodeMessage_InitializesEmptyDetails()
    {
        var error = new Error("code", "subCode", "message");

        Assert.Equal("code", error.Code);
        Assert.Equal("subCode", error.SubCode);
        Assert.Equal("message", error.Message);
        Assert.Empty(error.Details);
        Assert.Null(error.Exception);
    }

    [Fact]
    public void Constructor_WithDetails_UsesProvidedDetails()
    {
        var details = new Dictionary<string, List<string>> { { "field", ["required"] } };

        var error = new Error("code", "subCode", "message", details);

        Assert.Same(details, error.Details);
    }

    [Fact]
    public void Constructor_WithException_ExposesException()
    {
        var exception = new InvalidOperationException("failed");

        var error = new Error("code", "subCode", "message", exception);

        Assert.Same(exception, error.Exception);
        Assert.Empty(error.Details);
    }
}
