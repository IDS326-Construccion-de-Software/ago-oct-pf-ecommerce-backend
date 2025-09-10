# E-commerce Backend

A modern e-commerce backend solution built with .NET 9.0, following layered architecture best practices. It includes API, data access, and infrastructure services.

## 🚀 Features

- **RESTful API**: Well-structured REST endpoints for e-commerce operations
- **OpenAPI/Swagger**: Comprehensive API documentation and testing interface
- **Modern Architecture**: Clean separation of concerns with layered projects
- **Development Ready**: Configured for both development and production environments

## 🛠️ Tech Stack

- **Framework**: ASP.NET Core 9.0
- **Runtime**: .NET 9.0
- **Documentation**: OpenAPI/Swagger
- **License**: Apache License 2.0

## 📋 Prerequisites

Before running this solution, make sure you have the following installed:
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later
- A code editor like [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

## 🚀 Getting Started

### Installation and Setup

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd ago-oct-pf-ecommerce-backend
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore revenge-backend.sln
   ```

3. **Run the API**
   ```bash
   cd Revenge.API
   dotnet run
   ```

The application will be available at:
- **HTTP**: `http://localhost:5000`
- **HTTPS**: `https://localhost:5001`

When running in development mode, the OpenAPI/Swagger interface will be available at:
- `https://localhost:5001/swagger`

## 📖 API Documentation

Interactive API documentation is available via Swagger in development. The main endpoints are focused on e-commerce operations (products, users, orders, etc).

## ⚙️ Configuration

### Application Settings

The application uses two main configuration files:

- `appsettings.json` - Base configuration
- `appsettings.Development.json` - Development-specific overrides

### Environment Variables

You can override configuration values using environment variables following the ASP.NET Core configuration pattern.

## 🏗️ Project Structure

```
revenge-backend.sln                # Main solution file
Revenge.API/                       # Main API project
├── Properties/                    # Launch settings
├── Controllers/                   # API Controllers
└── appsettings.json              # Configuration files
Revenge.Data/                      # Data access layer
├── Models/                        # Entities and data models
└── Context/                       # Entity Framework context
Revenge.Infrastructure/            # Business logic and services
├── Services/                      # Service implementations
└── Repositories/                  # Data repositories
```

## 🔧 Development

### Building the Project
```bash
dotnet build revenge-backend.sln
```

### Running Tests
```bash
dotnet test revenge-backend.sln
```

### Publishing for Production
```bash
dotnet publish Revenge.API -c Release -o out
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the Apache License 2.0 - see the [LICENSE](LICENSE) file for details.

## 📞 Support

If you encounter any issues or have questions:
1. Check the existing [issues](../../issues)
2. Create a new issue if your problem isn't already reported
3. Provide detailed information about your environment and the issue

## 🚦 Status

![Build Status](https://github.com/IDS326-Construccion-de-Software/Revenge.APIoct-pf-ecommerce-backend/workflows/CI/badge.svg)

---

**Note**: This is an academic project developed as part of the Software Construction course (IDS326).