using AutoMapper;
using LojaOnline.Dtos;
using LojaOnline.Models;

namespace LojaOnline.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Produto, ProdutoDto>()
            .ForMember(
                dest => dest.CategoriaNome, 
                opt => opt.MapFrom(src => src.Categoria!.Nome));
        CreateMap<ProdutoCreateDto, Produto>();
        
        CreateMap<Categoria, CategoriaDto>();
        CreateMap<CategoriaCreateDto, Categoria>();
    }
}