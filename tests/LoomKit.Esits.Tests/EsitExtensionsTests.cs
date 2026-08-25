using LoomKit.Esits.Errors;
using LoomKit.Esits.Extensions;

namespace LoomKit.Esits.Tests;

public class EsitExtensionsTests
{
    [Fact]
    public void Switch_InvokesSuccess_WhenEsitSucceeded()
    {
        var esit = Esit.Success(42);
        int? successValue = null;

        esit.Switch(success: v => successValue = v, failure: _ => Assert.Fail("should not run"));

        Assert.Equal(42, successValue);
    }

    [Fact]
    public void Switch_InvokesFailure_WhenEsitFailed()
    {
        var error = GenericErrors.GenericError("boom");
        var esit = Esit<int>.Failure(error);
        Error? failureError = null;

        esit.Switch(success: _ => Assert.Fail("should not run"), failure: e => failureError = e);

        Assert.Same(error, failureError);
    }

    [Fact]
    public void Switch_InvokesThen_RegardlessOfOutcome()
    {
        var esit = Esit.Success(42);
        var thenCalled = false;

        esit.Switch(success: _ => { }, failure: _ => { }, then: _ => thenCalled = true);

        Assert.True(thenCalled);
    }

    [Fact]
    public void Map_TransformsValue_WhenEsitSucceeded()
    {
        var esit = Esit.Success(42);

        var mapped = esit.Map(v => v.ToString());

        Assert.True(mapped.IsSuccess);
        Assert.Equal("42", mapped.Value);
    }

    [Fact]
    public void Map_ReturnsFailure_WhenEsitFailed()
    {
        var error = GenericErrors.GenericError("boom");
        var esit = Esit<int>.Failure(error);

        var mapped = esit.Map(v => v.ToString());

        Assert.True(mapped.IsFailure);
        Assert.Same(error, mapped.Error);
    }

    [Fact]
    public void Match_ReturnsSuccessBranch_WhenEsitSucceeded()
    {
        var esit = Esit.Success(42);

        var result = esit.Match(success: v => v.ToString(), failure: e => e.Message);

        Assert.Equal("42", result);
    }

    [Fact]
    public void Match_ReturnsFailureBranch_WhenEsitFailed()
    {
        var esit = Esit<int>.Failure(GenericErrors.GenericError("boom"));

        var result = esit.Match(success: v => v.ToString(), failure: e => e.Message);

        Assert.Equal("boom", result);
    }

    [Fact]
    public void ToEsit_WrapsValueAsSuccessfulEsit()
    {
        var esit = 42.ToEsit();

        Assert.True(esit.IsSuccess);
        Assert.Equal(42, esit.Value);
    }

    [Fact]
    public async Task AsTask_ReturnsCompletedTask_WrappingTheEsit()
    {
        var esit = Esit.Success();

        var result = await esit.AsTask();

        Assert.Same(esit, result);
    }

    [Fact]
    public void EnsureAllSucceeded_ReturnsOriginalFailure_WhenEsitAlreadyFailed()
    {
        var error = GenericErrors.GenericError("boom");
        var esit = Esit.Failure(error);

        var result = esit.EnsureAllSucceeded(Esit.Success());

        Assert.Same(error, result.Error);
    }

    [Fact]
    public void EnsureAllSucceeded_ReturnsFailureFromOthers_WhenAnyOtherEsitFailed()
    {
        var esit = Esit.Success();
        var otherError = GenericErrors.GenericError("other boom");

        var result = esit.EnsureAllSucceeded(Esit.Success(), Esit.Failure(otherError));

        Assert.True(result.IsFailure);
        Assert.Same(otherError, result.Error);
    }

    [Fact]
    public void EnsureAllSucceeded_ReturnsOriginalEsit_WhenEverythingSucceeded()
    {
        var esit = Esit.Success();

        var result = esit.EnsureAllSucceeded(Esit.Success(), Esit.Success());

        Assert.Same(esit, result);
    }

    [Fact]
    public void EnsureAllSucceeded_Generic_ReturnsOriginalFailure_WhenEsitAlreadyFailed()
    {
        var error = GenericErrors.GenericError("boom");
        var esit = Esit<int>.Failure(error);

        var result = esit.EnsureAllSucceeded(Esit.Success());

        Assert.Same(error, result.Error);
    }

    [Fact]
    public void EnsureAllSucceeded_Generic_ReturnsFailureFromOthers_WhenAnyOtherEsitFailed()
    {
        var esit = Esit.Success(42);
        var otherError = GenericErrors.GenericError("other boom");

        var result = esit.EnsureAllSucceeded(Esit.Success(), Esit.Failure(otherError));

        Assert.True(result.IsFailure);
        Assert.Same(otherError, result.Error);
    }

    [Fact]
    public void EnsureAllSucceeded_Generic_ReturnsOriginalEsit_WhenEverythingSucceeded()
    {
        var esit = Esit.Success(42);

        var result = esit.EnsureAllSucceeded(Esit.Success(), Esit.Success());

        Assert.Same(esit, result);
    }
}
