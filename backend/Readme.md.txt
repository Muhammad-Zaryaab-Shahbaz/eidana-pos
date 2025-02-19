

## Prerequisites

Ensure you have the following installed before setting up the project:

- [.NET SDK](https://dotnet.microsoft.com/download) (Recommended: .NET [8.0])
- [Visual Studio](https://visualstudio.microsoft.com/) 
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)
- [Postman](https://www.postman.com/) (Optional for testing APIs)



"ConnectionStrings": {
  "DefaultConnection": "Server=your-mysql-server;Database=your-database-name;User Id=your-username;Password=your-password;"
}


by default running on;
https://localhost:6100/swagger


Publish for deployment
dotnet publish -c Release -o ./publish
