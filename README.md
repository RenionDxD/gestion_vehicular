# 🚗 **Gestión de Vehículos**

## Descripción 📄
Esta es una **Aplicación Web** diseñada para la gestión eficiente de vehículos. Desarrollada con **ASP.NET Core MVC**, cuenta con las siguientes características principales:

- **Lenguaje de Programación**: C♯
- **Base de Datos**: SQL Server
- **Automatización CRUD**: Implementación mediante **Entity Framework** para la generación automática de vistas, modelos y controladores.
- **Scaffold-DbContext**: Utilizado para crear el esquema de base de datos y los componentes de la aplicación.
- **Documentación**: Puedes consultar más detalles sobre **Entity Framework** y **ASP.NET Core** en la [documentación oficial de Microsoft](https://docs.microsoft.com).

---

## Instalación y Configuración 💡

### 1. **Preparación de la Base de Datos**

- **Ejecución del Script**:
  - Encuentra el archivo `BDGestionVehiculosRD.sql` en el proyecto.
  - Ejecuta este script para crear la base de datos, la cual incluye tres tipos de vehículos preconfigurados.
  - Si prefieres una base de datos más simple, utiliza el script alternativo `BDGestionVehiculosRD_Simple.sql`.

- **Opciones para Ejecutar el Script**:
  - Descarga e instala **SQL Server 2022** junto con **SQL Server Management Studio (SSMS)**:
    1. Abre el script en SSMS como un nuevo query.
    2. Ejecuta el archivo para crear la base de datos.
  - Si usas **Visual Studio**:
    1. Abre el archivo `.sql` directamente en Visual Studio.
    2. Ejecuta el script utilizando su integración de SQL.
  - Si no aparece el ejecutador de SQL:
    1. Instala la extensión **SQL Tools** desde el administrador de extensiones.
    2. Ejecuta el script desde el editor para crear la base de datos.

- **Nota Importante**:
  Asegúrate de actualizar la ruta de creación del archivo de la base de datos en el script. Modifica la línea que contiene la ruta, adaptándola a tu sistema. Ejemplo:


Cambia `C:` o `D:` según tu configuración local, si utilizas `BDGestionVehiculosRD_Simple.sql` no es necesario el cambio de rutas.

---

### 2. **Configuración de la Cadena de Conexión**

- Abre el archivo `appsettings.json` en el directorio raíz del proyecto.
- Localiza el bloque `ConnectionStrings` y ajusta la conexión de acuerdo con tu entorno local o servidor. Por ejemplo:
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=EstacionamientoDBS;Integrated Security=True;TrustServerCertificate=True;"
}



