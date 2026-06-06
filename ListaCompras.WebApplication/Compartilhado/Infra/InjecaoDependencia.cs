using Microsoft.Extensions.DependencyInjection;
using ListaCompras.WebApplication.Compartilhado.Infra.Arquivos;

// 1. Correção dos namespaces de Domínio e Repositorio conforme o seu JSON
using ListaCompras.WebApplication.ModuloListaCompras.Dominio; 
using ListaCompras.WebApplication.ModuloListaCompras; 

// 2. Correção das pastas de Serviços (removido o 'Aplicacao' intermediário se o namespace omitir, ou ajustado para minúsculo)
using ListaCompras.WebApplication.ModuloListaCompras.Servicos;
using ListaCompras.WebApplication.ModuloItensLista.Servicos;

namespace ListaCompras.WebApplication.Compartilhado.Infra;

public static class InjecaoDependencia
{
    public static void AddInfraRepositories(this IServiceCollection services)
    {
        // 1. Configura o contexto compartilhado
        services.AddScoped(provider =>
        {
            ContextoJson contextoJson = new ContextoJson();
            contextoJson.Carregar();
            return contextoJson;
        });

        // 2. Resolve o casamento da Interface do módulo 3 com sua implementação
        // Caso o erro de conversão continue, certifique-se de usar o namespace completo da interface aqui:
        services.AddScoped<ModuloListaCompras.Dominio.IListaDeComprasRepository, RepositorioListaComprasEmArquivo>();

        // 3. Injeta os serviços dos dois módulos independentes
        services.AddScoped<ListaDeComprasService>();
        services.AddScoped<ItemListaComprasService>();
    }
}