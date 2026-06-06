using ListaCompras.WebApplication.Compartilhado.Infra.Arquivos;
using ListaCompras.WebApplication.ModuloListaCompras;
using ListaCompras.WebApplication.ModuloListaCompras.Dominio;
using ListaCompras.WebApplication.ModuloListaCompras.Servicos;

var builder = WebApplication.CreateBuilder(args);

// =========================================================================
// REGISTRO DOS SERVIÇOS (DI - Injeção de Dependência)
// =========================================================================

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ContextoJson>();

builder.Services.AddScoped<IListaDeComprasRepository, RepositorioListaComprasEmArquivo>();
builder.Services.AddScoped<ListaDeComprasService>();

var app = builder.Build();

// =========================================================================
// CONFIGURAÇÃO DO PIPELINE DE REQUISIÇÕES (Middlewares)
// =========================================================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// 💡 MODIFICAÇÃO: Força o roteamento a ignorar a Home e buscar diretamente o seu Controller modular
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ListaCompras}/{action=Listar}/{id?}");

app.Run();