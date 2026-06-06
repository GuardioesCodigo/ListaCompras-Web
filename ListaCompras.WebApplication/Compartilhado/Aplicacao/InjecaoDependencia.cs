using ListaCompras.ConsoleApp.ModuloCategoria.Infra;
using ListaCompras.WebApplication.Compartilhado.Dominio;
using ListaCompras.WebApplication.ModuloCategoria.Aplicacao;
using ListaCompras.WebApplication.ModuloCategoria.Dominio;
using ListaCompras.WebApplication.ModuloProduto.Dominio;
using ListaCompras.WebApplication.ModuloProduto.Infra;

namespace ListaCompras.WebApp.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddAplicationServices(this IServiceCollection services)
    {
    services.AddScoped<ServicoCategoria>();

    services.AddScoped<IRepositorio<Categoria>, RepositorioCategoriaEmArquivo>();
    services.AddScoped<IRepositorio<Produto>, RepositorioProdutoEmArquivo>();
    }
}
