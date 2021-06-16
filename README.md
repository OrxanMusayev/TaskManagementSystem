# TaskManagementSystem

   ## Technologies

   - ASP.NET Core 5
   - AutoMapper
   - Entity Framework Core 5
   - FluentValidation

   The project is built on **Clean Architecture**. Dependencies for each project are separated. 
   
   ## Getting Started
   1. To run app you need to install [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0).
   2. Navigate to `src/TaskManagementSystem.WebAPI` and run dotnet run to launch the back end (ASP.NET Core Web API)

   ## Database Migrations
   To use dotnet-ef for your migrations please add the following flags to your command (values assume you are executing from repository root)
   
   - `--project src/Infrastructure`
   - `--startup-project src/TaskManagementSystem.WebAPI`
   - `--output-dir Persistence/Migrations`
   
   For example, to add a new migration from the root folder:

    `dotnet ef migrations add "InitialCreate" --project src\TaskManagementSystem.Infrastructure 
    --startup-project src\TaskManagementSystem.WebAPI --output-dir Persistence\Migrations`
    
   (Or you can use `Add-Migration` command to add migration.)
    
   (Note: change mail settings from appsettings.json for using email sending service correctly) 
   
   
