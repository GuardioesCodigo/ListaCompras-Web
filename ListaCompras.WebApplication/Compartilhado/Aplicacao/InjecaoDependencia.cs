using Microsoft.Extensions.DependencyInjection;
using ListaCompras.WebApplication.Compartilhado.Infra.Arquivos;
using ListaCompras.WebApplication.Compartilhado.Dominio;
using ListaCompras.WebApplication.ModuloListaCompras.Servicos;
using ListaCompras.WebApplication.ModuloListaCompras.Dominio;
using ListaCompras.WebApplication.ModuloListaCompras; // Onde está o RepositorioListaComprasEmArquivo
using ListaCompras.WebApplication.ModuloItensLista.Servicos;
using ListaCompras.WebApplication.ModuloProduto.Infra; // Onde está o RepositorioProdutoEmArquivo
using ListaCompras.WebApplication.ModuloProduto.Dominio; // Onde está a interface IRepositorioProduto

namespace ListaCompras.WebApplication.Compartilhado;

public static class InjecaoDependencia
{
    public static void AddInfrastructureAndApplication(this IServiceCollection services)
    {
        // 1. Contexto de Persistência
        services.AddSingleton<ContextoJson>();

        // 2. Repositórios (Registrando classes concretas)
        // Registros específicos:
        services.AddScoped<IListaDeComprasRepository, RepositorioListaComprasEmArquivo>();
        services.AddScoped<IRepositorioProduto, RepositorioProdutoEmArquivo>();

        // Mapeamento do Genérico para a implementação do Produto (exemplo para resolver o erro do ItemLista)
        services.AddScoped<IRepositorio<ModuloProduto.Dominio.Produto>>(sp => 
            sp.GetRequiredService<IRepositorioProduto>());

        // 3. Serviços de Aplicação
        services.AddScoped<ListaDeComprasService>();
        services.AddScoped<ItemListaComprasService>();
        
        // 4. Configurações de MVC e Razor
        services.AddControllersWithViews().AddRazorOptions(options =>
        {
            options.ViewLocationFormats.Clear();
            options.ViewLocationFormats.Add("/Modulo{1}/Apresentacao/Views/{0}.cshtml");
            options.ViewLocationFormats.Add("/Compartilhado/Apresentacao/Views/{0}.cshtml");
        });

        // 5. AutoMapper
        services.AddAutoMapper(config => config.AddMaps(typeof(Program)));
    }
}