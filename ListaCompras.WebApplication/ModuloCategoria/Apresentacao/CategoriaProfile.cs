using System;
using AutoMapper;
using ListaCompras.WebApplication.ModuloCategoria.Aplicacao;

namespace ListaCompras.WebApplication.ModuloCategoria.Apresentacao;

public class CategoriaProfile : Profile
{
    public CategoriaProfile()
    {
        CreateMap<ListarCategoriaDto, ListarCategoriasViewModel>();
        CreateMap<CadastrarCategoriasViewModel, CadastrarCategoriaDto>();
        CreateMap<EditarCategoriasViewModel, EditarCategoriaDto>();
        
        CreateMap<DetalhesCategoriaDto, EditarCategoriasViewModel>();
        CreateMap<DetalhesCategoriaDto, ExcluirCategoriasViewModel>();
    }
}
