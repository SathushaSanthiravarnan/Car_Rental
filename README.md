# 🚗 Car Rental Management System  
*A Clean Architecture project with CQRS and MS SQL Server*

## 📌 Overview
This project is built with **ASP.NET Core 9**, following the **Clean Architecture** principles.  
It implements **CQRS with MediatR**, uses **Entity Framework Core with MS SQL Server**, and supports modern authentication methods like **JWT** and **Google OAuth 2.0**.  

The solution is structured for **production readiness**, including validation, logging, testing, and containerization.

---

## 🏗️ Architecture
- **Presentation Layer (Web API)** → Controllers, Swagger, Exception handling  
- **Application Layer** → CQRS (Commands/Queries), MediatR, FluentValidation  
- **Domain Layer** → Entities, Value Objects, Enums, Domain rules  
- **Infrastructure Layer** → Services, External integrations (e.g., Google OAuth)  
- **Infrastructure.Persistence Layer** → EF Core, SQL Server, Repositories, Unit of Work  

---

## ✨ Features
-  **Clean Architecture** (separation of concerns, testable & maintainable)  
-  **CQRS** with MediatR (commands & queries separation)  
-  **EF Core + SQL Server** with Code-First Migrations  
-  **Authentication & Authorization**  
  - JWT-based Register/Login  
  - Google OAuth 2.0 login  
-  **Validation** using FluentValidation  
-  **Logging** with Serilog  
-  **API Documentation** via Swagger/OpenAPI  
-  **Testing** with xUnit (unit & integration tests)  
-  **Docker Support** (API + SQL Server in docker-compose)  

---

## 🗄️ Database
- **MS SQL Server** with EF Core Code-First  
- Migrations are stored in `Infrastructure.Persistence/Migrations`  

Run migration:
```bash
dotnet ef migrations add InitialCreate -p CarRentalSystem.Infrastructure -s CarRentalSystem.WebApi -o Persistence/Migrations
dotnet ef database update -p CarRentalSystem.Infrastructure.Persistence -s CarRentalSystem.WebApi

