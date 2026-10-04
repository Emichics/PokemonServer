# PokemonServer
Aplicación web para consultar un listado de Pokémon, con funcionalidades para exportar su información en archivos Excel y enviarla por correo electrónico.

## Requerimientos y configuración

Para la exportación a Excel es necesario instalar la librería ClosedXML.

Para habilitar el envío de correos, es necesario configurar en appsettings.json las credenciales de la cuenta que realizará el envío, indicando un Username y Password.

## Decisiones Técnicas

### Caché de información de Pokémon
Se decidió mantener en memoria toda la información general de los Pokémon, sin su detalle, desde la carga inicial. Esto permite disponer de todos los Pokémon en memoria y realizar el filtro por nombre mediante coincidencias.

Cuando un Pokémon es consultado, se obtiene su detalle y se almacena en caché. Esto evita repetir solicitudes a la API externa cuando la misma información vuelve a ser necesaria, especialmente considerando que algunos de los datos utilizados por el grid, como la imagen, requieren consultar el detalle de cada Pokémon.

La opción descartada fue consultar el detalle de todos los Pokémon desde el inicio, con el objetivo de evitar una sobrecarga de peticiones, reducir el tiempo de inicialización de la aplicación y evitar almacenar información que podría no llegar a utilizarse.

### Caché de especies
Para el filtro por especies se decidió realizar una carga inicial de las especies disponibles y obtener su información de genus una sola vez. Esto evita tener que consultar la API externa cada vez que se utiliza o cambia el filtro.

Se eligió esta estrategia porque estos datos son necesarios para construir el catálogo del filtro. Al mantenerlos disponibles desde el inicio, las búsquedas posteriores no dependen de nuevas solicitudes a la API externa.
