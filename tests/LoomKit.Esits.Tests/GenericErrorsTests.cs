using System.Net;
using LoomKit.Esits.Errors;

namespace LoomKit.Esits.Tests;

public class GenericErrorsTests
{
    [Fact]
    public void NullObjectError_UsesTypeName_WhenTypeProvided()
    {
        var error = GenericErrors.NullObjectError(typeof(string));

        Assert.Equal(GenericErrors.NullObject, error.Code);
        Assert.Contains("String", error.Message);
    }

    [Fact]
    public void NullObjectError_Generic_UsesGenericTypeName()
    {
        var error = GenericErrors.NullObjectError<int>();

        Assert.Contains("Int32", error.Message);
    }

    [Fact]
    public void NotFoundError_UsesEntityTypeNameAndId()
    {
        var error = GenericErrors.NotFoundError(typeof(string), "42");

        Assert.Equal(GenericErrors.NotFound, error.Code);
        Assert.Equal("String with id 42 not found.", error.Message);
    }

    [Fact]
    public void ConflictError_UsesEntityTypeNameAndId()
    {
        var error = GenericErrors.ConflictError(typeof(string), "42");

        Assert.Equal(GenericErrors.Conflict, error.Code);
        Assert.Equal("String with id 42 already exists.", error.Message);
    }

    [Fact]
    public void ValidationError_WithSubjectAndError_AddsSingleDetail()
    {
        var error = GenericErrors.ValidationError("field", "is required");

        Assert.Equal(GenericErrors.Validation, error.Code);
        Assert.Equal(["is required"], error.Details["field"]);
    }

    [Fact]
    public void ValidationError_WithSubjectAndErrors_AddsAllDetails()
    {
        var error = GenericErrors.ValidationError("field", ["is required", "too long"]);

        Assert.Equal(["is required", "too long"], error.Details["field"]);
    }

    [Fact]
    public void UnauthorizedFlowError_UsesDefaultMessage_WhenNoneProvided()
    {
        var error = GenericErrors.UnauthorizedFlowError();

        Assert.Equal("Unauthorized", error.Message);
    }

    [Fact]
    public void ForbiddenError_UsesDefaultMessage_WhenNoneProvided()
    {
        var error = GenericErrors.ForbiddenError();

        Assert.Equal(GenericErrors.Forbidden, error.Code);
        Assert.Equal("Forbidden", error.Message);
    }

    [Fact]
    public void UnsuccessfulHttpStatusCodeError_UsesStatusCodeAsSubCode()
    {
        var error = GenericErrors.UnsuccessfulHttpStatusCodeError(HttpStatusCode.NotFound);

        Assert.Equal(GenericErrors.UnsuccessfulHttpStatusCode, error.Code);
        Assert.Equal(nameof(HttpStatusCode.NotFound), error.SubCode);
    }

    [Fact]
    public void UnexpectedError_UsesExceptionMessage()
    {
        var exception = new InvalidOperationException("failed");

        var error = GenericErrors.UnexpectedError(exception);

        Assert.Equal(GenericErrors.Unexpected, error.Code);
        Assert.Equal("failed", error.Message);
    }
}
