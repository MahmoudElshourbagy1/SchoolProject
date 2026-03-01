# 🎓 SchoolProject

## 📌 Overview
**SchoolProject** is a modular ASP.NET Core Web API built using Clean Architecture principles.  
It demonstrates best practices for building scalable, maintainable backend systems with separation of concerns.

This project is ideal as a reference for enterprise-level backend development using C# and .NET.

---

## 🏗️ Project Architecture

The project follows **Layered / Clean Architecture** for maintainability and scalability.

### 🔹 Project Structure

- **SchoolProject.Api**
  - Handles HTTP requests and responses
  - Contains Controllers to manage different resources
  - Handles JWT Authentication & Role-based Authorization (Admin, Teacher, Student)

- **SchoolProject.Core**
  - Contains business rules and contracts
  - Includes Entities, Interfaces, and DTOs

- **SchoolProject.Data**
  - Handles database models and DbContext
  - Responsible for data persistence

- **SchoolProject.Service**
  - Implements business logic and application use cases

- **SchoolProject.Infrastructure**
  - Contains repository implementations and external service integrations
  - Implements Logging using Serilog

---

## 🧰 Technologies Used

- C# / .NET 8+
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication & Role-based Authorization
- Serilog for logging
- Swagger (API Documentation)
- CI/CD pipeline configured for automated deployment

----

## 🚀 Getting Started

### 1️⃣ Clone the repository
```bash
git clone https://github.com/MahmoudElshourbagy1/SchoolProject.git
