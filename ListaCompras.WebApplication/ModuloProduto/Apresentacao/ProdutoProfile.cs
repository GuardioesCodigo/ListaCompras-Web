using AutoMapper;
using ListaCompras.WebApplication.ModuloProduto.Aplicacao;

namespace ListaCompras.WebApplication.ModuloProduto.Apresentacao.Views;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<ListarProdutoDto, ListarProdutosViewModel>();
        CreateMap<CadastrarProdutoViewModel, CadastrarProdutoDto>();
        CreateMap<EditarProdutoViewModel, EditarProdutoDto>();

        CreateMap<DetalhesProdutoDto, EditarProdutoViewModel>();
        CreateMap<DetalhesProdutoDto, ExcluirProdutosViewModel>();
    }
}
