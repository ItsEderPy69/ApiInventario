Imports ApiInventario.Data
Imports ApiInventario.Middleware
Imports ApiInventario.Repositories
Imports ApiInventario.Services
Imports Microsoft.AspNetCore.Builder
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.Extensions.Configuration
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Hosting

Public Module Program

    Public Sub Main(args As String())
        Dim builder = WebApplication.CreateBuilder(args)

        Dim connectionString = builder.Configuration.GetConnectionString("InventarioDB")
        builder.Services.AddDbContext(Of InventarioDbContext)(
            Sub(options As DbContextOptionsBuilder)
                options.UseSqlServer(connectionString)
            End Sub)

        builder.Services.AddScoped(Of ICategoriaRepository, CategoriaRepository)()
        builder.Services.AddScoped(Of IProductoRepository, ProductoRepository)()
        builder.Services.AddScoped(Of ICategoriaService, CategoriaService)()
        builder.Services.AddScoped(Of IProductoService, ProductoService)()

        builder.Services.AddControllers()
        builder.Services.AddEndpointsApiExplorer()
        builder.Services.AddSwaggerGen()

        Dim app = builder.Build()

        app.UseMiddleware(Of ExceptionMiddleware)()

        app.UseSwagger()
        app.UseSwaggerUI()
        app.MapControllers()

        app.Run()
    End Sub

End Module
