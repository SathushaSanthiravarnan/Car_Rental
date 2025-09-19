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

# 🎨 Frontend – React + TypeScript (Vite)

## 📌 Overview
This frontend application is built with *React 19 + TypeScript, powered by **Vite* for lightning-fast development and optimized production builds.  
It follows a *modular and scalable architecture, integrates seamlessly with the **ASP.NET Core Web API backend, and provides a responsive UI with **TailwindCSS*.

The project is production-ready, including *API integration with Axios, routing, state management, and environment-based configurations*.

---

## 🏗 Architecture

- *Components Layer* → Reusable UI components  
- *Pages Layer* → Page-level views (Dashboard, Cars, Bookings, etc.)  
- *Routes Layer* → React Router configuration for navigation  
- *Services Layer* → API calls using Axios (with interceptors for authentication)  
- *Hooks Layer* → Custom React hooks for shared logic  
- *Styles Layer* → TailwindCSS and global styles  

---

## ✨ Features

- ⚛ *React 18 + TypeScript* – Strongly typed, modern UI  
- ⚡ *Vite* – Lightning-fast bundler & HMR  
- 🛣 *React Router v6* – Client-side navigation  
- 🌐 *Axios* – API integration with interceptors  
- 🎨 *TailwindCSS* – Utility-first responsive design  
- 🔑 *Authentication support* – JWT & Google OAuth integration with backend  
- 📱 *Responsive UI* – Works on mobile, tablet, and desktop  
- 🧩 *Reusable component library* – Buttons, forms, modals, etc.  
- 📦 *Environment-based config* – .env support (VITE_API_BASE_URL)

---

## 🗄️ Database
- **MS SQL Server** with EF Core Code-First  
- Migrations are stored in `Infrastructure.Persistence/Migrations`  

Run migration:
```bash
dotnet ef migrations add InitialCreate -p CarRentalSystem.Infrastructure -s CarRentalSystem.WebApi -o Persistence/Migrations
dotnet ef database update -p CarRentalSystem.Infrastructure.Persistence -s CarRentalSystem.WebApi

