using Microsoft.Extensions.DependencyInjection;
using ListaCompras.WebApplication.Compartilhado.Infra.Arquivos;
using ListaCompras.WebApplication.Compartilhado.Dominio;
using ListaCompras.WebApplication.ModuloListaCompras.Servicos;
using ListaCompras.WebApplication.ModuloItensLista.Servicos;
using ListaCompras.WebApplication.ModuloProduto.Infra;
using ListaCompras.WebApplication.ModuloProduto.Dominio;
using ListaCompras.WebApplication.ModuloListaCompras;
using ListaCompras.WebApplication.ModuloListaCompras.Dominio;

namespace ListaCompras.WebApplication.Compartilhado;

public static class InjecaoDependencia
{
    public static void AddInfrastructureAndApplication(this IServiceCollection services)
    {
        // Registro da persistência como Singleton
        services.AddSingleton<ContextoJson>();

        // Registros dos Repositórios
        services.AddScoped<RepositorioListaComprasEmArquivo>();
        services.AddScoped<RepositorioProdutoEmArquivo>();

        // Mapeamento das interfaces
        services.AddScoped<IListaDeComprasRepository>(sp => sp.GetRequiredService<RepositorioListaComprasEmArquivo>());
        services.AddScoped<IRepositorioProduto>(sp => sp.GetRequiredService<RepositorioProdutoEmArquivo>());
        
        // Correção para injeção genérica que estava faltando
        services.AddScoped<IRepositorio<Produto>>(sp => sp.GetRequiredService<RepositorioProdutoEmArquivo>());

        // Serviços
        services.AddScoped<ListaDeComprasService>();
        services.AddScoped<ItemListaComprasService>();
        
        services.AddControllersWithViews().AddRazorOptions(options => {
            options.ViewLocationFormats.Clear();
            options.ViewLocationFormats.Add("/Modulo{1}/Apresentacao/Views/{0}.cshtml");
            options.ViewLocationFormats.Add("/Compartilhado/Apresentacao/Views/{0}.cshtml");
        });

        services.AddAutoMapper(config => config.AddMaps(typeof(Program)));
    }
}