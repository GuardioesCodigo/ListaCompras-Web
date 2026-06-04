using ListaCompras.ConsoleApp.ModuloCategoria.Infra;
using ListaCompras.WebApplication.Compartilhado.Dominio;
using ListaCompras.WebApplication.Compartilhado.Infra.Arquivos;
using ListaCompras.WebApplication.ModuloCategoria.Aplicacao;
using ListaCompras.WebApplication.ModuloCategoria.Dominio;

// APS .NET core
// Montar um  servidor web

// Builder de um servidor web

WebApplicationBuilder builder =  WebApplication.CreateBuilder(args);

// Configuração de Serviços
builder.Services.AddScoped(provider =>
{
    ContextoJson contextoJson = new ContextoJson();
    contextoJson.Carregar();
    return contextoJson;
});

builder.Services.AddScoped<IRepositorio<Categoria>, RepositorioCategoriaEmArquivo>();
builder.Services.AddScoped<ServicoCategoria>();

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