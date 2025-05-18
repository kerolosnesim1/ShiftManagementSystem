# 📊 Shift Management System API

A modern REST API built with ASP.NET Core for managing employee shifts and schedules.

## 🌟 Overview

This Shift Management System provides a flexible backend for organizations needing to manage employee schedules, shift assignments, and user management. Built using clean architecture principles, the system is designed to be maintainable, testable, and scalable.

## ✅ Current Features

- **👥 User Management**: Create, update, and manage user accounts with role-based access
- **⏰ Shift Management**: Define shifts with start/end times and specific types
- **📝 Shift Assignment**: Assign users to shifts with validation
- **📆 Schedule Viewing**: Get shift schedules for specific date ranges
- **🔐 Basic Authentication**: Simple authentication mechanism

## 🛠️ Technology Stack

- **Framework**: ASP.NET Core 9.0
- **Architecture**: Clean Architecture pattern with Domain/Application/Infrastructure/API layers
- **ORM**: Entity Framework Core with SQL Server
- **Documentation**: Swagger/OpenAPI
- **Patterns**: Repository pattern, Dependency Injection

## 📂 Project Structure

```
ShiftManagementSystem/
├── ShiftManagementSystem.API           # API endpoints and configuration
├── ShiftManagementSystem.Application   # Application logic and services
├── ShiftManagementSystem.Domain        # Domain entities and interfaces
└── ShiftManagementSystem.Infrastructure # Data access and external services
```

## 🚀 Getting Started

### Prerequisites

- .NET 9.0 SDK
- SQL Server

### Installation

1. Clone the repository
```bash
git clone https://github.com/kerolosnesim1/ShiftManagementSystem.git
```

2. Update the connection string in `appsettings.json` to point to your SQL Server instance

3. Run Entity Framework migrations to create the database
```bash
dotnet ef database update --project .\ShiftManagementSystem.Infrastructure --startup-project .\ShiftManagementSystem.API
```

4. Build and run the application
```bash
dotnet build
dotnet run --project .\ShiftManagementSystem.API
```

5. The API will be available at `https://localhost:5001` and Swagger documentation at `https://localhost:5001/swagger`

## 🔌 API Endpoints

- `/api/auth` - Authentication endpoints
- `/api/users` - User management
- `/api/shifts` - Shift creation and management

## 🚧 In Development

The following features are currently being implemented:

- **JWT Authentication**: Secure token-based authentication 
- **Role-based authorization**: Fine-grained access control 
- **Unit and integration testing**: Comprehensive test coverage

  

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 👨‍💻 Author

Kerolos Nesim 
