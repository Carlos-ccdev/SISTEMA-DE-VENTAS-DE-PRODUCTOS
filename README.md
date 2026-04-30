# Sistema de Ventas - Aplicación de Escritorio

Desarrollé un sistema de ventas para tienda de productos electrónicos en C# con arquitectura en capas. Permite registrar ventas, gestionar pagos y generar comprobantes en PDF, integrando datos con SQL Server. Demuestra habilidades en backend, lógica de negocio y resolución de problemas reales.
---

# Tecnologías utilizadas

* C# (.NET - Windows Forms)
* SQL Server
* iTextSharp (Generación de PDF)
* AForge.NET (Captura de cámara para pagos QR)

---

## Arquitectura

El proyecto está estructurado en capas para mejorar la organización y mantenibilidad:

* **Capa Presentación:** Interfaz de usuario (WinForms)
* **Capa Negocio:** Lógica del sistema
* **Capa Datos:** Acceso a base de datos
* **Capa Entidad:** Modelos del sistema

---

## Funcionalidades principales

* Registro de ventas
* Procesamiento de pagos (QR y efectivo)
* Captura de imagen mediante cámara
* Generación de comprobantes en PDF
* Búsqueda de ventas por documento

---

## Requisitos

Antes de ejecutar el proyecto, asegúrate de tener instalado:

* Visual Studio
* SQL Server
* .NET Framework compatible

---

## Instalación y ejecución

1. Clonar el repositorio:

   ```bash
   git clone https://github.com/TU-USUARIO
   ```

2. Abrir el archivo `.sln` en Visual Studio

3. Restaurar paquetes NuGet:

   * Click derecho en la solución → Restore NuGet Packages

4. Configurar la cadena de conexión en la base de datos

5. Ejecutar el proyecto

---

## Notas importantes

* Asegúrate de que los archivos de recursos (como plantillas HTML para comprobantes) estén configurados con:

  * **Copy to Output Directory = Copy always**
* Verifica que SQL Server esté en ejecución
* El proyecto es de carácter educativo y puede requerir mejoras

---

## Autor

Desarrollado por [Carlos Colque]

---

## Estado del proyecto

Proyecto en desarrollo / mejora continua
