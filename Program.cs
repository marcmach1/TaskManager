// Configurações e dependências principais da aplicação TaskManager
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Services;

var builder = WebApplication.CreateBuilder(args);

// Registro de serviços de domínio
builder.Services.AddScoped<ITaskService, TaskService>();

// MVC Controllers
builder.Services.AddControllers();

// Política de CORS para permitir acesso do frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Configuração do Entity Framework Core com SQLite
// Observação: idealmente, mover a connection string para appsettings.json
builder.Services.AddDbContext<TaskContext>(options =>
    options.UseSqlite("Data Source=taskmanager.db"));

// Suporte a documentação e exploração de endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Swagger somente em ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilita CORS com a política definida
app.UseCors("AllowFrontend");

// Redirecionamento para HTTPS (opcional, habilite se tiver certificado configurado)
// app.UseHttpsRedirection();

// Mapeamento dos controllers
app.MapControllers();
app.Run();
// Inicialização da aplicação
app.Run();
