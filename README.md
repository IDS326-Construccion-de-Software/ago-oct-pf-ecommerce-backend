Proyecto E-Commerce Full Stack

![Status](https://img.shields.io/badge/status-en%20desarrollo-yellow)
![License](https://img.shields.io/badge/license-MIT-green)
![Tech](https://img.shields.io/badge/stack-Full%20Stack-blue)

Descripción

Este proyecto es una plataforma E-Commerce desarrollada bajo un enfoque Full Stack, que permite la gestión de productos, usuarios, órdenes y autenticación segura.  
El sistema está compuesto por un backend en .NET / C# con acceso a base de datos SQL Server y un frontend moderno implementado con React.js.

El propósito principal es ofrecer una arquitectura limpia, escalable y segura, con autenticación JWT, validación de datos, control de roles y operaciones CRUD para las entidades principales del comercio electrónico.

---

Tecnologías Utilizadas

Backend (.NET)
- ASP.NET Core 7.0 / C#
- Entity Framework Core
- SQL Server (almacenamiento de datos)
- log4net (registro de logs)
- Swagger (documentación de la API)
- JWT Authentication

 Frontend (React)
- React.js 18+
- Vite o Create React App
- Bootstrap / TailwindCSS
- Axios (consumo de API)
- HeroUI / NextUI

 Otros
- Git & GitHub (control de versiones)
- Visual Studio / VSCode
- Postman o Thunder Client (para pruebas de API)
- .env para variables de entorno

---

  Estructura del Proyecto

 proyecto-ecommerce/
├── backend/                - API REST en .NET Core
│   ├── Controllers/        - Controladores con endpoints
│   ├── Models/             - Entidades y DTOs
│   ├── Data/               - Contexto de base de datos
│   ├── Repositories/       - Acceso a datos (Patrón Repository)
│   ├── Services/           - Lógica de negocio
│   ├── appsettings.json    - Configuración de entorno
│   └── Program.cs          - Punto de entrada del backend
│
├── frontend/               - Aplicación React
│   ├── src/
│   │   ├── components/     - Componentes visuales
│   │   ├── pages/          - Páginas (Home, Login, Productos, etc.)
│   │   ├── services/       - Conexión con API
│   │   └── App.jsx
│   └── package.json        - Dependencias y scripts
│
└── README.md               - Archivo de documentación

---

 Requisitos Previos

Antes de ejecutar el proyecto, asegúrate de tener instalados:

-  .NET SDK 7.0 o superior  
-  SQL Server y SQL Server Management Studio (SSMS)  
-  Node.js v18+ y npm  
-  Visual Studio 2022 o VSCode

---

 Instalación y Configuración

 1. Clonar el repositorio

git clone https://github.com/tu-usuario/ago-oct-pf-ecommerce-backend-dev.git
cd ago-oct-pf-ecommerce-backend-dev

 2. Configurar el Backend

1. Abre la carpeta `backend` en Visual Studio.  
2. Verifica que la cadena de conexión en `appsettings.json` apunte a tu servidor local:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=EcommerceDB;User Id=sa;Password=tu_contraseña;"
   }
   ```
3. Ejecuta las migraciones:
   ```bash
   dotnet ef database update
   ```
4. Inicia el backend:
   ```bash
   dotnet run
   ```
   El backend se ejecutará en:  
   http://localhost:5000 o http://localhost:5041 (según configuración)

 3. Configurar el Frontend

1. Entra a la carpeta `frontend`:
   ```bash
   cd frontend
   ```
2. Instala las dependencias:
   ```bash
   npm install
   ```
3. Crea un archivo `.env` con la URL del backend:
   ```bash
   VITE_API_URL=http://localhost:5000/api
   ```
4. Ejecuta el frontend:
   ```bash
   npm run dev
   ```
   Accede a la app en:  
    http://localhost:5173

---

 Ejemplo de Uso

1. Regístrate o inicia sesión con tus credenciales.  
2. Navega al listado de productos.  
3. Agrega ítems al carrito y realiza una compra simulada.  
4. Si eres Administrador, puedes:
   - Crear, editar y eliminar productos.
   - Ver usuarios registrados.
   - Consultar logs y auditorías.

---

 Guía para Contribuir

1. Haz un fork del repositorio.  
2. Crea una nueva rama:
   ```bash
   git checkout -b feature/nueva-funcionalidad
   ```
3. Realiza tus cambios y haz commit:
   ```bash
   git commit -m "Añadida nueva funcionalidad X"
   ```
4. Envía tu rama al repositorio:
   ```bash
   git push origin feature/nueva-funcionalidad
   ```
5. Abre un Pull Request en GitHub.

---

Autores

Proyecto desarrollado por el Equipo de Desarrollo del Proyecto E-Commerce  
Asignación: Módulo Backend – [Tu nombre aquí]  
Colaboradores: Equipo Académico Ago-Oct

---

Licencia

Este proyecto está bajo la licencia MIT.  
Consulta el archivo [LICENSE](LICENSE) para más detalles.

---

Créditos y Reconocimientos

Agradecimientos a los instructores y mentores del período académico Ago-Oct  
por su orientación en el desarrollo de este proyecto Full Stack.
