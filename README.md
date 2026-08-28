# Gift of the Givers Relief Management System

ASP.NET Core MVC prototype for the APPR6312 Section C requirements.

## Technology
- Visual Studio 2022
- ASP.NET Core MVC
- .NET 8
- ASP.NET Identity
- Entity Framework Core
- SQL Server LocalDB
- Bootstrap 5

## Features
- Gift of the Givers branding and responsive navigation
- Donor registration/login
- Employee role
- Anonymous/guest donation by entering a donor name
- One-time and recurring donation options
- ZAR, USD and EUR symbolic currencies
- On-screen placeholder tax certificate with Print/Save as PDF
- Volunteer registration
- Employee dashboard
- Volunteer and donation lists
- Relief projects and employee project updates

## Demo accounts
Employee:
employee@givers.org
Employee123

Donor:
donor@example.com
Donor123

## Run in Visual Studio 2022
1. Install Visual Studio 2022 with the ASP.NET and web development workload.
2. Open `GiftOfTheGiversReliefSystem.csproj` (or create/open the solution in the folder).
3. Restore NuGet packages.
4. Ensure SQL Server LocalDB is installed.
5. Build > Rebuild Solution.
6. Run with Ctrl+F5 or F5.
7. The database `GiftOfTheGiversDB` is created automatically on first run.

## Important prototype note
No real payment is processed. Donation amounts are symbolic dummy records, as required for the prototype stage.
