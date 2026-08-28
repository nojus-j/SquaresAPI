# SquaresAPI

SquaresAPI is a .NET Web API built using Entity Framework core and SQLite that automatically detects all geometric squares formed in a 2D set.

## Prerequisites

Ensure you have the following installed in your computer:
* **[.NET 8.0 SDK]**
* **[Git]**

## Getting started

### Clone the Repository

```bash
git clone [https://github.com/your-username/SquaresAPI.git](https://github.com/your-username/SquaresAPI.git)
cd SquaresAPI

### Restore Dependencies

dotnet restore

### Database Setup

The project used SQLite for data persistance. To apply existing database migrations and create the local SQLite database file run:

dotnet ef database update --project SquareAPI

### Launching the App

Navigate to the project directory and run:

dotnet run --project SquaresAPI

### Tests

The project uses xUnit testing for the square detection algorithm, to execute the test suite run:

dotnet test