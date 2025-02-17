# Retro of the Week V2 - Back End API
[Retro of the Week](http://retrooftheweek.net/) was originally written on a [LAMP](https://en.wikipedia.org/wiki/LAMP_(software_bundle)) stack. This is a re-write of it in the (much more modern) ASP.NET MVC, .NET 6, C#. This is the back end repo, the front end will be contained in another repo.

## Techonlogies
- [ASP.NET](https://dotnet.microsoft.com/apps/aspnet)
- [.NET 6](https://dotnet.microsoft.com/download/dotnet/6.0)
    - Formerly known as ".Net Core", in contract to the .Net Framework, which was Windows only. This project was started a while go, the plan is to eventually upgrade to .NET 8
- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [MySQL](https://dev.mysql.com/doc/)
    - Using the official [Entity Framework support](https://dev.mysql.com/doc/connector-net/en/connector-net-entityframework60.html)
    - This is to use the same database from the original LAMP version
- [MSTest](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest)
    - For unit testing
