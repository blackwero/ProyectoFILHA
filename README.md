# 🛍️ Proyecto FILHA

## E-commerce de Cosméticos – Documentación Técnica

![.NET](https://img.shields.io/badge/.NET-ASP.NET%20Core-blue)
![EF Core](https://img.shields.io/badge/ORM-Entity%20Framework%20Core-green)
![SQL Server](https://img.shields.io/badge/Database-SQL%20Server-red)
![Arquitectura](https://img.shields.io/badge/Pattern-MVC-orange)

---

## 📌 1. Descripción General

**Proyecto FILHA** es una aplicación web basada en **ASP.NET Core MVC** que implementa un sistema de tipo **e-commerce enfocado en productos cosméticos**.

El sistema está diseñado bajo una arquitectura en capas, con separación clara entre:

* Dominio (Modelos)
* Aplicación (Controladores)
* Presentación (Vistas)
* Persistencia (Entity Framework Core)

---

## 🧠 2. Modelo de Dominio

El dominio está centrado en la entidad **Cosmetico**, que representa el núcleo del sistema.

---

### 💄 2.1 Entidad: Cosmetico

#### 📍 Descripción

Representa un producto dentro del catálogo del e-commerce.

#### 📊 Responsabilidad

* Modelar la información completa del producto
* Servir como agregado principal del dominio
* Permitir operaciones CRUD

#### 🧾 Estructura (conceptual)

```csharp
public class Cosmetico
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public string Categoria { get; set; }
    public string Descripcion { get; set; }
    public string RutaImagen { get; set; }
    public string Ingredientes { get; set; }
}
```

#### ⚙️ Reglas de negocio implícitas

* El precio debe ser mayor a 0
* El stock no puede ser negativo
* La categoría define clasificación lógica (no normalizada completamente)
* La ruta de imagen debe apuntar a un recurso válido en `wwwroot`

---

### 👤 2.2 Entidad: Cliente

#### 📍 Descripción

Representa a un usuario registrado en la plataforma.

#### 🧾 Responsabilidad

* Persistir datos del cliente
* Controlar estado de cuenta

#### 🧾 Estructura (conceptual)

```csharp
public class Cliente
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public DateTime FechaCreacion { get; set; }
    public int Estado { get; set; }
}
```

#### ⚙️ Reglas de negocio

* `FechaCreacion` se asigna automáticamente en el servidor
* `Estado = 1` indica cliente activo
* No se permite creación con estado manual

---

### 🏷️ 2.3 Entidad: Categoria (opcional)

#### 📍 Descripción

Entidad auxiliar para clasificación de productos.

#### Responsabilidad

* Organizar los cosméticos
* Permitir escalabilidad en filtros

---

## 🗃️ 3. Persistencia de Datos

### 📌 Tecnología

* Entity Framework Core (Code First)

### 📌 Componente principal

```csharp
public class ApplicationDbContext : DbContext
{
    public DbSet<Cosmetico> Cosmeticos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
}
```

### 🔄 Flujo de persistencia

1. El controlador recibe la solicitud HTTP
2. Se valida el modelo (`ModelState`)
3. Se envía la entidad al `DbContext`
4. EF Core traduce la operación a SQL
5. Se ejecuta en SQL Server

---

## 🎮 4. Capa de Aplicación (Controladores)

---

### 💄 4.1 CosmeticoController

#### 📍 Responsabilidad

Gestionar el ciclo de vida completo de los productos.

#### 🔧 Acciones principales

| Acción | Método HTTP | Descripción                |
| ------ | ----------- | -------------------------- |
| Index  | GET         | Lista todos los cosméticos |
| Create | GET         | Retorna formulario         |
| Create | POST        | Inserta nuevo producto     |
| Edit   | GET         | Obtiene producto           |
| Edit   | POST        | Actualiza producto         |
| Delete | GET         | Confirmación               |
| Delete | POST        | Elimina registro           |

#### 🔄 Flujo interno (Create)

```text
Request → Controller → Model Binding → Validación → DbContext → SaveChanges()
```

---

### 👤 4.2 ClienteController

#### 📍 Responsabilidad

Gestionar el registro de usuarios.

#### 🔧 Acciones clave

* Registro de cliente
* Persistencia en base de datos

#### ⚙️ Lógica crítica

```csharp
cliente.FechaCreacion = DateTime.Now;
cliente.Estado = 1;
```

---

## 🖥️ 5. Capa de Presentación (Views)

Las vistas están desarrolladas con **Razor Pages**.

---

### 📄 Cosmetico Views

| Vista  | Descripción          |
| ------ | -------------------- |
| Index  | Listado de productos |
| Create | Alta de producto     |
| Edit   | Modificación         |
| Delete | Confirmación         |

---

### 📄 Cliente Views

| Vista  | Descripción         |
| ------ | ------------------- |
| Create | Registro de cliente |

---

## 🔄 6. Flujo de la Aplicación

```text
Usuario → Vista (Razor) → Controller → Modelo → DbContext → Base de Datos
```

---

## 🧪 7. Validaciones

* Uso de `ModelState.IsValid`
* Validaciones mediante Data Annotations (si están implementadas)
* Control de errores en servidor

---

## 📁 8. Estructura Física

```bash
ProyectoFILHA/
│
├── Controllers/
├── Models/
├── Data/
├── Views/
├── wwwroot/
└── Program.cs
```

---

## 🚀 9. Ejecución

```bash
dotnet ef database update
dotnet run
```

---

## 📈 10. Consideraciones Técnicas

* Arquitectura monolítica en capas
* No se implementa aún autenticación
* Modelo de dominio simple (sin relaciones complejas)
* Persistencia sin repositorios abstractos (acceso directo a DbContext)

---

## 🔮 11. Roadmap Técnico

* Autenticación con Identity
* API REST desacoplada

---

## 👨‍💻 Autor

**Irving Hernández**
GitHub: https://github.com/blackwero

---

## 📄 Licencia

Uso académico y demostrativo.

---
