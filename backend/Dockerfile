# Use .NET 8.0 SDK for building the solution
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the solution file and project files first
COPY LMS.sln ./
COPY LMS.Core/LMS.Core.csproj LMS.Core/
COPY LMS.DAL/LMS.DAL.csproj LMS.DAL/
COPY LMS.Service/LMS.Service.csproj LMS.Service/
COPY LMS.WebAPI/LMS.WebAPI.csproj LMS.WebAPI/

# Restore dependencies
RUN dotnet restore LMS.sln

# Now copy all project files, including NLog.config
COPY . ./

# Build and publish the API project
RUN dotnet publish LMS.WebAPI/LMS.WebAPI.csproj -c Release -o /out

# Use .NET runtime to run the API
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out ./

# Ensure NLog.config is copied
COPY LMS.WebAPI/NLog.config .

# Expose necessary ports
EXPOSE 5000 5001
CMD ["dotnet", "LMS.WebAPI.dll"]
