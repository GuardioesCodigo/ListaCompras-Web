using AutoMapper;
using ListaCompras.WebApplication.ModuloListaCompras.Apresentacao; // Garante que enxerga os dois records
using ListaCompras.WebApplication.ModuloListaCompras.Aplicacao;
using ListaCompras.WebApplication.ModuloListaCompras.Dominio;

namespace ListaCompras.WebApplication.ModuloListaCompras.Apresentacao;

public class ListaComprasProfile : Profile
{
    public ListaComprasProfile()
    {
        // Agora o profile enxergará as definições separadas nos arquivos corretos
        CreateMap<ListarListaComprasDto, ListaComprasViewModel>()
            .ForMember(dest => dest.DataCriacaoFormatada, opt => opt.MapFrom(src => src.DataCriacao.ToString("dd/MM/yyyy HH:mm")))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<ListaComprasFormViewModel, CadastrarListaComprasDto>();
        CreateMap<ListaComprasFormViewModel, EditarListaComprasDto>();
    }
}