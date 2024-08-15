# XMLToData

## Description
The console app for reading data from an XML file and saving it to the database.
Used technologies:

- Visual Studio 2022
- .Net 8.0
- EntityFramework
- PostgreSQL

## How to start 

### 1) [Install PostgreSQL](https://www.postgresql.org/download/)

### 2)Create a  [User Secrets file](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows).

### 3) Add your the connection strings for the database in the secret file.

### 4) Add your the path to the XML file in the secret file.

### 5) Create database:

Write in package manager console in visual studio next command:
```
`Update-Database`
```
or write in CLI next command:
```
`dotnet ef database update`
```
### 6) Start the project:

You can start from Visual Studio by `F5` or clicking run button

or you can start by CLI in `XMLToData` directory writing `dotnet run` command
