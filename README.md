# 🛍️ Proyecto FILHA - E-commerce de Cosméticos

![.NET](https://img.shields.io/badge/.NET-ASP.NET%20Core-blue)
![EF Core](https://img.shields.io/badge/ORM-Entity%20Framework%20Core-green)
![SQL Server](https://img.shields.io/badge/Database-SQL%20Server-red)
![Arquitectura](https://img.shields.io/badge/Arquitectura-MVC-orange)

Sistema web tipo **E-commerce de Cosméticos** desarrollado con **ASP.NET Core MVC**, enfocado en la gestión de productos, clientes y persistencia de datos mediante **Entity Framework Core**.

Este repositorio está diseñado no solo como aplicación funcional, sino también como **referencia académica y técnica** para comprender la implementación de un sistema por capas.

---

## 📌 Descripción del sistema

**Proyecto FILHA** simula una tienda en línea especializada en cosméticos, donde se administran productos, clientes y su información asociada.

El núcleo del sistema gira en torno al modelo:

> 💄 **Cosmetico** → entidad principal del dominio

Este modelo concentra la mayor parte de la lógica de negocio relacionada con productos.

---

## 🧩 Arquitectura

El sistema implementa el patrón **MVC (Model - View - Controller)**:

| Capa        | Responsabilidad                                   |
| ----------- | ------------------------------------------------- |
| Models      | Definición de entidades y reglas de negocio       |
| Controllers | Manejo de solicitudes HTTP y lógica de aplicación |
| Views       | Renderizado de la interfaz de usuario             |

Además, se integra:

* **Entity Framework Core (Code First)**
* **DbContext** para acceso a datos
* Separación clara de responsabilidades

---

## 🧠 Modelos del sistema

A continuación se describen las entidades principales del sistema junto con su propósito dentro del dominio:

---

### 💄 Cosmetico

**Entidad principal del sistema**

Representa un producto cosmético disponible en el catálogo.

**Responsabilidades:**

* Almacenar información del producto
* Gestionar atributos como precio, stock y categoría
* Servir como base para operaciones CRUD

**Propiedades típicas:**

* `Id`
* `Nombre`
* `Precio`
* `Stock`
* `Categoria`
* `Descripcion`
* `Imagen (ruta)`
* `Ingredientes`

---

### 👤 Cliente

Representa a los usuarios registrados en el sistema.

**Responsabilidades:**

* Almacenar información del cliente
* Gestionar estado de cuenta

**Reglas de negocio:**

* `FechaCreacion` → se asigna automáticamente (`DateTime.Now`)
* `Estado` → activo por defecto (`1`)

---

### 🏷️ Categoria (si aplica)

Permite clasificar los cosméticos.

**Responsabilidades:**

* Agrupar productos
* Facilitar filtrado y organización

---

## 🎮 Controladores

Los controladores gestionan la lógica de la aplicación y conectan modelos con vistas.

---

### 🧾 CosmeticoController

Controlador principal del sistema.

**Funciones:**

* Listar productos
* Crear nuevos cosméticos
* Editar información
* Eliminar productos

**Relación:**

* Trabaja directamente con el modelo `Cosmetico`
* Usa Entity Framework para persistencia

---

### 👤 ClienteController

Gestiona el registro y administración de clientes.

**Funciones:**

* Registro de usuarios
* Creación de clientes en base de datos
* Validación de datos

---

## 🖥️ Vistas (Views)

Las vistas están construidas con **Razor** y representan la interfaz de usuario.

---

### Vistas de Cosméticos

* `Index` → Lista de productos
* `Create` → Formulario de alta
* `Edit` → Edición de producto
* `Delete` → Confirmación de eliminación

---

### Vistas de Clientes

* `Create` → Registro de cliente
* `Index` (opcional) → Listado de clientes

---

## 🗃️ Acceso a datos

El proyecto utiliza **Entity Framework Core (Code First)**.

### Componentes clave:

* `DbContext` → configuración de entidades
* `DbSet<T>` → representación de tablas
* Migraciones → control de cambios en BD

---

## 📁 Estructura del proyecto

```bash
ProyectoFILHA/
│
├── Controllers/
│   ├── CosmeticoController.cs
│   └── ClienteController.cs
│
├── Models/
│   ├── Cosmetico.cs
│   ├── Cliente.cs
│   └── Categoria.cs
│
├── Data/
│   └── ApplicationDbContext.cs
│
├── Views/
│   ├── Cosmetico/
│   └── Cliente/
│
├── wwwroot/
└── Program.cs
```

---

## ⚙️ Ejecución del proyecto

```bash
git clone https://github.com/blackwero/ProyectoFILHA.git
cd ProyectoFILHA
dotnet ef database update
dotnet run
```

---

## 🚀 Funcionalidades principales

* Gestión de productos cosméticos
* Registro de clientes
* Persistencia de datos
* Arquitectura MVC
* Separación de capas

---

## 🔍 Enfoque técnico

Este proyecto destaca por:

* Implementación de **Code First**
* Uso de **Entity Framework Core**
* Modelado de entidades orientado al dominio
* Aplicación de arquitectura MVC limpia
* Manejo básico de reglas de negocio

---

## 📈 Mejoras propuestas

* Autenticación con Identity
* Carrito de compras
* Órdenes y pagos
* API REST
* Paginación y filtros avanzados

---

## 👨‍💻 Autor

**Irving Hernández**
🔗 https://github.com/blackwero

---

## 📄 Licencia

Proyecto con fines académicos y demostrativos.

---
