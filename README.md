# API Inventario 
Api en .Net 10 y SQL SERVER

## Configuracion y ejecucion

ejecutar Database/01_crear_base.sql en la instancia a usar:

Ejecutar la API, apuntando a la bd creada:

Abrir Swagger:  https://localhost:50861/swagger/index.html y probar los endpoints.


## Endpoints

| Verbo | Ruta | Descripcion | Codigos |
|---|---|---|---|
| GET | `/api/categorias` | Listar categorias | 200 |
| GET | `/api/categorias/{id}` | Categoria por Id | 200, 404 |
| GET | `/api/categorias/{id}/productos` | Categoria incluyendo sus productos | 200, 404 |
| POST | `/api/categorias` | Registrar categoria | 201, 400, 409 |
| PUT | `/api/categorias/{id}` | Modificar categoria | 200, 400, 404, 409 |
| DELETE | `/api/categorias/{id}` | Eliminar (solo sin productos) | 204, 404, 409 |
| GET | `/api/productos` | Listar productos incluyendo su categoria | 200 |
| GET | `/api/productos/{id}` | Producto por Id | 200, 404 |
| GET | `/api/productos/categoria/{categoriaId}` | Productos de una categoria | 200, 404 |
| POST | `/api/productos` | Registrar producto | 201, 400, 404, 409 |
| PUT | `/api/productos/{id}` | Modificar producto | 200, 400, 404, 409 |
| DELETE | `/api/productos/{id}` | Eliminar producto | 204, 404 |

