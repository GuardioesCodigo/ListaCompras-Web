using ListaCompras.ConsoleApp.ModuloCategoria.Infra;
using ListaCompras.WebApplication.Compartilhado.Dominio;
using ListaCompras.WebApplication.Compartilhado.Infra.Arquivos;
using ListaCompras.WebApplication.ModuloCategoria.Aplicacao;
using ListaCompras.WebApplication.ModuloCategoria.Dominio;
using ListaCompras.WebApplication.ModuloItensLista.Servicos;
using ListaCompras.WebApplication.ModuloListaCompras;
using ListaCompras.WebApplication.ModuloListaCompras.Dominio;
using ListaCompras.WebApplication.ModuloListaCompras.Servicos;
using ListaCompras.WebApplication.ModuloProduto.Aplicacao;
using ListaCompras.WebApplication.ModuloProduto.Dominio;
using ListaCompras.WebApplication.ModuloProduto.Infra;

namespace ListaCompras.WebApp.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddAplicationServices(this IServiceCollection services)
{
    services.AddScoped<ServicoCategoria>();
    services.AddScoped<ServicoProduto>();

    // 1. Registre a classe concreta
    services.AddScoped<RepositorioProdutoEmArquivo>();

    // 2. Registre as interfaces apontando para a mesma instância da classe concreta
    services.AddScoped<IRepositorio<Produto>>(provider => provider.GetRequiredService<RepositorioProdutoEmArquivo>());
    services.AddScoped<IRepositorioProduto>(provider => provider.GetRequiredService<RepositorioProdutoEmArquivo>());

    services.AddScoped<IRepositorio<Categoria>, RepositorioCategoriaEmArquivo>();
    services.AddScoped<IListaDeComprasRepository, RepositorioListaComprasEmArquivo>();
    
    services.AddScoped<ListaDeComprasService>();
    services.AddScoped<ItemListaComprasService>();
}
    public static void AddInfraRepositories(this IServiceCollection services)
    {
       services.AddSingleton<ContextoJson>(provider =>
        {
            ContextoJson contextoJson = new ContextoJson();
            try 
            {
                contextoJson.Carregar();
            }
            catch { /* Ignora erro se arquivo não existir */ }
            return contextoJson;
         });
    }


    public static void AddPresentation(this IServiceCollection services)
    {
        services.AddControllersWithViews().AddRazorOptions(options =>
        {
            // Reseta a configuração padrão do MVC
            options.ViewLocationFormats.Clear();

            // Localização das Views dos módulos: /ModuloCategoria/Apresentacao/Views/Listar.cshtml
            options.ViewLocationFormats.Add("/Modulo{1}/Apresentacao/Views/{0}.cshtml");

            // Localização das Views compartilhadas: /Compartilhado/Apresentacao/Views/_Layout.cshtml
            options.ViewLocationFormats.Add("/Compartilhado/Apresentacao/Views/{0}.cshtml");
        });

        services.AddAutoMapper(config =>
        {
            config.AddMaps(typeof(Program));
        });
    }
}