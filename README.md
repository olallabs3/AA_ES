## Descripción

Nuestra aplicación sirve para ver los juegos disponibles en la tienda, añadir más, añadir nuevos, buscar un juego, todo dependiendo del usuario que haya hecho login al principio de iniciar la aplicación o se haya registrado.

Las cosas que puede hacer un usuario normal son ver los juegos disponibles en la tienda y buscar uno en concreto.

Las cosas que puede hacer un usuario administrador son ver el catálogo completo (con juegos que no están disponibles porque no tienen unidades), añadir nuevos juegos al catálogo, añadir unidades a los juegos o sacar y por último ver un listado de usuarios y  poder borrarlos. 

Es necesario estar registrado para usar la aplicación en su totalidad.

## Docker

Para construir la imagen del docker utilizamos este comando:

   docker build -t a25944/aa_es -f Dockerfile .

docker build → construir
-t → etiquetar la imagen (nombredeusuario/nombreimagen)
-f → nombre del dockerfile
. → indica que lo necesario para construir la imagen está en el directorio actual

Para crear un volumen utilizamos este comando:

   docker volume create volumen_aa

volumen_aa → nombre del volumen que queremos crear (si no se da nombre, docker pone uno random)

Para correr el contenedor usamos este comando:

   docker run -it --name contenedor_aa -p 5944:80 -v ${pwd}:/app a25944/aa_es

-p → le asignamos el puerto que queremos usar de escucha en nuestro ordenador
-i → mantiene abierto STDIN si no está conectado
-t → da un pseudo-tty
-v → para que use el volumen 
–name → nombre del contenedor
