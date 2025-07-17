# API REST - Prueba Técnica C# / .NET

Este proyecto es una API REST desarrollada en **.NET 8** como parte de una prueba técnica para el cargo de desarrollador. Implementa un sistema CRUD para productos e imágenes de productos, siguiendo principios de arquitectura limpia, buenas prácticas (SOLID, Clean Code) y separación por capas.

## 📦 Estructura de carpetas
/Controllers → Controladores REST
/Data → Contexto de base de datos (EF Core)
/DTOs → Objetos de transferencia (input/output)
/Models → Entidades de base de datos
Program.cs → Configuración de la app
appsettings.json → Configuración de conexión


Ejecutar migraciones

dotnet ef migrations add InitialCreate
dotnet ef database update


Endpoints disponibles
## Poductos

GET /api/productos

GET /api/productos/{id}

POST /api/productos

PUT /api/productos/{id}

DELETE /api/productos/{id}

GET /api/productos/ordenados-precio


## Imagenes Productos

POST /api/imagenes

GET /api/imagenes

PUT /api/imagenes/{id}

DELETE /api/imagenes/{id}
