# Retro of the Week V2
[Retro of the Week](http://retrooftheweek.net/) was originally written on a [LAMP](https://en.wikipedia.org/wiki/LAMP_(software_bundle)) stack. This is a re-write of it in the (much more modern) ASP.NET MVC, .NET 8, C#. This contains the back and front-ends, both written in ASP.NET MVC. They are separate projects due to separation of concerns, and also so that technology for each layer could more easily be swapped out for each layer in the future if the need arises.

## Techonlogies
- [ASP.NET](https://dotnet.microsoft.com/apps/aspnet)
- [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)
    - Formerly known as ".Net Core", in contract to the .Net Framework, which was Windows only
- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [MySQL](https://dev.mysql.com/doc/)
    - Using the official [Entity Framework support](https://dev.mysql.com/doc/connector-net/en/connector-net-entityframework60.html)
    - This is to use the same database from the original LAMP version
- [MSTest](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest)
    - For unit testing
- HTML/CSS/Javascript
    - Used the front-end views. JS is included, but its use is intended to be limited -- a design goal of this project is intended to be properly 'retro', in this case literally running on older hardware.

## Projects
The solution contains the following projects
- **RetroOfTheWeekAPI** - The back-end API layer
- **RetroOfTheWeekAPITests** - Unit tests for API layer
- **RetroOfTheWeekFrontEnd** - MVC front-end layer, commmunicates to back-end with `System.Net.Http.HttpClient`
- **RetroOfTheweekShared** - Anything that is shared betweent front and back end layers