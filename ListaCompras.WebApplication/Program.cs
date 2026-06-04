// APS .NET core
// Montar um  servidor web

// Builder de um servidor web
using ListaCompras.ConsoleApp.ModuloCategoria.Infra;
using ListaCompras.WebApp.Compartilhado.Aplicacao;
using ListaCompras.WebApplication.Compartilhado.Arquivos;
using ListaCompras.WebApplication.Compartilhado.Infra.Arquivos;
using ListaCompras.WebApplication.ModuloCategoria.Dominio;

WebApplicationBuilder builder =  WebApplication.CreateBuilder(args);

// Configuração de Serviços
builder.Services.AddScoped(provider =>
{
    ContextoJson contextoJson = new ContextoJson();
    contextoJson.Carregar();
    return contextoJson;
});

builder.Services.AddScoped<IRepositorioCategoria, RepositorioCategoriaEmArquivo>();

builder.Services.AddControllersWithViews().AddRazorOptions(options =>
{
    
    options.ViewLocationFormats.Clear();

    options.ViewLocationFormats.Add("/Modulo{1}/Apresentacao/Views/{0}.cshtml");

    // Views compartilhadas: /Compartilhado/Apresentacao/Views/_Layout.cshtml
    options.ViewLocationFormats.Add("/Compartilhado/Apresentacao/Views/{0}.cshtml");
});

// MVC tipo de aplicação web, como vamos apresentar as informações para o usuário
builder.Services.AddControllersWithViews();

// Criação da instância do servidor web
WebApplication app = builder.Build();

// Middlewares - funções que executam em cada chamada que o nosso servidor vai receber
app.UseStaticFiles();
app.UseRouting();
app.MapDefaultControllerRoute();

// Inicia o loop da aplicação
app.Run();