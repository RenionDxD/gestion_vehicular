# 🚗 Gestión de Vehículos

## Descripción 📄
Este proyecto es una **Aplicación Web** desarrollada en **ASP.NET Core MVC** para la gestión de vehículos. Sus principales características son:

- **Lenguaje de programación**: C♯
- **Base de datos**: SQL Server
- **Automatización de CRUD**: Uso de **Entity Framework** para generar automáticamente vistas, modelos y controladores (CRUD).
- **Scaffold-DbContext**: Configurado para crear el esquema de base de datos y generar automáticamente los componentes de la aplicación.
- Toda la documentación sobre Entity Framework y ASP.NET Core está disponible en la [documentación oficial de Microsoft](https://docs.microsoft.com).

---

## Instalación y Configuración 💡

Para ejecutar este proyecto, sigue los pasos a continuación:

1. **Base de Datos**: 
   - Crea la base de datos ejecutando el script `BDGestionVehiculosRD.sql`.
   - Este script ya incluye tres tipos de vehículos preconfigurados.
   - Para generar la BD descarge SQL Server 2022, abra BDGestionVehiculosRD.sql en Visual Studio y ejecute el script o ejecute en Sql Server managment studio como
     script nuevo o copie todo el texto e insertelo en nuevo QSLQuery y ejecute la hoja 
   - El gestor para mejor comodidad use Sql Server managment studio
   - para ejecutar de desde Visual code vaya a extenciones -> administrar extenciones -> busque e instale SQL Tools -> se abrira un ejecutor de SQL en el archivo .sql
     ejecutelo para crear la Base de datos

   -**importante!!!:**
   - En el .sql debera cambiar la ruta de creacion al archivo de usted ejemplo: `C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA`

3. **Cadena de Conexión**:
   - Modifica la cadena de conexión en el archivo `appsettings.json` para adaptarla a tu entorno local o a tu servidor:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=EstacionamientoDBS; integrated security=true;TrustServerCertificate=true;"
   }
