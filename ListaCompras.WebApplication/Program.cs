using ListaCompras.WebApp.Compartilhado.Aplicacao;
using ListaCompras.WebApplication.Compartilhado;
using ListaCompras.WebApplication.Compartilhado.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfraRepositories();

builder.Services.AddAplicationServices();

builder.Services.AddPresentation();

var app = builder.Build();

// Configuração de Middlewares
app.UseStaticFiles();

app.UseRouting();
app.MapDefaultControllerRoute();

// Execução do Servidor
app.Run();