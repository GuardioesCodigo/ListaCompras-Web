using System;
using AutoMapper;
using ListaCompras.WebApplication.ModuloCategoria.Aplicacao;

namespace ListaCompras.WebApplication.ModuloCategoria.Apresentacao;

public class CategoriaProfile : Profile
{
    public CategoriaProfile()
    {
        CreateMap<CadastrarCategoriasViewModel, CadastrarCategoriaDto>();

    }
}
