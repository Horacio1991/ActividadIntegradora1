# Actividad Integradora 01

Este es un proyecto de una aplicación de gestión de personas desarrollada en C# con WPF.

## Descripción

La aplicación permite realizar las siguientes acciones:

- Agregar personas con su DNI, nombre y apellido.
- Buscar personas por su DNI.
- Mostrar los detalles de las personas encontradas.
- Limpiar los campos del formulario con la tecla Escape.

## Funcionalidades

- Validación de campos:
  - El campo DNI solo acepta números.
  - Los campos de nombre y apellido solo aceptan letras.
  - Todos los campos son obligatorios para agregar una persona.

- Integración con archivo de datos:
  - Los datos de las personas se almacenan en un archivo de texto como una base de datos simple.
  - Al iniciar la aplicación, los datos se cargan desde el archivo.
  - Cada vez que se agrega una nueva persona, se actualiza el archivo de datos.

## Uso

1. Clona el repositorio.
2. Abre el proyecto en Visual Studio.
3. Compila y ejecuta la aplicación.
4. Usa la interfaz para agregar y buscar personas.

