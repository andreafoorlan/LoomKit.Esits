# LoomKit.Esits

A lightweight Result/Either-style outcome type for .NET. `Esit` and `Esit<TValue>` represent the outcome of an operation — success, or a structured `Error` — without relying on exceptions for control flow.

> **Status:** early stage. The public API may still change between versions — pin a commit/tag if you depend on it.

## Why

Returning `Esit`/`Esit<TValue>` from a method instead of throwing lets callers branch on `IsSuccess`/`IsFailure` explicitly, and lets you compose outcomes with `Map`, `Match`, `Switch` and `EnsureAllSucceeded` instead of scattering try/catch blocks through application code. `Error` carries a `Code`/`SubCode`/`Message`, optional structured `Details`, and an optional wrapped `Exception`, so a failure can be inspected and handled without ever throwing.

## Installation

```bash
dotnet add package LoomKit.Esits
```

Available on [nuget.org](https://www.nuget.org/packages/LoomKit.Esits) — a package version is published automatically for every `vX.Y.Z` tag pushed to this repo.

## Usage

```csharp
using LoomKit.Esits;
using LoomKit.Esits.Errors;
using LoomKit.Esits.Extensions;

Esit<User> GetUser(string id)
{
    var user = _repository.Find(id);

    return user is not null
        ? Esit.Success(user)
        : Esit<User>.Failure(GenericErrors.NotFoundError(typeof(User), id));
}

var esit = GetUser("42");

// branch on the outcome
esit.Switch(
    success: user => Console.WriteLine($"Found {user.Name}"),
    failure: error => Console.WriteLine($"{error.Code}: {error.Message}"));

// or fold it into a value
string message = esit.Match(
    success: user => user.Name,
    failure: error => error.Message);

// or transform the success value while short-circuiting on failure
Esit<string> nameEsit = esit.Map(user => user.Name);
```

## Contents

| Type | Kind | Purpose |
|---|---|---|
| `Esit` | class | Non-generic outcome: `IsSuccess`/`IsFailure` and an `Error?`. |
| `Esit<TValue>` | class | Outcome carrying a `Value` on success. |
| `Error` | class | `Code`, `SubCode`, `Message`, structured `Details`, optional wrapped `Exception`. |
| `GenericErrors` | static class | Ready-made `Error` factories: not found, conflict, validation, unauthorized, forbidden, unsuccessful HTTP status code, unexpected, ... |
| `FailedEsitException` | exception | Wraps a failed `Esit` for the rare case where throwing is the right call. |
| `EsitExtensions` | static class | `Map`, `Match`, `Switch`, `AsTask`, `ToEsit`, `EnsureAllSucceeded`. |
| `ErrorExtensions` | static class | `ToEsit`, `AsTask`, `AddDetail`. |

## License

[MIT](LICENSE)
