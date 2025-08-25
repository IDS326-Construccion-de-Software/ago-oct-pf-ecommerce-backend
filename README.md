# E-commerce Backend

A modern e-commerce backend API built with ASP.NET Core 9.0, designed for scalability and performance.

## 🚀 Features

- **RESTful API**: Well-structured REST endpoints for e-commerce operations
- **OpenAPI/Swagger**: Comprehensive API documentation and testing interface
- **Modern Architecture**: Built with .NET 9.0 and ASP.NET Core
- **Development Ready**: Configured for both development and production environments

## 🛠️ Tech Stack

- **Framework**: ASP.NET Core 9.0
- **Runtime**: .NET 9.0
- **Documentation**: OpenAPI/Swagger
- **License**: Apache License 2.0

## 📋 Prerequisites

Before running this application, make sure you have the following installed:

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later
- A code editor like [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

## 🚀 Getting Started

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd ago-oct-pf-ecommerce-backend
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Run the application**
   ```bash
   dotnet run
   ```

The application will start and be available at:
- **HTTP**: `http://localhost:5000`
- **HTTPS**: `https://localhost:5001`

### Development Mode

When running in development mode, the OpenAPI/Swagger interface will be available at:
- `https://localhost:5001/openapi`

## 📖 API Documentation

The API includes a sample weather forecast endpoint:

- **GET** `/weatherforecast` - Returns a 5-day weather forecast with random data

### Example Response
```json
[
  {
    "date": "2024-08-25",
    "temperatureC": 25,
    "temperatureF": 77,
    "summary": "Warm"
  }
]
```

## ⚙️ Configuration

### Application Settings

The application uses two main configuration files:

- `appsettings.json` - Base configuration
- `appsettings.Development.json` - Development-specific overrides

### Environment Variables

You can override configuration values using environment variables following the ASP.NET Core configuration pattern.

## 🏗️ Project Structure

```
ago-oct-pf-ecommerce-backend/
├── Program.cs                          # Application entry point
├── ago-oct-pf-ecommerce-backend.csproj # Project configuration
├── appsettings.json                    # Application configuration
├── appsettings.Development.json        # Development configuration
├── Properties/
│   └── launchSettings.json            # Launch profiles
├── LICENSE                            # Apache 2.0 License
└── README.md                          # This file
```

## 🔧 Development

### Building the Project

```bash
dotnet build
```

### Running Tests

```bash
dotnet test
```

### Publishing for Production

```bash
dotnet publish -c Release -o out
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

If you encounter any issues or have questions, please:

1. Check the existing [issues](../../issues)
2. Create a new issue if your problem isn't already reported
3. Provide detailed information about your environment and the issue

## 🚦 Status

![Build Status](https://github.com/IDS326-Construccion-de-Software/ago-oct-pf-ecommerce-backend/workflows/CI/badge.svg)

---

**Note**: This is an academic project developed as part of the Software Construction course (IDS326).